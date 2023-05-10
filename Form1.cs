using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_Do_List_exam_27._04._2023
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();

            comboBox1.Items.Add("День");
            comboBox1.Items.Add("Неделю");
            comboBox1.Items.Add("Месяц");
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            object selected = comboBox1.SelectedItem;
            string str_selected = selected.ToString();
            switch(str_selected) {
                
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            AddCase addCase = new AddCase();
            addCase.ShowDialog();
            

        }

        /*private void dateTimePicker1_ValueChanged(object sender, EventArgs e) {
              listBox1.Items.Clear();
              listBox1.Items.Add(dateTimePicker1.Value.ToLongDateString());
          }*/

    }
}
