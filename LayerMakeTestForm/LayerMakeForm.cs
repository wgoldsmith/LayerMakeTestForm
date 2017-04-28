//-----------------------------------------------------------------------
// <copyright file="LayerMakeForm.cs" company="Goldsmith Engineering">
//     Copyright (c) Goldsmith Engineering. All rights reserved.
// </copyright>
// <author>Winston Goldsmith</author>
//-----------------------------------------------------------------------

namespace LayerMakeTestForm
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
    using MoreTextWindow;

    public partial class LayerMakeForm : Form
    {
        private MoreForm textForm;

        public LayerMakeForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataStateListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            addZs();
            layerTextBox.Text = dataStateListBox.SelectedItem.ToString().Split('\t')[0] + "-" + layerTextBox.Text.Substring(2);
        }

        private void categoryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            addZs();
            layerTextBox.Text = layerTextBox.Text.Substring(0, 2) + categoryListBox.SelectedItem.ToString().Split('\t')[0] + "-" + layerTextBox.Text.Substring(5);
        }

        private void entityTypeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            addZs();
            layerTextBox.Text = layerTextBox.Text.Substring(0, 5) + entityTypeListBox.SelectedItem.ToString().Split('\t')[0] + "-" + layerTextBox.Text.Substring(7);
        }

        private void addZs()
        {
            while (layerTextBox.Text.Length < 17)
            {
                layerTextBox.Text += "Z";
            }
        }

        private void entityDescListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // check radio buttons
            //     if button1 checked
            //         fill 1st 3 chars
            //     if button2 checked
            //         fill 2nd 3 chars
            //     else // button3 checked
            //         fill last 4 chars

            string ent = entityDescListBox.SelectedItem.ToString().Split('\t')[0]; // get all characters before the tab in the selected item in the entity descriptor box
            ent = ent.Trim(); // trim extra white space. just in case there were spaces before tab
            while (ent.Length < 4) // add trailing Zs to fill out each section of the entity descriptor
            {
                ent += "Z";
            }

            addZs();

            if (radioButton1.Checked)
            {
                //                                                       (to take off extra Z)
                //                  first 7 characters in textBox     + first 3 chars of ent + starting at tenth char to the end of the string 
                layerTextBox.Text = layerTextBox.Text.Substring(0, 7) + ent.Substring(0, 3) + layerTextBox.Text.Substring(10);
            } 
            else if (radioButton2.Checked)
            {
                //                                                       (to take off extra Z)
                //                  first 10 characters in textBox     + first 3 chars of ent + starting at thirteenth char to the end of the string 
                layerTextBox.Text = layerTextBox.Text.Substring(0, 10) + ent.Substring(0, 3) + layerTextBox.Text.Substring(13);
            }
            else // radioButton3.checked
            {
                //                  first 13 characters in textBox     + ent
                layerTextBox.Text = layerTextBox.Text.Substring(0, 13) + ent;
            }
        }

        private void LayerMakeForm_Load(object sender, EventArgs e)
        {
            dataStateListBox.SelectedIndex = 0;
            categoryListBox.SelectedIndex = 0;
            entityTypeListBox.SelectedIndex = 0;
            entityDescListBox.SelectedIndex = 0;
            layerTextBox.Text = layerTextBox.Text.Substring(0, 7) + "ZZZZZZZZZZ";
        }

        private void makeButton_Click(object sender, EventArgs e)
        {
            // checked length.
            // if less than 10, add Zs
            // if more, cut off at 10

            addZs();
            layerTextBox.Text = layerTextBox.Text.ToUpper(); // make all text in textbox upper case

            if (layerTextBox.Text.Length > 17)
            {
                layerTextBox.Text = layerTextBox.Text.Substring(0, 17);
            }

            // search layersListBox to see if there is already an identical one.
            if (Unique())
            {
                layersListBox.Items.Add(layerTextBox.Text);
            }
        }

        // checks that the text in layer textBox is different from
        // all the items in layersListBox
        private bool Unique()
        {
            bool isUnique = true;

            for (int w = 0; w < layersListBox.Items.Count; w++ )
            {
                if (layersListBox.Items[w].Equals(layerTextBox.Text))
                {
                    isUnique = false;
                    break;
                }
            }

            return isUnique;
        }

        private void layersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (layersListBox.SelectedIndex >= 0) // checks if something is actually selected. if not, index will be -1
            {
                enableButtons();
            }
        }

        private void enableButtons()
        {
            if (!deleteButton.Enabled)
            {
                deleteButton.Enabled = true;
            }
            if (!colorButton.Enabled)
            {
                colorButton.Enabled = true;
            }
            if (!ltypeButton.Enabled)
            {
                ltypeButton.Enabled = true;
            }
        }

        private void disableButtons()
        {
            if (deleteButton.Enabled)
            {
                deleteButton.Enabled = false;
            }
            if (colorButton.Enabled)
            {
                colorButton.Enabled = false;
            }
            if (ltypeButton.Enabled)
            {
                ltypeButton.Enabled = false;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // get current index so that, when the selected item is deleted, the item that takes its place will be selected.
            int index = layersListBox.SelectedIndex;
            layersListBox.Items.RemoveAt(layersListBox.SelectedIndex);

            if (index < layersListBox.Items.Count) // if the deleted item was not the last in the list, select item that replaces deleted item
            {
                layersListBox.SelectedIndex = index;
            }
            else // deleted item was last in the list
            {
                disableButtons(); // don't select anything and disable buttons
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {

        }

        private void ltypeButton_Click(object sender, EventArgs e)
        {

        }

        private void moreCatButton_Click(object sender, EventArgs e)
        {
            textForm = new MoreForm(this.categoryListBox);
            textForm.Show();
        }
        
        private void moreEntButton_Click(object sender, EventArgs e)
        {
            textForm = new MoreForm(this.entityDescListBox);
            textForm.Show();
        }

        private void textForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("yup");
        }
    }
}
