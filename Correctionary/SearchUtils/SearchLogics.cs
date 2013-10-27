using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using GoogleSearchAPI.Query;
using Google.API.Search;
//using GoogleSearchAPI.Resources;
//using GoogleSearchAPI;

namespace SearchUtils
{
    //this is for avoiding collisions between 'CommonObjects.Language' and 'GoogleSearchAPI.Resources.Languages'
    //using gLanguages =  GoogleSearchAPI.Resources.Languages;
    //using CountryCode =  GoogleSearchAPI.Resources.CountryCode;
    //using NewsEdition =  GoogleSearchAPI.Resources.NewsEdition;
    using System.Diagnostics;
    using System.Net;
    using System.IO;
    using System.Drawing;
    /// <summary>
    /// Class for handling search logics
    /// </summary>
    public class SearchLogics : CommonObjects.ILoggable
    {
        /// <summary>
        /// Occurs when an event that is worth logging has occured.
        /// </summary>
        public event EventHandler<CommonObjects.LogArgs> onWorthLogging;

        /// <summary>
        /// Searches for images by query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="language">The language.not implemented yet)</param>
        /// <param name="numberOfresult">The number of results to return.</param>
        /// <returns></returns>
        public List<Image> SearchImage(string query, CommonObjects.Language language, int numberOfresult)
        {
            List<Image> returnImages = new List<Image>();
            if (!String.IsNullOrWhiteSpace(query)) // at least check if the user entered an input ...
            {

                string requestUrl = "http://www.google.com/search?hl=en&source=imghp&biw=1408&bih=637&q=" + query + "&gbv=2&aq=f&aqi=&aql=&oq=&gs_rfai=&tbm=isch"; // the actual request URL. example with keyword nba: http://www.google.com/search?hl=en&source=imghp&biw=1408&bih=637&q=nba&gbv=2&aq=f&aqi=&aql=&oq=&gs_rfai=&tbm=isch

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                using (HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = httpWebResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            string html = reader.ReadToEnd(); // here we go, we sent a request  using the steps above and saved the response in our StreamReader

                            String[] s = html.Split(new String[] { "&amp" }, StringSplitOptions.RemoveEmptyEntries)
                               .Where(data => data.Contains("imgres?imgurl="))
                               .Select(data => data.Substring(3)).ToArray();
                            
                            
                            // bla bla bla get the pieces of code where there's imgres?imgurl= (where google stores the images' urls)
                            int itteration = Math.Min(numberOfresult, s.Count());
                            for (int i = 0; i < itteration; i++) // loop the string array 
                            {
                                string imgUrl = s[i].Remove(0, s[i].IndexOf("imgurl=") + 7); // get the url (this was done by speculation, hence the +7) - hey it works for now > some geek can probably write a regular expression and get rid of the speculation method, but hey I just wanted to get some results asap
                                WebClient wc = new WebClient(); // webclient allows you to download urls
                                try
                                {

                                    byte[] bImage = wc.DownloadData(imgUrl); // download the images (which we are looping) into the directory (which we created earlier). In this example, I am naming the files nba1.jpg, nba2.jpg, etc ... YOU'RE DONE
                                    MemoryStream ms = new MemoryStream(bImage);
                                    returnImages.Add(Image.FromStream(ms));
                                    
                                }
                                catch (Exception ex)
                                {
                                    TriggerOnWorthLogging(String.Format("An error Occured while searching for image:{0}{1}",Environment.NewLine,ex.Message));
                                }
                            }
                        }
                    }
                }
            }
            return returnImages;
        }

        /// <summary>
        /// Searches for image by query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="language">The language.not implemented yet)</param>
        /// <param name="numberOfresult">The number of results to return.</param>
        /// <returns></returns>
        public Image SearchImage(string query, CommonObjects.Language language)
        {
            List<Image> images = this.SearchImage(query, language, 1);
            return images.Count > 0 ? images[0] : null;
        }

        /// <summary>
        /// Triggers the on worth logging.
        /// </summary>
        /// <param name="message">The message.</param>
        private void TriggerOnWorthLogging(string message)
        {
            if (this.onWorthLogging != null)
            {
                var e = new CommonObjects.LogArgs("SearchLogics", message);
                this.onWorthLogging(this, e);
            }
        }

        
    } 
}
