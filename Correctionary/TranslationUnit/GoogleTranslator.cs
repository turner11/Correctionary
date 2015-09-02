using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using CommonObjects;
using HtmlAgilityPack;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Threading;



namespace TranslationUnit
{
    /// <summary>
    /// The translation unit that uses google translate
    /// </summary>
    partial class GoogleTranslator
    {
        #region URL

        /// <summary>
        /// The template for translation requests from google 
        /// </summary>

        const string GOOGLE_TRANSLATE_URL_TEMPLATE = "https://translate.google.com/translate_a/single?client=t&sl={0}&tl={1}&hl={2}&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&dt=at&ie=UTF-8&oe=UTF-8&otf=1&ssel=3&tsel=4&kc=1&tk=522633|266647&q={3}";

 
#region OLD URLs

        //const string GOOGLE_TRANSLATE_URL_TEMPLATE = "http://translate.google.com/translate_a/t?client=p&hl={0}&sl={1}&tl={2}&ie=UTF-8&oe=UTF-8&multires=1&oc=2&otf=1&ssel=0&tsel=0&pc=1&sc=1&q={3}"; 
       //const string GOOGLE_TRANSLATE_URL_TEMPLATE ="http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}";
      
                                                  
        //"http://translate.google.com/translate_a/t?client=t&hl=iw&sl=auto&tl=iw&ie=UTF-8&oe=UTF-8&multires=1&oc=1&prev=conf&psl=en&ptl=iw&otf=1&it=sel.8772&ssel=3&tsel=6&uptl=iw&alttl=en&sc=1&q=dog"
                                                  
        //"http://translate.google.com/translate_a/t?client=p&text={0}&hl={1}&sl=en&tl={2}&ie=UTF-8&oe=UTF-8&multires=1&otf=2&ssel=2&tsel=2&sc=1";   
	#endregion
        #endregion

        #region C'tors
        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleTranslator"/> class.
        /// </summary>
        public GoogleTranslator()
        {
            // this is just for getting initial connection...
            var lang = new Language("en","English","English");
            Thread t = new Thread(()=>this.GetTranslation("Hello World", lang, lang));
            t.Start();

            
        } 
        #endregion

        #region Internal functions

        /// <summary>
        /// Translates the specified word in the context of sentence.
        /// </summary>
        /// <param name="word">The word to translate.</param>
        /// <param name="context">The sentence which is the context of the word.</param>
        /// <param name="languageFrom">The language to translate from.</param>
        /// <param name="languageTo">The language to translate to.</param>
        /// <returns>
        /// the translation of word in context
        /// </returns>
        /// <exception cref="System.ArgumentException">Language to translate to was not specified</exception>
        internal TranslationInContextPackage Translate(string word, string context, Language languageFrom, Language languageTo)
        {
            if (languageTo == null)
            {
                throw new ArgumentException("Language to translate to was not specified");
            }
            TranslationInContextPackage retPackage = this.GetTranslationWithContext(word, context, languageFrom, languageTo);
            return retPackage;

        } 
        #endregion

        #region Private functions
        /// <summary>
        /// Gets the translation for a word in a context.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="context">The context.</param>
        /// <param name="languageFrom">The language to translate from.</param>
        /// <param name="languageTo">The language to translate to.</param>
        /// <returns>The package contaning translation</returns>
        private TranslationInContextPackage GetTranslationWithContext(string word, string context, Language languageFrom, Language languageTo)
        {
            // TODO: document, if needed, simplify!
            TranslationInContextPackage retPackage = new TranslationInContextPackage();
            retPackage.LaguageFrom = languageFrom;
            retPackage.LaguageTo = languageTo;


            Translation translationSentence = null;

            if (!String.IsNullOrWhiteSpace(context))
            {
                // tanslate a sentence and put words's sentence in array
                translationSentence = this.GetTranslation(context, languageFrom, languageTo);

                retPackage.CopyBaseValues(translationSentence);
            }
            // if we had an error, don't bother
            if (!retPackage.ErrorEncounterd)
            {
                Translation translationWord = null;
                if (!String.IsNullOrWhiteSpace(word))
                {
                    translationWord = this.GetTranslation(word, languageFrom, languageTo);
                    retPackage.Word = word;
                    retPackage.Translations.AddRange(translationWord.Translations);
                }

                if (translationSentence != null)
                {
                    //Check if the word we are translating, appers in the tranlated context
                    if (translationWord == null)
                    {
                        retPackage.Word = context;
                        retPackage.Translations.AddRange(translationSentence.Translations);
                    }
                    else
                    {


                        foreach (string currWord in translationWord.Translations)
                        {
                            foreach (string currContextWord in translationSentence.Translations)
                            {
                                if (String.Compare(currWord, currContextWord) == 0)
                                {
                                    retPackage.BestMatch = currWord;
                                }

                            }
                        }
                    }
                }



                //if there was no match in sentence, we should still return a translation
                if (String.IsNullOrWhiteSpace(retPackage.BestMatch)
                    && retPackage.Translations.Count > 0)//only if we have a word we need to get a best match...
                {
                    retPackage.BestMatch = retPackage.Translations.First<string>();
                }
            }
            //HACK: this happens when we translate a sentence, it is because we don't undestand the structure yet...
            List<string> temp = retPackage.Translations.Distinct().ToList();
            retPackage.Translations.Clear();
            retPackage.Translations.AddRange(temp);

            return retPackage;
        }

        /// <summary>
        /// Gets the translation for an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="languageFrom">The language to translate from.</param>
        /// <param name="languageTo">The language to translate to.</param>
        /// <returns>the translation</returns>
        private Translation GetTranslation(string expression, Language languageFrom, Language languageTo)
        {
            //Will hold translated values
            Translation translation = null;
            //The translate symbol 
            String languagePairSymbol = String.Empty;


            //get the translation
            translation = this.TranslateExpression(expression, languageFrom, languageTo);

            return translation;
        }

        /// <summary>
        /// Translates a sentence.
        /// </summary>
        /// <param name="expression">The sentence.</param>
        /// <param name="source">The language to translate from.</param>
        /// <param name="target">The language to translate to.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The translation</returns>
        private Translation TranslateExpression(string expression, Language languageFrom, Language languageTo)
        {
            Translation trans = new Translation(expression);
            
            //Getting the url
            string url = GoogleTranslator.GetTranslateUrl(expression, languageFrom, languageTo);

            string reply = String.Empty;
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    webClient.Encoding = System.Text.Encoding.UTF8;
                    webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
                    webClient.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");//Encoding.GetEncoding(1255);

                    reply = webClient.DownloadString(url); ;
                    //Stream st = webClient.OpenRead(url);
                    //StreamReader reader = new StreamReader(st);
                    //reply = reader.ReadToEnd();
                }
                catch (WebException ex)
                {
                    trans = new Translation();
                    trans.ErrorException = ex;
                    return trans;
                    //TODO: add logging
                }

            }
            if (!string.IsNullOrEmpty(reply))
            {
                trans = this.GetTranslationFromReply(reply);
            }
            else
            {
                //TODO: Add log
            }


            return trans;
        }

        /// <summary>
        /// Gets the translation from googles reply.
        /// </summary>
        /// <param name="reply">Googls reply.</param>
        private Translation GetTranslationFromReply(string reply)
        {

            Translation translation = null;

            try
            {



                //this will contain all translations
                JObject jReply = JObject.Parse(reply);
                List<string> translations = GetTranslations(jReply);

                translation = new Translation(String.Empty, translations);
            }
            catch (Exception ex)
            {

                translation = new Translation();
                translation.ErrorException = ex;
            }
          

            return translation;
        }

        /// <summary>
        /// Gets the the translated word from google reply .
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <returns></returns>
        private string GetWord(JObject reply)
        {

            string word = String.Empty;
            List<JToken> sentences = reply["sentences"].ToList();
            if (sentences != null && sentences.Count > 0)
            {
                word = (string)sentences[0]["trans"];

            }

            return word;
        }

        /// <summary>
        /// Gets the translations from google reply.
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <returns></returns>
        private static List<string> GetTranslations(JObject reply)
        {
            List<string> translations = new List<string>();

            if (reply["dict"] != null) //for word
            {
                List<JToken> dictionary = reply["dict"].ToList();
                for (int i = 0; i < dictionary.Count; i++)
                {
                    if (dictionary[i]["terms"] != null)
                    {
                        List<JToken> terms = dictionary[i]["terms"].ToList();
                        if (terms != null)
                        {
                            for (int j = 0; j < terms.Count; j++)
                            {
                                string translation = (string)terms[j];
                                translations.Add(translation);
                            }
                        }
                    }


                }
            }
            else if (reply["sentences"] != null) //for setences
            {

                List<string> temp = new List<string>();
                List<JToken> sentences = reply["sentences"].ToList();
                for (int i = 0; i < sentences.Count; i++)
                {

                    if (sentences[i]["trans"] != null)
                    {
                        JToken trans = sentences[i]["trans"];
                        if (trans != null)
                        {
                            string translation = (string)trans;
                            temp.Add(translation);
                        }
                    }
                }
                string sentanceJoin = String.Join(" ", temp);
                translations.Add(sentanceJoin);
            }

            return translations;
        }

        /// <summary>
        /// Gets the translate URL.
        /// </summary>
        /// <param name="expression">The sentence.</param>
        /// <param name="source">The language to translate from.</param>
        /// <param name="target">The language to translate to.</param>
        /// <returns>the url for requesting translate</returns>
        private static string GetTranslateUrl(string expression, Language languageFrom, Language languageTo)
        {

            bool isAutoDetect = languageFrom == null;

            string targetSymbol = languageTo.Symbol;
            string sourceSymbol = isAutoDetect ? String.Empty : languageFrom.Symbol;



            return GetUrlBySymbols(expression, sourceSymbol, targetSymbol);
        }

        /// <summary>
        /// Gets the URL by symbols.
        /// </summary>
        /// <param name="expression">The expression to translate.</param>
        /// <param name="sourceSymbol">The symbol of source language.</param>
        /// <param name="targetSymbol">The symbol of to source language.</param>
        /// <returns>the url</returns>
        private static string GetUrlBySymbols(string expression, string sourceSymbol, string targetSymbol)
        {
            // for a parameter called SL (source language) in the URL...
            string slParam = String.IsNullOrWhiteSpace(sourceSymbol) ? "auto" : sourceSymbol;

            //this is for passing the expression in encodeURIcomponent format for non English expressions
            string encodedSentene = Uri.EscapeUriString(expression);

            string url = String.Format(GoogleTranslator.GOOGLE_TRANSLATE_URL_TEMPLATE, slParam, targetSymbol, sourceSymbol, encodedSentene);

           
            return url;
        }
        
        #endregion

    }

    #region Sub classes

    public class WordTranslationPair : ICloneable
    {
        string _translatedWord;
        /// <summary>
        /// Gets the word translated.
        /// </summary>
        public string TranslatedWord
        {
            get { return _translatedWord; }
        }

        List<string> _translationList;
        /// <summary>
        /// Gets the list containing Translations for word<string>.
        /// </summary>
        public ICollection<string> TranslationList
        {
            get { return _translationList; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WordTranslationPair"/> class.
        /// </summary>
        public WordTranslationPair()
            : this(String.Empty, null)
        {

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="WordTranslationPair"/> class.
        /// </summary>
        /// <param name="translatedWord">The translated word.</param>
        /// <param name="translationList">The translation list.</param>
        public WordTranslationPair(string translatedWord, ICollection<string> translationList)
        {
            this._translatedWord = translatedWord ?? String.Empty;
            this._translationList = new List<string>();
            //Adding the words from list to new instance. (we are doing it this way, in order not to change parameter's pointer)
            ICollection<string> collection = translationList;
            if (collection == null)
            {
                collection = new List<string>();
            }

            this._translationList.AddRange(translationList);


        }


        public object Clone()
        {
            WordTranslationPair clone = new WordTranslationPair(this.TranslatedWord, this.TranslationList);
            return clone;
        }
    } 
    #endregion

}