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
                string adrive = String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"\Amazon Drive\"); // Get Amazon Drive path
                string newname = String.Concat(adrive, fi.Name, "-encoded.png"); // Create new name based on old file
                using (FileStream fsSource = File.OpenRead(f)) // Opens original file for reading
                {
                    using (FileStream fsDest = File.OpenWrite(newname)) // Creates and opens the new file for writing
                    {
                        fsDest.Write(pngHeader, 0, pngHeader.Length); // Writes the PNG header as the first bytes of the file
                        byte[] buffer = new byte[2097152]; // Creates a buffer of 2MB to avoid an OutOfMemory exception
                        int bytesRead = 0; // Used to tell how many bytes have been read into the buffer
                        while ((bytesRead = fsSource.Read(buffer, 0, buffer.Length)) > 0) // Reads the file in 2MB chunks until it reaches the end of the file
                        {
                            fsDest.Write(buffer, 0, bytesRead); // Writes the 2MB chunks to the new file
                        }
                        fsDest.Close();
                    }
                    fsSource.Close();
                }

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
