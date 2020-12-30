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
    public partial class E1 : Form
    {
        private String tymczasowy;
        private Edytor Forma1;

        public E1(Edytor Forma1)
        {
            InitializeComponent();
            this.Forma1 = Forma1;
        }

        public void Widok_T()
        {
            Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            tymczasowy = Forma1.textBox1.Text;

            while (i < Forma1.textBox1.Text.LastIndexOf(textBox1.Text))
            {
                Forma1.textBox1.Find(textBox1.Text, i, Forma1.textBox1.TextLength, RichTextBoxFinds.None);
                Forma1.textBox1.SelectionBackColor = Color.Aqua;
                i = Forma1.textBox1.Text.IndexOf(textBox1.Text, i) + 1;
            }
        }

        private void Form2_Exit(object sender, EventArgs e)
        {
            Forma1.textBox1.Text = tymczasowy;
        }

    }
}
