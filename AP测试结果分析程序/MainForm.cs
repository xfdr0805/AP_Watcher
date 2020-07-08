using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using SharpConfig;
using Spire.Pdf;
using Timer = System.Windows.Forms.Timer;

namespace AP_Watcher {
    public partial class MainForm : Form
    {
        private MySqlConnection conn;
        string path_watch;
        string path_ok;
        string path_ng;
        string server;
        string amp_name;
        string fac_name;
        string prc_name;
        string year;
        string week;
        int count_ok = 0;
        int count_ng = 0;
        Timer timer1 = new Timer();
        private List<string> order_list = new List<string>();
        public delegate void SetLabelTextCallback(Control control, string text);
        public delegate void ShowFormHandler(Form owner, string msg, int time_out, Color fc, Color bc);
        public MainForm()
        {
            InitializeComponent();

            timer1.Interval = 1000;
            label_start_time.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer2.Interval = 10000;
            timer2.Enabled = true;
            if (File.Exists("setup.ini") == true) {
                var config = Configuration.LoadFromFile("setup.ini");
                if (config["SYS"]["local_save"].BoolValue) { //本地保存
                    path_ok = config["PATH_LOCAL"]["path_ok"].StringValue;
                    path_ng = config["PATH_LOCAL"]["path_ng"].StringValue;
                    path_watch = config["PATH_LOCAL"]["path_watch"].StringValue;
                    SetLabelText(this, "AP Test Result  Watcher ---> 本地保存");
                    CheckBox_Local.Checked = true;
                    if (Directory.Exists(path_ok) == false) {
                        Directory.CreateDirectory(path_ok);
                    }
                    if (Directory.Exists(path_ng) == false) {
                        Directory.CreateDirectory(path_ng);
                    }
                    if (Directory.Exists(path_watch) == false) {
                        Directory.CreateDirectory(path_watch);
                    }
                    //WatcherStart(path_watch, "*.pdf");
                    Btn_Browser.Enabled = true;

                } else {
                    server = config["SYS"]["server"].StringValue;
                    path_ok = config["PATH_SERVER"]["path_ok"].StringValue;
                    path_ng = config["PATH_SERVER"]["path_ng"].StringValue;
                    path_watch = config["PATH_SERVER"]["path_watch"].StringValue;
                    this.Text = "AP Test Result  Watcher ---> 连接数据库中......";
                    Thread t = new Thread(new ThreadStart(delegate () {
                        Connect_DataBase();
                    }));
                    t.Start();
                }

                if(config["SYS"]["auto_run"].StringValue == "true") {
                    checkBox_AutoRun.Checked = true;
                } else {
                    checkBox_AutoRun.Checked = false;
                }

                string[] servers = config["SYS"]["servers"].StringValueArray;
                comboBox_Server.Items.Clear();
                comboBox_Server.Items.Add(servers[0]);
                comboBox_Server.Items.Add(servers[1]);
                if (server == "192.168.1.8") {
                    comboBox_Server.SelectedIndex = 0;
                } else {
                    comboBox_Server.SelectedIndex = 1;
                }

            } else {
                CheckBox_Local.Checked = false;
                var myConfig = new Configuration();
                myConfig["SYS"]["auto_run"].BoolValue = false;
                myConfig["SYS"]["local_save"].BoolValue = false;
                myConfig["SYS"]["server"].StringValue = "192.168.2.8";
                myConfig["SYS"]["servers"].StringValueArray = new [] { "192.168.1.8","10.28.3.18"};
                myConfig["PATH_SERVER"]["passed"].StringValue = @"\01 测试结果\AP-Passed";
                myConfig["PATH_SERVER"]["failed"].StringValue = @"\01 测试结果\AP-Failed";
                myConfig["PATH_SERVER"]["watch"].StringValue = @"\01 测试结果\AP-Unshorted";
                myConfig["PATH_SERVER"]["path_ok"].StringValue = @"\\192.168.2.8\01 测试结果\AP-Passed";
                myConfig["PATH_SERVER"]["path_ng"].StringValue = @"\\192.168.2.8\01 测试结果\AP-Failed";
                myConfig["PATH_SERVER"]["path_watch"].StringValue = @"\\192.168.2.8\01 测试结果\AP-Unshorted";
                myConfig["PATH_LOCAL"]["path_ok"].StringValue = @"D:\测试结果\AP-Passed";
                myConfig["PATH_LOCAL"]["path_ng"].StringValue = @"D:\测试结果\AP-Failed";
                myConfig["PATH_LOCAL"]["path_watch"].StringValue = @"D:\测试结果\AP-Watched";
                myConfig.SaveToFile("setup.ini");
            }
            TextBox_Path_Ok.Text = path_ok;
            TextBox_Path_Ng.Text = path_ng;
            TextBox_Path_Watch.Text = path_watch;
            if (Directory.Exists(path_ok) == false) {
                Directory.CreateDirectory(path_ok);
            }
            if (Directory.Exists(path_ng) == false) {
                Directory.CreateDirectory(path_ng);
            }
            if (Directory.Exists(path_watch) == false) {
                Directory.CreateDirectory(path_watch);
            }
            CheckBox_Local.CheckedChanged += new System.EventHandler(this.CheckBox_Local_CheckedChanged);
            count_ok = 0;
            count_ng = 0;


        }
        private void Btn_Browser_Click( object sender, EventArgs e )
        {

            //folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            folderBrowserDialog1.SelectedPath = this.TextBox_Path_Watch.Text;
            string path = folderBrowserDialog1.SelectedPath;
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                TextBox_Path_Watch.Text = folderBrowserDialog1.SelectedPath;
                path = folderBrowserDialog1.SelectedPath;
                if (CheckBox_Local.Checked) {
                    string[] str = path.Split('\\');
                    amp_name = str[str.Length - 1];

                }

                count_ok = 0;
                count_ng = 0;
            } else {
                MessageBox.Show("请选择AP测试结果保存的路径", "提示");

            }

        }

        private void Btn_Start_Click( object sender, EventArgs e )
        {
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //connectState(@"\\192.168.1.8\AP测试结果\","sonavox","123456");
            //UpLoadFile(@"1.pdf", result_path, "sonavox", "123456");
            saveFileDialog1.FileName = "文件列表.txt";
            saveFileDialog1.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            if(saveFileDialog1.ShowDialog() == DialogResult.OK) {
                List<string> ls = FindFile(TextBox_Path_Ok.Text);
                StringBuilder sb = new StringBuilder();
                foreach (String item in ls) {
                    sb.Append(item + "\r\n");
                }
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                sw.Write(sb);
                sw.Flush();
                sw.Close();
            }


        }
        private void SetLabelText(Control  control,string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (control.InvokeRequired) { //如果调用控件的线程和创建创建控件的线程不是同一个则为True
                while (!control.IsHandleCreated) {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (control.Disposing || control.IsDisposed)
                        return;
                }
                SetLabelTextCallback d = new SetLabelTextCallback(SetLabelText);
                control.Invoke(d, new object[] {control, text });
            } else {
                control.Text = text;
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if(conn.State == ConnectionState.Closed ) {
                Connect_DataBase();
            }
            timer2.Enabled = false;
            string path_ok_save = "";
            string path_ng_save = "";

            if (System.IO.Directory.Exists(TextBox_Path_Watch.Text)) {
                string[] files = System.IO.Directory.GetFiles(TextBox_Path_Watch.Text);
                for(int i=0; i<files.Length; i++) {


                    //仅返回路径字符串的文件名及后缀
                    string fileName = System.IO.Path.GetFileName(files[i]);
                    //string destFile = System.IO.Path.Combine(targetPath, fileName);
                    Console.WriteLine(fileName);
                    string barcode = fileName.Substring(0, fileName.Length - 4);
                    barcode = barcode.ToUpper();
                    if (!CheckBox_Local.Checked) {
                        if (barcode.Length != 18) {
                            File.Delete(Path.Combine(path_watch, fileName));
                            continue;
                        } else if (!barcode.StartsWith("A")) {
                            File.Delete(Path.Combine(path_watch, fileName));
                            continue;
                        }
                        SetLabelText(textBox_BarCode, barcode);

                        GetInfoFromMySql(textBox_BarCode.Text);
                        path_ok_save = path_ok + @"\" + amp_name + @"\" + year + "年" + week + "周";
                        path_ng_save = path_ng + @"\" + amp_name + @"\" + year + "年" + week + "周";
                        SetLabelText(TextBox_Path_Ok, path_ok_save);
                        SetLabelText(TextBox_Path_Ng, path_ng_save);
                    } else {
                        path_ok_save = path_ok + @"\" + amp_name + @"\" + DateTime.Now.ToLongDateString();
                        path_ng_save = path_ng + @"\" + amp_name + @"\" + DateTime.Now.ToLongDateString();
                        SetLabelText(TextBox_Path_Ok, path_ok_save);
                        SetLabelText(TextBox_Path_Ng, path_ng_save);
                    }
                    //// Create a new PDF document
                    string result = "PASSED";
                    ////Thread.Sleep(2000);
                    //// WaitForFile(e.FullPath);
                    ////FileStream fs = new FileStream(e.FullPath, FileMode.Open, FileAccess.Write, FileShare.Read);
                    PdfDocument doc = new PdfDocument();
                    try {

                        doc.LoadFromFile(Path.Combine(path_watch,fileName));
                        StringBuilder buffer = new StringBuilder();
                        foreach (PdfPageBase page in doc.Pages) {
                            buffer.Clear();
                            string str = page.ExtractText();
                            buffer.Append(str);
                            if (str.Contains("FAILED")) {
                                result = "FAILED";
                                break;
                            }

                        }
                        Console.WriteLine("识别结果：" + result);
                    } catch (Exception ee) {
                        result = "";
                        //MessageBox.Show("检测到异常", "提示");
                        Console.WriteLine(ee.Message);
                        timer2.Enabled = true;
                    } finally {
                        doc.Close();
                        doc.Dispose();
                                             
                    }
                    //2020.06.19  修复在文件不能正常打开时，PASSED文件会被移动到FAILED文件夹中
                    if (result == "")
                        return;
                    if (Directory.Exists(path_ok_save) == false) {
                        Directory.CreateDirectory(path_ok_save);
                    }
                    if (Directory.Exists(path_ng_save) == false) {
                        Directory.CreateDirectory(path_ng_save);
                    }
                    // Model.ShowMsg show = new Model.ShowMsg();

                    //myDialog.Owner = this;
                    if (result == "PASSED") {

                        //if (CheckBox_Local.Checked) {
                        //    File.Copy(e.FullPath, Path.Combine(result_path, e.Name),true);
                        //} else {
                        //   // UpLoadFile(e.FullPath, result_path, "sonavox", "123456");

                        //}
                        //WaitForFile(e.FullPath);
                        // 确认目标文件不存在
                        string _path_ok = Path.Combine(path_ok_save, fileName);
                        if (File.Exists(_path_ok)) {
                            File.Delete(_path_ok);
                        }
                        // 移动
                        try {
                            FileInfo fi = new FileInfo(Path.Combine(path_watch, fileName));

                            Console.WriteLine(fi.CreationTime.ToString());
                            File.Copy(Path.Combine(path_watch, fileName), Path.Combine(path_ok_save, fileName.ToUpper()), true);
                            File.Delete(Path.Combine(path_watch, fileName));
                            //File.Move(Path.Combine(path_watch, fileName), Path.Combine(path_ok_save, fileName));
                            //Console.WriteLine(Path.Combine(path_watch, fileName));
                            //Console.WriteLine(Path.Combine(path_ok_save, fileName));
                            if(GetProductDataFromMySql(barcode)) {

                                UpdateProductDataToMySql("PASSED", string.Format("{0:yyyy-MM-dd HH:mm:ss}", fi.CreationTime), barcode);
                            }
                            Console.WriteLine("PASSED-文件移动完成");
                            count_ok++;
                            SetLabelText(Label_Count_OK, count_ok + "");
                        } catch (Exception) {
                            timer2.Enabled = true;
                        } finally {
                            timer2.Enabled = true;
                        }

                        //File.Copy(e.FullPath, Path.Combine(result_path, e.Name), true);
                        ShowFormHandler delShowForm = new ShowFormHandler(ShowMyDialog);
                        this.BeginInvoke(delShowForm, new Object[] { this, "识别结果---PASSED!", 2000, Color.Green, Color.White });
                        //show.ShowMessageBoxTimeout("识别结果---PASSED!", "提示", MessageBoxButtons.OK, 2000); //单位毫秒
                    } else if (result == "FAILED"){

                        //WaitForFile(e.FullPath);
                        string _path_ng = Path.Combine(path_ng_save, fileName);
                        if (File.Exists(_path_ng)) {
                            File.Delete(_path_ng);
                        }
                        try {
                            File.Copy(Path.Combine(path_watch, fileName), Path.Combine(path_ng_save, fileName.ToUpper()), true);
                            File.Delete(Path.Combine(path_watch, fileName));
                            //File.Move(Path.Combine(path_watch, fileName), Path.Combine(path_ng_save, fileName));
                            Console.WriteLine("FAILED-文件移动完成");
                            count_ng++;
                            SetLabelText(Label_Count_NG, count_ng + "");
                        } catch (Exception) {
                            timer2.Enabled = true;
                        } finally {
                            timer2.Enabled = true;
                        }


                        //File.Copy(e.FullPath, Path.Combine(result_path_ng, e.Name), true);
                        ShowFormHandler delShowForm = new ShowFormHandler(ShowMyDialog);
                        this.BeginInvoke(delShowForm, new Object[] { this, "识别结果---FAILED!", 2000, Color.Red, Color.White });
                        //show.ShowMessageBoxTimeout("识别结果---FAILED!", "提示", MessageBoxButtons.OK, 2000); //单位毫秒
                        //UpLoadFile(e.FullPath, result_path, "sonavox", "123456");

                    }
                }

            }
            label_last_time.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            timer2.Enabled = true;
        }
        private void ShowMyDialog(Form owner, string msg, int time_out, Color fc, Color bc)
        {
            MyDialog myDialog = new MyDialog();
            myDialog.Owner = owner;
            myDialog.StartPosition = FormStartPosition.CenterParent;
            //myDialog.Size = new Size(260, 60);
            myDialog.SetMessageColor(fc, bc);
            myDialog.SetMessage(msg, time_out);
            myDialog.ShowDialog();
        }
        public bool connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = @"net use " + path + " /User:" + userName + " " + passWord + " /PERSISTENT:YES";
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited) {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg)) {
                    Flag = true;
                } else {
                    throw new Exception(errormsg);
                }
            } catch (Exception) {

            } finally {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }
        public List<string> FindFile(string sSourcePath)
        {

            List<String> list = new List<string>();

            //遍历文件夹

            DirectoryInfo theFolder = new DirectoryInfo(sSourcePath);

            FileInfo[] thefileInfo = theFolder.GetFiles("*.pdf", SearchOption.TopDirectoryOnly);

            foreach (FileInfo NextFile in thefileInfo)  //遍历文件

                list.Add(NextFile.Name);


            //遍历子文件夹

            DirectoryInfo[] dirInfo = theFolder.GetDirectories();

            foreach (DirectoryInfo NextFolder in dirInfo)

            {

                //list.Add(NextFolder.ToString());

                FileInfo[] fileInfo = NextFolder.GetFiles("*.pdf", SearchOption.AllDirectories);

                foreach (FileInfo NextFile in fileInfo)  //遍历文件

                    list.Add(NextFile.Name);


            }

            return list;
        }
        private void CheckBox_Local_CheckedChanged(object sender, EventArgs e)
        {

            count_ng = 0;
            count_ok = 0;
            if (CheckBox_Local.Checked) {
                SetLabelText(this, "AP Test Result  Watcher ---> 本地保存");
                Btn_Browser.Enabled = true;

                string[] str = TextBox_Path_Watch.Text.Split('\\');
                amp_name = str[str.Length - 1];
                path_ok = @"D:\AP-Passed\" + amp_name + @"\" + DateTime.Now.ToLongDateString();
                path_ng = @"D:\AP-Failed\" + amp_name + @"\" + DateTime.Now.ToLongDateString();
                TextBox_Path_Ok.Text = path_ok;
                TextBox_Path_Ng.Text = path_ng;
                if (File.Exists("setup.ini") == true) {
                    var config = Configuration.LoadFromFile("setup.ini");
                    path_ok = config["PATH_LOCAL"]["path_ok"].StringValue;
                    path_ng = config["PATH_LOCAL"]["path_ng"].StringValue;
                    path_watch = config["PATH_LOCAL"]["path_watch"].StringValue;
                    TextBox_Path_Ok.Text = path_ok;
                    TextBox_Path_Ng.Text = path_ng;
                    TextBox_Path_Watch.Text = path_watch;
                    config["SYS"]["local_save"].BoolValue = true;
                    config.SaveToFile("setup.ini");
                }
            } else {

                Btn_Browser.Enabled = false;
                if(conn == null) {
                    Thread t = new Thread(new ThreadStart(delegate () {
                        Connect_DataBase();
                    }));
                    t.Start();
                } else {
                    SetLabelText(this, "AP Test Result  Watcher ---> 数据库连接正常");
                }
                if (File.Exists("setup.ini") == true) {
                    var config = Configuration.LoadFromFile("setup.ini");
                    server = config["SYS"]["server"].StringValue;
                    config["SYS"]["local_save"].BoolValue = false;
                    string[] servers = config["SYS"]["servers"].StringValueArray;
                    comboBox_Server.Items.Clear();
                    comboBox_Server.Items.Add(servers[0]);
                    comboBox_Server.Items.Add(servers[1]);
                    if (server == "192.168.2.8") {
                        comboBox_Server.SelectedIndex = 0;
                    } else {
                        comboBox_Server.SelectedIndex = 1;
                    }
                    path_ok = config["PATH_SERVER"]["path_ok"].StringValue;
                    path_ng = config["PATH_SERVER"]["path_ng"].StringValue;
                    path_watch = config["PATH_SERVER"]["path_watch"].StringValue;
                    TextBox_Path_Ok.Text = path_ok;
                    TextBox_Path_Ng.Text = path_ng;
                    TextBox_Path_Watch.Text = path_watch;
                    config.SaveToFile("setup.ini");
                }
            }

        }
        void Connect_DataBase()
        {
            string connStr = String.Format("server={0};user id={1}; password={2}; database=mysql; pooling=false;characterset=utf8;allow zero datetime=true",server, "root", "Sonavox168*");
            try {
                conn = new MySqlConnection(connStr);
                conn.Open();
                SetLabelText(this, "AP Test Result  Watcher ---> " + DateTime.Now.ToString());
                //WatcherStart(TextBox_Path_Watch.Text, "*.pdf");
            } catch (MySqlException ex) {
                SetLabelText(this, "AP Test Result  Watcher ---> 数据库连接失败");
                MessageBox.Show("无法连接数据库: " + ex.Message);
            }
        }
        private void GetInfoFromMySql(string barcode)
        {
            if (conn == null) {
                return ;
            }
            ShowFormHandler delShowForm = new ShowFormHandler(ShowMyDialog);
            if (barcode.Length != 18) {
                this.BeginInvoke(delShowForm, new Object[] { this, "条码长度不匹配!", 3000, Color.Red, Color.WhiteSmoke });
                return;
            } else if (!barcode.StartsWith("A")) {
                this.BeginInvoke(delShowForm, new Object[] { this, "条码不是A开始!", 3000, Color.Red, Color.WhiteSmoke });
                return;
            }
            string fac_code = barcode.Substring(0, 1);
            string prc_code = barcode.Substring(1, 2);
            string custom_code = barcode.Substring(3, 3);
            string amp_code = barcode.Substring(6, 3);
            year = barcode.Substring(9, 2);
            week = barcode.Substring(11, 2);
            string sn = barcode.Substring(13, 5);
            SetLabelText(label_date, "生产日期:" + year.Trim() + "年," + week.Trim() + "周");
            SetLabelText(label_sn, "流水号:" + sn);

            if (barcode.Length != 18) {
                return;
            } else if (!barcode.StartsWith("A")) {
                return;
            }

            MySqlDataReader reader = null;
            string str = string.Format("SELECT fac_name,prc_name,amp_name FROM web_manager_line.hgc_barcode_rule where fac_code=\"{0}\" and prc_code=\"{1}\" and amp_code=\"{2}\"", fac_code, prc_code, amp_code);
            try {

                MySqlCommand cmd = new MySqlCommand(str, conn);
                reader = cmd.ExecuteReader();
                if(reader.Read()) {
                    fac_name = reader.GetString(0);
                    prc_name = reader.GetString(1);
                    //custom_name = reader.GetString(2);
                    amp_name = reader.GetString(2);
                    SetLabelText(label_amp, "机种名称:" + amp_name);
                    //SetLabelText(label_custom, "客户名称:" + custom_name);
                    SetLabelText(label_type,"产品类别:" + prc_name);

                } else {
                    this.BeginInvoke(delShowForm, new Object[] { this, "未查询到此机种相关代码!", 3000, Color.Red, Color.WhiteSmoke });
                    SetLabelText(label_amp, "机种名称:");
                    //SetLabelText(label_custom, "客户名称:");
                    SetLabelText(label_type, "产品类别:");
                    SetLabelText(label_date, "生产日期:");
                    SetLabelText(label_sn, "流水号:");
                    return;
                }


            } catch (MySqlException ex) {
                Console.WriteLine("Failed to populate database list: " + ex.Message);
            } finally {
                if (reader != null) reader.Close();

            }
            //return msg;
        }
        private bool GetProductDataFromMySql(string barcode)
        {
            if (conn.State == ConnectionState.Closed)
            {
                Connect_DataBase();
            }
            bool res = false;
            MySqlDataReader reader = null;
            string str = string.Format("SELECT * FROM web_manager_line.hgc_repair_log where serial_num=\"{0}\"",barcode);
            try {

                MySqlCommand cmd = new MySqlCommand(str, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    res = true;
                }

            } catch (MySqlException ex) {
                Console.WriteLine("Failed to populate database list: " + ex.Message);
            } finally {
                if (reader != null) reader.Close();

            }
            return res;
        }
        private void UpdateProductDataToMySql(string result,string datetime,string barcode)
        {
            if (conn == null) {
                return;
            }
            int reader = 0;
            string str = string.Format("UPDATE web_manager_line.hgc_repair_log SET ap_result=\"{0}\",ap_datetime=\"{1}\" where serial_num=\"{2}\"", result,datetime, barcode);
            try {

                MySqlCommand cmd = new MySqlCommand(str, conn);
                reader = cmd.ExecuteNonQuery();
                Console.WriteLine(reader);

            } catch (MySqlException ex) {
                Console.WriteLine("Failed to populate database list: " + ex.Message);
            } finally {


            }
            //return msg;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            //Connect_DataBase();
            //GetOrderInfoFromMySql();
        }

        //private void ComboBox_Amp_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Btn_Browser.Enabled = true;
        //    Btn_Start.Enabled = true;
        //    CheckBox_Local.Enabled = true;
        //    if (CheckBox_Local.Checked) {
        //        result_path = @"D:\AP-Passed\" + amp_name + @"\" + order_sn + "-" + order_num;
        //    } else {
        //        result_path = @"\\192.168.1.8\01 测试结果\AP-Passed\" + amp_name + @"\" + order_sn + "-" + order_num;
        //    }

        //    //TextBox_SharePath.Text = result_path;
        //    if (Directory.Exists("resut_path") == false) {
        //        Directory.CreateDirectory(result_path);
        //    }
        //}

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认要关闭吗，关闭后将无法再监控文件夹！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if(dr == DialogResult.Cancel) {
                e.Cancel = true;
            }
        }

        private void Btn_BarCode_Click(object sender, EventArgs e)
        {

            string barcode = textBox_BarCode.Text;
            GetInfoFromMySql(barcode);
            //GetProductDataFromMySql(barcode);
            //UpdateProductDataToMySql("PASSED", string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now), barcode);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //WatcherStart(TextBox_Path.Text, "*.pdf");

            //Ping ping = new Ping();
            //try {
            //    if (ping.Send(server).Status == IPStatus.Success) {
            //        Console.WriteLine("ping ok");
            //        if (!status_net) {
            //            status_net = true;

            //            //if(conn == null) {
            //            //    Connect_DataBase();
            //            //}
            //            this.Text = "AP Test Result  Watcher ---> 网络正常";
            //        }

            //    }
            //} catch (Exception) {
            //    status_net = false;
            //    conn = null;
            //    this.Text = "AP Test Result  Watcher ---> 网络错误";
            //}
            //timer1.Enabled = false;
            //if (conn == null) {
            //    Connect_DataBase();
            //    toolStripStatusLabel1.Text = "数据库连接正常";

            //}
            //WatcherStart(TextBox_Path_Watch.Text, "*.pdf");

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_AutoRun.Checked) { //设置开机自启动
                string path = Application.ExecutablePath;
                if (File.Exists("setup.ini") == true) {
                    var config = Configuration.LoadFromFile("setup.ini");
                    config["SYS"]["auto_run"].StringValue = "true";
                    config.SaveToFile("setup.ini");
                }
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.SetValue("AP Watcher", path);
                rk2.Close();
                rk.Close();
            } else { //取消开机自启动

                string path = Application.ExecutablePath;
                if (File.Exists("setup.ini") == true) {
                    var config = Configuration.LoadFromFile("setup.ini");
                    config["SYS"]["auto_run"].StringValue = "false";
                    config.SaveToFile("setup.ini");
                }
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.DeleteValue("AP Watcher", false);
                rk2.Close();
                rk.Close();
            }
        }

        private void comboBox_Server_SelectionChangeCommitted(object sender, EventArgs e)
        {
            server = comboBox_Server.SelectedItem.ToString();
            //Console.WriteLine(server);

            if (File.Exists("setup.ini") == true) {
                var config = Configuration.LoadFromFile("setup.ini");

                path_watch = @"\\" + server + config["PATH_SERVER"]["watch"].StringValue;
                path_ok = @"\\" + server + config["PATH_SERVER"]["passed"].StringValue;
                path_ng = @"\\" + server + config["PATH_SERVER"]["failed"].StringValue;
                TextBox_Path_Ok.Text = path_ok;
                TextBox_Path_Ng.Text = path_ng;
                TextBox_Path_Watch.Text = path_watch;
                config["SYS"]["server"].StringValue = server;
                config["PATH_SERVER"]["path_ok"].StringValue = path_ok;
                config["PATH_SERVER"]["path_ng"].StringValue = path_ng;
                config["PATH_SERVER"]["path_watch"].StringValue = path_watch;
                config.SaveToFile("setup.ini");
            }
            //WatcherStart(TextBox_Path_Watch.Text, "*.pdf");
            DialogResult dr =  MessageBox.Show(this, "需要重新启动程序，点击确定关闭！", "提示！", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
            if(dr == DialogResult.OK) {
                this.Close();
            }
        }


    }
}
