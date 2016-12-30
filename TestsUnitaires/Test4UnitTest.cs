using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterfacesGraphiques;
using System.Windows.Forms;
using Classes;

namespace TestsUnitaires
{
    /// <summary>
    /// Test unitaires sur le Test 4 (Problemes mathematiques) et classe associee (BDDQuestions)
    /// </summary>
    [TestClass]
    public class Test4UnitTest
    {
        public Test4UnitTest()
        {
            F4 = new Test4ProblemesMathematiques();
        }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        public Test4ProblemesMathematiques F4;
        #endregion

        [TestMethod]
        public void choisirProchaineQuestion_Maths_nombre()
        {
            int valeur = F4.questionsMathematiques.Count();

            F4.choisirProchaineQuestion();

            int resultat = F4.questionsMathematiques.Count();
            Assert.IsTrue(valeur == resultat + 1, "La valeur doit être egale au resultat  + 1 (1 de moins pour resultat)");
        }

        [TestMethod]
        public void numeroBonneReponse_Maths_nombre()
        {
            F4.questionEnCours = new Question("question", "d", new List<string> { "a", "b", "c", "d" });

            int valeur = 4;
            int resultat = F4.numeroBonneReponse();
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 4 (numero bonne reponse = 4)");
        }
    }
}
