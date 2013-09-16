using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


    public static class ControlExtensions
    {
        #region Delegates
        delegate void ControlStringDelegate(Control txb, string str);
        delegate void ControlBooleanDelegate(Control ctrl, bool boolVal);
        delegate void ControlDelegate(Control ctrl);
        #endregion

        /// <summary>
        /// Sets the control visibility state.
        /// </summary>
        /// <param name="ctrl">The CTRL.</param>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        public static void SetControlVisibility(this Control ctrl, bool visible)
        {
            if (ctrl != null)
            {
                if (ctrl.InvokeRequired)
                {
                    ctrl.Invoke(new ControlBooleanDelegate(SetControlVisibility), new object[] { ctrl, visible });
                }
                else
                {
                    ctrl.Visible = visible;
                }
            }
        }

        /// <summary>
        /// Centers the control (Thread safe).
        /// </summary>
        /// <param name="ctrl">The control.</param>
        public static void CenterControl(this Control ctrl)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.BeginInvoke(new ControlDelegate(CenterControl), new object[] { ctrl });
            }
            else
            {
                ctrl.Left = (ctrl.Parent.ClientSize.Width - ctrl.Width) / 2;
                ctrl.Top = (ctrl.Parent.ClientSize.Height - ctrl.Height) / 2;
            }


        }

        /// <summary>
        /// Sets the size of the label to fit all text. if neccesary text will be wrapped.
        /// </summary>
        /// <param name="lbl">The label.</param>
        /// <remarks>The <see cref="AutoSize"/> will be set to <b>false</b></remarks>
        public static void SetLabelAutoSize(this Label lbl)
        {
            lbl.AutoSize = false;

            Size preferedSize = lbl.Size;// new Size(200, 200);// new Size(preferedWidth ?? lbl.Width, lbl.Height);
            Graphics graphics = lbl.CreateGraphics();
            Size size = TextRenderer.MeasureText((IDeviceContext)graphics, lbl.Text, lbl.Font, preferedSize, TextFormatFlags.WordBreak);
            lbl.Size = size;
        } 
    }

