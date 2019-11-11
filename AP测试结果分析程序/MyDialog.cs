using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AP_Watcher {
    public partial class MyDialog : Form
    {
        public MyDialog()
        {
            InitializeComponent();
        }
        public void SetMessage(string msg,int time_out)
        {
            Label_Msg.Text = msg;
            this.Size = new Size(msg.Length * 16 + 10,60);
            timer1.Interval = time_out;
            timer1.Enabled = true;
        }
        public void SetMessageColor(Color force_color, Color back_color)
        {
            Label_Msg.ForeColor = force_color;
            this.BackColor = back_color;

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.Close();
            this.Dispose();
        }
    }

}
