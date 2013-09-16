namespace Correctionary
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.rtfBox = new System.Windows.Forms.RichTextBox();
            this.lbl_instructions = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.nfi = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // rtfBox
            // 
            this.rtfBox.Location = new System.Drawing.Point(12, 228);
            this.rtfBox.Name = "rtfBox";
            this.rtfBox.Size = new System.Drawing.Size(397, 62);
            this.rtfBox.TabIndex = 0;
            this.rtfBox.Text = "";
            this.rtfBox.TextChanged += new System.EventHandler(this.rtfBox_TextChanged);
            // 
            // lbl_instructions
            // 
            this.lbl_instructions.AutoSize = true;
            this.lbl_instructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_instructions.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbl_instructions.Location = new System.Drawing.Point(56, 9);
            this.lbl_instructions.Name = "lbl_instructions";
            this.lbl_instructions.Size = new System.Drawing.Size(292, 180);
            this.lbl_instructions.TabIndex = 1;
            this.lbl_instructions.Text = "Welcom to Correctionary!\r\n\r\nTo translate a word:\r\n   1) Choose a word chose\r\n2) C" +
                "hoose a Sentence\r\n\r\n*use left_ctrl and left mouse button\r\n\r\nGOOD LUCK!";
            this.lbl_instructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(167, 199);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "Close";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // nfi
            // 
            this.nfi.Icon = ((System.Drawing.Icon)(resources.GetObject("nfi.Icon")));
            this.nfi.Text = "Correctionary";
            this.nfi.Visible = true;
            this.nfi.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nfi_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 315);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.lbl_instructions);
            this.Controls.Add(this.rtfBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtfBox;
        private System.Windows.Forms.Label lbl_instructions;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.NotifyIcon nfi;

    }
}

