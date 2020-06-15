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

/*
1. Main函数里读一下这两个文件 存进array里面 
2. 读一下用户选的下拉栏 
3.把随机的部分填上
4.生成出来
*/

namespace RandomWords
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> AllNs;
        List<string> AllAdjs;
        List<string> AllPreps;
        List<string> AllVerbs;
        List<string> AllAdvs;

        List<TextBox> TBs = new List<TextBox>();
        List<ComboBox> CBs = new List<ComboBox>();
        private void Form1_Load(Object sender, EventArgs e)
        {
            TBs.Add(textBox1);
            TBs.Add(textBox2);
            TBs.Add(textBox3);
            TBs.Add(textBox4);
            TBs.Add(textBox5);
            TBs.Add(textBox6);
            TBs.Add(textBox7);
            TBs.Add(textBox8);

            CBs.Add(comboBox1);
            CBs.Add(comboBox2);
            CBs.Add(comboBox3);
            CBs.Add(comboBox4);
            CBs.Add(comboBox5);
            CBs.Add(comboBox6);
            CBs.Add(comboBox7);
            CBs.Add(comboBox8);

            try
            {
                AllAdjs = new List<string>(File.ReadAllLines(".\\形容词.txt"));
                AllNs = new List<string>(File.ReadAllLines(".\\名词.txt"));
                AllPreps = new List<string>(File.ReadAllLines(".\\介词.txt"));
                AllVerbs = new List<string>(File.ReadAllLines(".\\动词.txt"));
                AllAdvs = new List<string>(File.ReadAllLines(".\\副词.txt"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"\n"+"缺少上述的词语文件，且不能为空。");
                Application.Exit();
            }
        }
        private static long GenerateIntID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
        void FeedData(TextBox tb, List<string> raws)
        {
            Random random = new Random((int)GenerateIntID());
            tb.Text = raws[random.Next(0, raws.Count)];
        }

        private void btn_gen_Click(Object sender, EventArgs e)
        {
            for (Int32 i = 0; i < CBs.Count; i++)
            {
                ComboBox cb = CBs[i];
                switch (cb.Text)
                {
                    case "自选词":
                        // do nothing
                        break;
                    case "形容词":
                        FeedData(TBs[i], AllAdjs);
                        break;
                    case "名词":
                        FeedData(TBs[i], AllNs);
                        break;
                    case "介词":
                        FeedData(TBs[i], AllPreps);
                        break;
                    case "动词":
                        FeedData(TBs[i], AllVerbs);
                        break;
                    case "副词":
                        FeedData(TBs[i], AllAdvs);
                        break;
                    case "空":
                        TBs[i].Text = "";
                        break;
                    default:
                        MessageBox.Show("未知");
                        return;
                }
            }
        }

        private void Btn_add_record_Click(object sender, EventArgs e)
        {
            string result = "";
            for (int i = 0; i < TBs.Count; i++)
            {
                TextBox tb = TBs[i];
                if (tb.Text != "")
                {
                    if (result == "")
                    {
                        result = tb.Text;
                    }
                    else
                    {
                        result = result + " " + tb.Text;
                    }
                }
            }
            if (result != "")
            {
                File.AppendAllText(".\\组合结果.txt", result + "\n");
            }
        }

        private void Btn_copy_clipboard_Click(object sender, EventArgs e)
        {
            string result = "";
            for (int i = 0; i < TBs.Count; i++)
            {
                TextBox tb = TBs[i];
                if (tb.Text != "")
                {
                    if (result == "")
                    {
                        result = tb.Text;
                    }
                    else
                    {
                        result = result + " " + tb.Text;
                    }
                }
            }
            if (result != "")
            {
                Clipboard.SetText(result);
            }
        }

        private void Btn_random_Click(object sender, EventArgs e)
        {
            for (Int32 i = 0; i < TBs.Count; i++)
            {
                Random random = new Random((int)GenerateIntID());
                int ran = random.Next(0, 7);
                switch (ran)
                {
                    case 0:
                        // do nothing
                        break;
                    case 1:
                        FeedData(TBs[i], AllAdjs);
                        break;
                    case 2:
                        FeedData(TBs[i], AllNs);
                        break;
                    case 3:
                        FeedData(TBs[i], AllPreps);
                        break;
                    case 4:
                        FeedData(TBs[i], AllVerbs);
                        break;
                    case 5:
                        FeedData(TBs[i], AllAdvs);
                        break;
                    case 6:
                        TBs[i].Text = "";
                        break;
                    default:
                        MessageBox.Show("未知");
                        return;
                }
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
