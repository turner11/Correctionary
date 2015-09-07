using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TransparentControls
{
    /// <summary>
    /// Base class that gives any form that derives from it the effect of slowly 
    /// appearing and then disapperaing. Much like outlook email notification pop-ups
    /// </summary>
    public class TransDialog : System.Windows.Forms.Form
    {
        const int CLOCK_INTERVAL = 100;
        bool _cancelClose = false;

        #region Constructor
        public TransDialog()
        {
			InitializeComponents();
        }
		public TransDialog(bool disposeAtEnd)
		{
			m_bDisposeAtEnd = disposeAtEnd;
            InitializeComponents();
		}
        void InitializeComponents()
		{
            this.components = new System.ComponentModel.Container();
            this.m_clock =  new Timer(this.components);
            this.m_clock.Interval = CLOCK_INTERVAL;
            this.SuspendLayout();
            //m_clock
            this.m_clock.Tick += new EventHandler(m_clock_Tick);
            //TransDialog
            this.Load += new EventHandler(TransDialog_Load);
            this.FormClosing += new FormClosingEventHandler(TransDialog_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();
		}

        
        #endregion

        #region Event handlers
        private void TransDialog_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.0;
            m_bShowing = true;

            m_clock.Start();
        }
        private void TransDialog_Closing(object sender,FormClosingEventArgs e)
        {
			if (!m_bForceClose)
			{
				m_origDialogResult = this.DialogResult;
				e.Cancel = true;
				m_bShowing = false;
				m_clock.Start();
			}
			else
			{
				this.DialogResult = m_origDialogResult;
			}
        }

        #endregion

        #region protected methods

        protected void CancelClose()
        {
            this._cancelClose = true;
        }

        protected void CloseQuickly()
        {
            this.m_bForceClose = true;
            this._cancelClose = false;
            this.m_clock.Stop();
            this.m_clock.Interval = 1;
            this.m_clock.Start();

        }


        #endregion

        void m_clock_Tick(object sender, EventArgs e)
        {
            //if we were asked to stop close, stop the timer...
            if (this._cancelClose)
            {
                this.Opacity = 1;
                this.m_clock.Stop();
                //resetting
                this._cancelClose = false;
            }

            if (m_bShowing)
            {
                if (this.Opacity < 1)
                {
                    this.Opacity += 0.1;
                }
                else
                {
                    m_clock.Stop();
                }
            }
            else
            {
                if (this.Opacity > 0)
                {
                    this.Opacity -= 0.1;
                }
                else
                {
                    m_clock.Stop();
                    m_bForceClose = true;

                    this.Close(); 
                    if (m_bDisposeAtEnd)
                        this.Dispose();
                   
                }
            }
        }

        #region overrides
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region private variables
        private System.ComponentModel.IContainer components = null;
        private Timer m_clock;
        private bool m_bShowing = true;
        private bool m_bForceClose = false;
		private DialogResult m_origDialogResult;
		private bool m_bDisposeAtEnd = false;
        #endregion // private variables

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TransDialog
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "TransDialog";
            this.ResumeLayout(false);

        }

       

    }
}