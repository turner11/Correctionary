using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CommonObjects;
using System.Reflection;

namespace TranslationUnit
{
    [TestFixture]
    class TranslationUnitTests
    {
        CorrectionaryUnit _translationUnit = new CorrectionaryUnit();
        readonly string _fairResponce = TranslationUnit.Properties.Resources.fairTranslation.Trim();

        Language[] _languages;

        [TestFixtureSetUp]
        public void Init()
        {
            this._translationUnit.SetLanguageAutoDetectionState(false);
            this._translationUnit.SetReverseLanguageState(false);
            // TODO: Move to logics and remove reference to forms and correctionary form
            this._languages = CorrectionaryUnit.GetLanguages();
        }


       
        [TestCase(TestName = "Testing parse of 'fair' reply")]
        public void ParseFairReply()
        {
            var gt = new GoogleTranslator();         
            var reply = this._fairResponce;
            var mi =gt.GetType().GetMethod("GetTranslationFromReply", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            var trans = mi.Invoke(gt, new object[] { reply,"fair" }) as Translation;

            var expected = "הוֹגֶן,בֵּינוֹנִי,בָּהִיר,יָפֶה,טוֹב,טוֹב לְמַדַי, נוֹחַ, כָּשֵׁר".Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim()).ToList();

            //bool areEqual = expected.All(e => trans.Translations.Contains(e));
            var diff1 = expected.Except(trans.Translations).ToArray();
            var diff2 = trans.Translations.Except(expected).ToArray();
            var totalDiff = diff1.Union(diff2).Distinct().ToArray();
            Assert.IsTrue(totalDiff.Length == 0, "Got translation difference:\n"+String.Join(",",totalDiff));
        }
    }
}
