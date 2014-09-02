using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TransparentControls
{
	public class Notification : TransDialog
	{
        const int DEFAULT_DISPLAY_TIME = 3000;
        readonly int _timeToDisplay;
        private Label lblCredit;
        private TableLayoutPanel tlpMain;
        private PictureBox pbImage;
        bool _ClosingBecauseOFTimer;

        #region Ctor, init code and dispose

        public Notification()
            : this(DEFAULT_DISPLAY_TIME)
        {            
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="timeToDisplay">The time to display in milliseconds.</param>
		public Notification(int timeToDisplay)
            : base(true)
		{
			InitializeComponent();
            this._timeToDisplay = timeToDisplay;
            this.timer1.Interval = this._timeToDisplay;
            this.lblInnerText.Text = String.Empty;
            this.HookMouseMove(this.Controls);

            ContextMenu cm = new ContextMenu();
            var item = cm.MenuItems.Add("Copy");
            item.Click += (s, e) =>
                {
                    Clipboard.SetText(this.lblInnerText.Text);
                };
            

            this.lblInnerText.ContextMenu = cm;

           
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
        #endregion // Ctor and init code

        public void SetInnerTexst(string textToDisplay)
        {
            this.lblInnerText.Text = textToDisplay;
        }

        public void SetImage(Image image)
        {
            this.pbImage.Image =image;

            
        }

        /// <summary>
        /// Stops the count down to closing.
        /// </summary>
        private void StopCountDown()
        {
            this.timer1.Stop();
            this.Opacity = 1;
        }

        /// <summary>
        /// Starts the count down to closing.
        /// </summary>
        private void StartCountDown()
        {
            this.timer1.Stop();
            this.timer1.Interval = this._timeToDisplay;
            this.timer1.Start();
        }

        private void HookMouseMove(Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                ctl.MouseEnter += OnControlMouseEnter;
                ctl.MouseLeave += OnControlMouseLeave;
                HookMouseMove(ctl.Controls);
            }
        }

        #region Event handler

        private void pbImage_MouseEnter(object sender, EventArgs e)
        {
            this.lblInnerText.Visible = false;            
        }

        private void pbImage_MouseLeave(object sender, EventArgs e)
        {
            this.lblInnerText.Visible = true;
        }
        private void lblInnerText_TextChanged(object sender, EventArgs e)
        {
            //setting size of form to fit text
            Size lblSize = this.lblInnerText.Size;
            int width = Math.Max(this.lblCredit.Width, lblSize.Width);
            this.Size = new Size(width + 10, lblSize.Height + this.lblCredit.Height + 30);
            //this.Size = new Size(lblSize.Width + 10, lblSize.Height+ 10);

        }

        /// <summary>
        /// Handles the BackgroundImageChanged event of the pbImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pbImage_BackgroundImageChanged(object sender, EventArgs e)
        {
            pbImage.Visible = pbImage.Image != null;
        }
        private void Notification_Load(object sender, System.EventArgs e)
        {

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Left = screenWidth - this.Width;
            this.Top = screenHeight - this.Height;

            this.StartCountDown();

        }

        private void Notification_MouseEnter(object sender, EventArgs e)
        {
            this.StopCountDown();
            this.CancelClose();
            this._ClosingBecauseOFTimer = false;
        }

        private void OnControlMouseEnter(object sender, EventArgs e)
        {
            this.StopCountDown();
        }

        private void OnControlMouseLeave(object sender, EventArgs e)
        {
            this.StartCountDown();
        }

        private void Notification_MouseLeave(object sender, EventArgs e)
        {
            this.StartCountDown();
        }

       

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            this._ClosingBecauseOFTimer = true;
            this.Close();
        }
       
        private void Notification_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this._ClosingBecauseOFTimer)
            {
                this.CloseQuickly();
            }
        }

       
        #endregion // Event handler
        
        #region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblInnerText = new TransparentControls.WrapLabel();
            this.lblCredit = new System.Windows.Forms.Label();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblInnerText
            // 
            this.lblInnerText.Location = new System.Drawing.Point(5, 5);
            this.lblInnerText.Margin = new System.Windows.Forms.Padding(5);
            this.lblInnerText.Name = "lblInnerText";
            this.lblInnerText.Padding = new System.Windows.Forms.Padding(5);
            this.lblInnerText.Size = new System.Drawing.Size(243, 23);
            this.lblInnerText.TabIndex = 2;
            this.lblInnerText.Text = "---";
            this.lblInnerText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInnerText.TextChanged += new System.EventHandler(this.lblInnerText_TextChanged);
            // 
            // lblCredit
            // 
            this.lblCredit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCredit.AutoSize = true;
            this.lblCredit.BackColor = System.Drawing.Color.Gainsboro;
            this.tlpMain.SetColumnSpan(this.lblCredit, 2);
            this.lblCredit.Location = new System.Drawing.Point(3, 64);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(375, 13);
            this.lblCredit.TabIndex = 3;
            this.lblCredit.Text = "Brought to you by Avi Turner: avi.turner111@gmail.com";
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.Controls.Add(this.lblCredit, 0, 1);
            this.tlpMain.Controls.Add(this.lblInnerText, 0, 0);
            this.tlpMain.Controls.Add(this.pbImage, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(381, 77);
            this.tlpMain.TabIndex = 4;
            // 
            // pbImage
            // 
            this.pbImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(256, 3);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(122, 58);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 4;
            this.pbImage.TabStop = false;
            this.pbImage.BackgroundImageChanged += new System.EventHandler(this.pbImage_BackgroundImageChanged);
            this.pbImage.MouseEnter += new System.EventHandler(this.pbImage_MouseEnter);
            this.pbImage.MouseLeave += new System.EventHandler(this.pbImage_MouseLeave);
            // 
            // Notification
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(381, 77);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(600, 600);
            this.Name = "Notification";
            this.Text = "Notification";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Notification_FormClosing);
            this.Load += new System.EventHandler(this.Notification_Load);
            this.MouseEnter += new System.EventHandler(this.Notification_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Notification_MouseLeave);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

        #region Designer generated variables

        private System.Windows.Forms.Timer timer1;
        private WrapLabel lblInnerText;
        private System.ComponentModel.IContainer components;
        #endregion

       

       

        

       

        

        

        


      
    }
}
