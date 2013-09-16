using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using IvanAkcheurov.NClassify;
//using IvanAkcheurov.Commons;
//using IvanAkcheurov.NTextCat;
using IvanAkcheurov.NTextCat.Lib;
using IvanAkcheurov.NTextCat.Lib.Legacy;

namespace TranslationUnit
{
    /// <summary>
    /// A class for detecting a language of a string
    /// </summary>
    class LanguageDetector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageDetector"/> class.
        /// </summary>
        public LanguageDetector()
        {
            //CharLanguageIdentifier detector = new CharLanguageIdentifier();
            
          /*  RankedLanguageIdentifier detector= new RankedLanguageIdentifier(50,50,50,50,50,50);
            IEnumerable<Tuple<LanguageInfo, double>> result = detector.Identify("שלום");
            List<Tuple<LanguageInfo, double>> results = result.ToList().OrderByDescending(tup => tup.Item2).ToList();*/

            //IvanAkcheurov.NTextCat.Lib.NaiveBayesLanguageIdentifier languageIdentifier = new NaiveBayesLanguageIdentifier();
            //IvanAkcheurov.NTextCat.Lib.RankedLanguageIdentifier a = new RankedLanguageIdentifier();


            //IvanAkcheurov.NTextCat.Lib.RankedLanguageIdentifierFactory a = new RankedLanguageIdentifierFactory();
            
            //a.TrainModels();
            
            
            
            LanguageIdentifier languageIdentifier = new LanguageIdentifier();

            string str = "אני דובר עברית";
            List<Tuple<LanguageInfo, double>> languages = 
                languageIdentifier.ClassifyText(str,null).ToList();

            
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

            List<Tuple<LanguageInfo, double>> languagesB =
               languageIdentifier.ClassifyBytes(bytes, null, null).ToList();

            List<Tuple<LanguageInfo, double>> languagesC =
               languageIdentifier.ClassifyBytes(bytes, Encoding.ASCII, null).ToList();
            
            var mostCertainLanguage = languages.FirstOrDefault(); 
            if (mostCertainLanguage != null)  
                Console.WriteLine("Language of text is {0} with uncertainty {1}", mostCertainLanguage.Item1, mostCertainLanguage.Item2);  
            else 
                Console.WriteLine("Language couldn’t be identified with acceptable degree of certainty");

            


        }
    }
}
