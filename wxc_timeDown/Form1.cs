using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wxc_timeDown
{
    public struct user_time
    {
        public DateTime time;
        public Color fontColor;
        public int touming;
        public int localX;
        public int localY;
        public int sizeW;
        public int sizeH;
    }
    public partial class Form1 : Form
    {

        public user_time curUserTime;
        public int lastTime = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void initAppinfo()
        {
            string downFilePath = "file.json";

            if (File.Exists(downFilePath) == false)
            {
                MessageBox.Show("还没有存储过数据,已经重新生成");
                FileStream file = File.Open(downFilePath, FileMode.Create);
            }
            foreach (string line in File.ReadLines(downFilePath))
            {
                try
                {
                    user_time temp = JsonConvert.DeserializeObject<user_time>(line);
                    curUserTime = temp;
                }
                catch (Exception)
                {
                    int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;

                    int iActulaHeight = Screen.PrimaryScreen.Bounds.Height;

                    curUserTime.localX = iActulaWidth / 2;
                    curUserTime.localY = iActulaHeight / 2;
                    curUserTime.sizeH = 100;
                    curUserTime.sizeW = 100;
                    curUserTime.touming = 100;
                    curUserTime.time = DateTime.Now;
                    curUserTime.fontColor = Color.Red;
                    //(DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                }
            }
        }
        private int getDiffTime()
        {
            int diffTime = 0;
            TimeSpan midTime = curUserTime.time - DateTime.Now;
            diffTime = (int)midTime.TotalSeconds;
            return diffTime;
        }
        private int getDataSec()
        {
            TimeSpan sp = curUserTime.time - DateTime.Now;
            return (int)sp.TotalSeconds;
        }
        private string getDateDateTime(int sec)
        {
            string st = sec.ToString();
            var t = TimeSpan.FromSeconds(sec);
            st = t.ToString(@"dd\:hh\:mm\:ss\.f", null);
            return st;
        }
        private void setAppFace()
        {
            this.Location = new Point(curUserTime.localX, curUserTime.localY);
            //this.Size = new Size(curUserTime.sizeW, curUserTime.sizeH);
            label1.ForeColor = curUserTime.fontColor;
            label1.Size = new Size(curUserTime.sizeW, curUserTime.sizeH);
            label2.ForeColor = curUserTime.fontColor;
            label2.Size = new Size(curUserTime.sizeW, curUserTime.sizeH);
            lastTime = getDiffTime();

        }
        private void Form1_Load(object sender, EventArgs e)
        {           
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;
            //read json file
            initAppinfo();
            //set face
            setAppFace();
            timer1.Start();
            this.ShowInTaskbar = false;
            this.notifyIcon1.Visible = true;
        }
        private void saveDataTojson()
        {
            string initPath = "file.json";
            FileStream file = File.Open(initPath, FileMode.Create);
            string strinit = JsonConvert.SerializeObject(curUserTime);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(strinit + "\r\n");
            file.Write(byteArray, 0, byteArray.Length);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 显示窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppSetface asf = new AppSetface(curUserTime);
            if (asf.ShowDialog() == DialogResult.OK)
            {
                curUserTime = asf.getData();
                setAppFace();
                saveDataTojson();
                timer1.Start();
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lastTime = getDataSec();
            if(lastTime == 0)
            {
                timer1.Stop();
                MessageBox.Show("time out");
            }
            label1.Text = lastTime.ToString();
            label2.Text = getDateDateTime(lastTime);  
        }

        private void 透明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;
        }
    }
}
