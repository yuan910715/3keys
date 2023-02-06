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

        private string[] mediakeys = { "CONSUMER_POWER", "CONSUMER_SLEEP", "MEDIA_RECORD", "MEDIA_FAST_FORWARD", "MEDIA_REWIND", "MEDIA_NEXT", "MEDIA_PREVIOUS", "MEDIA_STOP", "MEDIA_PLAY_PAUSE", "MEDIA_PAUSE", "MEDIA_VOLUME_MUTE", "MEDIA_VOLUME_UP", "MEDIA_VOLUME_DOWN","CONSUMER_BRIGHTNESS_UP","CONSUMER_BRIGHTNESS_DOWN","CONSUMER_SCREENSAVER","CONSUMER_PROGRAMMABLE_BUTTON_CONFIGURATION","CONSUMER_CONTROL_CONFIGURATION","CONSUMER_EMAIL_READER","CONSUMER_CALCULATOR","CONSUMER_EXPLORER","CONSUMER_BROWSER_HOME","CONSUMER_BROWSER_BACK","CONSUMER_BROWSER_FORWARD","CONSUMER_BROWSER_REFRESH","CONSUMER_BROWSER_BOOKMARKS"};
        private ushort[] mediakeysbytes = { 0x30, 0x32, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xCD, 0xB0, 0xE2, 0xE9, 0xEA ,0x06F, 0x70F, 0x19e, 0x182, 0x183,0x18A,0x192,0x194,0x223,0x224,0x225,0x227,0x22A};
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
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
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
                    radioButton10.Checked = false;
                    radioButton11.Checked = false;
                    radioButton12.Checked = false;
                    radioButton13.Checked = false;
                    radioButton14.Checked = false;
                    radioButton15.Checked = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
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
                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                    textBox6.Enabled = false;
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
                    byte[] bs = { 0x02, 0x03 };
                    com.Write(bs, 0, 2);                    
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
            byte key1func = Convert.ToByte(str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[3], 16);
            byte key2func = Convert.ToByte(str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[4], 16);
            byte key3func = Convert.ToByte(str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[5], 16);
            byte key1f = Convert.ToByte(str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[6], 16);
            byte key2f = Convert.ToByte(str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[7], 16);
            byte key3f = Convert.ToByte(str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[8], 16);
            string s1 = str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[9];
            string s2 = str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[10];
            string s3 = str.Split(new string[] { "\r\n" }, StringSplitOptions.None)[11];

            bool key1label= false;
            if (key1func == 0x01)
            { //media
                if (key1f == 0xff)
                {
                    for (int i = 0; i < mediakeysbytes.Length; i++)
                    {
                        if (key1 == (byte)(mediakeysbytes[i] & 0xff))
                        {
                            radioButton3.Checked = true;
                            comboBox2.Items.Clear();
                            comboBox2.Items.AddRange(mediakeys);
                            comboBox2.SelectedItem = mediakeys[i];
                            key1label = true;
                        }
                    }
                }
                else {
                    ushort key1tmp = (ushort)((key1f<<8) + key1);
                    for (int i = 0; i < mediakeysbytes.Length; i++)
                    {
                        if (key1tmp == mediakeysbytes[i])
                        {
                            radioButton3.Checked = true;
                            comboBox2.Items.Clear();
                            comboBox2.Items.AddRange(mediakeys);
                            comboBox2.SelectedItem = mediakeys[i];
                            key1label = true;
                        }
                    }
                }            
            }
            else if (key1func == 0x02) //mix
            {
                radioButton10.Checked = true;
                for (int i = 0; i < funtionkeysbytes.Length; i++)
                {
                    if (key1f == funtionkeysbytes[i])
                    {                      
                        comboBox2.Items.Clear();
                        comboBox2.Items.AddRange(funtionkeys);
                        comboBox2.SelectedItem = funtionkeys[i];
                    }
                }
                textBox1.Text = ((char)key1).ToString();
            }
            else if (key1func == 0x03) //string
            {
                radioButton11.Checked = true;
                textBox4.Text = s1;
            }
            else { //normal
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
            if (key2func == 0x01)
            { //media
                for (int i = 0; i < mediakeysbytes.Length; i++)
                {
                    if (key2 == (byte)(mediakeysbytes[i] & 0xff))
                    {
                        radioButton4.Checked = true;
                        comboBox3.Items.Clear();
                        comboBox3.Items.AddRange(mediakeys);
                        comboBox3.SelectedItem = mediakeys[i];
                        key2label = true;
                    }
                }
            }
            else if (key2func == 0x02) //mix
            {
                radioButton12.Checked = true;
                for (int i = 0; i < funtionkeysbytes.Length; i++)
                {
                    if (key2f == funtionkeysbytes[i])
                    {
                        comboBox3.Items.Clear();
                        comboBox3.Items.AddRange(funtionkeys);
                        comboBox3.SelectedItem = funtionkeys[i];
                    }
                }
                textBox2.Text = ((char)key2).ToString();
            }
            else if (key2func == 0x03) //string
            {
                radioButton13.Checked = true;
                textBox5.Text = s2;
            }
            else
            { //normal
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
            if (key3func == 0x01)
            { //media
                for (int i = 0; i < mediakeysbytes.Length; i++)
                {
                    if (key3 == (byte)(mediakeysbytes[i] & 0xff))
                    {
                        radioButton7.Checked = true;
                        comboBox4.Items.Clear();
                        comboBox4.Items.AddRange(mediakeys);
                        comboBox4.SelectedItem = mediakeys[i];
                        key3label = true;
                    }
                }
            }
            else if (key3func == 0x02) //mix
            {
                radioButton14.Checked = true;
                for (int i = 0; i < funtionkeysbytes.Length; i++)
                {
                    if (key3f == funtionkeysbytes[i])
                    {
                        comboBox4.Items.Clear();
                        comboBox4.Items.AddRange(funtionkeys);
                        comboBox4.SelectedItem = funtionkeys[i];
                    }
                }
                textBox3.Text = ((char)key3).ToString();
            }
            else if (key3func == 0x03) //string
            {
                radioButton15.Checked = true;
                textBox6.Text = s3;
            }
            else
            { //normal
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
                byte[] bs = { 0x02, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    0x03 };
                //key1
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
                        bs[4] = 0x00;
                    }
                }
                else if (radioButton11.Checked) //string
                {
                    if (textBox4.Text.Length == 0)
                    {
                        MessageBox.Show("key1字符串不能为空", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    for (int i = 0; i < textBox4.Text.ToArray().Length; i++) {
                        if (textBox4.Text.ToArray()[i] < 0 || textBox4.Text.ToArray()[i] > 255)
                        {
                            MessageBox.Show("key1字符串输入错误，无此键位", "错误");
                            button3.Enabled = true;
                            return;
                        }
                        bs[10 + i] = (byte)textBox4.Text.ToArray()[i];
                    }
                    bs[4] = 0x03;
                }
                else
                {
                    if (comboBox2.SelectedItem == null) {
                        MessageBox.Show("key1选择错误", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    if (radioButton2.Checked) //func
                    {
                        bs[1] = funtionkeysbytes[comboBox2.SelectedIndex];
                        bs[4] = 0x00;
                    }
                    else if (radioButton3.Checked)  //media
                    {
                        bs[1] = (byte)(mediakeysbytes[comboBox2.SelectedIndex] & 0xff);
                        if ((byte)((mediakeysbytes[comboBox2.SelectedIndex] >> 8) & 0xff) != 0) {
                            bs[7] = (byte)((mediakeysbytes[comboBox2.SelectedIndex] >> 8) & 0xff);
                        }
                        bs[4] = 0x01;
                    }
                    else if (radioButton10.Checked) //mix
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
                        bs[1] = (byte)textBox1.Text.ToArray()[0];
                        bs[7] = funtionkeysbytes[comboBox2.SelectedIndex];
                        bs[4] = 0x02;
                    }
                    else {
                        MessageBox.Show("key1选择错误", "错误");
                        button3.Enabled = true;
                        return;
                    }               
                }
                //key2
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
                        bs[5] = 0x00;
                    }
                }
                else if (radioButton13.Checked) //string
                {
                    if (textBox5.Text.Length == 0)
                    {
                        MessageBox.Show("key2字符串不能为空", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    for (int i = 0; i < textBox5.Text.ToArray().Length; i++)
                    {
                        if (textBox5.Text.ToArray()[i] < 0 || textBox5.Text.ToArray()[i] > 255) {
                            MessageBox.Show("key2字符串输入错误，无此键位", "错误");
                            button3.Enabled = true;
                            return;
                        }
                        bs[40 + i] = (byte)textBox5.Text.ToArray()[i];
                    }
                    bs[5] = 0x03;
                }
                else {
                    if (comboBox3.SelectedItem == null)
                    {
                        MessageBox.Show("key2选择错误", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    if (radioButton6.Checked) //func
                    {
                        bs[2] = funtionkeysbytes[comboBox3.SelectedIndex];
                        bs[5] = 0x00;
                    }
                    else if (radioButton4.Checked)  //media
                    {
                        bs[2] = (byte)(mediakeysbytes[comboBox3.SelectedIndex] & 0xff);
                        if ((byte)((mediakeysbytes[comboBox3.SelectedIndex] >> 8) & 0xff) != 0)
                        {
                            bs[8] = (byte)((mediakeysbytes[comboBox3.SelectedIndex] >> 8) & 0xff);
                        }
                        bs[5] = 0x01;
                    }
                    else if (radioButton12.Checked) //mix
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
                        bs[2] = (byte)textBox2.Text.ToArray()[0];
                        bs[8] = funtionkeysbytes[comboBox3.SelectedIndex];
                        bs[5] = 0x02;
                    }
                    else
                    {
                        MessageBox.Show("key2选择错误", "错误");
                        button3.Enabled = true;
                        return;
                    }

                }
                //key3
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
                        bs[6] = 0x00;
                    }
                }
                else if (radioButton15.Checked) //string
                {
                    if (textBox6.Text.Length == 0)
                    {
                        MessageBox.Show("key3字符串不能为空", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    for (int i = 0; i < textBox6.Text.ToArray().Length; i++)
                    {
                        if (textBox6.Text.ToArray()[i] < 0 || textBox6.Text.ToArray()[i] > 255)
                        {
                            MessageBox.Show("key3字符串输入错误，无此键位", "错误");
                            button3.Enabled = true;
                            return;
                        }
                        bs[70 + i] = (byte)textBox6.Text.ToArray()[i];
                    }
                    bs[6] = 0x03;
                }
                else {
                    if (comboBox4.SelectedItem == null)
                    {
                        MessageBox.Show("key3选择错误", "错误");
                        button3.Enabled = true;
                        return;
                    }
                    if (radioButton9.Checked) //func
                    {
                        bs[3] = funtionkeysbytes[comboBox4.SelectedIndex];
                        bs[6] = 0x00;
                    }
                    else if (radioButton7.Checked)  //media
                    {
                        bs[3] = (byte)(mediakeysbytes[comboBox4.SelectedIndex] & 0xff);
                        if ((byte)((mediakeysbytes[comboBox4.SelectedIndex] >> 8) & 0xff) != 0)
                        {
                            bs[9] = (byte)((mediakeysbytes[comboBox4.SelectedIndex] >> 8) & 0xff);
                        }
                        bs[6] = 0x01;
                    }
                    else if (radioButton14.Checked) //mix
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
                        bs[3] = (byte)textBox3.Text.ToArray()[0];
                        bs[9] = funtionkeysbytes[comboBox4.SelectedIndex];
                        bs[6] = 0x02;
                    }
                    else
                    {
                        MessageBox.Show("key3选择错误", "错误");
                        button3.Enabled = true;
                        return;
                    }

                }
                
                com.Write(bs,0,101);                
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
                textBox4.Enabled = false;
                comboBox2.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox1.Enabled = false;
                textBox4.Enabled = false;
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
                textBox4.Enabled = false;
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

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
            {
                textBox1.Enabled = true;
                textBox4.Enabled = false;
                comboBox2.Enabled = true;
                comboBox2.Text = "";
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(funtionkeys);
            }
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton11.Checked)
            {
                textBox1.Enabled = false;
                textBox4.Enabled = true;
                comboBox2.Enabled = false;           
            }
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked)
            {
                textBox2.Enabled = true;
                textBox5.Enabled = false;
                comboBox3.Enabled = true;
                comboBox3.Text = "";
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(funtionkeys);
            }
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton13.Checked)
            {
                textBox2.Enabled = false;
                textBox5.Enabled = true;
                comboBox3.Enabled = false;
            }
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton14.Checked)
            {
                textBox3.Enabled = true;
                textBox6.Enabled = false;
                comboBox4.Enabled = true;
                comboBox4.Text = "";
                comboBox4.Items.Clear();
                comboBox4.Items.AddRange(funtionkeys);
            }
        }

        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton15.Checked)
            {
                textBox3.Enabled = false;
                textBox6.Enabled = true;
                comboBox4.Enabled = false;
            }
        }
    }
}
