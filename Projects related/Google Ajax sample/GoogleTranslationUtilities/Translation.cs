using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTranslationAPI
{
   [Serializable]
   public class Translation: JSONResponse
   {
      public TranslationResponseData responseData = new TranslationResponseData();
   }

   
}
