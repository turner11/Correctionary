using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace CommonObjects
{

    /// <summary>
    /// a translation package
    /// </summary>
    public class Translation
    {
        #region Data members
        protected string _word;
        /// <summary>
        /// The word trnslated
        /// </summary>
        public string Word
        {
            get { return _word; }
            set { this._word = value; }
        }

        protected List<string> _transLations;
        /// <summary>
        /// The  translation
        /// </summary>
        public ICollection<string> Translations
        {
            get { return _transLations; }

        }

        bool _errorEncounterd;
        /// <summary>
        /// Gets a value indicating whether an error encounterd during translation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if an error encounterd; otherwise, <c>false</c>.
        /// </value>
        public bool ErrorEncounterd
        {
            get
            {
                return _errorEncounterd ||
                    this.ErrorException != null
                    || !String.IsNullOrEmpty(this.ErrorMessage);
            }
            internal set { _errorEncounterd = value; }
        }

        private string _errorMessage;
        /// <summary>
        /// Gets the message of error (if occured) while error encountered.
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                string retVal = _errorMessage;
                if (String.IsNullOrEmpty(retVal) && this._errorException != null)
                {
                    retVal = this.ErrorException.Message;
                }
                return retVal;
            }
            set { _errorMessage = value; }
        }

        Exception _errorException; 
        /// <summary>
        /// Exception that occured during attempt to translate
        /// </summary>
        ///  /// <summary>
        /// Gets the exception (if occured) while error encountered.
        /// </summary>
        public Exception ErrorException
        {
            get { return _errorException; }
            set
            {
                _errorException = value;
                this._errorEncounterd |= this._errorException != null;
            }

        }
        
        #endregion

        #region C'tors
        /// <summary>
        /// Initializes a new instance of the <see cref="Translation"/> class.
        /// </summary>
        public Translation()
            : this(String.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Translation"/> class.
        /// </summary>
        /// <param name="word">The word that the translation is for.</param>
        public Translation(string word)
            : this(word, null)
        {

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Translation"/> class.
        /// </summary>
        /// <param name="word">The word that the translation is for.</param>
        /// <param name="traslatedWords">The translations for the <see cref="word"/>.</param>
        public Translation(string word,ICollection<string> traslatedWords)
        {
            this._transLations = new List<string>();
            this._word = word;
            if (traslatedWords != null)
            {
                this._transLations.AddRange(traslatedWords);
            }
        }
        #endregion
       
        public void CopyBaseValues(Translation other)
        {
            this._word = other.Word;
            this.Translations.Clear();
            this._transLations.AddRange(other.Translations);
            this._errorEncounterd = other._errorEncounterd;
            this._errorMessage = other.ErrorMessage;
            this._errorException = other._errorException;
        }

    }
    /// <summary>
    ///  translation package when the translation had no specific context
    /// </summary>
    public class TranslationPackage : Translation
    {
       
        public TranslationPackage()
        {
            this._word = String.Empty;


        }
    }

    /// <summary>
    /// A translation package when the translation was in a specific context
    /// </summary>
    public class TranslationInContextPackage : TranslationPackage
    {
        string _bestMatch;
        public string BestMatch
        {
            get { return _bestMatch; }
            set { _bestMatch = value; }
        }

        List<string> _transLatedContext;
        public List<string> TransLatedSentenceWords
        {
            get { return _transLatedContext; }
        }

        public bool IsEmpty
        {
            get
            {
                return String.IsNullOrWhiteSpace(this.BestMatch)
                    && this._transLatedContext.Count == 0;
            }
        }

        Language _laguageFrom;
        /// <summary>
        /// Gets or sets the laguage from
        /// </summary>
        /// <value>
        /// The laguage from.
        /// </value>
        public Language LaguageFrom
        {
            get { return _laguageFrom; }
            set { _laguageFrom = value; }
        }

        Language _laguageTo;
        /// <summary>
        /// Gets or sets the laguage To
        /// </summary>
        /// <value>
        /// The laguage to.
        /// </value>
        public Language LaguageTo
        {
            get { return _laguageTo; }
            set { _laguageTo = value; }
        }

        public TranslationInContextPackage()
        {
            this._bestMatch = String.Empty;
            this._transLatedContext = new List<string>();
        }

       
    }

    /// <summary>
    /// The location to display translation
    /// </summary>
    [TypeConverter(typeof(EnumToStringUsingDescription))]
    public enum TranslationDisplayLocation
    {
        [Description("Default Location")]
        DefaultLocation = 0,
        [Description("Last Location")]
        LastLocation = 1,
        [Description("Cursor Location")]
        CursorLocation = 2
    }

    /// <summary>
    /// Actions that the system does
    /// </summary>
    public enum HotkeysActions
    {
        [Description("None")]
        None = 0,
        [Description("Obtain Context")]
        ObtainContext = 1,
        [Description("Translate Word")]
        TranslateWord = 2,
        [Description("Reverse Translate")]
        ReverseTranslate = 3,
        [Description("Translate Paragraph")]
        TranslateParagraph = 4
        
    }

    /// <summary>
    /// A package for passing Modifier + key
    /// </summary>
    [Serializable]
    public class HotkeyPackage
    {
        ModifierKeys _modifier;
        /// <summary>
        /// Gets or sets the modifier.
        /// </summary>
        /// <value>
        /// The modifier.
        /// </value>
        public ModifierKeys Modifier
        {
            get { return _modifier; }
            set { this._modifier = value;}
        }

        Keys _hotkey;
        /// <summary>
        /// Gets or sets the hotkey.
        /// </summary>
        /// <value>
        /// The hotkey.
        /// </value>
        public Keys Hotkey
        {
            get { return _hotkey; }
            set { _hotkey = value; }
        }

        public HotkeyPackage(ModifierKeys modifier, Keys hotKey)
        {
            this._modifier = modifier;
            this._hotkey = hotKey;
        }


        /// <summary>
        /// Prevents a default instance of the <see cref="HotkeyPackage"/> class from being created.
        /// This is for allowinf serialization (empty c'tor)
        /// </summary>
        private HotkeyPackage():this(ModifierKeys.None,Keys.None)
        {
            
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
         public override string ToString()
        {
            string str =String.Empty;
            if (this.Modifier != ModifierKeys.None)
            {
                str = this.Modifier + "+";
            }
            str += this.Hotkey;

            return str;
        }
    }

    /// <summary>
    /// represents a language
    /// </summary>
    [Serializable]
    [DebuggerDisplay("{ToString()}")]
    public class Language: IComparable
    {
        #region Data Members
        string _symbol;
        /// <summary>
        /// Gets the language symbol.
        /// </summary>
        public string Symbol
        {
            get { return _symbol; }
            set {this._symbol = value;}
        }
        
        string _nativeName;
        /// <summary>
        /// Gets the native name of the language.
        /// </summary>
        /// <value>
        /// The the native name of the language.
        /// </value>
        public string NativeName
        {
            get { return _nativeName; }
            set { this._nativeName = value; }
        }
        
        string _englishName;
        /// <summary>
        /// Gets the English name of the language.
        /// </summary>
        /// <value>
        /// The the English name of the language.
        /// </value>
        public string EnglishName
        {
            get { return _englishName; }
            set { this._englishName = value; }
        }

        bool _isRightToLeft;
        /// <summary>
        /// Gets or sets a value indicating whether this language is right to left.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this language is right to left; otherwise, <c>false</c>.
        /// </value>
        public bool IsRightToLeft
        {
            get { return _isRightToLeft; }
            set { _isRightToLeft = value; }
        }

        #endregion

        #region C'tors
        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="nativeName">Name of the native.</param>
        /// <param name="englishName">Name of the english.</param>
        public Language(string symbol, string nativeName, string englishName)
        {
            this._symbol = symbol;
            this._nativeName = nativeName;
            this._englishName = englishName;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Language"/> class from being created.
        /// </summary>
        public Language()
        {
            //this is for deserializing
        }
        #endregion

        #region Overrrides

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return String.Format("{0}, {1} ({2})", Symbol, EnglishName, NativeName);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            Language other = obj as Language;
            if (other == null)
            {
                return false;
            }
            return String.Equals(this.Symbol,other.Symbol,StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            Language other = obj as Language;
            if (other == null)
            {
                throw new ArgumentException("Cannot compare non Language object to Language.");
            }
            return this._englishName.CompareTo(other._englishName);
        }

     

        #endregion

    }

    /// <summary>
    /// The settings for a user
    /// </summary>
    [Serializable]
    public class UserSettings:ICloneable
    {
        #region Data members
        Language _laguageFrom;
        /// <summary>
        /// Gets or sets the laguage A (typically the laguage to translate FROM).
        /// </summary>
        /// <value>
        /// The laguage A.
        /// </value>
        public Language LaguageFrom
        {
            get { return _laguageFrom; }
            set { _laguageFrom = value; }
        }

        Language _laguageTo;
        /// <summary>
        /// Gets or sets the laguage B (typically the laguage to translate TO).
        /// </summary>
        /// <value>
        /// The laguage B.
        /// </value>
        public Language LaguageTo
        {
            get { return _laguageTo; }
            set { _laguageTo = value; }
        }

        bool _autoDetectLanguage;
        /// <summary>
        /// Gets or sets a value indicating whether attempt to auto detect language to translate from.
        /// </summary>
        /// <value>
        ///   <c>true</c> if should attempt to auto detect language to translate from.; otherwise, <c>false</c>.
        /// </value>
        public bool AutoDetectLanguage
        {
            get { return _autoDetectLanguage; }
            set { _autoDetectLanguage = value; }
        }

        TranslationDisplayLocation _translationLocation;
        /// <summary>
        /// Gets or sets the translation display location.
        /// </summary>
        /// <value>
        /// The translation location.
        /// </value>
        public TranslationDisplayLocation TranslationLocation
        {
            get { return _translationLocation; }
            set { _translationLocation = value; }
        }

        Point _lastTranslationDisplayLocation;
        /// <summary>
        /// Gets or sets the last location where the translation was displayed.
        /// </summary>
        /// <value>
        /// The last display location.
        /// </value>
        public Point LastTranslationDisplayLocation
        {
            get { return _lastTranslationDisplayLocation; }
            set { _lastTranslationDisplayLocation = value; }
        }

        bool _showDebugMessages;
        /// <summary>
        /// Gets or sets a value indicating whether should show debug messages to user.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show debug messages]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowDebugMessages
        {
            get { return _showDebugMessages; }
            set { _showDebugMessages = value; }
        }
        
        //assign default value so it will be assigned also when deserialized before it was ever assignedt
        HotkeyPackage _translationHotKey = new HotkeyPackage(ModifierKeys.Control, Keys.F6);
                                        
        /// <summary>
        /// Gets or sets the translation hot key.
        /// </summary>
        /// <value>
        /// The translation hot key.
        /// </value>
        public HotkeyPackage TranslationHotKey
        {
            get { return _translationHotKey; }
            set { _translationHotKey = value; }
        }
        
        //assign default value so it will be assigned also when deserialized before it was ever assignedt
        HotkeyPackage _reverseTranslationHotKey = new HotkeyPackage(ModifierKeys.Control, Keys.F7);
        /// <summary>
        /// Gets or sets the reverse translation hot key.
        /// </summary>
        /// <value>
        /// The reverse translation hot key.
        /// </value>       
        public HotkeyPackage ReverseTranslationHotKey
        {
            get { return _reverseTranslationHotKey; }
            set { _reverseTranslationHotKey = value; }
        } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSettings"/> class.
        /// </summary>
        public UserSettings()
        {
            
        }
        
        #region Public Methods
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            //using reflection to clone settings
            UserSettings clone = new UserSettings();

            PropertyInfo[] propertyInfos = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in propertyInfos)
            {
                propertyInfo.SetValue(clone, propertyInfo.GetValue(this, null), null);
            }
            return clone;
        }

       
        #endregion
    }

    /// <summary>
    /// A drop-in converter that returns the strings from 
    /// <see cref="System.ComponentModel.DescriptionAttribute"/>
    /// of items in an enumaration when they are converted to a string,
    /// like in ToString().
    /// </summary>
    public class EnumToStringUsingDescription : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType.Equals(typeof(Enum)));
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return (destinationType.Equals(typeof(String)));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType.Equals(typeof(String)))
            {
                string name = value.ToString();
                Type effectiveType = value.GetType();
                effectiveType = value.GetType();


                if (name != null)
                {
                    object[] attrs =
                        effectiveType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
                    return (attrs.Length > 0) ? ((DescriptionAttribute)attrs[0]).Description : name;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Coverts an Enums to string by it's description. falls back to ToString.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public string EnumToString(Enum value)
        {
            //getting the actual values
            List<Enum> values = EnumToStringUsingDescription.GetFlaggedValues(value);
            values.ToString();
            //Will hold results for each value
            List<string> results = new List<string>();
            //getting the representing strings
            foreach (Enum currValue in values)
            {
                string currresult = this.ConvertTo(null, null, currValue, typeof(String)).ToString(); ;
                results.Add(currresult);
            }

            return String.Join("\n", results);

        }

        /// <summary>
        /// All of the values of enumeration that are represented by specified value.
        /// If it is not a flag, the value will be the only value retured
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static List<Enum> GetFlaggedValues(Enum value)
        {
            //checking if this string is a flaged Enum
            Type enumType = value.GetType();
            object[] attributes = enumType.GetCustomAttributes(true);
            bool hasFlags = false;
            foreach (object currAttibute in attributes)
            {
                if (enumType.GetCustomAttributes(true)[0] is System.FlagsAttribute)
                {
                    hasFlags = true;
                    break;
                }
            }
            //If it is a flag, add all fllaged values
            List<Enum> values = new List<Enum>();
            if (hasFlags)
            {
                Array allValues = Enum.GetValues(enumType);
                foreach (Enum currValue in allValues)
                {
                    if (value.HasFlag(currValue))
                    {
                        values.Add(currValue);
                    }
                }



            }
            else//if not just add current value
            {
                values.Add(value);
            }
            return values;
        }

        public string GetEnumDescrition(object value)
        {
            return this.ConvertTo(null, null, value, typeof(String)).ToString();

        }

    }

}
