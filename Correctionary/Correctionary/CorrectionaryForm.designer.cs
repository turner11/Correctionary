
using Correctionary.GuiFramework;
namespace Correctionary
{
    partial class CorrectionaryForm:System.Windows.Forms.Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CorrectionaryForm));
            this.nfi = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsTxbWord = new Correctionary.GuiFramework.WaterMarkToolStripTextBox();
            this.tsTxbContext = new Correctionary.GuiFramework.WaterMarkToolStripTextBox();
            this.tsmTranslate = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtfContext = new System.Windows.Forms.RichTextBox();
            this.rtfWord = new System.Windows.Forms.RichTextBox();
            this.lblWord = new System.Windows.Forms.Label();
            this.lblContext = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.chbShowLoadingNotification = new System.Windows.Forms.CheckBox();
            this.chbTopMost = new System.Windows.Forms.CheckBox();
            this.pbLoading = new System.Windows.Forms.PictureBox();
            this.cmsTray.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // nfi
            // 
            this.nfi.BalloonTipText = "Highlight the text you want to translate, and hit the Hotkey.";
            this.nfi.BalloonTipTitle = "Correctionary";
            this.nfi.ContextMenuStrip = this.cmsTray;
            this.nfi.Icon = ((System.Drawing.Icon)(resources.GetObject("nfi.Icon")));
            this.nfi.Text = "Correctionary";
            this.nfi.Visible = true;
            this.nfi.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nfi_MouseDoubleClick);
            // 
            // cmsTray
            // 
            this.cmsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsTxbWord,
            this.tsTxbContext,
            this.tsmTranslate,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.cmsTray.Name = "cmsTray";
            this.cmsTray.Size = new System.Drawing.Size(161, 142);
            // 
            // tsTxbWord
            // 
            this.tsTxbWord.ForeColor = System.Drawing.Color.Gray;
            this.tsTxbWord.Name = "tsTxbWord";
            this.tsTxbWord.Size = new System.Drawing.Size(100, 23);
            this.tsTxbWord.WatermarkActive = true;
            this.tsTxbWord.WatermarkText = "Word to translate";
            this.tsTxbWord.TextChanged += new System.EventHandler(this.tsTxbWord_TextChanged);
            // 
            // tsTxbContext
            // 
            this.tsTxbContext.ForeColor = System.Drawing.Color.Gray;
            this.tsTxbContext.Name = "tsTxbContext";
            this.tsTxbContext.Size = new System.Drawing.Size(100, 23);
            this.tsTxbContext.WatermarkActive = true;
            this.tsTxbContext.WatermarkText = "Type context of word";
            this.tsTxbContext.TextChanged += new System.EventHandler(this.tsTxbContext_TextChanged);
            // 
            // tsmTranslate
            // 
            this.tsmTranslate.Name = "tsmTranslate";
            this.tsmTranslate.Size = new System.Drawing.Size(160, 22);
            this.tsmTranslate.Text = "Translate!";
            this.tsmTranslate.ToolTipText = "Click on me toTranslate!";
            this.tsmTranslate.Click += new System.EventHandler(this.tsmTranslate_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // rtfContext
            // 
            this.rtfContext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfContext.Location = new System.Drawing.Point(65, 104);
            this.rtfContext.Name = "rtfContext";
            this.rtfContext.Size = new System.Drawing.Size(385, 205);
            this.rtfContext.TabIndex = 3;
            this.rtfContext.Text = "";
            // 
            // rtfWord
            // 
            this.rtfWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfWord.Location = new System.Drawing.Point(65, 71);
            this.rtfWord.Name = "rtfWord";
            this.rtfWord.Size = new System.Drawing.Size(386, 18);
            this.rtfWord.TabIndex = 4;
            this.rtfWord.Text = "";
            // 
            // lblWord
            // 
            this.lblWord.AutoSize = true;
            this.lblWord.Location = new System.Drawing.Point(5, 74);
            this.lblWord.Name = "lblWord";
            this.lblWord.Size = new System.Drawing.Size(33, 13);
            this.lblWord.TabIndex = 5;
            this.lblWord.Text = "Word";
            // 
            // lblContext
            // 
            this.lblContext.AutoSize = true;
            this.lblContext.Location = new System.Drawing.Point(3, 107);
            this.lblContext.Name = "lblContext";
            this.lblContext.Size = new System.Drawing.Size(43, 13);
            this.lblContext.TabIndex = 6;
            this.lblContext.Text = "Context";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.chbShowLoadingNotification);
            this.pnlMain.Controls.Add(this.chbTopMost);
            this.pnlMain.Controls.Add(this.lblContext);
            this.pnlMain.Controls.Add(this.lblWord);
            this.pnlMain.Controls.Add(this.rtfWord);
            this.pnlMain.Controls.Add(this.rtfContext);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(462, 319);
            this.pnlMain.TabIndex = 7;
            // 
            // chbShowLoadingNotification
            // 
            this.chbShowLoadingNotification.AutoSize = true;
            this.chbShowLoadingNotification.Checked = true;
            this.chbShowLoadingNotification.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbShowLoadingNotification.Location = new System.Drawing.Point(6, 26);
            this.chbShowLoadingNotification.Name = "chbShowLoadingNotification";
            this.chbShowLoadingNotification.Size = new System.Drawing.Size(148, 17);
            this.chbShowLoadingNotification.TabIndex = 9;
            this.chbShowLoadingNotification.Text = "Show Loading notification";
            this.chbShowLoadingNotification.UseVisualStyleBackColor = true;
            // 
            // chbTopMost
            // 
            this.chbTopMost.AutoSize = true;
            this.chbTopMost.Location = new System.Drawing.Point(8, 3);
            this.chbTopMost.Name = "chbTopMost";
            this.chbTopMost.Size = new System.Drawing.Size(86, 17);
            this.chbTopMost.TabIndex = 7;
            this.chbTopMost.Text = "Show on top";
            this.chbTopMost.UseVisualStyleBackColor = true;
            this.chbTopMost.CheckedChanged += new System.EventHandler(this.chbTopMost_CheckedChanged);
            // 
            // pbLoading
            // 
            this.pbLoading.BackColor = System.Drawing.SystemColors.Control;
            this.pbLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLoading.Image = ((System.Drawing.Image)(resources.GetObject("pbLoading.Image")));
            this.pbLoading.Location = new System.Drawing.Point(132, -2);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(100, 100);
            this.pbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbLoading.TabIndex = 9;
            this.pbLoading.TabStop = false;
            this.pbLoading.UseWaitCursor = true;
            this.pbLoading.Visible = false;
            // 
            // CorrectionaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 319);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pbLoading);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CorrectionaryForm";
            this.Text = "Correctionary";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CorrectionaryForm_FormClosing);
            this.Load += new System.EventHandler(this.CorrectionaryForm_Load);
            this.Resize += new System.EventHandler(this.Correctionary_Resize);
            this.cmsTray.ResumeLayout(false);
            this.cmsTray.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon nfi;
        private System.Windows.Forms.RichTextBox rtfContext;
        private System.Windows.Forms.RichTextBox rtfWord;
        private System.Windows.Forms.Label lblWord;
        private System.Windows.Forms.Label lblContext;
        private System.Windows.Forms.ContextMenuStrip cmsTray;
        private WaterMarkToolStripTextBox tsTxbWord;
        private WaterMarkToolStripTextBox tsTxbContext;
        private System.Windows.Forms.ToolStripMenuItem tsmTranslate;
        private System.Windows.Forms.Panel pnlMain;
        private System.Drawing.Icon IconRegular = Correctionary.Properties.Resources.dictionary;
        private System.Drawing.Icon IconHotkey = Correctionary.Properties.Resources.dictionary_green;
        private System.Drawing.Icon IconTranslating = Correctionary.Properties.Resources.dictionary_Blue;
        private System.Windows.Forms.PictureBox pbLoading;
        private System.Windows.Forms.CheckBox chbTopMost;
        private System.Windows.Forms.CheckBox chbShowLoadingNotification;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
       

    }
}

