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

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Left = screenWidth - this.Width;
            this.Top = screenHeight - this.Height;
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
        private void Notification_Load(object sender, System.EventArgs e)
        {
            

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
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblInnerText
            // 
            this.lblInnerText.Location = new System.Drawing.Point(6, 6);
            this.lblInnerText.Margin = new System.Windows.Forms.Padding(5);
            this.lblInnerText.Name = "lblInnerText";
            this.lblInnerText.Padding = new System.Windows.Forms.Padding(5);
            this.lblInnerText.Size = new System.Drawing.Size(301, 27);
            this.lblInnerText.TabIndex = 2;
            this.lblInnerText.Text = "---";
            this.lblInnerText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInnerText.TextChanged += new System.EventHandler(this.lblInnerText_TextChanged);
            // 
            // Notification
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(269, 93);
            this.Controls.Add(this.lblInnerText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(720, 692);
            this.Name = "Notification";
            this.Text = "Notification";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Notification_FormClosing);
            this.Load += new System.EventHandler(this.Notification_Load);
            this.MouseEnter += new System.EventHandler(this.Notification_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Notification_MouseLeave);
            this.ResumeLayout(false);

        }
		#endregion

        #region Designer generated variables

        private System.Windows.Forms.Timer timer1;
        private WrapLabel lblInnerText;
        private System.ComponentModel.IContainer components;
        #endregion

        private void lblInnerText_TextChanged(object sender, EventArgs e)
        {
            //setting size of form to fit text
            Size lblSize = this.lblInnerText.Size;
            this.Size = new Size(lblSize.Width + 10, lblSize.Height + 10);
            
        }

        

       

        

        

        


      
    }
}
