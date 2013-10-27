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
            this.chbShowDebugMessages = new System.Windows.Forms.CheckBox();
            this.gbTrnaslationWindow = new System.Windows.Forms.GroupBox();
            this.chbSearchForImages = new System.Windows.Forms.CheckBox();
            this.gbHotKeys = new System.Windows.Forms.GroupBox();
            this.cmbReverseTranslationHotKey = new System.Windows.Forms.ComboBox();
            this.cmbTranslationHotKey = new System.Windows.Forms.ComboBox();
            this.cmbReverseTranslationMofifier = new System.Windows.Forms.ComboBox();
            this.cmbTranslationMofifier = new System.Windows.Forms.ComboBox();
            this.lblReverseTranslation = new System.Windows.Forms.Label();
            this.lblHotKeyTranslation = new System.Windows.Forms.Label();
            this.gbLanguages.SuspendLayout();
            this.lblGeneral.SuspendLayout();
            this.gbTrnaslationWindow.SuspendLayout();
            this.gbHotKeys.SuspendLayout();
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
            this.lblGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnOk.Location = new System.Drawing.Point(4, 361);
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
            this.btnCancel.Location = new System.Drawing.Point(63, 361);
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
            this.cmbDisplayLocation.Location = new System.Drawing.Point(147, 19);
            this.cmbDisplayLocation.Name = "cmbDisplayLocation";
            this.cmbDisplayLocation.Size = new System.Drawing.Size(106, 21);
            this.cmbDisplayLocation.TabIndex = 3;
            this.cmbDisplayLocation.SelectedIndexChanged += new System.EventHandler(this.cmbDisplayLocation_SelectedIndexChanged);
            // 
            // lblDisplayLocation
            // 
            this.lblDisplayLocation.AutoSize = true;
            this.lblDisplayLocation.Location = new System.Drawing.Point(6, 22);
            this.lblDisplayLocation.Name = "lblDisplayLocation";
            this.lblDisplayLocation.Size = new System.Drawing.Size(112, 13);
            this.lblDisplayLocation.TabIndex = 2;
            this.lblDisplayLocation.Text = "Display Translation At:";
            // 
            // chbShowDebugMessages
            // 
            this.chbShowDebugMessages.AutoSize = true;
            this.chbShowDebugMessages.Location = new System.Drawing.Point(9, 69);
            this.chbShowDebugMessages.Name = "chbShowDebugMessages";
            this.chbShowDebugMessages.Size = new System.Drawing.Size(139, 17);
            this.chbShowDebugMessages.TabIndex = 4;
            this.chbShowDebugMessages.Text = "Show Debug Messages";
            this.chbShowDebugMessages.UseVisualStyleBackColor = true;
            this.chbShowDebugMessages.CheckedChanged += new System.EventHandler(this.chbShowDebugMessages_CheckedChanged);
            // 
            // gbTrnaslationWindow
            // 
            this.gbTrnaslationWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTrnaslationWindow.Controls.Add(this.chbSearchForImages);
            this.gbTrnaslationWindow.Controls.Add(this.chbShowDebugMessages);
            this.gbTrnaslationWindow.Controls.Add(this.lblDisplayLocation);
            this.gbTrnaslationWindow.Controls.Add(this.cmbDisplayLocation);
            this.gbTrnaslationWindow.Location = new System.Drawing.Point(10, 185);
            this.gbTrnaslationWindow.Name = "gbTrnaslationWindow";
            this.gbTrnaslationWindow.Size = new System.Drawing.Size(267, 92);
            this.gbTrnaslationWindow.TabIndex = 5;
            this.gbTrnaslationWindow.TabStop = false;
            this.gbTrnaslationWindow.Text = "Translation Window";
            // 
            // chbSearchForImages
            // 
            this.chbSearchForImages.AutoSize = true;
            this.chbSearchForImages.Location = new System.Drawing.Point(9, 46);
            this.chbSearchForImages.Name = "chbSearchForImages";
            this.chbSearchForImages.Size = new System.Drawing.Size(156, 17);
            this.chbSearchForImages.TabIndex = 4;
            this.chbSearchForImages.Text = "Search For Images (Slower)";
            this.chbSearchForImages.UseVisualStyleBackColor = true;
            this.chbSearchForImages.CheckedChanged += new System.EventHandler(this.chbSearchForImages_CheckedChanged);
            // 
            // gbHotKeys
            // 
            this.gbHotKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHotKeys.Controls.Add(this.cmbReverseTranslationHotKey);
            this.gbHotKeys.Controls.Add(this.cmbTranslationHotKey);
            this.gbHotKeys.Controls.Add(this.cmbReverseTranslationMofifier);
            this.gbHotKeys.Controls.Add(this.cmbTranslationMofifier);
            this.gbHotKeys.Controls.Add(this.lblReverseTranslation);
            this.gbHotKeys.Controls.Add(this.lblHotKeyTranslation);
            this.gbHotKeys.Location = new System.Drawing.Point(10, 283);
            this.gbHotKeys.Name = "gbHotKeys";
            this.gbHotKeys.Size = new System.Drawing.Size(267, 73);
            this.gbHotKeys.TabIndex = 6;
            this.gbHotKeys.TabStop = false;
            this.gbHotKeys.Text = "Hot Keys";
            // 
            // cmbReverseTranslationHotKey
            // 
            this.cmbReverseTranslationHotKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReverseTranslationHotKey.FormattingEnabled = true;
            this.cmbReverseTranslationHotKey.Location = new System.Drawing.Point(200, 40);
            this.cmbReverseTranslationHotKey.Name = "cmbReverseTranslationHotKey";
            this.cmbReverseTranslationHotKey.Size = new System.Drawing.Size(53, 21);
            this.cmbReverseTranslationHotKey.TabIndex = 1;
            this.cmbReverseTranslationHotKey.SelectedIndexChanged += new System.EventHandler(this.cmbReverseTranslationHotKey_SelectedIndexChanged);
            // 
            // cmbTranslationHotKey
            // 
            this.cmbTranslationHotKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTranslationHotKey.FormattingEnabled = true;
            this.cmbTranslationHotKey.Location = new System.Drawing.Point(200, 13);
            this.cmbTranslationHotKey.Name = "cmbTranslationHotKey";
            this.cmbTranslationHotKey.Size = new System.Drawing.Size(53, 21);
            this.cmbTranslationHotKey.TabIndex = 1;
            this.cmbTranslationHotKey.SelectedIndexChanged += new System.EventHandler(this.cmbTranslationHotKey_SelectedIndexChanged);
            // 
            // cmbReverseTranslationMofifier
            // 
            this.cmbReverseTranslationMofifier.FormattingEnabled = true;
            this.cmbReverseTranslationMofifier.Location = new System.Drawing.Point(120, 40);
            this.cmbReverseTranslationMofifier.Name = "cmbReverseTranslationMofifier";
            this.cmbReverseTranslationMofifier.Size = new System.Drawing.Size(74, 21);
            this.cmbReverseTranslationMofifier.TabIndex = 1;
            this.cmbReverseTranslationMofifier.SelectedIndexChanged += new System.EventHandler(this.cmbReverseTranslationMofifier_SelectedIndexChanged);
            // 
            // cmbTranslationMofifier
            // 
            this.cmbTranslationMofifier.FormattingEnabled = true;
            this.cmbTranslationMofifier.Location = new System.Drawing.Point(120, 13);
            this.cmbTranslationMofifier.Name = "cmbTranslationMofifier";
            this.cmbTranslationMofifier.Size = new System.Drawing.Size(74, 21);
            this.cmbTranslationMofifier.TabIndex = 1;
            this.cmbTranslationMofifier.SelectedIndexChanged += new System.EventHandler(this.cmbTranslationMofifier_SelectedIndexChanged);
            // 
            // lblReverseTranslation
            // 
            this.lblReverseTranslation.AutoSize = true;
            this.lblReverseTranslation.Location = new System.Drawing.Point(6, 40);
            this.lblReverseTranslation.Name = "lblReverseTranslation";
            this.lblReverseTranslation.Size = new System.Drawing.Size(102, 13);
            this.lblReverseTranslation.TabIndex = 0;
            this.lblReverseTranslation.Text = "Reverse Translation";
            // 
            // lblHotKeyTranslation
            // 
            this.lblHotKeyTranslation.AutoSize = true;
            this.lblHotKeyTranslation.Location = new System.Drawing.Point(6, 16);
            this.lblHotKeyTranslation.Name = "lblHotKeyTranslation";
            this.lblHotKeyTranslation.Size = new System.Drawing.Size(59, 13);
            this.lblHotKeyTranslation.TabIndex = 0;
            this.lblHotKeyTranslation.Text = "Translation";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 396);
            this.ControlBox = false;
            this.Controls.Add(this.gbHotKeys);
            this.Controls.Add(this.gbTrnaslationWindow);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblGeneral);
            this.Controls.Add(this.gbLanguages);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(302, 412);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.gbLanguages.ResumeLayout(false);
            this.gbLanguages.PerformLayout();
            this.lblGeneral.ResumeLayout(false);
            this.lblGeneral.PerformLayout();
            this.gbTrnaslationWindow.ResumeLayout(false);
            this.gbTrnaslationWindow.PerformLayout();
            this.gbHotKeys.ResumeLayout(false);
            this.gbHotKeys.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.CheckBox chbShowDebugMessages;
        private System.Windows.Forms.GroupBox gbTrnaslationWindow;
        private System.Windows.Forms.GroupBox gbHotKeys;
        private System.Windows.Forms.ComboBox cmbReverseTranslationHotKey;
        private System.Windows.Forms.ComboBox cmbTranslationHotKey;
        private System.Windows.Forms.ComboBox cmbReverseTranslationMofifier;
        private System.Windows.Forms.ComboBox cmbTranslationMofifier;
        private System.Windows.Forms.Label lblReverseTranslation;
        private System.Windows.Forms.Label lblHotKeyTranslation;
        private System.Windows.Forms.CheckBox chbSearchForImages;
    }
}