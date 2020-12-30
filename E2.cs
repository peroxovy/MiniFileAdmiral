using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace File_Admiral
{
    public partial class E2 : Form
    {
        private Edytor Forma1;

        public E2(Edytor Forma1)
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
            String Do_Zamiany = textBox1.Text;
            String Zamiennik = textBox2.Text;
            int i = 0;
            int znalezionych = 0;
            int a = Zamiennik.Length - Do_Zamiany.Length;
            foreach (Match znaleziony in Regex.Matches(Forma1.textBox1.Text, Do_Zamiany))
            {
                Forma1.textBox1.Select(znaleziony.Index + i, Do_Zamiany.Length);
                i = i + a;
                Forma1.textBox1.SelectedText = Zamiennik;
                znalezionych++;
            }
            MessageBox.Show("Zamieniono " + znalezionych + " wyników.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Forma1.textBox1.SelectedText = textBox2.Text;
            MessageBox.Show("Zamieniono zaznaczone.");
        }
    }
}
