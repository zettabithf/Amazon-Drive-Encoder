using System;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace AmazonEncoder
{
    public partial class Form1 : Form
    {
        private readonly BackgroundWorker _worker;
        private OpenFileDialog _fileDialog;
        public Form1()
        {
            InitializeComponent();
            _worker= new BackgroundWorker();
            _worker.DoWork += StartEncoding;
            _worker.ProgressChanged += ProgressChanged;
            _worker.RunWorkerCompleted += FinishEncoding;
            _fileDialog = new OpenFileDialog {Multiselect = true};
        }

        // Represents the first bytes of a PNG file
        readonly byte[] _pngHeader = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

        private void button1_Click(object sender, EventArgs e)
        {
            if (_fileDialog.ShowDialog(this) != DialogResult.OK) return;
            int count = _fileDialog.FileNames.GetLength(0); // Gets the number of files selected
            label2.Text = $@"0 of {count} Completed"; // Updates the form to show starting status
            progressBar1.Value = 0; // Reset the progress bar's value to 0 in case there is multiple uses in this instance
            progressBar1.Maximum = count; // Sets the maximum of the progress bar to the number of files selected
            progressBar1.Step = 1;
            checkBox1.Enabled = false;
            button1.Enabled = false;
            _worker.RunWorkerAsync(_fileDialog.FileNames);
        }

        private void FinishEncoding(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            checkBox1.Enabled = true;
            button1.Enabled = true;
            MessageBox.Show(this, @"All files have been encoded and written to the Amazon Drive folder.", @"Done",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            progressBar1.PerformStep();
            label2.Text = progressBar1.Value+label2.Text.Substring(label2.Text.IndexOf(' '));
        }

        private void StartEncoding(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            string adrive = String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"\Amazon Drive\"); // Get Amazon Drive path
            foreach (string f in (string[])doWorkEventArgs.Argument)
            {
                FileInfo fi = new FileInfo(f); // Used for getting the original file name
                using (var fs = File.OpenWrite($"{adrive}{fi.Name}-encoded.png"))
                {
                    fs.Write(_pngHeader, 0, _pngHeader.Length);
                    fs.Write(File.ReadAllBytes(f),0,(int)fi.Length);
                    if(checkBox1.Checked)
                        fi.Delete();
                }
            }
            _worker.ReportProgress(1);
        }
    }
}

