using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.SqlServer.MessageBox;

namespace Correctionary
{
    /// <summary>
    /// The program that to launch the correctionary
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += 
                new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler        (CurrentDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CorrectionaryForm());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception)
            {
                HandleException(e.ExceptionObject as Exception);
            }
            else
            {
                string msg = "Caught unhandled exception with " +
                    (e != null ? ("object: '" + e.ToString() + "'.") : "null object.");
                
                ShowExceptionBox(null,
                                  msg,
                                   "An unhadled exception occured",
                                   ExceptionMessageBoxButtons.OK,
                                   null,
                                   ExceptionMessageBoxSymbol.Stop); 

            }
           
        }

       

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        private static void HandleException(Exception ex)
        {
           
                ShowExceptionBox(null, 
                    ex.Message, 
                    "An unhadled exception occured", 
                    ExceptionMessageBoxButtons.OK, 
                    ex, 
                    ExceptionMessageBoxSymbol.Stop);            
        }

        private static void ShowExceptionBox(IWin32Window owner, string message, string caption, ExceptionMessageBoxButtons btns, Exception e, ExceptionMessageBoxSymbol symble)
        {


            ExceptionMessageBox emb = new ExceptionMessageBox(e);
            emb.Caption = caption;
            emb.Symbol = symble;
            emb.Buttons = btns;

            try
            {
                emb.Show(owner);
            }
            catch (Exception)
            {

                emb.Show(null);
            }
        }
    }
}
