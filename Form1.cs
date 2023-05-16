using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Windows.ApplicationModel.Activation;

namespace To_Do_List_exam_27._04._2023
{
    public partial class Form1 : Form
    {
        ListViewItem item;

        List<string> all_items = new List<string>();
        List<string> all_sub_items = new List<string>();

        public Form1() {
            InitializeComponent();

            comboBox1.Items.Add("Все");
            comboBox1.Items.Add("День");
            comboBox1.Items.Add("Неделю");
            comboBox1.Items.Add("Месяц");
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            timer1.Interval = 2000;
            timer1.Enabled = true;
            timer1.Start();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            List<string> sort_items = new List<string>();
            List<string> sub_items = new List<string>();

            object selected = comboBox1.SelectedItem;
            string str_selected = selected.ToString();
            switch(str_selected) {
                case "День":
                    for(int i = 0; i < listView1.Items.Count; i++) {
                        if(listView1.Items[i].Text == DateTime.Today.ToLongDateString()) {
                            sort_items.Add(listView1.Items[i].Text);
                            MessageBox.Show(listView1.Items[i].Text);
                            for (int j = 0; j < listView1.Columns.Count; j++) {
                                sub_items.Add(item.SubItems[j].Text);
                                MessageBox.Show(item.SubItems[j].Text);
                            }
                        }
                    }
                    
                    listView1.Items.Clear();
                    for (int i = 0; i < sort_items.Count; i++) {
                        item = new ListViewItem(sort_items[i]);
                        for(int j = 1; j < listView1.Columns.Count; j++) {
                            item.SubItems.Add(sub_items[j]);
                        }
                        listView1.Items.AddRange(new ListViewItem[] { item });
                    }

                    break;

                case "Все":
                    listView1.Items.Clear();
                    for(int i = 0; i < all_items.Count; i++) {
                        item = new ListViewItem(all_items[i]);
                        for (int j = 0; j < all_sub_items.Count; j++) {
                            item.SubItems.Add(all_sub_items[j]);
                        }
                        listView1.Items.AddRange(new ListViewItem[] { item });
                    }
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            /*items.Clear();
            for (int i = 0; i < listView1.Items.Count; i++) {
                items.Add(listView1.Items[i].Text);
                for (int j = 1; j < listView1.Columns.Count; j++) {
                    all_sub_items.Add(item.SubItems[j].Text);
                }
            }*/
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            button1_Click(sender, e);
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e) {
            FileStream file = new FileStream("file.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(file);
            foreach(ListViewItem l in listView1.Items) {
                sw.WriteLine(l.Text);
            }
            sw.Close();
        }

        public string ReceiveDate {
            get { return listView1.Items.ToString(); }
            set { item = listView1.Items.Add(value); } 
        }
        public string ReceiveTime {
            get { return listView1.Items.ToString(); }
            set { item.SubItems.Add(value); }
        }
        public string ReceivePriority {
            get { return listView1.Items.ToString(); }
            set { item.SubItems.Add(value); }
        }
        public string ReceiveTag {
            get { return listView1.Items.ToString(); }
            set { item.SubItems.Add(value); }
        }
        public string ReceiveAllSubItems {
            get { return null; }
            set { all_sub_items.Add(value); }
        }
        public string ReceiveAllItems {
            get { return null; }
            set { all_items.Add(value); }
        }

        private void button1_Click(object sender, EventArgs e) {
            AddCase addCase = new AddCase();
            addCase.Owner = this;
            addCase.ShowDialog();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e) {
            listView1.ListViewItemSorter = new ListViewComparer(e.Column);
            listView1.Sort();
        }

        private void listView1_DoubleClick(object sender, EventArgs e) {
            System.Windows.Forms.ListView.SelectedIndexCollection collection = listView1.SelectedIndices;
            if (collection.Count != 0) {
                listView1.Items.RemoveAt(collection[0]);
            } 
        }


        private void savePDFFileToolStripMenuItem_Click(object sender, EventArgs e) {
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfWriter.GetInstance(document, new FileStream("To-do list.pdf", FileMode.OpenOrCreate));
            document.Open();

            BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            PdfPTable table = new PdfPTable(listView1.Columns.Count);
            PdfPCell cell = new PdfPCell(new Phrase("Мой список дел"));
            cell.Colspan = listView1.Columns.Count;
            cell.HorizontalAlignment = 1;
            cell.Border = 0;
            table.AddCell(cell);

            //Заголовок таблицы
            for (int j = 0; j < listView1.Columns.Count; j++) {
                 cell = new PdfPCell(new Phrase(new Phrase(listView1.Columns[j].Text)));
                 table.AddCell(cell);
            }

            //Элементы таблицы
            for (int i = 0; i < all_sub_items.Count; i++) {
                table.AddCell(new PdfPCell(new Phrase(all_sub_items[i], font)));
            }
            document.Add(table);
            document.Close();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|PDF files (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                if ((myStream = saveFileDialog1.OpenFile()) != null) {
                    StreamWriter sw = new StreamWriter(myStream);
                    foreach (ListViewItem l in listView1.Items) {
                        sw.WriteLine(l.Text);
                    }
                    sw.Close();
                    myStream.Close();
                }
            }            
        }

        
    }



    public class ListViewComparer : System.Collections.IComparer {
        public int colNumber;

        public ListViewComparer(int column) {
            colNumber = column;
        }

        public int Compare(object objectA, object objectB) {
            ListViewItem item1 = objectA as ListViewItem;
            ListViewItem item2 = objectB as ListViewItem;

            string textA = string.Empty;
            string textB = string.Empty;

            if (item1.SubItems.Count > colNumber) {
                textA = item1.SubItems[colNumber].Text;
            }
            if (item2.SubItems.Count > colNumber) {
                textB = item2.SubItems[colNumber].Text;
            }

            if (colNumber == 0) {
                DateTime date1, date2;
                if (DateTime.TryParse(textA, out date1) && DateTime.TryParse(textB, out date2)) {
                    return date1.CompareTo(date2);
                }
            }
            return textB.CompareTo(textA);
        }
    }


}
