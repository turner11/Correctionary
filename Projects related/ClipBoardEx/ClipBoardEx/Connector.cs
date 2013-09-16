using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;

namespace Correctionary
{

    class Connector
    {
        private String expression;
        private char[] hebrewLetters = new Char[] { 'א', 'ב', 'ג', 'ד', 'ה', 'ו', 'ז', 'ח', 'ט', 'י', 'כ', 'ל', 'מ', 'נ', 'ס', 'ע', 'פ', 'צ', 'ק', 'ר', 'ש', 'ת', 'ך', 'ף', 'ם', 'ן', 'ץ' };

        public Connector(String exp)
        {
            expression = exp;
        }
        public String Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        public List<String> getTranslation()
        {

            char[] hebrewLetters = new Char[] { 'א', 'ב', 'ג', 'ד', 'ה', 'ו', 'ז', 'ח', 'ט', 'י', 'כ', 'ל', 'מ', 'נ', 'ס', 'ע', 'פ', 'צ', 'ק', 'ר', 'ש', 'ת', 'ך', 'ף', 'ם', 'ן', 'ץ' };

            String languagePair = String.Empty;
            List<String> translation = new List<string>();
            //check which way to translate
            if (expression.IndexOfAny(hebrewLetters) == -1)
                languagePair = "en|he";
            else
                languagePair = "he|en";

            //get translation
            if (expression.IndexOf(" ") < 0 && languagePair.Equals("en|he"))
                translation = TranslateWord(expression, languagePair, System.Text.Encoding.GetEncoding(1255));
            else
                translation = TranslateSentence(expression, languagePair, System.Text.Encoding.GetEncoding(1255));


            //to print list
            String strResult = String.Empty;
            if (translation != null && !translation.Equals(""))
            {
                foreach (String str in translation)
                {
                    strResult += str + ", ";
                }
                strResult = strResult.Substring(0, strResult.Length - 2);
            }
            return (translation);
        }


        private List<string> TranslateSentence(string input, string languagePair, Encoding encoding)
        {
            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);

            string result = String.Empty;

            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = encoding;
                result = webClient.DownloadString(url);
            }

            result = result.Substring(result.IndexOf("id=result_box") + 33, 500);
            List<String> list = new List<String>();
            // get a legal regular expression
            result = result.Substring(0, result.IndexOf("</div"));
            result = result.Substring(0, result.Length - 7);

            //get the translation
            result = result.Substring(result.IndexOf(">") + 1, result.Length - result.IndexOf(">") - 1);
            result = result.Substring(0, result.IndexOf("<"));

            list.Add(result);
            return list;
        }

        private List<String> TranslateWord(string input, string languagePair, Encoding encoding)
        {

            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);

            string result = String.Empty;

            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = encoding;
                result = webClient.DownloadString(url);
            }


            result = result.Substring(result.IndexOf("<table><tr><td><b>") + 18, 500);

            // get a legal regular expression
            try
            {
                if (result.IndexOf("</tr></table>") > 0)
                    result = result.Substring(0, result.IndexOf("</tr></table>"));

                else
                    return (TranslateSentence(input, languagePair, encoding));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                //MessageBox.Show("Must enter word/sentence to translate");
                return (null);

            }



            List<String> list = new List<String>();
            string temp = String.Empty;
            do
            {
                temp = result.Substring(result.IndexOf("<li>") + 4, result.Length - result.IndexOf("<li>") - 4);
                temp = temp.Substring(0, temp.IndexOf("</li>"));
                list.Add(temp);
                result = result.Substring(result.IndexOf("</li>") + 5, result.Length - result.IndexOf("</li>") - 5);

            } while (result.IndexOf("<li>") >= 0);


            for (int i = 0; i < list.Count; i++)
            {
                string word = list[i];
                

                List<char> charArr = new List<char>();
                List<char> charArrCopy = new List<char>();
                charArr.AddRange(word.ToCharArray());
                charArrCopy.AddRange(word.ToCharArray());
                charArrCopy.Reverse();
                int j = charArr.Count - 1;

                foreach (char currChar in charArrCopy)
                {
                    if ((int)currChar < 1488 || (int)currChar > 1514 || currChar == ' ')
                    {
                        if (currChar == ' ')
                        {
                            charArr[j] = '_';
                        }
                        else
                        charArr.RemoveAt(j);
                    }
                    j--;
                }
                word = new string(charArr.ToArray());

                list[i] = word;  
            } 
            return list;
        }
       
    }
}