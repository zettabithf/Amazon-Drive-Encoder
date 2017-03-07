using System;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace AmazonEncoder
{
    public partial class AmazonEncoder : Form
    {
        private readonly BackgroundWorker _worker;
        private readonly OpenFileDialog _fileDialog;
        private readonly string _amazonDrive =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Amazon Drive\\"; // Get Amazon Drive path
        
        // Represents the first bytes of a PNG file
        readonly byte[] _pngHeader = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

        public AmazonEncoder()
        {
            InitializeComponent();
            _worker= new BackgroundWorker();
            _worker.DoWork += StartEncoding;
            _worker.ProgressChanged += ProgressChanged;
            _worker.RunWorkerCompleted += FinishEncoding;
            _worker.WorkerReportsProgress = true;
            _fileDialog = new OpenFileDialog {Multiselect = true};
            progressBar.Step = 1;
        }

        private void btn_EncodeFiles_Click(object sender, EventArgs e)
        {
            if (_fileDialog.ShowDialog(this) != DialogResult.OK) return;
            int count = _fileDialog.FileNames.GetLength(0); // Gets the number of files selected
            label2.Text = $@"0 of {count} Completed"; // Updates the form to show starting status
            progressBar.Value = 0; // Reset the progress bar's value to 0 in case there is multiple uses in this instance
            progressBar.Maximum = count; // Sets the maximum of the progress bar to the number of files selected
            chk_DeleteOriginal.Enabled = false;
            btn_EncodeFiles.Enabled = false;
            _worker.RunWorkerAsync(_fileDialog.FileNames);
        }

        private void FinishEncoding(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            chk_DeleteOriginal.Enabled = true;
            btn_EncodeFiles.Enabled = true;
            MessageBox.Show(this, @"All files have been encoded and written to the Amazon Drive folder.", @"Done",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            progressBar.PerformStep();
            label2.Text = progressBar.Value+label2.Text.Substring(label2.Text.IndexOf(' '));
        }

        private void StartEncoding(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            byte[] buffer = new byte[4096];
            foreach (string f in (string[])doWorkEventArgs.Argument)
            {
                FileInfo fi = new FileInfo(f); // Used for getting the original file name
                using (var fs = File.OpenWrite($"{_amazonDrive}{fi.Name}-encoded.png"))
                {
                    fs.Write(_pngHeader, 0, _pngHeader.Length);
                    using (var inFile = File.OpenRead(fi.FullName))
                    {
                        int count;
                        while ((count = inFile.Read(buffer, 0, buffer.Length)) != 0)
                            fs.Write(buffer, 0, count);
                    }
                    if(chk_DeleteOriginal.Checked)
                        fi.Delete();
                }
                _worker.ReportProgress(1);
            }
        }
    }
}

