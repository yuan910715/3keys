using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private System.IO.Ports.SerialPort com = new System.IO.Ports.SerialPort();
        private delegate void succSet();
        private delegate void succClient(string str);
        private string[] funtionkeys = { "KEY_LEFT_CTRL", "KEY_LEFT_SHIFT", "KEY_LEFT_ALT", "KEY_LEFT_GUI", "KEY_RIGHT_CTRL", "KEY_RIGHT_SHIFT", "KEY_RIGHT_ALT", "KEY_RIGHT_GUI", "KEY_UP_ARROW", "KEY_DOWN_ARROW", "KEY_LEFT_ARROW", "KEY_RIGHT_ARROW", "KEY_BACKSPACE", "KEY_TAB", "KEY_RETURN", "KEY_ESC", "KEY_INSERT", "KEY_DELETE", "KEY_PAGE_UP", "KEY_PAGE_DOWN", "KEY_HOME", "KEY_END", "KEY_CAPS_LOCK", "KEY_F1", "KEY_F2", "KEY_F3", "KEY_F4", "KEY_F5", "KEY_F6", "KEY_F7", "KEY_F8", "KEY_F9", "KEY_F10", "KEY_F11", "KEY_F12", "KEY_F13", "KEY_F14", "KEY_F15", "KEY_F16", "KEY_F17", "KEY_F18", "KEY_F19", "KEY_F20", "KEY_F21", "KEY_F22", "KEY_F23", "KEY_F24" };
        private byte[] funtionkeysbytes = { 0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0xDA, 0xD9, 0xD8, 0xD7, 0xB2, 0xB3, 0xB0, 0xB1, 0xD1, 0xD4, 0xD3, 0xD6, 0xD2, 0xD5, 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0xCA, 0xCB, 0xCC, 0xCD, 0xF0, 0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0xFA, 0xFB };

        private string[] mediakeys = { "CONSUMER_POWER", "CONSUMER_SLEEP", "MEDIA_RECORD", "MEDIA_FAST_FORWARD", "MEDIA_REWIND", "MEDIA_NEXT", "MEDIA_PREVIOUS", "MEDIA_STOP", "MEDIA_PLAY_PAUSE", "MEDIA_PAUSE", "MEDIA_VOLUME_MUTE", "MEDIA_VOLUME_UP", "MEDIA_VOLUME_DOWN"};
        private byte[] mediakeysbytes = { 0x30, 0x32, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xCD, 0xB0, 0xE2, 0xE9, 0xEA };
        public Form1()
        {
            InitializeComponent();
            String[] coms = System.IO.Ports.SerialPort.GetPortNames();
            comboBox1.Items.Clear();
            foreach (String a in coms)
            {
                comboBox1.Items.Add(a);
                comboBox1.SelectedIndex = 0;
            }
            if (comboBox1.Items.Count > 0)
            {
                button1.Enabled = true;
            }
            panel1.Enabled = false;
            panel2.Enabled = false;
            panel3.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (com.IsOpen)
                {
                    com.Close();
                    button1.Text = "连接";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;                  
                    radioButton5.Checked = false;
                    radioButton6.Checked = false;                  
                    radioButton8.Checked = false;
                    radioButton9.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                    radioButton7.Checked = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                    comboBox2.Items.Clear();
                    comboBox3.Items.Clear();
                    comboBox4.Items.Clear();
                    button3.Enabled = false;
                    panel1.Enabled = false;
                    panel2.Enabled = false;
                    panel3.Enabled = false;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    
                }
                else
                { 
                    com = new System.IO.Ports.SerialPort();
                    com.PortName = comboBox1.SelectedItem.ToString();
                    com.BaudRate = 115200;
                    com.DataBits = 8;
                    com.StopBits = System.IO.Ports.StopBits.One;
                    com.Parity = System.IO.Ports.Parity.None;
                    com.DtrEnable = true;
                    com.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(dataReceived);
                    com.Open();
                    byte[] bs = { 0x03, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x23 };
                    com.Write(bs, 0, 8);                    
               }
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message,"错误");
            }
        }

        private void dataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort com1 = (System.IO.Ports.SerialPort)sender;            
            string str = com1.ReadExisting();
            if (str.Equals("1"))
            {
                succSet sc = new succSet(successSet);
                this.BeginInvoke(sc);
                MessageBox.Show("设置成功!","提示");
            }
            else {
                succClient sc = new succClient(successCon);
                this.BeginInvoke(sc,str);                 
            }
        }
        private void successCon(string str) 
        {
            button1.Text = "断开连接";
            button3.Enabled = true;
            panel1.Enabled = true;
            panel2.Enabled = true;
            panel3.Enabled = true;
            byte key1 = Convert.ToByte(str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0], 16);
            byte key2 = Convert.ToByte(str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[1], 16);
            byte key3 = Convert.ToByte(str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[2], 16);
            byte key1media = Convert.ToByte(str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[3], 16);
            byte key2media = Convert.ToByte(str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[4], 16);
            byte key3media = Convert.ToByte(str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[5], 16);
            bool key1label= false;
            if (key1media == 0x01)
            {
                for (int i = 0; i < mediakeysbytes.Length; i++)
                {
                    if (key1 == mediakeysbytes[i])
                    {
                        radioButton3.Checked = true;
                        comboBox2.Items.Clear();
                        comboBox2.Items.AddRange(mediakeys);
                        comboBox2.SelectedItem = mediakeys[i];
                        key1label = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < funtionkeysbytes.Length; i++)
                {
                    if (key1 == funtionkeysbytes[i])
                    {
                        radioButton2.Checked = true;
                        comboBox2.Items.Clear();
                        comboBox2.Items.AddRange(funtionkeys);
                        comboBox2.SelectedItem = funtionkeys[i];
                        key1label = true;
                    }
                }
                if (!key1label)
                {
                    radioButton1.Checked = true;
                    textBox1.Text = ((char)key1).ToString();
                }
            }
            bool key2label = false;
            if (key2media == 0x01)
            {
                for (int i = 0; i < mediakeysbytes.Length; i++)
                {
                    if (key2 == mediakeysbytes[i])
                    {
                        radioButton4.Checked = true;
                        comboBox3.Items.Clear();
                        comboBox3.Items.AddRange(mediakeys);
                        comboBox3.SelectedItem = mediakeys[i];
                        key2label = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < funtionkeysbytes.Length; i++)
                {
                    if (key2 == funtionkeysbytes[i])
                    {
                        radioButton6.Checked = true;
                        comboBox3.Items.Clear();
                        comboBox3.Items.AddRange(funtionkeys);
                        comboBox3.SelectedItem = funtionkeys[i];
                        key2label = true;
                    }
                }
                if (!key2label)
                {
                    radioButton5.Checked = true;
                    textBox2.Text = ((char)key2).ToString();
                }
            }
            bool key3label = false;
            if (key3media == 0x01)
            {
                for (int i = 0; i < mediakeysbytes.Length; i++)
                {
                    if (key3 == mediakeysbytes[i])
                    {
                        radioButton7.Checked = true;
                        comboBox4.Items.Clear();
                        comboBox4.Items.AddRange(mediakeys);
                        comboBox4.SelectedItem = mediakeys[i];
                        key3label = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < funtionkeysbytes.Length; i++)
                {
                    if (key3 == funtionkeysbytes[i])
                    {
                        radioButton9.Checked = true;
                        comboBox4.Items.Clear();
                        comboBox4.Items.AddRange(funtionkeys);
                        comboBox4.SelectedItem = funtionkeys[i];
                        key3label = true;
                    }
                }
                if (!key3label)
                {
                    radioButton8.Checked = true;
                    textBox3.Text = ((char)key3).ToString();
                }
            }
        }
        private void successSet()
        {
            button3.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            String[] coms = System.IO.Ports.SerialPort.GetPortNames();
            comboBox1.Text = "";
           comboBox1.Items.Clear();
            foreach (String a in coms)
            {
                if (!comboBox1.Items.Contains(a))
                {
                    comboBox1.Items.Add(a);
                }
                comboBox1.SelectedIndex = 0;
            }
            if (comboBox1.Items.Count > 0)
            {
                button1.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try {
                button3.Enabled = false;
                byte[] bs = { 0x03,0xff,0xff,0xff, 0x00, 0x00, 0x00, 0x40 };
                if (radioButton1.Checked)
                {
                    if (textBox1.Text.Length != 1)
                    {
                        MessageBox.Show("key1输入错误", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    else if (textBox1.Text.ToArray()[0] < 0 || textBox1.Text.ToArray()[0] > 255)
                    {
                        MessageBox.Show("key1输入错误", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    else
                    {
                        bs[1] = (byte)textBox1.Text.ToArray()[0];
                    }
                }
                else {
                    if (comboBox2.SelectedItem == null) {
                        MessageBox.Show("key1选择错误", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    if (radioButton2.Checked)
                    {
                        bs[1] = funtionkeysbytes[comboBox2.SelectedIndex];
                    }
                    else if (radioButton3.Checked)
                    {
                        bs[1] = mediakeysbytes[comboBox2.SelectedIndex];
                        bs[4] = 0x01;
                    }
                    else {
                        MessageBox.Show("key1选择错误", "错误");
                        button3.Enabled = true;
                        return;
                    }               
                }
                if (radioButton5.Checked)
                {
                    if (textBox2.Text.Length != 1)
                    {
                        MessageBox.Show("key2输入错误", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    else if (textBox2.Text.ToArray()[0] < 0 || textBox2.Text.ToArray()[0] > 255)
                    {
                        MessageBox.Show("key2输入错误", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    else
                    {
                        bs[2] = (byte)textBox2.Text.ToArray()[0];
                    }
                }
                else {
                    if (comboBox3.SelectedItem == null)
                    {
                        MessageBox.Show("key2选择错误", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    if (radioButton6.Checked)
                    {
                        bs[2] = funtionkeysbytes[comboBox3.SelectedIndex];
                    }
                    else if (radioButton4.Checked)
                    {
                        bs[2] = mediakeysbytes[comboBox3.SelectedIndex];
                        bs[5] = 0x01;
                    }
                    else
                    {
                        MessageBox.Show("key2选择错误", "错误");
                        button3.Enabled = true;
                        return;
                    }

                }
                if (radioButton8.Checked)
                {
                    if (textBox3.Text.Length != 1)
                    {
                        MessageBox.Show("key3输入错误", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    else if (textBox3.Text.ToArray()[0] < 0 || textBox3.Text.ToArray()[0] > 255)
                    {
                        MessageBox.Show("key3输入错误", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    else
                    {
                        bs[3] = (byte)textBox3.Text.ToArray()[0];
                    }
                }
                else {
                    if (comboBox4.SelectedItem == null)
                    {
                        MessageBox.Show("key3选择错误", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    if (radioButton9.Checked)
                    {
                        bs[3] = funtionkeysbytes[comboBox4.SelectedIndex];
                    }
                    else if (radioButton7.Checked)
                    {
                        bs[3] = mediakeysbytes[comboBox4.SelectedIndex];
                        bs[6] = 0x01;
                    }
                    else
                    {
                        MessageBox.Show("key3选择错误", "错误");
                        button3.Enabled = true;
                        return;
                    }

                }
                com.Write(bs,0,8);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
                button3.Enabled = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) {
                textBox1.Enabled = true;
                comboBox2.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox1.Enabled = false;
                comboBox2.Enabled = true;
                comboBox2.Text = "";
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(funtionkeys);
            }
        }
             
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                textBox2.Enabled = true;
                comboBox3.Enabled = false;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                textBox2.Enabled = false;
                comboBox3.Enabled = true;
                comboBox3.Text = "";
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(funtionkeys);
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                textBox3.Enabled = true;
                comboBox4.Enabled = false;
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                textBox3.Enabled = false;
                comboBox4.Enabled = true;
                comboBox4.Text = "";
                comboBox4.Items.Clear();
                comboBox4.Items.AddRange(funtionkeys);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                textBox1.Enabled = false;
                comboBox2.Enabled = true;
                comboBox2.Text = "";
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(mediakeys);
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                textBox2.Enabled = false;
                comboBox3.Enabled = true;
                comboBox3.Text = "";
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(mediakeys);
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
            {
                textBox3.Enabled = false;
                comboBox4.Enabled = true;
                comboBox4.Text = "";
                comboBox4.Items.Clear();
                comboBox4.Items.AddRange(mediakeys);
            }
        }
    }
}
