namespace AP_Watcher {
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBox_Path_Watch = new System.Windows.Forms.TextBox();
            this.Btn_Browser = new System.Windows.Forms.Button();
            this.TextBox_Path_Ok = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Btn_Start = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Label_Count_OK = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Label_Count_NG = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.CheckBox_Local = new System.Windows.Forms.CheckBox();
            this.label_amp_name = new System.Windows.Forms.Label();
            this.textBox_BarCode = new System.Windows.Forms.TextBox();
            this.Btn_BarCode = new System.Windows.Forms.Button();
            this.label_type = new System.Windows.Forms.Label();
            this.label_amp = new System.Windows.Forms.Label();
            this.label_date = new System.Windows.Forms.Label();
            this.label_sn = new System.Windows.Forms.Label();
            this.checkBox_AutoRun = new System.Windows.Forms.CheckBox();
            this.TextBox_Path_Ng = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox_Server = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label_start_time = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_last_time = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(4, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "AP 测试结果路径:";
            // 
            // TextBox_Path_Watch
            // 
            this.TextBox_Path_Watch.Location = new System.Drawing.Point(124, 50);
            this.TextBox_Path_Watch.Name = "TextBox_Path_Watch";
            this.TextBox_Path_Watch.ReadOnly = true;
            this.TextBox_Path_Watch.Size = new System.Drawing.Size(313, 21);
            this.TextBox_Path_Watch.TabIndex = 1;
            this.TextBox_Path_Watch.Text = "\\\\192.168.1.19\\pmdb\\AP 测试结果\\AP-Unshorted";
            // 
            // Btn_Browser
            // 
            this.Btn_Browser.Enabled = false;
            this.Btn_Browser.Location = new System.Drawing.Point(443, 48);
            this.Btn_Browser.Name = "Btn_Browser";
            this.Btn_Browser.Size = new System.Drawing.Size(75, 23);
            this.Btn_Browser.TabIndex = 2;
            this.Btn_Browser.Text = "浏览";
            this.Btn_Browser.UseVisualStyleBackColor = true;
            this.Btn_Browser.Click += new System.EventHandler(this.Btn_Browser_Click);
            // 
            // TextBox_Path_Ok
            // 
            this.TextBox_Path_Ok.Font = new System.Drawing.Font("宋体", 9F);
            this.TextBox_Path_Ok.Location = new System.Drawing.Point(124, 83);
            this.TextBox_Path_Ok.Name = "TextBox_Path_Ok";
            this.TextBox_Path_Ok.ReadOnly = true;
            this.TextBox_Path_Ok.Size = new System.Drawing.Size(313, 21);
            this.TextBox_Path_Ok.TabIndex = 4;
            this.TextBox_Path_Ok.Text = "根据条码自动生成文件夹路径 ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(4, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Passed 结果路径:";
            // 
            // Btn_Start
            // 
            this.Btn_Start.Location = new System.Drawing.Point(437, 313);
            this.Btn_Start.Name = "Btn_Start";
            this.Btn_Start.Size = new System.Drawing.Size(75, 23);
            this.Btn_Start.TabIndex = 5;
            this.Btn_Start.Text = "导出列表";
            this.Btn_Start.UseVisualStyleBackColor = true;
            this.Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 16F);
            this.label3.ForeColor = System.Drawing.Color.LimeGreen;
            this.label3.Location = new System.Drawing.Point(120, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(263, 22);
            this.label3.TabIndex = 6;
            this.label3.Text = "AP测试结果监控程序 V1.7";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F);
            this.label4.ForeColor = System.Drawing.Color.SlateBlue;
            this.label4.Location = new System.Drawing.Point(7, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(413, 36);
            this.label4.TabIndex = 7;
            this.label4.Text = "1.如果连接车间WIFI，请选择192.168.2.8 办公电脑选择192.168.1.8\r\n2.保存路径修改为自动识别二维码来保存到对应文件夹下\r\n3.勾选本地" +
    "时，由于无法从服务器获获取信息，结果会保存在本地对应路径下\r\n";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 16F);
            this.label5.ForeColor = System.Drawing.Color.LimeGreen;
            this.label5.Location = new System.Drawing.Point(3, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "PASSED:";
            // 
            // Label_Count_OK
            // 
            this.Label_Count_OK.AutoSize = true;
            this.Label_Count_OK.Font = new System.Drawing.Font("宋体", 16F);
            this.Label_Count_OK.ForeColor = System.Drawing.Color.LimeGreen;
            this.Label_Count_OK.Location = new System.Drawing.Point(86, 195);
            this.Label_Count_OK.Name = "Label_Count_OK";
            this.Label_Count_OK.Size = new System.Drawing.Size(21, 22);
            this.Label_Count_OK.TabIndex = 9;
            this.Label_Count_OK.Text = "0";
            // 
            // Label_Count_NG
            // 
            this.Label_Count_NG.AutoSize = true;
            this.Label_Count_NG.Font = new System.Drawing.Font("宋体", 16F);
            this.Label_Count_NG.ForeColor = System.Drawing.Color.OrangeRed;
            this.Label_Count_NG.Location = new System.Drawing.Point(86, 228);
            this.Label_Count_NG.Name = "Label_Count_NG";
            this.Label_Count_NG.Size = new System.Drawing.Size(21, 22);
            this.Label_Count_NG.TabIndex = 11;
            this.Label_Count_NG.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 16F);
            this.label7.ForeColor = System.Drawing.Color.OrangeRed;
            this.label7.Location = new System.Drawing.Point(3, 226);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 22);
            this.label7.TabIndex = 10;
            this.label7.Text = "FAILED:";
            // 
            // CheckBox_Local
            // 
            this.CheckBox_Local.AutoSize = true;
            this.CheckBox_Local.Location = new System.Drawing.Point(443, 120);
            this.CheckBox_Local.Name = "CheckBox_Local";
            this.CheckBox_Local.Size = new System.Drawing.Size(72, 16);
            this.CheckBox_Local.TabIndex = 12;
            this.CheckBox_Local.Text = "本地保存";
            this.CheckBox_Local.UseVisualStyleBackColor = true;
            // 
            // label_amp_name
            // 
            this.label_amp_name.AutoSize = true;
            this.label_amp_name.Font = new System.Drawing.Font("宋体", 10F);
            this.label_amp_name.Location = new System.Drawing.Point(4, 154);
            this.label_amp_name.Name = "label_amp_name";
            this.label_amp_name.Size = new System.Drawing.Size(126, 14);
            this.label_amp_name.TabIndex = 14;
            this.label_amp_name.Text = "实时获取的二维码:";
            // 
            // textBox_BarCode
            // 
            this.textBox_BarCode.Location = new System.Drawing.Point(129, 152);
            this.textBox_BarCode.Name = "textBox_BarCode";
            this.textBox_BarCode.Size = new System.Drawing.Size(227, 21);
            this.textBox_BarCode.TabIndex = 15;
            // 
            // Btn_BarCode
            // 
            this.Btn_BarCode.Location = new System.Drawing.Point(362, 151);
            this.Btn_BarCode.Name = "Btn_BarCode";
            this.Btn_BarCode.Size = new System.Drawing.Size(75, 23);
            this.Btn_BarCode.TabIndex = 16;
            this.Btn_BarCode.Text = "解析二维码";
            this.Btn_BarCode.UseVisualStyleBackColor = true;
            this.Btn_BarCode.Click += new System.EventHandler(this.Btn_BarCode_Click);
            // 
            // label_type
            // 
            this.label_type.AutoSize = true;
            this.label_type.Location = new System.Drawing.Point(163, 232);
            this.label_type.Name = "label_type";
            this.label_type.Size = new System.Drawing.Size(65, 12);
            this.label_type.TabIndex = 18;
            this.label_type.Text = "产品类别：";
            // 
            // label_amp
            // 
            this.label_amp.AutoSize = true;
            this.label_amp.Location = new System.Drawing.Point(300, 234);
            this.label_amp.Name = "label_amp";
            this.label_amp.Size = new System.Drawing.Size(65, 12);
            this.label_amp.TabIndex = 20;
            this.label_amp.Text = "机种名称：";
            // 
            // label_date
            // 
            this.label_date.AutoSize = true;
            this.label_date.Location = new System.Drawing.Point(163, 201);
            this.label_date.Name = "label_date";
            this.label_date.Size = new System.Drawing.Size(65, 12);
            this.label_date.TabIndex = 21;
            this.label_date.Text = "生产日期：";
            // 
            // label_sn
            // 
            this.label_sn.AutoSize = true;
            this.label_sn.Location = new System.Drawing.Point(303, 201);
            this.label_sn.Name = "label_sn";
            this.label_sn.Size = new System.Drawing.Size(53, 12);
            this.label_sn.TabIndex = 22;
            this.label_sn.Text = "流水号：";
            // 
            // checkBox_AutoRun
            // 
            this.checkBox_AutoRun.AutoSize = true;
            this.checkBox_AutoRun.Location = new System.Drawing.Point(443, 155);
            this.checkBox_AutoRun.Name = "checkBox_AutoRun";
            this.checkBox_AutoRun.Size = new System.Drawing.Size(72, 16);
            this.checkBox_AutoRun.TabIndex = 24;
            this.checkBox_AutoRun.Text = "开机启动";
            this.checkBox_AutoRun.UseVisualStyleBackColor = true;
            this.checkBox_AutoRun.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // TextBox_Path_Ng
            // 
            this.TextBox_Path_Ng.Font = new System.Drawing.Font("宋体", 9F);
            this.TextBox_Path_Ng.Location = new System.Drawing.Point(124, 117);
            this.TextBox_Path_Ng.Name = "TextBox_Path_Ng";
            this.TextBox_Path_Ng.ReadOnly = true;
            this.TextBox_Path_Ng.Size = new System.Drawing.Size(313, 21);
            this.TextBox_Path_Ng.TabIndex = 26;
            this.TextBox_Path_Ng.Text = "根据条码自动生成文件夹路径 ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F);
            this.label8.Location = new System.Drawing.Point(4, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 14);
            this.label8.TabIndex = 25;
            this.label8.Text = "Failed 结果路径:";
            // 
            // comboBox_Server
            // 
            this.comboBox_Server.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Server.FormattingEnabled = true;
            this.comboBox_Server.Location = new System.Drawing.Point(394, 262);
            this.comboBox_Server.Name = "comboBox_Server";
            this.comboBox_Server.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Server.TabIndex = 27;
            this.comboBox_Server.SelectionChangeCommitted += new System.EventHandler(this.comboBox_Server_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F);
            this.label6.Location = new System.Drawing.Point(299, 265);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(84, 14);
            this.label6.TabIndex = 28;
            this.label6.Text = "Server IP：";
            // 
            // timer2
            // 
            this.timer2.Interval = 10000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label_start_time
            // 
            this.label_start_time.AutoSize = true;
            this.label_start_time.Font = new System.Drawing.Font("宋体", 10F);
            this.label_start_time.ForeColor = System.Drawing.Color.Teal;
            this.label_start_time.Location = new System.Drawing.Point(106, 259);
            this.label_start_time.Name = "label_start_time";
            this.label_start_time.Size = new System.Drawing.Size(14, 14);
            this.label_start_time.TabIndex = 30;
            this.label_start_time.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10F);
            this.label10.ForeColor = System.Drawing.Color.Teal;
            this.label10.Location = new System.Drawing.Point(6, 259);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 14);
            this.label10.TabIndex = 29;
            this.label10.Text = "软件启动时间:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10F);
            this.label9.ForeColor = System.Drawing.Color.Magenta;
            this.label9.Location = new System.Drawing.Point(6, 280);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 14);
            this.label9.TabIndex = 31;
            this.label9.Text = "最后处理时间：";
            // 
            // label_last_time
            // 
            this.label_last_time.AutoSize = true;
            this.label_last_time.Font = new System.Drawing.Font("宋体", 10F);
            this.label_last_time.ForeColor = System.Drawing.Color.Fuchsia;
            this.label_last_time.Location = new System.Drawing.Point(106, 280);
            this.label_last_time.Name = "label_last_time";
            this.label_last_time.Size = new System.Drawing.Size(14, 14);
            this.label_last_time.TabIndex = 32;
            this.label_last_time.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 347);
            this.Controls.Add(this.label_last_time);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label_start_time);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox_Server);
            this.Controls.Add(this.TextBox_Path_Ng);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkBox_AutoRun);
            this.Controls.Add(this.label_sn);
            this.Controls.Add(this.label_date);
            this.Controls.Add(this.label_amp);
            this.Controls.Add(this.label_type);
            this.Controls.Add(this.Btn_BarCode);
            this.Controls.Add(this.textBox_BarCode);
            this.Controls.Add(this.label_amp_name);
            this.Controls.Add(this.CheckBox_Local);
            this.Controls.Add(this.Label_Count_NG);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Label_Count_OK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Btn_Start);
            this.Controls.Add(this.TextBox_Path_Ok);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Btn_Browser);
            this.Controls.Add(this.TextBox_Path_Watch);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "AP Test Result  Watcher    Bulid:2020.06.19";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBox_Path_Watch;
        private System.Windows.Forms.Button Btn_Browser;
        private System.Windows.Forms.TextBox TextBox_Path_Ok;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Btn_Start;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Label_Count_OK;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label Label_Count_NG;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.CheckBox CheckBox_Local;
        private System.Windows.Forms.Label label_amp_name;
        private System.Windows.Forms.TextBox textBox_BarCode;
        private System.Windows.Forms.Button Btn_BarCode;
        private System.Windows.Forms.Label label_type;
        private System.Windows.Forms.Label label_amp;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.Label label_sn;
        private System.Windows.Forms.CheckBox checkBox_AutoRun;
        private System.Windows.Forms.TextBox TextBox_Path_Ng;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox_Server;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label_start_time;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label_last_time;
    }
}

