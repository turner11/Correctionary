using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HookUtils;
using CommonObjects;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using TranslationUnit;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Diagnostics;
using SearchUtils;
using System.Threading.Tasks;
using System.Drawing;


namespace nsLogics
{
    //We need to inherit from "Form" in order to get a handle 
    /// <summary>
    /// This handles all logics and requests related to the correctinary logics
    /// </summary>
    public class CorrectionaryLogics : Form
    {
        #region Delegates
        /// <summary>
        /// Occurs when got new translation.
        /// </summary>
        public event EventHandler<TranslationEventArgs> onGotTranslation;
        /// <summary>
        /// Occurs when a hot key is pressed.
        /// </summary>
        public event EventHandler<HotkeyPressedArgs> onHotkeyPressed;

        /// <summary>
        /// Occurs when failes registrating hot key.
        /// </summary>
        public event EventHandler<ErrorRegistratingHotKeyArgs> onFailedRegistratingHotKeys;
        /// <summary>
        /// Occurs when translation started.
        /// </summary>
        public event EventHandler onTranslationStarted;
        /// <summary>
        /// Occurs when translation completed.
        /// </summary>
        public event EventHandler onTranslationCompleted;
        #endregion

        #region Data Members
        /// <summary>
        /// The clip board logics object
        /// </summary>
        HookLogics _HookLogics;

        /// <summary>
        /// The translation unit
        /// </summary>
        CorrectionaryUnit _translationUnit;

        /// <summary>
        /// The search unit
        /// </summary>
        SearchLogics _searchLogics;

        /// <summary>
        /// Counts how many threads were created
        /// </summary>
        int _threadCounter;
       
        /// <summary>
        /// Indicates if we are during a translate process
        /// </summary>
        bool _isDuringTranslate;
        /// <summary>
        /// Gets a value indicating whether this instance is currently proccessing a translation.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is during translation; otherwise, <c>false</c>.
        /// </value>
        public bool IsDuringTranslate
        {
            get { return _isDuringTranslate; }
        }

        /// <summary>
        /// The last action we were requested to do, based on the hot key pressed
        /// </summary>
        private HotkeysActions _lastActionRequest;

        bool _searchForImages;
        #endregion

        #region C'tors
        /// <summary>
        /// Initializes a new instance of the <see cref="CorrectionaryLogics"/> class.
        /// </summary>
        public CorrectionaryLogics()
        {
            this._lastActionRequest = HotkeysActions.None;
            this._threadCounter = 0;
            this._HookLogics = new HookLogics();
            this._translationUnit = new CorrectionaryUnit();
            this._searchLogics = new SearchLogics();
            this.BindEvents();
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Sets the settings to match specified user settings.
        /// </summary>
        /// <param name="userSettings">The user settings.</param>
        public void SetUserSettings(UserSettings userSettings)
        {
            this.SetLanguages(userSettings.LaguageFrom, userSettings.LaguageTo);
            this.SetLanguageAutoDetectionState(userSettings.AutoDetectLanguage);
            this.SetHotKeys(userSettings.TranslationHotKey, userSettings.ReverseTranslationHotKey);
            this._searchForImages = userSettings.SearchForImages;
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            Debug.WriteLine(">========================");
            Debug.WriteLine(message);
            Debug.WriteLine("========================<");
        }

       
        /// <summary>
        /// Gets the language that is set for translating from.
        /// </summary>
        /// <returns>the language that is set for translating from.</returns>
        public Language GetLanguageFrom()
        {
            return this._translationUnit.GetLanguageFrom();
        }
        /// <summary>
        /// Gets the language that is set for translating to.
        /// </summary>
        /// <returns>the language that is set for translating to.</returns>
        public Language GetLanguageTo()
        {
            return this._translationUnit.GetLanguageTo();
        }

        /// <summary>
        /// Translates a text.
        /// </summary>
        /// <param name="text">The text to translate.</param>
        /// <returns>the translation</returns>
        public TranslationInContextPackage TranslateText(string text)
        {
            //TODO: go over. Is this how we should get context? I doubt it
            TranslationInContextPackage translation = new TranslationInContextPackage();

            string[] splittedText = text.Split(new char[] { ' ', '\t', '\n' },
                                            StringSplitOptions.RemoveEmptyEntries);
            string context = String.Empty;
            string word = String.Empty;

            //If there is more than 1 sentence
            if (splittedText.Length > 1)
            {
                context = text;
            }
            else
            {
                word = text;
            }

            if (!(String.IsNullOrWhiteSpace(context)
                    && String.IsNullOrWhiteSpace(word)))
            {
                translation = TranslateWordInContext(word, context);
                
            }
            return translation;
        }

        /// <summary>
        /// Translates a word in specified context.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="context">The context.</param>
        /// <returns>the translation</returns>
        public TranslationInContextPackage TranslateWordInContext(string word, string context)
        {
            this._isDuringTranslate = true;
            this.TriggerOnTranslationStarted();
            //getting translation
            //TranslationInContextPackage translation = this._translationUnit.Translate(word, context);
            List<Task> tasks = new List<Task>();
            TranslationInContextPackage translation = null;
            var tTranslation = Task<TranslationInContextPackage>.Factory.StartNew(() => translation = this._translationUnit.Translate(word, context));
            tasks.Add(tTranslation);
            Image image = null;
            if (this._searchForImages)
            {
                
                var tImage = Task<Image>.Factory.StartNew(() => image = this._searchLogics.SearchImage(word, this._translationUnit.GetLanguageFrom()));
                tasks.Add(tImage);
            }
           
            Task.WaitAll(tasks.ToArray());
            
            this._isDuringTranslate = false;

            if (translation != null)
            {
                translation.Image = image;
            }
            this.TriggerOnTranslationCompleted();


            //letting everyone know I got the translation...
            this.TriggerOnGotTranslation(translation);            

            return translation;
        }

        /// <summary>
        /// Gets the languages array from resource xml.
        /// </summary>
        /// <returns>an array of all languages from resources</returns>
        public static Language[] GetLanguages()
        {
            return CorrectionaryUnit.GetLanguages();
        } 
        #endregion

        #region Private functions

        /// <summary>
        /// Sets the languages for translation.
        /// </summary>
        /// <param name="languageFrom">The to translate language from.</param>
        /// <param name="languageTo">The language to translate To.</param>
        private void SetLanguages(Language languageFrom, Language languageTo)
        {
            this._translationUnit.SetLanguages(languageFrom, languageTo);
        }

        /// <summary>
        /// Sets the state of the language auto detection.
        /// </summary>
        /// <param name="autoDetect">if set to <c>true</c> auto detect will be enabled.</param>
        private void SetLanguageAutoDetectionState(bool autoDetect)
        {
            this._translationUnit.SetLanguageAutoDetectionState(autoDetect);
        }

        /// <summary>
        /// Sets the hot keys.
        /// </summary>
        /// <param name="translationKeys">The translation keys.</param>
        /// <param name="reverseTranslationKeys">The reverse translation keys.</param>
        private void SetHotKeys(HotkeyPackage translationKeys, HotkeyPackage reverseTranslationKeys)
        {
            this._HookLogics.SetHotKeys(translationKeys,reverseTranslationKeys);
        }

        /// <summary>
        /// Binds the events that is required for logics.
        /// </summary>
        private void BindEvents()
        {
            this._HookLogics.onHotkeyPressed += new EventHandler<HotkeyPressedArgs>(this.cbLogics_onHotkeyPressed);
            this._HookLogics.onGotHighlightedText += new EventHandler<ClipBoardDataArgs>(this.HookLogics_onGotHighlightedText);
            this._HookLogics.onFailedRegistratingHotKey += new EventHandler<ErrorRegistratingHotKeyArgs>(_HookLogics_onFailedRegistratingHotKeyArgs);
        }

      

        /// <summary>
        /// Handles the hot key pressed a synchronusly.
        /// </summary>
        /// <param name="HotkeyArgs">The hot key args.</param>
        private void HandleHotkeyPressed_aSync(object HotkeyArgs)
        {
            //we know what to do only if we got the appropriate type of argument
            HotkeyPressedArgs e = HotkeyArgs as HotkeyPressedArgs;
            if (e != null)
            {
                this.HandleHotkeyPressed(e);
            }
            else
            {
                throw new ArgumentException("Parameter of HandleHotkeyPressed was not HotkeyPressedArgs!");
            }
        }

        /// <summary>
        /// Translates the Highlighted text in active window and inputs it into the translation data object.
        /// </summary>
        /// <param name="clipBoardDataObject">The clip board data object.</param>
        private TranslationInContextPackage TranslateHighlightedText(ClipBoardDataObject clipBoardDataObject)
        {
            string expression = clipBoardDataObject.Text.Trim();

            return TranslateText(expression);
        }

        /// <summary>
        /// Triggers the on got translation event.
        /// </summary>
        /// <param name="translation">The translation.</param>
        private void TriggerOnGotTranslation(TranslationInContextPackage translation)
        {
            if (this.onGotTranslation != null)
            {
                TranslationEventArgs e = new TranslationEventArgs(translation);
                this.onGotTranslation(this, e);
            }
        }

        /// <summary>
        /// Triggers the on translation started event.
        /// </summary>
        private void TriggerOnTranslationStarted()
        {

            if (this.onTranslationStarted != null)
            {
                this.onTranslationStarted(this, new EventArgs());
            }
        }

        /// <summary>
        /// Triggers the on translation completed event.
        /// </summary>
        private void TriggerOnTranslationCompleted()
        {

            if (this.onTranslationCompleted != null)
            {
                this.onTranslationCompleted(this, new EventArgs());
            }
        }

        /// <summary>
        /// Triggers the on hot key pressed.
        /// </summary>
        private void TriggerOnHotkeyPressed(HotkeyPressedArgs e)
        {
            if (this.onHotkeyPressed != null)
            {
                this.onHotkeyPressed(this, e);
            }
        }

        /// <summary>
        /// Triggers the on failed to register hot key event.
        /// </summary>
        /// <param name="e">The e.</param>
        private void TriggerOnFailedToRegisterHotKey(ErrorRegistratingHotKeyArgs e)
        {
            if (this.onFailedRegistratingHotKeys != null)
            {
                this.onFailedRegistratingHotKeys(this, e);
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the clipboard logic hot key event pressed
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The argument.</param>
        private void cbLogics_onHotkeyPressed(object sender, HotkeyPressedArgs e)
        {
            this.TriggerOnHotkeyPressed(e);
            //If we are not during a translation, we can process...
            if (!this._isDuringTranslate)
            {
                //Start the translation in other thread
                this.StartTranslateThread(e);
            }

        }

        /// <summary>
        /// _s the hook logics_on got highlighted text.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        private void HookLogics_onGotHighlightedText(object sender, ClipBoardDataArgs e)
        {
            if (e.DataObject != null)
            {
                switch (this._lastActionRequest)
                {
                    case HotkeysActions.None:
                        break;
                    case HotkeysActions.ObtainContext:
                    case HotkeysActions.TranslateParagraph:
                        break;
                    case HotkeysActions.TranslateWord:                        
                    case HotkeysActions.ReverseTranslate:                       
                        TranslationInContextPackage translation = this.TranslateHighlightedText(e.DataObject);
                        break;
                    default:
                        break;
                }
                //translating
            }

        }

        /// <summary>
        /// handles the hook logics on failed registrating hot key event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void _HookLogics_onFailedRegistratingHotKeyArgs(object sender, ErrorRegistratingHotKeyArgs e)
        {
            TriggerOnFailedToRegisterHotKey(e);
        }

        

        /// <summary>
        /// Starts the translate thread.
        /// </summary>
        /// <param name="e">The e.</param>
        private void StartTranslateThread(HotkeyPressedArgs e)
        {
            ParameterizedThreadStart ts = new ParameterizedThreadStart(this.HandleHotkeyPressed_aSync);
            Thread hotKeyPressedThread = new Thread(ts);
            this._threadCounter++;
            hotKeyPressedThread.Name = "hotKeyPressedThread_" + this._threadCounter;
            hotKeyPressedThread.SetApartmentState(ApartmentState.STA); //Set the thread to STA (this will allow it to communicate with clip)
            hotKeyPressedThread.Start(e);
        }

        /// <summary>
        /// Handles the hot key pressed event.
        /// </summary>
        /// <param name="e">Hot Key args we got from the press.</param>
        private void HandleHotkeyPressed(HotkeyPressedArgs e)
        {

            //figuring out what the action is
            HotkeysActions action = this._HookLogics.GetActionByKeys(e.Modifier, e.Key);
            this._lastActionRequest = action;
            bool reverseTranslations = this._lastActionRequest == HotkeysActions.ReverseTranslate;
            this._translationUnit.SetReverseLanguageState(reverseTranslations);


            //getting the data to translate
            this._HookLogics.GetHighlightedText();
            //The handling will be continued in the HookLogics_onGotHighlightedText function 
            //it is triggered by this._HookLogics.onGotHighlightedText 

        }
        #endregion

    }
}
