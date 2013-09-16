using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTranslationAPI
{
   [Serializable]
   public class LanguageDetector : JSONResponse
   {
      public LanguageDetectionResponseData responseData = new LanguageDetectionResponseData();
   }

   
}
