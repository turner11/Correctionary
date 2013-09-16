using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace Correctionary.GuiFramework
{
    public class ControlHighlighter : IDisposable
    {
        #region Data members
        private Timer _timer = null;
        private Color _startColor;
        private Color _endColor;
        private PropertyInfo _piVisualStyleBackColor;
        private bool _useVisualStyleBackColorOrig;
        private int _currentStep;
        private Control _control;
        private const double NUMBER_OF_STEPS = 30.0;

        /// <summary>
        /// A list of all controls that are currently durign flashing. this will help us to avoid "reflashing"
        /// </summary>
        static List<Control> arrHighlightedControls;
        #endregion //Data members

        #region C'tors
        static ControlHighlighter()
        {
            ControlHighlighter.arrHighlightedControls = new List<Control>();
        }

        /// <summary>
        /// Highlights the specified control using the specified color.
        /// </summary>
        /// <param name="control">The Control to highlight.</param>
        /// <param name="highlightColor">The Color to hightlight it with,
        /// which will gradually fade backto the Control's origional
        /// color.</param>
        public ControlHighlighter(Control control, Color highlightColor)
        {
            if (ControlHighlighter.arrHighlightedControls.Contains(control))
            {
                return;
            }
            ControlHighlighter.arrHighlightedControls.Add(control);

            this._control = control;
            
            this._endColor = Color.FromArgb(255, control.BackColor);
            this._startColor = highlightColor;

            this._piVisualStyleBackColor = this._control.GetType().GetProperty("UseVisualStyleBackColor");
            if (this._piVisualStyleBackColor != null)
            {
                this._useVisualStyleBackColorOrig =
                    (bool)this._piVisualStyleBackColor.GetGetMethod().Invoke(this._control, new object[] { });
            }
            if (control.Visible)
            {
                this._currentStep = 1;
                this._control.BackColor = this._startColor;
                this._timer = new Timer();
                this._timer.Interval = 20;
                this._timer.Tick += new EventHandler(this.highlightTimer_Tick);
                this._timer.Enabled = true;
            }

        }

        /// <summary>
        /// Highlights the specified control using the default highlight color.
        /// </summary>
        /// <param name="control">The Control to highlight.</param>
        /// <remarks>The default color is Orange.</remarks>
        public ControlHighlighter(Control control) : this(control, Color.Orange) { }
        #endregion //C'tors

        #region Private methods
        private void updateControlBackColor(Color newBackColor)
        {
            if (this._control.InvokeRequired && !this._control.IsDisposed)
            {
                this._control.Invoke(new Action<Color>(updateControlBackColor), new object[] { newBackColor });
            }
            else
            {
                if (!this._control.IsDisposed)
                {
                    this._control.BackColor = newBackColor;
                }
            }
        }
        #endregion

        #region Event Handlers

        private void highlightTimer_Tick(object sender, EventArgs e)
        {
            if (this._currentStep > ControlHighlighter.NUMBER_OF_STEPS)
            {
                if (ControlHighlighter.arrHighlightedControls.Contains(_control))
                {
                    ControlHighlighter.arrHighlightedControls.Remove(_control);
                }
                this._timer.Enabled = false;
                this.Dispose();
                return;
            }

            double stepFactor = this._currentStep / ControlHighlighter.NUMBER_OF_STEPS;
            int R = (int)((this._endColor.R - this._startColor.R) * stepFactor) + this._startColor.R;
            int G = (int)((this._endColor.G - this._startColor.G) * stepFactor) + this._startColor.G;
            int B = (int)((this._endColor.B - this._startColor.B) * stepFactor) + this._startColor.B;
            Color newBackColor = Color.FromArgb(255, R, G, B);
            this.updateControlBackColor(newBackColor);
            ++this._currentStep;
        }


        #endregion

        #region IDisposable and friends

        private bool isDisposed = false;

        protected void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (this._control != null)
                {
                    try
                    {
                        this.updateControlBackColor(this._endColor);
                        if (this._piVisualStyleBackColor != null)
                        {
                            this._piVisualStyleBackColor.GetSetMethod().Invoke(this._control, new object[] { this._useVisualStyleBackColorOrig });
                        }
                    }
                    catch { }
                }

                if (this._timer != null)
                {
                    this._timer.Dispose();
                    this._timer = null;
                }
            }

            this.isDisposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ControlHighlighter()
        {
            this.Dispose(false);
        }

        #endregion
    }
}
