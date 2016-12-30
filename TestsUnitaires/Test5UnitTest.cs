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
    public class Test5UnitTest
    {
        public Test5UnitTest()
        {
            F5 = new Test5ProblemesPhysiques();
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
        public Test5ProblemesPhysiques F5;
        #endregion

        [TestMethod]
        public void choisirProchaineQuestion_Physique_nombre()
        {
            int valeur = F5.questionsPhysiques.Count();

            F5.choisirProchaineQuestion();

            int resultat = F5.questionsPhysiques.Count();
            Assert.IsTrue(valeur == resultat + 1, "La valeur doit être egale au resultat  + 1 (1 de moins pour resultat)");
        }

        [TestMethod]
        public void numeroBonneReponse_Physique_nombre()
        {
            F5.questionEnCours = new Question("question", "d", new List<string> { "a", "b", "c", "d" });

            int valeur = 4;
            int resultat = F5.numeroBonneReponse();
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 4 (numero bonne reponse = 4)");
        }
    }
}
