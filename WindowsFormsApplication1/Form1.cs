using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double _buyaode = 1;
        private void jisuan_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            _count = 0;
            _buyaode = Convert.ToDouble(textBox3.Text);
            double _daozhang = Convert.ToDouble(price.Text) * Convert.ToDouble(textBox2.Text);
            label6.Text = (_daozhang - _buyaode).ToString();
            jisuanBeishuDijian(Convert.ToDouble(price.Text) * Convert.ToDouble(textBox2.Text), Convert.ToDouble(beishu.Text));
        }
        int _count = 0;
        public void jisuanBeishuDijian(double price, double beishu)
        {
            if (price > _buyaode)
            {
                _count++;
                double _fanli = price * beishu;
                textBox1.AppendText(string.Format("返利次数{0}，剩余价格{1}，返利价格{2}", _count, price, _fanli) + System.Environment.NewLine);
                jisuanBeishuDijian(price - _fanli, beishu);
            }
        }
    }
}
