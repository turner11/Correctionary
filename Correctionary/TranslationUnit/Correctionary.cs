using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonObjects;
using System.IO;
using System.Xml.Serialization;

namespace TranslationUnit
{
    /// <summary>
    /// The wrapper calss that is in charge of translations
    /// </summary>
    public class CorrectionaryUnit
    {
        #region Data members

        static readonly Language DEFAULT_LANGUAGE_FOR = new Language("en", "English", "English");
        static readonly Language DEFAULT_LANGUAGE_TO = new Language("iw", "עברית", "Hebrew");
        
        /// <summary>
        /// The object that translate text
        /// </summary>
        private GoogleTranslator _translator;

        /// <summary>
        /// The to translate language from
        /// </summary>
        Language _languageFrom;
        /// <summary>
        /// The to translate language to
        /// </summary>
        Language _languageTo;

        /// <summary>
        ///if set to <c>true</c> translations will be reverse 
        /// (will translate from language to => to language from)
        /// </summary>
        bool _revereseTranslationDirection;

        /// <summary>
        /// indicates if the auto detecting language is enabled
        /// </summary>
        bool _isAutoDetectingLanguage;
        #endregion

        #region C'tors
        /// <summary>
        /// Initializes a new instance of the <see cref="CorrectionaryUnit"/> class.
        /// </summary>
        public CorrectionaryUnit()
        {
            this._translator = new GoogleTranslator();
            this._languageFrom = DEFAULT_LANGUAGE_FOR;
            this._languageTo = DEFAULT_LANGUAGE_TO;
            this._isAutoDetectingLanguage = false;

           // LanguageDetector detector = new LanguageDetector();
        } 
        #endregion

        #region Public functions
        /// <summary>
        /// Translates the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public TranslationPackage Translate(string text)
        {
            return Translate(text, String.Empty);
        }

        /// <summary>
        /// Translates the specified text in a specific context.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public TranslationInContextPackage Translate(string text, string context)
        {
            
            // if we are reversing direction, swap to and from
            Language to = !this._revereseTranslationDirection ? this._languageTo:this._languageFrom;
            Language fromCandidate = !this._revereseTranslationDirection ? this._languageFrom : this._languageTo;

            // if we plan to use auto detection, pass null as "from" argument
            Language from = this._isAutoDetectingLanguage ? null : fromCandidate;
            TranslationInContextPackage translation = this._translator.Translate(text, context, from, to);
            return translation;

        }

        /// <summary>
        /// Sets the state of the reverse language.
        /// </summary>
        /// <param name="reverseTranslations">if set to <c>true</c> translations will be reverse 
        /// (will translate from language to => to language from) .</param>
        public void SetReverseLanguageState(bool reverseTranslations)
        {   
            this._revereseTranslationDirection = reverseTranslations;
        }

        /// <summary>
        /// Sets the languages for translation.
        /// </summary>
        /// <param name="languageFrom">The to translate language from.</param>
        /// <param name="languageTo">The language to translate To.</param>
        public void SetLanguages(Language languageFrom, Language languageTo)
        {
            this._languageFrom = languageFrom;
            this._languageTo = languageTo;
        }

        /// <summary>
        /// Sets the state of the language auto detection.
        /// </summary>
        /// <param name="autoDetect">if set to <c>true</c> auto detect will be enabled.</param>
        public void SetLanguageAutoDetectionState(bool autoDetect)
        {
            this._isAutoDetectingLanguage = autoDetect;
        }

        /// <summary>
        /// Gets the language that is set for translating from.
        /// </summary>
        /// <returns>the language that is set for translating from.</returns>
        public Language GetLanguageFrom()
        {
            return this._languageFrom;
        }
        /// <summary>
        /// Gets the language that is set for translating to.
        /// </summary>
        /// <returns>the language that is set for translating to.</returns>
        public Language GetLanguageTo()
        {
            return this._languageTo;
        }

        /// <summary>
        /// Gets the languages array from resource xml.
        /// </summary>
        /// <returns>an array of all languages from resources</returns>
        public static Language[] GetLanguages()
        {
            /*the resource containg languges*/
            string LangugesXml = TranslationUnit.Properties.Resources.languages.Trim();
            Language[] languages = new Language[0];

            MemoryStream stream; ;
            //Restore from string
            using (stream = new MemoryStream())
            {
                /*Creating the stream from string*/
                StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
                writer.Write(LangugesXml);
                writer.Flush();
                stream.Position = 0;


                /*deserializing stream*/
                XmlSerializer formatter = new XmlSerializer(typeof(Language[]));
                object objLangs = formatter.Deserialize(stream);
                languages = objLangs as Language[];
                stream.Close();
            }

            return languages;
        }
        #endregion

    }
}
