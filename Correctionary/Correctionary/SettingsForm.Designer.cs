namespace Correctionary
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.gbLanguages = new System.Windows.Forms.GroupBox();
            this.chbDisplayNativeName = new System.Windows.Forms.CheckBox();
            this.lblLanguageTo = new System.Windows.Forms.Label();
            this.lblLanguageFrom = new System.Windows.Forms.Label();
            this.cmbLanguageB = new System.Windows.Forms.ComboBox();
            this.cmbLanguageA = new System.Windows.Forms.ComboBox();
            this.chbIdentifyLanguageAutomaticaly = new System.Windows.Forms.CheckBox();
            this.lblGeneral = new System.Windows.Forms.GroupBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbDisplayLocation = new System.Windows.Forms.ComboBox();
            this.lblDisplayLocation = new System.Windows.Forms.Label();
            this.gbLanguages.SuspendLayout();
            this.lblGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLanguages
            // 
            this.gbLanguages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLanguages.Controls.Add(this.chbDisplayNativeName);
            this.gbLanguages.Controls.Add(this.lblLanguageTo);
            this.gbLanguages.Controls.Add(this.lblLanguageFrom);
            this.gbLanguages.Controls.Add(this.cmbLanguageB);
            this.gbLanguages.Controls.Add(this.cmbLanguageA);
            this.gbLanguages.Location = new System.Drawing.Point(10, 16);
            this.gbLanguages.Name = "gbLanguages";
            this.gbLanguages.Size = new System.Drawing.Size(267, 92);
            this.gbLanguages.TabIndex = 0;
            this.gbLanguages.TabStop = false;
            this.gbLanguages.Text = "Languages";
            // 
            // chbDisplayNativeName
            // 
            this.chbDisplayNativeName.AutoSize = true;
            this.chbDisplayNativeName.Location = new System.Drawing.Point(9, 69);
            this.chbDisplayNativeName.Name = "chbDisplayNativeName";
            this.chbDisplayNativeName.Size = new System.Drawing.Size(123, 17);
            this.chbDisplayNativeName.TabIndex = 4;
            this.chbDisplayNativeName.Text = "Display Native name";
            this.chbDisplayNativeName.UseVisualStyleBackColor = true;
            this.chbDisplayNativeName.CheckedChanged += new System.EventHandler(this.chbDisplayNativeName_CheckedChanged);
            // 
            // lblLanguageTo
            // 
            this.lblLanguageTo.AutoSize = true;
            this.lblLanguageTo.Location = new System.Drawing.Point(6, 49);
            this.lblLanguageTo.Name = "lblLanguageTo";
            this.lblLanguageTo.Size = new System.Drawing.Size(71, 13);
            this.lblLanguageTo.TabIndex = 2;
            this.lblLanguageTo.Text = "Language To";
            // 
            // lblLanguageFrom
            // 
            this.lblLanguageFrom.AutoSize = true;
            this.lblLanguageFrom.Location = new System.Drawing.Point(6, 22);
            this.lblLanguageFrom.Name = "lblLanguageFrom";
            this.lblLanguageFrom.Size = new System.Drawing.Size(81, 13);
            this.lblLanguageFrom.TabIndex = 0;
            this.lblLanguageFrom.Text = "Language From";
            // 
            // cmbLanguageB
            // 
            this.cmbLanguageB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLanguageB.FormattingEnabled = true;
            this.cmbLanguageB.Location = new System.Drawing.Point(98, 46);
            this.cmbLanguageB.Name = "cmbLanguageB";
            this.cmbLanguageB.Size = new System.Drawing.Size(163, 21);
            this.cmbLanguageB.TabIndex = 3;
            this.cmbLanguageB.SelectedIndexChanged += new System.EventHandler(this.cmbLanguage_SelectedIndexChanged);
            // 
            // cmbLanguageA
            // 
            this.cmbLanguageA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLanguageA.FormattingEnabled = true;
            this.cmbLanguageA.Location = new System.Drawing.Point(98, 19);
            this.cmbLanguageA.Name = "cmbLanguageA";
            this.cmbLanguageA.Size = new System.Drawing.Size(163, 21);
            this.cmbLanguageA.TabIndex = 1;
            this.cmbLanguageA.SelectedIndexChanged += new System.EventHandler(this.cmbLanguage_SelectedIndexChanged);
            // 
            // chbIdentifyLanguageAutomaticaly
            // 
            this.chbIdentifyLanguageAutomaticaly.AutoSize = true;
            this.chbIdentifyLanguageAutomaticaly.Location = new System.Drawing.Point(9, 19);
            this.chbIdentifyLanguageAutomaticaly.Name = "chbIdentifyLanguageAutomaticaly";
            this.chbIdentifyLanguageAutomaticaly.Size = new System.Drawing.Size(221, 43);
            this.chbIdentifyLanguageAutomaticaly.TabIndex = 0;
            this.chbIdentifyLanguageAutomaticaly.Text = "Attempt to detect language automatically \r\n(and translate to language B)\r\n - To b" +
    "e implemented";
            this.chbIdentifyLanguageAutomaticaly.UseVisualStyleBackColor = true;
            this.chbIdentifyLanguageAutomaticaly.CheckedChanged += new System.EventHandler(this.chbIdentifyLanguageAutomaticaly_CheckedChanged);
            // 
            // lblGeneral
            // 
            this.lblGeneral.Controls.Add(this.chbIdentifyLanguageAutomaticaly);
            this.lblGeneral.Location = new System.Drawing.Point(10, 114);
            this.lblGeneral.Name = "lblGeneral";
            this.lblGeneral.Size = new System.Drawing.Size(267, 65);
            this.lblGeneral.TabIndex = 1;
            this.lblGeneral.TabStop = false;
            this.lblGeneral.Text = "General";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.Location = new System.Drawing.Point(10, 229);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(55, 24);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(69, 229);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbDisplayLocation
            // 
            this.cmbDisplayLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDisplayLocation.FormattingEnabled = true;
            this.cmbDisplayLocation.Location = new System.Drawing.Point(157, 185);
            this.cmbDisplayLocation.Name = "cmbDisplayLocation";
            this.cmbDisplayLocation.Size = new System.Drawing.Size(114, 21);
            this.cmbDisplayLocation.TabIndex = 3;
            this.cmbDisplayLocation.SelectedIndexChanged += new System.EventHandler(this.cmbDisplayLocation_SelectedIndexChanged);
            // 
            // lblDisplayLocation
            // 
            this.lblDisplayLocation.AutoSize = true;
            this.lblDisplayLocation.Location = new System.Drawing.Point(16, 188);
            this.lblDisplayLocation.Name = "lblDisplayLocation";
            this.lblDisplayLocation.Size = new System.Drawing.Size(112, 13);
            this.lblDisplayLocation.TabIndex = 2;
            this.lblDisplayLocation.Text = "Display Translation At:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 264);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblDisplayLocation);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblGeneral);
            this.Controls.Add(this.cmbDisplayLocation);
            this.Controls.Add(this.gbLanguages);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(302, 257);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.gbLanguages.ResumeLayout(false);
            this.gbLanguages.PerformLayout();
            this.lblGeneral.ResumeLayout(false);
            this.lblGeneral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLanguages;
        private System.Windows.Forms.Label lblLanguageTo;
        private System.Windows.Forms.Label lblLanguageFrom;
        private System.Windows.Forms.ComboBox cmbLanguageB;
        private System.Windows.Forms.ComboBox cmbLanguageA;
        private System.Windows.Forms.CheckBox chbIdentifyLanguageAutomaticaly;
        private System.Windows.Forms.GroupBox lblGeneral;
        private System.Windows.Forms.CheckBox chbDisplayNativeName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbDisplayLocation;
        private System.Windows.Forms.Label lblDisplayLocation;
    }
}