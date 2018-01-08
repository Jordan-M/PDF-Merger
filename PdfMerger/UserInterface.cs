using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfMerger
{
    public partial class UserInterface : Form
    {
        private bool _operationInProgress = false;

        public UserInterface()
        {
            InitializeComponent();
        }

        private void uxSourceButton_Click(object sender, EventArgs e)
        {
            if (uxSourceBrowser.ShowDialog() == DialogResult.OK)
            {
                uxSource.Text = uxSourceBrowser.SelectedPath;
            }
        }

        private void uxDestButton_Click(object sender, EventArgs e)
        {
            if (uxDestBrowser.ShowDialog() == DialogResult.OK)
            {
                uxDest.Text = uxDestBrowser.SelectedPath;
            }
        }

        private async void uxMergeButton_ClickAsync(object sender, EventArgs e)
        {
            if (uxSource.Text != String.Empty && uxDest.Text != String.Empty)
            {
                ResetUI();
                _operationInProgress = true;

                DirectoryInfo dir = new DirectoryInfo(uxSource.Text);
                Task<bool> test = await Task.Factory.StartNew(() => MergeFolder(dir, uxDest.Text));

                if (test.Result)
                {
                    uxProgressLabel.Text = "Cleaning folders...";
                    RemoveEmptyFolders(Path.Combine(uxDest.Text, dir.Name));
                    MessageBox.Show("Finished merging PDFs!");
                }

                _operationInProgress = false;
                ResetUI();
            }
        }

        private void UserInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            // We don't want to stop windows from trying to close down
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (_operationInProgress)
            {
                DialogResult result = MessageBox.Show("An operation is currently in progress, are you sure you would like to quit?", "Confirm exit", MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                    e.Cancel = true;
                else
                    return;
            }
        }

        /// <summary>
        /// Toggles all the UI buttons, and resets the progress bar
        /// </summary>
        private void ResetUI()
        {
            ToggleUIButtons();
            ResetProgress();
        }

        /// <summary>
        /// Resets the progress bar to its idle state
        /// </summary>
        private void ResetProgress()
        {
            uxProgressBar.Value = 0;
            uxProgressBar.Maximum = FileHelper.CalculateNumFiles(uxSource.Text, true);
            uxProgressLabel.Text = "0/" + uxProgressBar.Maximum;
        }

        /// <summary>
        /// Sets all buttons to the opposite enabled status
        /// </summary>
        private void ToggleUIButtons()
        {
            uxSourceButton.Enabled = !uxSourceButton.Enabled;
            uxDestButton.Enabled = !uxDestButton.Enabled;
            uxMergeButton.Enabled = !uxMergeButton.Enabled;
        }


        /// <summary>
        /// Removes all empty folders in the root folder
        /// </summary>
        /// <param name="root">Path to folder you want to delete the empty folders of</param>
        private void RemoveEmptyFolders(string root)
        {
            foreach (var directory in Directory.GetDirectories(root))
            {
                RemoveEmptyFolders(directory);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }
        }

        /// <summary>
        /// Merges all the pdfs in a specified folder
        /// </summary>
        /// <param name="root">Folder of pdfs to merge</param>
        /// <param name="savePath">The location where you want to save the pdfs</param>
        /// <returns>True if the operation succeded false otherwise</returns>
        private async Task<bool> MergeFolder(DirectoryInfo root, string savePath)
        {
            savePath = Path.Combine(savePath, root.Name);

            if (Directory.Exists(savePath))
            {
                MessageBox.Show("Folder Conflict! Please choose a diffrent save location.");
                return false;
            }

            Directory.CreateDirectory(savePath);

            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            try
            {
                files = root.GetFiles("*.pdf");
            }
            catch(UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (files != null)
            {
                if (files.Count() > 0)
                {
                    string realSavePath = savePath.Substring(0, savePath.LastIndexOf('\\'));

                    PdfDocument pdf = MergePdfs(files);

                    if (uxUseFolderSchema.Checked)
                    {
                        string siteName = realSavePath.Substring(realSavePath.LastIndexOf('\\') + 1);
                        pdf.Save(Path.Combine(realSavePath, String.Format("{0} - {1}.pdf", siteName, root.Name)));
                    }
                    else
                    {
                        pdf.Save(Path.Combine(realSavePath, String.Format("{0}.pdf", root.Name)));
                    }

                }

                subDirs = root.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    await MergeFolder(dirInfo, savePath);
                }
            }

            return true;
        }

        /// <summary>
        /// Merges pdfs into one file
        /// </summary>
        /// <param name="pdfFilePaths">An array contating the paths of the pdf files</param>
        /// <returns>A single pdf created from all the pdfs in pdfFilePaths</returns>
        private PdfDocument MergePdfs(FileInfo[] pdfFilePaths)
        {
            // Create the PDF 
            using (PdfDocument mergedPDF = new PdfDocument())
            {
                foreach (FileInfo pdf in pdfFilePaths)
                {
                    using (PdfDocument inputDoc = PdfReader.Open(pdf.FullName, PdfDocumentOpenMode.Import))
                    {
                        // We shouldn't need this because all of the pages are singles, but I am keeping it for more portability
                        for (int i = 0; i < inputDoc.PageCount; i++)
                        {
                            PdfPage page = inputDoc.Pages[i];
                            mergedPDF.AddPage(page);
                            page.Close();
                        }

                        if (InvokeRequired)
                        {
                            BeginInvoke(new Action(() =>
                            {
                                uxProgressBar.Value += inputDoc.PageCount;
                                uxProgressLabel.Text = uxProgressBar.Value + "/" + uxProgressBar.Maximum;
                            }));
                        }
                    }
                }

                return mergedPDF;
            }
        }


    }
}
