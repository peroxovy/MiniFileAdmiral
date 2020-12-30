using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace File_Admiral
{
    public partial class Edytor : Form
    {
        private E1 Wyszukaj_O;
        private string sciezka_plik;

        public Edytor()
        {
            InitializeComponent();            
        }

        //Konstruktor przyjmuje ściezkę pliku i go wczytuje
        public Edytor(String sciezka) 
        {
            InitializeComponent();
            this.sciezka_plik = sciezka;
            wczytaj(sciezka);
        }

        //Wczytuje plik określonej ścieżki
        private void wczytaj(string sciezka)
        {
            StreamReader pobranie = new StreamReader(sciezka);
                textBox1.Text = pobranie.ReadToEnd();
                pobranie.Close();
        }

        //Zapisanie pliku jako
        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog Okno_Dialogowe = new SaveFileDialog();
            Okno_Dialogowe.Filter = "Plik tekstowy (*.txt)|*.txt";
            if (Okno_Dialogowe.ShowDialog() == DialogResult.OK)
            {
                StreamWriter zapis = new StreamWriter(Okno_Dialogowe.FileName);
                zapis.Write(textBox1.Text);
                zapis.Close();
                MessageBox.Show("Plik został poprawnie zapisany.");
            }            
        }
        
        //Otwieranie Pliku
        private void otwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog Okno_Dialogowe = new OpenFileDialog();
            Okno_Dialogowe.Filter = "Plik tekstowy (*.txt)|*.txt";
            if (Okno_Dialogowe.ShowDialog() == DialogResult.OK)
            {
                StreamReader pobranie = new StreamReader(Okno_Dialogowe.FileName);
                textBox1.Text = pobranie.ReadToEnd();
                pobranie.Close();
            }
        }

        //Nowy
        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
        }

        //Wyszukaj
        public void wyszukajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            E1 Wyszukaj_O = new E1(this);
            Wyszukaj_O.Widok_T();
        }

        //Zamień
        private void zamieńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            E2 Zamien_O = new E2(this);
            Zamien_O.Widok_T();
        }

        //Zapisz do scieżki
        private void zapiszToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter zapis = new StreamWriter(sciezka_plik);
                zapis.Write(textBox1.Text);
                zapis.Close();
                MessageBox.Show("Plik został nadpisany.");
            }
            catch (Exception)
            {
                MessageBox.Show("Plik nie został nadpisany.");
            }
            
        }
    }
}
