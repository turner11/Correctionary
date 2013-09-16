using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Correctionary
{
   
    public partial class Form1 : Form
    {
        static public String word= String.Empty, sentence=String.Empty;
        IntPtr nextClipboardViewer;
        HookFunctions actHook;
        public bool CopyMouse, CopyKybrd;
        public Form1()
        {
            nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
            InitializeComponent();
            CopyMouse = false;
            CopyKybrd = false;
        }

        #region Windows function
        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            
            actHook = new HookFunctions(); // crate an instance with global hooks
            actHook.Start(true, true);

            // register the events to the Hook class.b
            actHook.KeyDown += new KeyEventHandler(MyKeyDown);
            actHook.OnMouseActivity += new MouseEventHandler(MouseMoved);
        }

        // Handle Mouse event.
        public void MyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.ToString() == "LControlKey")
                CopyKybrd = true;
        }

        // Handle key event.
        public void MouseMoved(object sender, MouseEventArgs e)
        {
            if ((e.Clicks > 0) && (e.Button == MouseButtons.Left))
            {
                if (CopyKybrd)
                {
                    CopyMouse = true;
                    actHook.VirtualKeyPressCopy();
                }
            }
        }

        // I don't remember what this is for, but it's very important. :)
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    {
                        if ((CopyMouse) && (CopyKybrd))
                        {
                            DisplayClipboardData();
                        }
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    }
                    break;

                case WM_CHANGECBCHAIN:
                    {
                        if (m.WParam == nextClipboardViewer)
                            nextClipboardViewer = m.LParam;
                        else
                            SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    }
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }

            CopyMouse = false;
            CopyKybrd = false; 
        }

        /*
        * get from the clipboard the txt and write id in the rtf box. 
        * 
        * pay attention that the text format is in rtf (Reach Text Format)
        * so to convert is to regulat foramt you need to do toString to textBox.
        * 
        */
        public  void DisplayClipboardData()
        {
            try
            {
                IDataObject iData = new DataObject();
                iData = Clipboard.GetDataObject();
              
                if (iData.GetDataPresent(DataFormats.Rtf))
                {
                    rtfBox.Rtf = (string)iData.GetData(DataFormats.Rtf) + Environment.NewLine;
                }
                else if (iData.GetDataPresent(DataFormats.Text))
                {
                    rtfBox.Text = (string)iData.GetData(DataFormats.Text) + Environment.NewLine;
                   
                }
                else
                {
                    rtfBox.Text = "[Clipboard data is not RTF or ASCII Text]" + Environment.NewLine;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        private void rtfBox_TextChanged(object sender, EventArgs e)
        {
            String exp = rtfBox.Text;
            string[] sp = exp.Split(' ', '\t', '\n');
            if (sp.Length > 2)
            {
                  sentence = exp;

            }
            else
                word = exp;
            if (!sentence.Equals("") && !word.Equals(""))
            {
                Correctionary cor = new Correctionary(word, sentence);
                sentence = String.Empty;
                word = string.Empty;
                String chosen = cor.Special_word;
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void nfi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            Show();
            WindowState = FormWindowState.Normal;
            Activate();

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (System.Windows.Forms.FormWindowState.Minimized == WindowState)
            {
                //this.WindowState = FormWindowState.Minimized;
                this.Visible = false;
            }

        }
    }
}
