using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wxc_timeDown
{
    public partial class AppSetface : Form
    {
        public user_time tempTime;
        public AppSetface()
        {
            InitializeComponent();
        }
        public AppSetface(user_time curT)
        {
            InitializeComponent();
            tempTime = curT;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color cc = colorDialog1.Color;
                button1.BackColor = cc;
            }
        }
        public int ConverData(string data)
        {
            return Convert.ToInt32(data);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbW.Text != "")
                {
                    tempTime.sizeW = ConverData(tbW.Text);
                }
                if (tbH.Text != "")
                {
                    tempTime.sizeH = ConverData(tbH.Text);
                }
                if (tbT.Text != "")
                {
                    if (ConverData(tbT.Text) > 100)
                    {
                        tempTime.touming = 100;
                    }
                    tempTime.touming = ConverData(tbT.Text);
                }
                tempTime.time = dateTimePicker1.Value;
                tempTime.fontColor = button1.BackColor;
                tempTime.localX = this.Location.X;
                tempTime.localY = this.Location.Y;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception)
            {
                MessageBox.Show("设置数据出错");
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
    
        }

        private void AppSetface_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";

            button1.BackColor = tempTime.fontColor;
            tbW.Text = tempTime.sizeW.ToString();
            tbH.Text = tempTime.sizeH.ToString();
            tbT.Text = tempTime.touming.ToString();
            try
            {
                dateTimePicker1.Value = tempTime.time;
            }
            catch (Exception)
            {
                dateTimePicker1.Value = DateTime.Now;
            }
            
            labLocal.Text = "X:" + this.Location.X.ToString() + " Y:" + this.Location.Y.ToString();
            this.Location = new Point(tempTime.localX, tempTime.localY);


        }

        private void AppSetface_LocationChanged(object sender, EventArgs e)
        {
            labLocal.Text = "X:" + this.Location.X.ToString() + " Y:" + this.Location.Y.ToString();
        }
        public user_time getData()
        {
            return tempTime;
        }

        private void AppSetface_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                this.DialogResult = DialogResult.Cancel;             
            }

        }
    }
}
