

namespace MoreTextWindow
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class MoreForm : Form
    {
        private ListBox lBox;

        public MoreForm()
        {
            InitializeComponent();
        }

        public MoreForm(ListBox lBox)
        {
            InitializeComponent();
            this.lBox = lBox;
            listBox1.Items.AddRange(lBox.Items);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0) // if something is selected
                lBox.SelectedItem = listBox1.SelectedItem;
            this.Close();
        }
    }
}
