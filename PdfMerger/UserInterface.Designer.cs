namespace PdfMerger
{
    partial class UserInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uxMergeButton = new System.Windows.Forms.Button();
            this.uxSourceBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.uxProgressBar = new System.Windows.Forms.ProgressBar();
            this.uxSourceButton = new System.Windows.Forms.Button();
            this.uxDestButton = new System.Windows.Forms.Button();
            this.uxSource = new System.Windows.Forms.TextBox();
            this.uxDest = new System.Windows.Forms.TextBox();
            this.uxDestBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.uxProgressLabel = new System.Windows.Forms.Label();
            this.uxUseFolderSchema = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uxMergeButton
            // 
            this.uxMergeButton.Location = new System.Drawing.Point(138, 126);
            this.uxMergeButton.Name = "uxMergeButton";
            this.uxMergeButton.Size = new System.Drawing.Size(185, 23);
            this.uxMergeButton.TabIndex = 0;
            this.uxMergeButton.Text = "Merge";
            this.uxMergeButton.UseVisualStyleBackColor = true;
            this.uxMergeButton.Click += new System.EventHandler(this.uxMergeButton_ClickAsync);
            // 
            // uxProgressBar
            // 
            this.uxProgressBar.Location = new System.Drawing.Point(13, 70);
            this.uxProgressBar.Name = "uxProgressBar";
            this.uxProgressBar.Size = new System.Drawing.Size(310, 23);
            this.uxProgressBar.TabIndex = 1;
            // 
            // uxSourceButton
            // 
            this.uxSourceButton.Location = new System.Drawing.Point(291, 12);
            this.uxSourceButton.Name = "uxSourceButton";
            this.uxSourceButton.Size = new System.Drawing.Size(32, 23);
            this.uxSourceButton.TabIndex = 2;
            this.uxSourceButton.Text = "...";
            this.uxSourceButton.UseVisualStyleBackColor = true;
            this.uxSourceButton.Click += new System.EventHandler(this.uxSourceButton_Click);
            // 
            // uxDestButton
            // 
            this.uxDestButton.Location = new System.Drawing.Point(291, 41);
            this.uxDestButton.Name = "uxDestButton";
            this.uxDestButton.Size = new System.Drawing.Size(32, 23);
            this.uxDestButton.TabIndex = 3;
            this.uxDestButton.Text = "...";
            this.uxDestButton.UseVisualStyleBackColor = true;
            this.uxDestButton.Click += new System.EventHandler(this.uxDestButton_Click);
            // 
            // uxSource
            // 
            this.uxSource.Location = new System.Drawing.Point(79, 12);
            this.uxSource.Name = "uxSource";
            this.uxSource.ReadOnly = true;
            this.uxSource.Size = new System.Drawing.Size(206, 20);
            this.uxSource.TabIndex = 4;
            // 
            // uxDest
            // 
            this.uxDest.Location = new System.Drawing.Point(79, 44);
            this.uxDest.Name = "uxDest";
            this.uxDest.ReadOnly = true;
            this.uxDest.Size = new System.Drawing.Size(206, 20);
            this.uxDest.TabIndex = 5;
            // 
            // uxProgressLabel
            // 
            this.uxProgressLabel.AutoSize = true;
            this.uxProgressLabel.Location = new System.Drawing.Point(143, 96);
            this.uxProgressLabel.Name = "uxProgressLabel";
            this.uxProgressLabel.Size = new System.Drawing.Size(24, 13);
            this.uxProgressLabel.TabIndex = 6;
            this.uxProgressLabel.Text = "0/0";
            // 
            // uxUseFolderSchema
            // 
            this.uxUseFolderSchema.AutoSize = true;
            this.uxUseFolderSchema.Location = new System.Drawing.Point(13, 130);
            this.uxUseFolderSchema.Name = "uxUseFolderSchema";
            this.uxUseFolderSchema.Size = new System.Drawing.Size(119, 17);
            this.uxUseFolderSchema.TabIndex = 7;
            this.uxUseFolderSchema.Text = "Use Folder Schema";
            this.uxUseFolderSchema.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Source:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Destination:";
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 161);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uxUseFolderSchema);
            this.Controls.Add(this.uxProgressLabel);
            this.Controls.Add(this.uxDest);
            this.Controls.Add(this.uxSource);
            this.Controls.Add(this.uxDestButton);
            this.Controls.Add(this.uxSourceButton);
            this.Controls.Add(this.uxProgressBar);
            this.Controls.Add(this.uxMergeButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 200);
            this.MinimumSize = new System.Drawing.Size(350, 200);
            this.Name = "UserInterface";
            this.Text = "Pdf Merge Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserInterface_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uxMergeButton;
        private System.Windows.Forms.FolderBrowserDialog uxSourceBrowser;
        private System.Windows.Forms.ProgressBar uxProgressBar;
        private System.Windows.Forms.Button uxSourceButton;
        private System.Windows.Forms.Button uxDestButton;
        private System.Windows.Forms.TextBox uxSource;
        private System.Windows.Forms.TextBox uxDest;
        private System.Windows.Forms.FolderBrowserDialog uxDestBrowser;
        private System.Windows.Forms.Label uxProgressLabel;
        private System.Windows.Forms.CheckBox uxUseFolderSchema;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

