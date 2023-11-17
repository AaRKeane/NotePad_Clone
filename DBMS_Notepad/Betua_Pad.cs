using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS_Notepad
{
    public partial class Betua_Pad : Form
    {
        public Betua_Pad()
        {
            InitializeComponent();
            statusView.Visible = true;
            statusStripAction.Text = "Action: Standby";
        }

        private void actionLabel(string userAction)
        {
            statusStripAction.Text = "Action: " + userAction;
        }

        bool txtChange = false; // Flag for the change in TextBox Area or Change in File

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(txtChange == false)
            {
                OpenFile(); //Refer to Code Line 212
            }
            else
            {
               DialogResult result = MessageBox.Show("Do you want to save changes?", "Save Changes",
               MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SaveFileDialog op = new SaveFileDialog();
                    op.Title = "Save";
                    op.Filter = "Text Document(*.txt)|*.txt| All Files(*.*)|*.*";
                    if (op.ShowDialog() == DialogResult.OK)
                        richTextBox1.SaveFile(op.FileName, RichTextBoxStreamType.PlainText);
                    this.Text = op.FileName;
                    txtChange = false;
                }
                else if (result == DialogResult.No) { OpenFile(); }
                else { this.Show(); }
            }
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog op = new SaveFileDialog();
            op.Title = "Save";
            op.Filter = "Text Document(*.txt)|*.txt| All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                string flName = Path.GetFileName(op.FileName);
                richTextBox1.SaveFile(op.FileName, RichTextBoxStreamType.PlainText);
                this.Text = flName; // Set the Form Title to TxtFile Name
                txtChange = false; // Set the flag to No change in TextBox Area or Change in File
            }   
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Do you want to close this window?";
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Have Fun Noting Details", "Sige Padayon", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        //Undo
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
            actionLabel("Action: Undo");
        }
        //Redo
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
            actionLabel("Redo");
        }
        //Copy
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
            actionLabel("Copy");
        }
        //Paste
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
            actionLabel("Paste");
        }
        //Cut
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
            actionLabel("Cut");
        }
        //Select All
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            actionLabel("Select All");
        }
        //Date or Time
        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = System.DateTime.Now.ToString();
            actionLabel("Show Date");
        }
        //Font
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog op = new FontDialog();
            if (op.ShowDialog() == DialogResult.OK)
                richTextBox1.Font = op.Font;

            actionLabel("Change Font");

        }
        //Color
        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog op = new ColorDialog();
            if (op.ShowDialog() == DialogResult.OK)
                richTextBox1.ForeColor = op.Color;
            actionLabel("Change Color");
        }

        private void Betua_Pad_FormClosing(object sender, FormClosingEventArgs e)
        {

            if(txtChange)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Save Changes",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SaveFileDialog op = new SaveFileDialog();
                    op.Title = "Save";
                    op.Filter = "Text Document(*.txt)|*.txt| All Files(*.*)|*.*";
                    if (op.ShowDialog() == DialogResult.OK)
                        richTextBox1.SaveFile(op.FileName, RichTextBoxStreamType.PlainText);
                    this.Text = op.FileName;
                }
                else if(result == DialogResult.Cancel) { e.Cancel = true; }
            }
            else { Application.Exit(); }
        }

        private void LinColStatus()
        {
            int position = richTextBox1.SelectionStart;
            int line = richTextBox1.GetLineFromCharIndex(position) + 1;
            int column = position - richTextBox1.GetFirstCharIndexOfCurrentLine() + 1;

            StatusLinCol.Text = string.Concat("Ln ", line, ", ", "Cl ", column);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Validate or Verify Changes of Text File
            if (richTextBox1.Text != "")
            {
                txtChange = true;
                
            }
            else
            {
                txtChange = false;
            }
            LinColStatus();
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Untitled"; //Change Form TitleName
            richTextBox1.Text = ""; //Reset to empty Text box
        }

        private void statusBarTool_Click(object sender, EventArgs e)
        {
            if (statusBarTool.Checked)
            {
                statusView.Visible = true;
            }
            else
            {
                statusView.Visible = false;
            }
        }

        private void OpenFile()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "open";
            op.Filter = "Text Document(*.txt)|*.txt| All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                string flName = Path.GetFileName(op.FileName); //Trim out the Path Directory
                richTextBox1.LoadFile(op.FileName, RichTextBoxStreamType.PlainText);
                this.Text = flName; // Set the Form Title to TxtFile Name
                txtChange = false; // Set the flag to No change in TextBox Area or Change in File
            }
        }
    }
}
