using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static void ShowChecked()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var x = checkedListBox1.Items.Count;
            var y = checkedListBox1.CheckedItems;
            var z = checkedListBox1.CheckedIndices;

            var checkedItems = checkedListBox1.CheckedItems.Cast<object>()
                .Aggregate(string.Empty, (current, item) => current + item.ToString());
            var checkedIndices = checkedListBox1.CheckedIndices.Cast<int>()
                .Aggregate(0, (current, item) => current + item);

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                
            }

            MessageBox.Show(checkedItems + checkedIndices.ToString());
        }
    }
}
