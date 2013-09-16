using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTranslationAPI
{
   [Serializable]
   public class LanguageDetectionResponseData
   {
      public string language;
      public string isReliable;
      public string confidence;
   }
}
