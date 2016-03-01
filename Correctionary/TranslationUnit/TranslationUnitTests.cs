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
    using Resources = TranslationUnit.Properties.Resources;

    [TestFixture]
    class TranslationUnitTests
    {
        //CorrectionaryUnit _translationUnit = new CorrectionaryUnit();
        //Language[] _languages;

        Dictionary<ExpressionReplyBundle, IList<string>> _knownTranslations;

        public TranslationUnitTests()
        {
            this._knownTranslations = new Dictionary<ExpressionReplyBundle, IList<string>>();
            Func<string, IList<string>> getExpectedResultsAsList = 
                (str)=> str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim()).ToList();

            this._knownTranslations[new ExpressionReplyBundle("fair", Resources.fairTranslation)] = 
                            getExpectedResultsAsList("הוֹגֶן,בֵּינוֹנִי,בָּהִיר,יָפֶה,טוֹב,טוֹב לְמַדַי, נוֹחַ, כָּשֵׁר");
            this._knownTranslations[new ExpressionReplyBundle("language", Resources.languageTranslation)] = 
                            getExpectedResultsAsList("הוֹגֶן,בֵּינוֹנִי,בָּהִיר,יָפֶה,טוֹב,טוֹב לְמַדַי, נוֹחַ, כָּשֵׁר");
        }


        [TestFixtureSetUp]
        public void Init()
        {
            //this._translationUnit.SetLanguageAutoDetectionState(false);
            //this._translationUnit.SetReverseLanguageState(false);
            // TODO: Move to logics and remove reference to forms and correctionary form
            //this._languages = CorrectionaryUnit.GetLanguages();
        }


       
        [TestCase(TestName = "Testing knwon replys parsing")]
        public void ParseFairReply()
        {
            var gt = new GoogleTranslator();
            foreach (var pair in this._knownTranslations)
            {
                var expression = pair.Key.Expression;
                var reply = pair.Key.Reply;
                var expected = pair.Value;

                var mi = gt.GetType().GetMethod("GetTranslationFromReply", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
                var trans = mi.Invoke(gt, new object[] { reply, expression }) as Translation;


                //bool areEqual = expected.All(e => trans.Translations.Contains(e));
                var diff1 = expected.Except(trans.Translations).ToArray();
                var diff2 = trans.Translations.Except(expected).ToArray();
                var totalDiff = diff1.Union(diff2).Distinct().ToArray();
                Assert.IsTrue(totalDiff.Length == 0, "Got translation difference:\n" + String.Join(",", totalDiff));
            }
        }


        class ExpressionReplyBundle
        {
            public readonly string Expression;
            public readonly string Reply;
            public ExpressionReplyBundle(string expression, string reply)
            {
                this.Expression = expression;
                this.Reply = reply;
            }
            public override int GetHashCode()
            {
                return this.Expression.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                var casted = obj as ExpressionReplyBundle;
                return casted != null && this.Expression == casted.Expression;
            }

            public override string ToString()
            {
                return this.Expression;
            }
        }

        
    }
}
