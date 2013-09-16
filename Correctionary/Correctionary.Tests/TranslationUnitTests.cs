using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TranslationUnit;
using CommonObjects;
using System.Globalization;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Correctionary.Tests
{
    [TestFixture]
    //[AttributeUsageAttribute(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class TranslationUnitTests
    {
        CorrectionaryUnit _translationUnit;
        Language[] _languages;

        #region Initialization
        
        [TestFixtureSetUp]
        public void Init()
        {
            this._translationUnit = new CorrectionaryUnit();
            this._translationUnit.SetLanguageAutoDetectionState(false);
            this._translationUnit.SetReverseLanguageState(false);
            // TODO: Move to logics and remove reference to forms and correctionary form
            this._languages = CorrectionaryUnit.GetLanguages();
        } 
        #endregion

        #region Tests
        [TestCase("Dog", "כֶּלֶב", "en", "he", Description = "Testing translation from english to hebrew")]
        [TestCase("כלב", "dog","he", "en", Description = "Testing translation from hebrew to english")]
        public void HebrewToEnglishTest(string word, string expected, string fromSymbol, string toSymbol)
        {
            
            //Arrange
            Language from = this.GetLanguageBySymol(fromSymbol);
            Language to = this.GetLanguageBySymol(toSymbol);

            Assert.IsTrue(from != null && to != null, "Could not get languages for translation");

            this._translationUnit.SetLanguages(from, to);

            //act
            TranslationPackage pack = this._translationUnit.Translate(word);
            
            //assert
            bool hasTranslation = pack.Translations.Contains(expected,new TranslationComparer());
            string errorMessage = String.Format("Failed to translate '{0}'. expected '{1}' but got: '{2}'"
                                                , word
                                                , expected,
                                                String.Join(", ", pack.Translations));

            Assert.IsTrue(hasTranslation, errorMessage);
        } 
        #endregion

        #region Helper methods
        Language GetLanguageBySymol(string symbol)
        {
            Language ret = null;
            foreach (Language lang in this._languages)
            {
                if (String.Equals(lang.Symbol, symbol, StringComparison.OrdinalIgnoreCase))
                {
                    ret = lang;
                    break;
                }
            }

            return ret;
        } 
        #endregion

        #region Sub classes

        class TranslationComparer : IEqualityComparer<string>
        {

            #region IEqualityComparer<string> Members

            public bool Equals(string x, string y)
            {
                return (String.Compare(x, y, CultureInfo.InvariantCulture, CompareOptions.IgnoreSymbols) == 0);
            }

            public int GetHashCode(string obj)
            {
                throw new NotImplementedException();
            }

            #endregion
        } 
        #endregion
    }

   
}
