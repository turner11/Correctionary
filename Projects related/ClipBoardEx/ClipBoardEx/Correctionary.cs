using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Correctionary
{
    class Correctionary
    {
        private Translator translator;
        private string word, sentence;
        string special_word;
        List<String> all_word_translations;


     
        public Correctionary(String m_word, String m_sentence)
        {

            word = m_word;
            sentence = m_sentence;
            translator = new Translator(word, sentence);
            all_word_translations = translator.get_all_word_translations();
            special_word = translator.Cross();

            //to print list
            String strResult = String.Empty;
            foreach (String str in All_word_translations)
            {
               
                if (!str.Equals(Special_word))
                     strResult += str + "\n ";
      
            }

            Result res = new Result(Special_word, strResult);
            res.Show();
            
        }
        public string Special_word
        {
            get { return special_word; }
            set { special_word = value; }
        }
        public List<String> All_word_translations
        {
            get { return all_word_translations; }
            set { All_word_translations = value; }
        }
    }


}
