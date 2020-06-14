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

namespace RandomWords {
    public partial class Form1 : Form {
        public Form1()
        {
            InitializeComponent();
        }

        string[] Items = {
            "自选词",
            "形容词",
            "名词",
            "空"
        };

        enum ItemE {
            opt,
            adj,
            n,
            nil
        }

        List<string> AllNs;
        List<string> AllAdjs;

        List<TextBox> TBs = new List<TextBox>();
        List<ComboBox> CBs = new List<ComboBox>();
        private void Form1_Load( Object sender, EventArgs e )
        {
            TBs.Add( textBox1 );
            TBs.Add( textBox2 );
            TBs.Add( textBox3 );
            TBs.Add( textBox4 );
            TBs.Add( textBox5 );
            TBs.Add( textBox6 );

            CBs.Add( comboBox1 );
            CBs.Add( comboBox2 );
            CBs.Add( comboBox3 );
            CBs.Add( comboBox4 );
            CBs.Add( comboBox5 );
            CBs.Add( comboBox6 );

            try {
                AllAdjs = new List<string>( File.ReadAllLines( ".\\adjs.txt" ) );
                AllNs = new List<string>( File.ReadAllLines( ".\\ns.txt" ) );
            } catch ( Exception ex ) {
                MessageBox.Show( ex.Message );
                Application.Exit();
            }
        }
        private static long GenerateIntID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64( buffer, 0 );
        }
        void FeedData( TextBox tb, List<string> raws )
        {
            Random random = new Random( (int)GenerateIntID() );
            tb.Text =  raws[random.Next( 0, raws.Count )];
        }

        private void btn_gen_Click( Object sender, EventArgs e )
        {
            for ( Int32 i = 0; i < CBs.Count; i++ ) {
                ComboBox cb = CBs[i];
                switch ( cb.Text ) {
                    case "自选词":
                        // do nothing
                        break;
                    case "形容词":
                        FeedData( TBs[i], AllAdjs );
                        break;
                    case "名词":
                        FeedData( TBs[i], AllNs );
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
    }
}
