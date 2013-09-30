using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;


namespace CommonObjects
{
    public class ClipBoardDataArgs:EventArgs
    {
        ClipBoardDataObject _dataObject;
        /// <summary>
        /// Gets or sets the clip board data object.
        /// </summary>
        /// <value>
        /// The data object.
        /// </value>
        public ClipBoardDataObject DataObject
        {
            get { return _dataObject; }
            set { _dataObject = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClipBoardDataArgs"/> class.
        /// </summary>
        public ClipBoardDataArgs(ClipBoardDataObject dataObject)
        {
            this._dataObject = dataObject;
        }
    }

    public class HotkeyPressedArgs : EventArgs
    {
        ModifierKeys _modifier;
        /// <summary>
        /// Gets the modifier pressed.
        /// </summary>
        public ModifierKeys Modifier
        {
            get { return _modifier; }
        }
        Keys _key;
        /// <summary>
        /// Gets the key pressed.
        /// </summary>
        public Keys Key
        {
            get { return _key; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyPressedArgs"/> class.
        /// </summary>
        /// <param name="modifier">The modifier.</param>
        /// <param name="key">The key.</param>
        public HotkeyPressedArgs(ModifierKeys modifier, Keys key)
        {
            this._modifier = modifier;
            this._key = key;
        }
    }

    public class ErrorRegistratingHotKeyArgs: EventArgs
    {
        HotkeyPackage _hotKeyPackage;

        /// <summary>
        /// Gets or sets the hot key package.
        /// </summary>
        /// <value>
        /// The hot key package.
        /// </value>
        public HotkeyPackage HotKeyPackage
        {
            get { return _hotKeyPackage; }
        }
        int _errorCode;

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public int ErrorCode
        {
            get { return _errorCode; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRegistratingHotKeyArgs"/> class.
        /// </summary>
        /// <param name="hotKeyPackage">The hot key package.</param>
        /// <param name="errorCode">The error code.</param>
        public ErrorRegistratingHotKeyArgs(HotkeyPackage hotKeyPackage, int errorCode)
        {
            this._hotKeyPackage = hotKeyPackage;
            this._errorCode = errorCode;
        }
    }

    public class ClipBoardDataObject
    {
       
        IDataObject _rawData;
        /// <summary>
        /// Gets or sets the raw I data that was recived from clipboard.
        /// </summary>
        /// <value>
        /// The raw data.
        /// </value>
        public IDataObject RawData
        {
            get { return _rawData; }
            set { _rawData = value; }
        }

        string _format;


        /// <summary>
        /// Gets the format of the data in clipboard.
        /// </summary>
        public string Format
        {
            get { return _format; }
            
        }
        string _text;
        public string Text
        {
            get { return _text ?? String.Empty; }
        }
       
        string _rtf;
        public string Rtf
        {
            get { return _rtf ?? String.Empty; }
        }

        bool _isRtf;
        public bool IsRtf
        {
            get { return _isRtf ; }
        }

        bool _errorOccured;
        /// <summary>
        /// Gets a value indicating whether an error occured while getting data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if error occured; otherwise, <c>false</c>.
        /// </value>
        public bool ErrorOccured
        {
            get { return _errorOccured; }
        }

        string _errorMessage;
        /// <summary>
        /// Gets the error message (if occured).
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage ?? String.Empty; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClipBoardDataObject"/> class.
        /// </summary>
        /// <param name="dataObj">The data obj.</param>
        /// <param name="errorOccured">if set to <c>true</c> [error occured].</param>
        /// <param name="errorMessage">The error message.</param>
        public ClipBoardDataObject(IDataObject dataObj, bool errorOccured, string errorMessage)
        {
            this._errorOccured = errorOccured;
            this._errorMessage = errorMessage;
            this._rawData = dataObj;
            this.SetPrivateMembers();

        }

        /// <summary>
        /// Sets the private members according to the _rawData object.
        /// </summary>
        private void SetPrivateMembers()
        {
            if (this._rawData != null)
            {
                object iDataOjb;
                //This RichTextBox will help us to convert text to RTF and viceversa
                RichTextBox rtfBox = new RichTextBox();
                //TODO: Chaeck if has text is enough
                if (this._rawData.GetDataPresent(DataFormats.Rtf))
                {
                    this._format = DataFormats.Rtf.ToString();
                    iDataOjb = this._rawData.GetData(this._format, false);
                    if (iDataOjb == null)
                    {
                        this._errorOccured = true;
                        this._errorMessage = "Data object was null";
                    }

                   
                    this._isRtf = true;

                    if (iDataOjb != null)
                    {
                        this._rtf = (iDataOjb ?? String.Empty).ToString() + Environment.NewLine;
                    }
                   
                    //Getting the clean text
                    rtfBox.Rtf = this._rtf;
                    this._text = rtfBox.Text;
                }
                else
                {
                    this._isRtf = false;
                    //getting the format of data
                    string[] formats = this._rawData.GetFormats();
                    //Set text if possible. I am doing that just for having more ceritny
                    if (formats.Contains<string>(DataFormats.UnicodeText))
                    {
                        this._format = DataFormats.UnicodeText.ToString();
                    }
                    else
                    {
                        this._format = formats.Length > 0 ? formats[0] : String.Empty;
                    }
                    

                    iDataOjb = this._rawData.GetData(this._format, true);
                    if (iDataOjb == null)
                    {
                        this._errorOccured = true;
                        this._errorMessage = "Data object was null";
                    }
                    //getting the text

                    if (iDataOjb != null)
                    {
                        this._text = (iDataOjb ?? String.Empty).ToString() + Environment.NewLine;
                    }

                    //Getting the rtf format
                    rtfBox.Text = this.Text;
                    this._rtf = rtfBox.Rtf;
                }
            }
        }

       
    }
}
