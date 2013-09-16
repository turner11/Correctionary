using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GoogleTranslationAPI
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.SetWindowPosition(0, 0);
         string s = string.Empty;
         string again = "y";
         const string key = "correctionary";

         while (again.ToLower().Equals("y"))
         {
            Console.Write("Enter something in English to translate into Hebrew > ");
            s = Console.ReadLine();

            GoogleLangaugeDetector detector =
               new GoogleLangaugeDetector(s, VERSION.ONE_POINT_ZERO, key);

            GoogleTranslator gTranslator = new GoogleTranslator(s, VERSION.ONE_POINT_ZERO,
               detector.LanguageDetected.Equals("iw") ? LANGUAGE.HEBREW : LANGUAGE.ENGLISH,
               detector.LanguageDetected.Equals("iw") ? LANGUAGE.ENGLISH : LANGUAGE.HEBREW,
               key);

            MessageBox.Show(gTranslator.Translation, "Google Translation of '" + s + "'", MessageBoxButtons.OK,
               MessageBoxIcon.Information);

            Console.Write("Again? [Y/N] > ");
            again = Console.ReadLine();
         }
           
      }
   }
}
