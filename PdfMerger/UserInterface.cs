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
                    RemoveEmptyFolders(Path.Combine(uxDest.Text, dir.Name));
                    uxProgressLabel.Text = "Cleaning folders...";
                    MessageBox.Show("Finished merging PDFs!");
                }

                _operationInProgress = false;
                ResetUI();
            }
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

        private void ResetUI()
        {
            ToggleUIButtons();
            ResetProgress();
        }

        private void ResetProgress()
        {
            uxProgressBar.Value = 0;
            uxProgressBar.Maximum = CalculateNumFiles(uxSource.Text, true);
            uxProgressLabel.Text = "0/" + uxProgressBar.Maximum;
        }

        private void ToggleUIButtons()
        {
            uxSourceButton.Enabled = !uxSourceButton.Enabled;
            uxDestButton.Enabled = !uxDestButton.Enabled;
            uxMergeButton.Enabled = !uxMergeButton.Enabled;
        }

        public static int CalculateNumFiles(string location, bool searchSubfolders)
        {
            SearchOption option = (searchSubfolders) ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            return Directory.GetFiles(location, "*.*", option).Length;
        }

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
                    PdfDocument pdf = MergePdfs(files);

                    string realSavePath = savePath.Substring(0, savePath.LastIndexOf('\\'));

                    pdf.Save(Path.Combine(realSavePath, root.Name + ".pdf"));
                }

                subDirs = root.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    await MergeFolder(dirInfo, savePath);
                }
            }

            return true;
        }

        private PdfDocument MergePdfs(FileInfo[] pdfFilePaths)
        {
            // Create the PDF 
            PdfDocument mergedPDF = new PdfDocument();

            foreach (FileInfo pdf in pdfFilePaths)
            {
                PdfDocument inputDoc = PdfReader.Open(pdf.FullName, PdfDocumentOpenMode.Import);

                // We shouldn't need this because all of the pages are singles, but I am keeping it for more portability
                for (int i = 0; i < inputDoc.PageCount; i++)
                {
                    PdfPage page = inputDoc.Pages[i];
                    mergedPDF.AddPage(page);

                    if (InvokeRequired)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            uxProgressBar.Value++;
                            uxProgressLabel.Text = uxProgressBar.Value + "/" + uxProgressBar.Maximum;
                        }));
                    }
                }
            }

            return mergedPDF;
        }

        private void UserInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
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
    }
}
