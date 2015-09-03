using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonObjects;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Correctionary
{
    /// <summary>
    /// A form for modifying user settings
    /// </summary>
#if DEBUGAA
    [assembly:InternalsVisibleToAttribute("TranslationUnitTests")] 
#endif
    public partial class SettingsForm : Form
    {
        #region Data members
        /// <summary>
        /// The languages available
        /// </summary>
        Language[] _languages;
        
        UserSettings _userSettings;
        /// <summary>
        /// The user settings
        /// </summary>
        public UserSettings UserSettings
        {
            get { return _userSettings; }
        }
        /// <summary>
        /// Indicates if the form completed to initializes
        /// </summary>
        bool _isInitialized;

        #endregion

        #region C'tors
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsForm"/> class.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public SettingsForm(UserSettings setting)
        {
            this._isInitialized = false;
            
            InitializeComponent();
            this._userSettings = setting.Clone() as UserSettings;
            this._languages = nsLogics.CorrectionaryLogics.GetLanguages();
            this.BindLanguagesComboBoxes(this._languages);
            this.BindDislpayLocationOptions();
            this.BindHotKeys();
            this.MatchGuiToUserSettings(setting);
            
            this._isInitialized = true;


        }
       
        #endregion

        #region Private functions
        /// <summary>
        /// Matches the GUI to user settings.
        /// </summary>
        /// <param name="userSettings">The user settings.</param>
        private void MatchGuiToUserSettings(UserSettings userSettings)
        {
            /*Languages*/
            if (userSettings.LaguageFrom != null && this.cmbLanguageA.Items.Contains(userSettings.LaguageFrom))
            {
                this.cmbLanguageA.SelectedItem = userSettings.LaguageFrom;
            }
            if (userSettings.LaguageTo != null && this.cmbLanguageB.Items.Contains(userSettings.LaguageTo))
            {
                this.cmbLanguageB.SelectedItem = userSettings.LaguageTo;
            }
            /*Display location*/
            if (this.cmbDisplayLocation.Items.Contains(userSettings.TranslationLocation))
            {
                this.cmbDisplayLocation.SelectedItem = userSettings.TranslationLocation;
            }
            /*AutoDetect*/
            this.chbIdentifyLanguageAutomaticaly.Checked = userSettings.AutoDetectLanguage;
            /*Debug messages*/
            this.chbShowDebugMessages.Checked = userSettings.ShowDebugMessages;
            /*Images search*/
            this.chbSearchForImages.Checked = userSettings.SearchForImages;
            /*Hot keys*/
            this.cmbTranslationMofifier.SelectedItem = (ModifierKeys)userSettings.TranslationHotKey.Modifier;
            this.cmbTranslationHotKey.SelectedItem = (Keys)userSettings.TranslationHotKey.Hotkey;
            this.cmbReverseTranslationMofifier.SelectedItem = (ModifierKeys)userSettings.ReverseTranslationHotKey.Modifier;
            this.cmbReverseTranslationHotKey.SelectedItem = (Keys)userSettings.ReverseTranslationHotKey.Hotkey;
        }

        /// <summary>
        /// Binds the languages combo boxes to specified combo boxes.
        /// </summary>
        /// <param name="language">The languages to bind.</param>
        private void BindLanguagesComboBoxes(Language[] languages)
        {
            this.BindLanguageComboBox(this.cmbLanguageA, languages);
            this.BindLanguageComboBox(this.cmbLanguageB, languages);
        }

        /// <summary>
        /// Binds the dislpay location options.
        /// </summary>
        private void BindDislpayLocationOptions()
        {
            TranslationDisplayLocation selectedValue = this._userSettings.TranslationLocation;
            TranslationDisplayLocation[] locationOption = Enum.GetValues(typeof(TranslationDisplayLocation)).Cast<TranslationDisplayLocation>().ToArray();
            this.cmbDisplayLocation.DataSource = locationOption;
            this.cmbDisplayLocation.SelectedItem = selectedValue;
        }

        /// <summary>
        /// Binds the hot keys combo boxes.
        /// </summary>
        private void BindHotKeys()
        {

            ModifierKeys[] modifiers = Enum.GetValues(typeof(ModifierKeys)).OfType<ModifierKeys>().ToArray() ;
            this.cmbTranslationMofifier.DataSource = modifiers.Clone();
            this.cmbReverseTranslationMofifier.DataSource = modifiers.Clone();

            Keys[] keys = Enum.GetValues(typeof(Keys)).OfType<Keys>().ToArray();
            this.cmbTranslationHotKey.DataSource = keys.Clone();
            this.cmbReverseTranslationHotKey.DataSource = keys.Clone();
           
        }

        /// <summary>
        /// Binds the language combo box.
        /// </summary>
        /// <param name="comboBox">The combo box.</param>
        /// <param name="languages">The languages to bind.</param>
        private void BindLanguageComboBox(ComboBox comboBox, Language[] languages)
        {
            comboBox.SuspendLayout();
            object selectedValue = comboBox.SelectedItem;

            comboBox.ValueMember = "Symbol";
            comboBox.DisplayMember = this.chbDisplayNativeName.Checked ? "NativeName" : "EnglishName";
            comboBox.DataSource = languages.Clone();
            //this is for not losing selection
            if (selectedValue != null && comboBox.Items.Contains(selectedValue))
            {
                comboBox.SelectedItem = selectedValue;
            }
            comboBox.ResumeLayout(true);

        }

       
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the CheckedChanged event of the chbDisplayNativeName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void chbDisplayNativeName_CheckedChanged(object sender, EventArgs e)
        {
            this.BindLanguagesComboBoxes(this._languages);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the Language conmboboxes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._isInitialized)
            {
                return;
            }
            ComboBox cmb = sender as ComboBox;
            if (cmb != null)
            {
                if (cmb.Name == this.cmbLanguageA.Name)
                {
                    this._userSettings.LaguageFrom = this.cmbLanguageA.SelectedItem as Language;
                }
                else if (cmb.Name == this.cmbLanguageB.Name)
                {
                    this._userSettings.LaguageTo = this.cmbLanguageB.SelectedItem as Language;
                }
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbDisplayLocation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbDisplayLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._userSettings.TranslationLocation = (TranslationDisplayLocation)this.cmbDisplayLocation.SelectedItem;
        }

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chbIdentifyLanguageAutomaticaly control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chbIdentifyLanguageAutomaticaly_CheckedChanged(object sender, EventArgs e)
        {
            this._userSettings.AutoDetectLanguage = this.chbIdentifyLanguageAutomaticaly.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chbShowDebugMessages control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chbShowDebugMessages_CheckedChanged(object sender, EventArgs e)
        {
            this._userSettings.ShowDebugMessages = this.chbShowDebugMessages.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chbSearchForImages control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chbSearchForImages_CheckedChanged(object sender, EventArgs e)
        {
            this._userSettings.SearchForImages = this.chbSearchForImages.Checked;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbTranslationMofifier control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbTranslationMofifier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._isInitialized)
                return;
            this._userSettings.TranslationHotKey.Modifier = (ModifierKeys)this.cmbTranslationMofifier.SelectedValue;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbReverseTranslationMofifier control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbReverseTranslationMofifier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._isInitialized)
                return;
            this._userSettings.ReverseTranslationHotKey.Modifier = (ModifierKeys)this.cmbReverseTranslationMofifier.SelectedValue;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbTranslationHotKey control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbTranslationHotKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._isInitialized)
                return;
            this._userSettings.TranslationHotKey.Hotkey = (Keys)this.cmbTranslationHotKey.SelectedValue;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbReverseTranslationHotKey control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbReverseTranslationHotKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._isInitialized)
                return;
            this._userSettings.ReverseTranslationHotKey.Hotkey = (Keys)this.cmbReverseTranslationHotKey.SelectedValue;
        }
        #endregion

        
      
    }
}
