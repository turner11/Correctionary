using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Correctionary
{
    class Translator
    {
        private Connector _word;
        private Connector _sentence;
        private List<String> _transWord;

        public Translator(String word, String sentence)
        {
            _word = new Connector(word);
            _sentence = new Connector(sentence);
            _transWord = _word.getTranslation();

        }

        public List<String> get_all_word_translations()
        { //function that return array of strings
            return (_transWord);
        }

        public String Cross()
        {
            String bestWord = "";
           
            String[] transSentence = (_sentence.getTranslation()[0]).Split(' '); //tanslate a sentence and put words's sentence in array
            foreach (string word in _transWord)
                foreach (string tran in transSentence)
                    if (String.Compare(word, tran) == 0)
                        bestWord = word;
            return (bestWord);
        }
    }
}