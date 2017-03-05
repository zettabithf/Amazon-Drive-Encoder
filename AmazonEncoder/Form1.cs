using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace AmazonEncoder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Represents the first bytes of a PNG file
        byte[] pngHeader = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int count = ofd.FileNames.GetLength(0); // Gets the number of files selected
                label2.Text = String.Format("0 of {0} Completed", count); // Updates the form to show starting status
                progressBar1.Value = 0; // Reset the progress bar's value to 0 in case there is multiple uses in this instance
                progressBar1.Maximum = count; // Sets the maximum of the progress bar to the number of files selected
                Thread x = new Thread(() => EncodeFiles(ofd.FileNames, count, checkBox1.Checked)); // Creates the new thread which encodes the files
                x.Start(); // Starts the thread
            }
        }

        void EncodeFiles(string[] filepaths, int total, bool delete)
        {
            int done = 0; // Used to show how many files we've completed
            foreach (string f in filepaths)
            {
                FileInfo fi = new FileInfo(f); // Used for getting the original file name
                byte[] b = File.ReadAllBytes(f); // Get original files' bytes
                byte[] ret = new byte[pngHeader.Length + b.Length]; // Create a new byte array the length of the PNG header + the length of original file
                Buffer.BlockCopy(pngHeader, 0, ret, 0, pngHeader.Length); // Copies the PNG header bytes to the beginning of the new byte array
                Buffer.BlockCopy(b, 0, ret, pngHeader.Length, b.Length); // Copies the original file bytes to into the new byte array, after the PNG header

                string adrive = String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"\Amazon Drive\"); // Get Amazon Drive path
                string newname = String.Concat(adrive, fi.Name, "-encoded.png"); // Create new name based on old file
                File.WriteAllBytes(newname, ret); // Write new file to Amazon Drive folder

                // Delete old file if necessary
                if (delete)
                {
                    fi.Delete();
                }

                // Updates the form to show status
                Invoke(new MethodInvoker(() =>
                {
                    done += 1;
                    progressBar1.Value += 1;
                    label2.Text = String.Format("{0} of {1} Completed", done, total);
                }));
            }

            // Notify the user of completion
            Invoke(new MethodInvoker(() => MessageBox.Show(String.Format("{0} files have been encoded and written to the Amazon Drive folder.", total), "Done", MessageBoxButtons.OK, MessageBoxIcon.Information)));
        }
    }
}
