using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonObjects;
using System.Threading;
using System.Net;
using System.IO;

namespace TranslationUnit
{
    class GooglePageParserTranslator: GoogleTranslator
    {
        const string GOOGLE_TRNASLATE_URL = "https://translate.google.com/";
        System.Windows.Forms.WebBrowser _browser;
        public GooglePageParserTranslator():base()
        {
            var initBroser = new Action(() => {
                Thread.CurrentThread.Name = this.GetType().Name+": Init Browser thread";
                this._browser = new System.Windows.Forms.WebBrowser();
                

                bool complete = false;
                this._browser.DocumentCompleted += (s,e)=>
                {
                    if (complete)
                        return;
                    complete = true;
                    // DocumentCompleted is fired before window.onload and body.onload
                    this._browser.Document.Window.AttachEventHandler("onload", BroserLoadCompleted);

                };

                this._browser.Navigating += (s, e) =>
                  {
                      1.ToString();
                  };


                this._browser.Navigate(GOOGLE_TRNASLATE_URL);

            });

           
            Thread newThread = new Thread(new ThreadStart(initBroser));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
            newThread.Join();


        }

        private void BroserLoadCompleted(object sender, EventArgs e)
        {
            /*Trying...*/
            CookieContainer container = new CookieContainer();

            foreach (string cookie in this._browser.Document.Cookie.Split(';'))
            {
                string name = cookie.Split('=')[0];
                string value = cookie.Substring(name.Length + 1);
                string path = "/";
                string domain = ".google.com"; //change to your domain name
                container.Add(new Cookie(name.Trim(), value.Trim(), path, domain));
            }
            // Defer this to make sure all possible onload event handlers got fired
            System.Threading.SynchronizationContext.Current.Post((obj) => GetSourceTextBox(),null);
                //{
                //    // try webBrowser1.Document.GetElementById("id") here
                //    //MessageBox.Show("window.onload was fired, can access DOM!");
                //}, null);
           
        }

        private void GetSourceTextBox()
        {
            var sourceTxb = this._browser.Document.GetElementById("source");
        }



        /// <summary>
        /// Translates a sentence.
        /// </summary>
        /// <param name="expression">The sentence.</param>
        /// <param name="source">The language to translate from.</param>
        /// <param name="target">The language to translate to.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The translation</returns>
        protected override Translation TranslateExpression(string expression, Language languageFrom, Language languageTo)
        {

            /*Trying...*/

            string translatedText = null;

            try
            {
                
                //create the web client
                WebClient client = new WebClient();

                //Open the page and get the results
                string url = GOOGLE_TRNASLATE_URL +"#"+ languageFrom + "/" + languageTo+"/"+  expression;
                string sPage = client.DownloadString(url);

                // Parse as the page as a string
                //  Page can have bad HTML causing problems if you try to parse as xml
                //  Find the span with the title of the original string
                int tagStart = sPage.IndexOf("<span title=\"" + expression + "\"");
                int tagEnd = sPage.IndexOf("</span>", tagStart);
                string resultsTag = sPage.Substring(tagStart, (tagEnd - tagStart));
                //get rid of the start tag
                resultsTag = resultsTag.Substring(resultsTag.IndexOf(">") + 1);

                //You now have the translated text
                translatedText = resultsTag.Trim();


                //dispose of the web client
                client.Dispose();
            }
            catch (Exception err)
            {
                throw new Exception("Failed to download results from Google Translator.\r\n" + err.Message);
            }

            return null;
            //return base.TranslateExpression(expression, languageFrom, languageTo);
        }
    }
}
