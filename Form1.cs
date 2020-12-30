using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace File_Admiral
{
    public partial class Form1 : Form
    {
        private ListView ActiveList = null;
        private ListView NonActiveList = null;
        private string ActivePath;        
        private string NonActivePath;
        private string Path1st = "C:\\";
        private string Path2nd = "C:\\";

        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(button1_KeyDown);
            this.KeyDown += new KeyEventHandler(button2_KeyDown);
            this.KeyDown += new KeyEventHandler(button3_KeyDown);
            this.KeyDown += new KeyEventHandler(button4_KeyDown);
            this.KeyDown += new KeyEventHandler(button5_KeyDown);
            this.KeyDown += new KeyEventHandler(button6_KeyDown);

            string[] DirNames;
            DirNames = Directory.GetLogicalDrives();
            foreach (string element in DirNames) 
            {
                comboBox1.Items.Add(element);
                comboBox2.Items.Add(element);
            }
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf("C:\\"); 
            comboBox2.SelectedIndex = comboBox2.Items.IndexOf("C:\\");
        }

        private void listView1_Enter(object sender, EventArgs e)
        {
            ActiveList = listView1;
            ActivePath = Path1st;
            NonActiveList = listView2;
            NonActivePath = Path2nd;
        }
        private void listView2_Enter(object sender, EventArgs e)
        {
            ActiveList = listView2;
            ActivePath = Path2nd;
            NonActiveList = listView1;
            NonActivePath = Path1st;
        }


        private void ListWrite(ListView lista, string Kat)
        {
                lista.Items.Clear();
                string[] DirNames;
                ListViewItem tmp;
                FileInfo FileInfo;
                DirectoryInfo DirInfo;

                if (!comboBox1.Items.Contains(Kat))
                {
                    lista.Items.Add(new ListViewItem("..."));
                }                   

                int i;
                DirNames = Directory.GetDirectories(Kat); 
                for (i = 0; i < DirNames.Length; i++) 
                {
                    DirInfo = new DirectoryInfo(DirNames[i]);
                    tmp = new ListViewItem(new string[] { DirInfo.Name, "<DIR>" });
                    lista.Items.Add(tmp); 
                }
                string FileName;
                int kropka;
                DirNames = Directory.GetFiles(Kat); 
                for (i = 0; i < DirNames.Length; i++) 
                {
                    FileInfo = new FileInfo(DirNames[i]);
                    FileName = FileInfo.Name;
                    kropka = FileInfo.Name.LastIndexOf('.');
                    if (kropka > 0)
                        FileName = FileName.Substring(0, FileInfo.Name.LastIndexOf('.'));
                    tmp = new ListViewItem(new string[] { FileName, FileInfo.Extension.Replace(".", ""), FileInfo.Length.ToString() });
                    lista.Items.Add(tmp);
                }
        }

        private void DirCopy(string from, string to) 
        {
                string[] DirNames;
                if (!Directory.Exists(to)) Directory.CreateDirectory(to);
                DirNames = Directory.GetFileSystemEntries(from);
                foreach (string element in DirNames)
                {
                    if (Directory.Exists(element))
                    {
                        DirCopy(element, to + Path.GetFileName(element) + "\\");
                    }
                    else
                    {
                        File.Copy(element, to + Path.GetFileName(element), true); 
                    }
                }                   
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Path1st = comboBox1.Text;
            try
            {
                ListWrite(listView1, Path1st);
                LabelDiskSpace(Path1st, this.label1);
            }
            catch
            {
                MessageBox.Show("Wystąpił błąd.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf("C:\\");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Path2nd = comboBox2.Text;
            try
            {
                ListWrite(listView2, Path2nd);
                LabelDiskSpace(Path2nd, this.label2);
            }
            catch
            {
                MessageBox.Show("Wystąpił błąd.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf("C:\\");
            }
        }


        private void LabelDiskSpace(string path, Label label)
        {
            System.IO.DriveInfo DyskInfo = new System.IO.DriveInfo(path);       
            label.Text = "Wolne: " + Convert.ToString(DyskInfo.TotalFreeSpace / 1073741824) + "GB z " + Convert.ToString(DyskInfo.TotalSize / 1073741824) + "GB";
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
                if (ActiveList.SelectedItems[0].Text == "...") 
                {
                    ActivePath = ActivePath.TrimEnd(new char[] { '\\' }); 
                    int lastchar;
                    lastchar = ActivePath.LastIndexOf('\\');
                    ActivePath = ActivePath.Substring(0, lastchar + 1);
                    ListWrite(ActiveList, ActivePath);
                }
                else if (ActiveList.SelectedItems[0].SubItems[1].Text == "<DIR>")
                {
                    ActivePath += (ActiveList.SelectedItems[0].Text + "\\");
                    try
                    {
                        ListWrite(ActiveList, ActivePath);
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("Nie można otworzyć folderu.", "Błąd",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    string nazwa = ActivePath + ActiveList.SelectedItems[0].Text + "." + ActiveList.SelectedItems[0].SubItems[1].Text;
                    if (ActiveList.SelectedItems[0].SubItems[1].Text == "txt")
                    {
                        Edytor edytor = new Edytor(nazwa);
                        edytor.ShowDialog();
                        edytor.Close();
                    }
                    else
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(nazwa);
                        }
                        catch
                        {
                            MessageBox.Show("Nie można otworzyć pliku.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return;
                    }                    
                }
                if (ActiveList == listView1)
                {
                    Path1st = ActivePath;
                }
                else
                {
                    Path2nd = ActivePath;
                }
        }


        //Otwórz
        private void button1_Click(object sender, EventArgs e)
        {
            if (ActiveList == null) return;
            if (ActiveList.SelectedItems.Count != 1) return;
            listView_MouseDoubleClick(null, null);
        }

        //Przenieś
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveList == null) return;
                for (int i = 0; i < ActiveList.SelectedItems.Count; i++)
                {
                    if (ActiveList.SelectedItems[i].Text == "...") return;
                    string from, to;
                    if (ActiveList.SelectedItems[i].SubItems[1].Text == "<DIR>")
                    {
                        from = ActivePath + ActiveList.SelectedItems[i].Text + "\\";
                        to = NonActivePath + ActiveList.SelectedItems[i].Text + "\\";
                        DirCopy(from, to);
                        Directory.Delete(from, true);
                    }
                    else
                    {
                        from = ActivePath + ActiveList.SelectedItems[i].Text + "." + ActiveList.SelectedItems[i].SubItems[1].Text;
                        to = NonActivePath + ActiveList.SelectedItems[i].Text + "." + ActiveList.SelectedItems[i].SubItems[1].Text;
                        File.Copy(from, to, true); 
                        File.Delete(from);
                    }
                }
                ListWrite(listView1, Path1st);
                ListWrite(listView2, Path2nd);
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd przy przenoszeniu.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Kopiuj
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveList == null) return;
                for (int i = 0; i < ActiveList.SelectedItems.Count; i++)
                {
                    if (ActiveList.SelectedItems[i].Text == "...") return;
                    string from, to;
                    if (ActiveList.SelectedItems[i].SubItems[1].Text == "<DIR>")
                    {
                        from = ActivePath + ActiveList.SelectedItems[i].Text + "\\";
                        to = NonActivePath + ActiveList.SelectedItems[i].Text + "\\";
                        DirCopy(from, to);
                    }
                    else
                    {
                        from = ActivePath + ActiveList.SelectedItems[i].Text + "." + ActiveList.SelectedItems[i].SubItems[1].Text;
                        to = NonActivePath + ActiveList.SelectedItems[i].Text + "." + ActiveList.SelectedItems[i].SubItems[1].Text;
                        File.Copy(from, to, true);
                    }
                }
                ListWrite(NonActiveList, NonActivePath);
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd przy kopiowaniu.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }        

        
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveList == null) return;
                if (ActiveList.SelectedItems[0].Text == "...") return;
                DialogResult result = MessageBox.Show("Czy na pewno usunąć ten plik/folder?", "Usuwanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;
                string path;
                for (int i = 0; i < ActiveList.SelectedItems.Count; i++)
                {
                    if (ActiveList.SelectedItems[i].SubItems[1].Text == "<DIR>")
                    {
                        path = ActivePath + ActiveList.SelectedItems[i].Text;
                        Directory.Delete(path, true);
                    }
                    else
                    {
                        path = ActivePath + ActiveList.SelectedItems[i].Text + "." + ActiveList.SelectedItems[i].SubItems[1].Text;
                        File.Delete(path);
                    }
                }
                ListWrite(ActiveList, ActivePath);
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd przy usuwaniu.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }

        
        private void button5_Click(object sender, EventArgs e)
        {
            if (ActiveList == null) return;
            if (ActiveList.SelectedItems.Count != 1) return;
            else
            {
                string to = ActivePath;
                Form2 form2 = new Form2(this, ActivePath);
                form2.ShowDialog();
                to = form2.sciezka_nazwa();
                form2.Close();

                ListWrite(listView1, Path1st);
                ListWrite(listView2, Path2nd);
            }
        }

        
        private void button6_Click(object sender, EventArgs e)
        {
            Edytor edytor = new Edytor();
            edytor.ShowDialog();
            edytor.Close();
        }

       

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F3")
            {
                button1.PerformClick();
            }
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F4")
            {
                button2.PerformClick();
            }
        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F5")
            {
                button3.PerformClick();
            }
        }

        private void button4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F6")
            {
                button4.PerformClick();
            }
        }

        private void button5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F7")
            {
                button5.PerformClick();
            }
        }

        private void button6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F8")
            {
                button6.PerformClick();
            }
        }
    }
}
