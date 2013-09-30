using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Correctionary.GuiFramework
{
    /// <summary>
    /// A textbox that supports a watermak hint.
    /// </summary>
    public partial class WaterMarkToolStripTextBox : ToolStripTextBox
    {
        /// <summary>
        /// Gets or sets the text to be displayed on the hosted control.
        /// </summary>
        /// <returns>A <see cref="T:System.String" /> representing the text.</returns>
        public override string Text
        {
            get
            {
                string text = base.Text;
                if (text == this.WatermarkText)
                {
                    text = String.Empty;
                }
                return text;
            }
            set
            {
                base.Text = value;                
            }
        }
        /// <summary>
        /// The text that will be presented as the watermak hint
        /// </summary>
        private string _watermarkText = String.Empty;
        /// <summary>
        /// Gets or Sets the text that will be presented as the watermak hint
        /// </summary>
        public string WatermarkText
        {
            get { return _watermarkText; }
            set
            {
                _watermarkText = value;
                if (!this.Focused)
                {
                    this.ApplyWatermark();
                }

            }
        }

        /// <summary>
        /// Whether watermark effect is enabled or not
        /// </summary>
        private bool _watermarkActive = true;
        /// <summary>
        /// Gets or Sets whether watermark effect is enabled or not
        /// </summary>
        public bool WatermarkActive
        {
            get { return _watermarkActive; }
            set { _watermarkActive = value; }
        }

        /// <summary>
        /// Create a new TextBox that supports watermak hint
        /// </summary>
        public WaterMarkToolStripTextBox()
        {
            InitializeComponent();

            this._watermarkActive = true;
            if (String.IsNullOrWhiteSpace(this.Text))
            {
                ApplyWatermark();
            }

            this.ForeColor = Color.Gray;



            GotFocus += (source, e) =>
            {
                RemoveWatermak();
            };

            LostFocus += (source, e) =>
            {
                ApplyWatermark();
            };

        }

        /// <summary>
        /// Remove watermark from the textbox
        /// </summary>
        public void RemoveWatermak()
        {
            if (this._watermarkActive)
            {
                this._watermarkActive = false;
                this.Text = "";
                this.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Applywatermak immediately
        /// </summary>
        public void ApplyWatermark()
        {
            if (!this._watermarkActive && string.IsNullOrEmpty(this.Text)
                || ForeColor == Color.Gray)
            {
                this._watermarkActive = true;
                this.Text = _watermarkText;
                this.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Apply watermak to the textbox. 
        /// </summary>
        /// <param name="newText">Text to apply</param>
        public void ApplyWatermark(string newText)
        {
            WatermarkText = newText;
            ApplyWatermark();
        }
    }

}
