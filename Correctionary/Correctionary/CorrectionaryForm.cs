#if DEBUG
#define ShowMessages
#endif

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using nsLogics;
using CommonObjects;
using TransparentControls;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace Correctionary
{

    /// <summary>
    /// The main form of correctionary
    /// </summary>
    public partial class CorrectionaryForm : Form
    {
        #region Delegates
        delegate void ControlStringDelegate(Control txb, string str);
        #endregion

        #region Data Members

        /// <summary>
        /// The name of user settings file
        /// </summary>
        const string USER_SETTINGS_FILE_NAME = "settings.xml";
        /// <summary>
        /// The translation background color
        /// </summary>
        static readonly Color DEFAULT_TRANSLATION_BACKGROUND = Color.Lavender;
        /// <summary>
        /// Gets the user setting path.
        /// </summary>
        string UserSettingPath
        {
            get
            {
                return Path.Combine(Environment.CurrentDirectory, CorrectionaryForm.USER_SETTINGS_FILE_NAME);
            }
        }

        //Hack: I have this alread in translator. put it somewhere that is available for both...
        static readonly char[] HEBREW_LETTERS;
        /// <summary>
        /// The instancs of logics unit
        /// </summary>
        CorrectionaryLogics _logics;
        /// <summary>
        /// The timer that will handle all NFI timed Icon changes
        /// </summary>
        Timer _timerNfiIcon;

        /// <summary>
        /// the time, in milliseconds, before the notify icon return to the regular one after a hot key was pressed
        /// </summary>
        const int HOT_KKEY_ICON_DURATION_IN_MILLISECONDS = 500;


        /// <summary>
        /// The user settings
        /// </summary>
        UserSettings _userSettings;
    


        #endregion

        #region C'tors
        /// <summary>
        /// Initializes the <see cref="CorrectionaryForm"/> class.
        /// </summary>
        static CorrectionaryForm()
        {
            CorrectionaryForm.HEBREW_LETTERS =
            #region The alphabet
 new Char[] { 'א', 'ב', 'ג', 'ד', 'ה', 'ו', 'ז', 'ח', 'ט', 'י', 'כ', 'ל', 'מ', 'נ', 'ס', 'ע', 'פ', 'צ', 'ק', 'ר', 'ש', 'ת', 'ך', 'ף', 'ם', 'ן', 'ץ' };
            #endregion
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CorrectionaryForm"/> class.
        /// </summary>
        public CorrectionaryForm()
        {

            InitializeComponent();
            this._logics = new CorrectionaryLogics();
            this._timerNfiIcon = new Timer();
            this._timerNfiIcon.Tick += new EventHandler(timerNfiIcon_Tick);
            this.PauseTimer(this._timerNfiIcon);
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            this.ShowInTaskbar = false;
            this.BindEvents();
            this._userSettings = this.GetUserSettings(this.UserSettingPath);
            if (this._userSettings == null)
            {
                this._userSettings = new UserSettings();
                this._userSettings.LaguageFrom = this._logics.GetLanguageFrom();
                this._userSettings.LaguageTo = this._logics.GetLanguageTo();

            }
            this.SetBtnTranslationState();
            //applying settings
            this._logics.SetUserSettings(this._userSettings);

            this.ShowWelcomeMessage();
        }


        #endregion

        #region Private functions
        /// <summary>
        /// Binds the events the form should listen to.
        /// </summary>
        private void BindEvents()
        {
            this._logics.onGotTranslation += new EventHandler<TranslationEventArgs>(this.logics_onGotTranslation);
            this._logics.onTranslationStarted += new EventHandler(this.logics_onTranslationStarted);
            this._logics.onTranslationCompleted += new EventHandler(this.logics_onTranslationCompleted);
            this._logics.onHotkeyPressed += new EventHandler<HotkeyPressedArgs>(this.logics_onHotkeyPressed);
            this._logics.onFailedRegistratingHotKeys += new EventHandler<ErrorRegistratingHotKeyArgs>(logics_onFailedRegistratingHotKey);
        }

        

        /// <summary>
        /// Displaytranslations the specified e.
        /// </summary>
        /// <param name="e">The <see cref="nsLogics.TranslationEventArgs"/> instance containing the event data.</param>
        private void DisplayTranslation(TranslationInContextPackage translation)
        {
            string header = String.Empty;
            string textToDisplay = String.Empty;
            Image imageToDisplay = null;
            if (!translation.IsEmpty && !translation.ErrorEncounterd)
            {
                #region Get text values from translations
                string context = String.Join(" ", translation.TransLatedSentenceWords);

                //If we have translated only a sentence, it means that the "context" should be treated as the "word"
                string wordToDisplay = translation.BestMatch;

                #region Setting test form
                //if we had a sentence
                if (!String.IsNullOrWhiteSpace(translation.BestMatch)
                    && translation.Translations.Count == 0)
                {
                    wordToDisplay = String.Join(" ", translation.TransLatedSentenceWords);
                }


                //if there are no values or only best match, don't show additional info
                string otherWords = translation.Translations.Count <= 1 ?
                    String.Empty : " ( " + String.Join(" ,", translation.Translations) + " )";
                //we have word and context
                if (!String.IsNullOrEmpty(translation.BestMatch)
                    && !String.IsNullOrEmpty(otherWords))
                {
                    otherWords = otherWords.Replace(translation.BestMatch, String.Empty);

                }

                this.SetTextBoxText(this.rtfContext, context);
                this.SetTextBoxText(this.rtfWord, translation.BestMatch + otherWords);
                #endregion

                textToDisplay = wordToDisplay;
                foreach (String str in translation.Translations)
                {

                    String word = str.Replace('_', ' ');
                    if (str != wordToDisplay)
                    {
                        textToDisplay += "\n" + word;
                    }
                }
                #endregion

                header = translation.Word.Substring(0, Math.Min(translation.Word.Length, 10))
                            + ": " + wordToDisplay.Substring(0, Math.Min(wordToDisplay.Length, 10));
                imageToDisplay = translation.Image;
            }
            else //we had an error
            {
                if (translation.ErrorEncounterd)
                {
                    header = "Error!";
                    textToDisplay = translation.ErrorException.Message;
                }
                else
                {
                    header = "Got an empty translation";
                    textToDisplay = "Got an empty translation";
                }
            }

            RightToLeft rtl = translation.LaguageTo.IsRightToLeft ? RightToLeft.Yes: RightToLeft.No;
            this.ShowMessage(header, textToDisplay,5000,DEFAULT_TRANSLATION_BACKGROUND,rtl,imageToDisplay);
            

        }

        /// <summary>
        /// Sets the Control  text, Thread safe.
        /// </summary>
        /// <param name="ctrl">The Control.</param>
        /// <param name="text">The text.</param>
        private void SetTextBoxText(Control ctrl, string text)
        {
            if (ctrl != null)
            {
                if (ctrl.InvokeRequired)
                {
                    ctrl.Invoke(new ControlStringDelegate(SetTextBoxText), new object[] { ctrl, text });
                }
                else
                {
                    ctrl.Text = text;
                    ctrl.RightToLeft = this.HasHebrewCharachtars(text) ?
                        System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No;

                }
            }
        }

        /// <summary>
        /// Sets the component icon.
        /// </summary>
        /// <param name="notifyIcon">The notify icon.</param>
        /// <param name="icon">The icon.</param>
        private void SetComponentIcon(NotifyIcon notifyIcon, System.Drawing.Icon icon)
        {
            notifyIcon.Icon = icon;
        }

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        private void PauseTimer(Timer timer)
        {
            timer.Stop();
        }

        /// <summary>
        /// Determines whether the specified expression has hebrew charachtars.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///   <c>true</c> if the specified expression has hebrew charachtars; otherwise, <c>false</c>.
        /// </returns>
        private bool HasHebrewCharachtars(string expression)
        {
            return expression.IndexOfAny(CorrectionaryForm.HEBREW_LETTERS) >= 0;
        }

        /// <summary>
        /// Gets the user settings from file.
        /// </summary>
        /// <param name="path">The path of file.</param>
        /// <returns></returns>
        private UserSettings GetUserSettings(string path)
        {
            UserSettings retSettings = null;
            if (File.Exists(path))
            {
                // Construct an instance of the XmlSerializer with the type
                // of object that is being deserialized.
                XmlSerializer serializer = new XmlSerializer(typeof(UserSettings));

                try
                {
                    // To read the file, create a FileStream.
                    FileStream steam;
                    using (steam = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        // Call the Deserialize method and cast to the object type.
                        retSettings = serializer.Deserialize(steam) as UserSettings;
                    }
                }
                catch (Exception ex)
                {
                    this.ShowMessage("Error while getting user settings", ex.Message);
                }

            }
            return retSettings;
        }

        /// <summary>
        /// saves the user settings to file.
        /// </summary>
        /// <param name="path">The path of file.</param>
        /// <returns></returns>
        private bool SaveUserSettings(string path, UserSettings settings)
        {
            bool success = true;
            if (settings != null)
            {
                // Construct an instance of the XmlSerializer with the type
                // of object that is being deserialized.
                XmlSerializer serializer = new XmlSerializer(typeof(UserSettings));

                try
                {
                    // To read the file, create a FileStream.
                    StreamWriter stream;
                    using (stream = new StreamWriter(path))
                    {
                        serializer.Serialize(stream, settings);
                        stream.Close();
                    }
                }
                catch (Exception ex)
                {
                    this.ShowMessage( "Error while saving user settings",ex.Message);
                }

            }
            return success;
        }

        /// <summary>
        /// Shows a message.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void ShowMessage(string caption, string message)
        {
            this.ShowMessage(caption, message,null);
        }

        /// <summary>
        /// Shows a message.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="message">The message.</param>
        /// <param name="image">The image.</param>
        private void ShowMessage(string caption, string message, Image image)
        {
            this.ShowMessage(caption,message,3500,Color.Empty,System.Windows.Forms.RightToLeft.No, image);
        }
        /// <summary>
        /// Shows a message.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="message">The message.</param>
        /// <param name="showTime">The time to display (miliseconds).</param>
        /// <param name="background">The background color.</param>
        /// <param name="rtl">The right-to-left mode.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void ShowMessage(string caption, string message, int showTime, Color background, RightToLeft rtl, Image image)
        {
            if (this.InvokeRequired)
            {
                var action = new Action<string, string, int, Color, RightToLeft, Image>(ShowMessage);
                object[] args = new object[] { caption, message, showTime, background, rtl, image };
                this.BeginInvoke(action, args);
            }
            else
            {
                this._logics.Log(message);

                Notification notificationDisplay = new Notification(showTime);
                notificationDisplay.FormClosed += new FormClosedEventHandler(Notification_FormClosed);


                notificationDisplay.Text = caption;
                notificationDisplay.SetInnerTexst(message);
                notificationDisplay.SetImage(image);

                notificationDisplay.BackColor = background;
                notificationDisplay.RightToLeft = rtl;




                notificationDisplay.Show();

                this.SetNotificationLocation(notificationDisplay);

                notificationDisplay.Flash();
            }
        }

        /// <summary>
        /// Sets the specified notification form  location.
        /// </summary>
        /// <param name="notificationDisplay">The notification display.</param>
        private void SetNotificationLocation(Notification notificationDisplay)
        {
            this._logics.Log(String.Format("Location: {0}", this._userSettings.TranslationLocation));
            //point is a struct and is passed by value
            Point? location = null ; 

            switch (this._userSettings.TranslationLocation)
            {
                case TranslationDisplayLocation.DefaultLocation:
                    location = null;
                    break;
                case TranslationDisplayLocation.LastLocation:
                    if (this._userSettings.LastTranslationDisplayLocation != null)
                    {
                        location = this._userSettings.LastTranslationDisplayLocation;
                    }
                    break;
                case TranslationDisplayLocation.CursorLocation:
                    location = Cursor.Position;
                    break;
                default:
                    location = null;
                    break;
            }



            notificationDisplay.Location = location.HasValue?location.Value : notificationDisplay.Location;
        }

        /// <summary>
        /// Shows the welcome message.
        /// </summary>
        private void ShowWelcomeMessage()
        {
            string transKey = this._userSettings.TranslationHotKey.ToString();
            string rTranslationKey = this._userSettings.ReverseTranslationHotKey.ToString();
            this.ShowMessage("Correctionary is now running.",
                             "Correctionary is now running.\n\n"
                            + "To translate text highlight it, and click " + transKey + ".\n"
                            + "For a reverse translation click " + rTranslationKey + ".");
        }

        /// <summary>
        /// Sets the state of the BTN translation.
        /// </summary>
        private void SetBtnTranslationState()
        {
            bool enable = !String.IsNullOrWhiteSpace(this.tsTxbWord.Text) && !String.IsNullOrWhiteSpace(this.tsTxbContext.Text);
            this.tsmTranslate.Enabled = enable;
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the FormClosed event of the notificationDisplay control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void Notification_FormClosed(object sender, FormClosedEventArgs e)
        {
            Notification frmNotification = sender as Notification;
            if (frmNotification != null)
            {
                this._userSettings.LastTranslationDisplayLocation = frmNotification.Location;
            }

        }

        
        /// <summary>
        /// Handles the Tick event of the timerNfiIcon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void timerNfiIcon_Tick(object sender, EventArgs e)
        {
            //we want the timer to tick only once. so: pausing the timer and setting regular icon to nfi
            this.PauseTimer(this._timerNfiIcon);
            this.SetComponentIcon(this.nfi, this.IconRegular);

        }

        /// <summary>
        /// Handles the onHotkeyPressed event of logics .
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void logics_onHotkeyPressed(object sender,  HotkeyPressedArgs e)
        {
            this.SetComponentIcon(this.nfi, this.IconHotkey);

            this._timerNfiIcon.Interval = HOT_KKEY_ICON_DURATION_IN_MILLISECONDS;
            this._timerNfiIcon.Start();

            
            if (this._userSettings.ShowDebugMessages)
            {
                //we adoing it in another thread because other wise we are blocking the logic thread and missing system messages.
                //TODO: do the threading thingy on the logic layer or lower. optionally for all events
                System.Threading.ParameterizedThreadStart ts = 
                    new System.Threading.ParameterizedThreadStart(ShowHotKeyPressedMessage);
                
                System.Threading.Thread th = new System.Threading.Thread(ts);

                th.Start(e);
                
            }

        }

        void logics_onFailedRegistratingHotKey(object sender, ErrorRegistratingHotKeyArgs e)
        {
            string msg =
                String.Format("Failed to register Hotkey ('{0}').{1}Please try another hot key."
                , e.HotKeyPackage.ToString(), Environment.NewLine);
            this.ShowMessage("Failed to register Hot key.", msg);
        }

        void ShowHotKeyPressedMessage(object argument)
        {
            HotkeyPressedArgs e = argument as HotkeyPressedArgs;
            if (e != null)
            {
                this.ShowMessage("Hot Key was pressed", String.Format("Hot Key was pressed: {0} {1}", e.Modifier, e.Key));

            }
            else if (this._userSettings.ShowDebugMessages)            
            {
                this.ShowMessage("Cannot display hot key pressed",
                    String.Format("Cannot display hot key pressed argument was of type: '{0}'",
                    argument == null? "Null": argument.GetType().ToString()));
            }
        }

        /// <summary>
        /// Handles the onTranslationStarted event of logics.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void logics_onTranslationStarted(object sender, EventArgs e)
        {
            this.pnlMain.SetControlVisibility(false);
            this.pbLoading.CenterControl();
            this.pbLoading.SetControlVisibility(true);
            if (this.chbShowLoadingNotification.Checked)
            {
                this.SetComponentIcon(this.nfi, this.IconTranslating);
            }

        }

        /// <summary>
        /// Handles the onTranslationCompleted event of logics.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void logics_onTranslationCompleted(object sender, EventArgs e)
        {
            this.pnlMain.SetControlVisibility(true);
            this.pbLoading.SetControlVisibility(false);
        }

        /// <summary>
        /// Handles the onGotTranslation event of logics.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="nsLogics.TranslationEventArgs"/> instance containing the event data.</param>
        private void logics_onGotTranslation(object sender, TranslationEventArgs e)
        {
            //Setting back regular icon
            this.SetComponentIcon(this.nfi, this.IconRegular);
            this.DisplayTranslation(e.Translation);

        }

        /// <summary>
        /// Handles the Click event of the tool strip menu Translate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tsmTranslate_Click(object sender, EventArgs e)
        {
            this._logics.TranslateWordInContext(this.tsTxbWord.Text, this.tsTxbContext.Text);
            this.tsTxbWord.Text = String.Empty;
            this.tsTxbContext.Text = String.Empty;
            this.tsTxbWord.ApplyWatermark();
            this.tsTxbContext.ApplyWatermark();
        }

        /// <summary>
        /// Handles the FormClosing event of the CorrectionaryForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void CorrectionaryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.nfi.Visible = false;
            this.nfi.Icon = null;
            this.nfi.Visible = false;
            this.nfi.Dispose();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chbTopMost control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void chbTopMost_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = this.chbTopMost.Checked;
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the nfi control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void nfi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();

        }

        /// <summary>
        /// Handles the Resize event of the Correctionary control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Correctionary_Resize(object sender, EventArgs e)
        {
            if (System.Windows.Forms.FormWindowState.Minimized == WindowState)
            {
                //this.WindowState = FormWindowState.Minimized;
                this.Visible = false;
            }

        }

        /// <summary>
        /// Handles the Click event of the exitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        /// <summary>
        /// Handles the Click event of the settingsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm stn;
            using (stn = new SettingsForm(this._userSettings.Clone() as UserSettings))
            {
                if (stn.ShowDialog(this) == DialogResult.OK)
                {
                    this._userSettings = stn.UserSettings.Clone() as UserSettings;
                    bool success = this.SaveUserSettings(this.UserSettingPath, this._userSettings);
                    this._logics.SetUserSettings(this._userSettings);
                    if (!success)
                    {
                        this.ShowMessage("Error", "Error one saving settings");
                    }
                }
            }
           
        }
        /// <summary>
        /// Handles the TextChanged event of the tsTxbWord control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsTxbWord_TextChanged(object sender, EventArgs e)
        {
            this.SetBtnTranslationState();
        }

        /// <summary>
        /// Handles the TextChanged event of the tsTxbContext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsTxbContext_TextChanged(object sender, EventArgs e)
        {
            this.SetBtnTranslationState();
        }
        
        #endregion

       

    }
}
