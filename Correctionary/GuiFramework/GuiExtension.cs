using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Correctionary.GuiFramework;
using System.Windows.Forms;
using System.Drawing;


    public static class GuiExtension
    {
        /// <summary>
        /// Highlights the specified control.
        /// </summary>
        /// <param name="control">The Control to highlight.</param>
        /// color.</param>
        public static void Flash(this Control ctrl)
        {
            new ControlHighlighter(ctrl);
        }

        /// <summary>
        /// Highlights the specified control using the specified color.
        /// </summary>
        /// <param name="control">The Control to highlight.</param>
        /// <param name="highlightColor">The Color to hightlight it with,
        /// which will gradually fade backto the Control's origional
        /// color.</param>
        public static void Flash(this Control ctrl, Color color)
        {
            new ControlHighlighter(ctrl, color);
        }
    }

