using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonObjects;


namespace nsLogics
{
    public class TranslationEventArgs:EventArgs
    {
        TranslationInContextPackage translation;

        public TranslationInContextPackage Translation
        {
            get { return translation; }
            
        }
        public TranslationEventArgs(TranslationInContextPackage translation)
        {
            this.translation = translation;
        }
    }
}
