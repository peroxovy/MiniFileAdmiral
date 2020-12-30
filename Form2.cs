using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Admiral
{
    public partial class Form2 : Form
    {
        private string sciezka;
        Form1 Forma1;

        public Form2(Form1 Forma1, string sciezka)
        {
            InitializeComponent();
            this.Forma1 = Forma1;
            this.sciezka = sciezka;
        }
        public string sciezka_nazwa()
        {
            return this.sciezka;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.sciezka = this.sciezka + "\\" + textBox1.Text;
            this.Close();
        }
    }
}
