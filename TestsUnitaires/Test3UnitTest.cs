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
    /// Test unitaires sur le Test 3 (Calcul Mental) et classe associee (Calcul)
    /// </summary>
    [TestClass]
    public class Test3UnitTest
    {
        public Test3UnitTest()
        {
            F3 = new Test3CalculMental();
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
        public Test3CalculMental F3;
        #endregion

        [TestMethod]
        public void initialiserCalculs_nombre()
        {
            F3.operateur = "+";
            F3.initialiserCalculs();

            int valeur = 10;
            int resultat = F3.listeCalculs.Count();
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 10 (10 calculs)");
        }

        [TestMethod]
        public void resultatCalcul_nombre()
        {
            Calcul C = new Calcul("+", 333, 444);
            int valeur = 777;
            int resultat = C.resultatCalcul();
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 777 (333+444)");

            C = new Calcul("-", 444, 333);
            valeur = 111;
            resultat = C.resultatCalcul();
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 111 (444-333)");

            C = new Calcul("*", 111, 4);
            valeur = 444;
            resultat = C.resultatCalcul();
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 444 (111*4)");

            C = new Calcul("/", 102, 5);
            valeur = 20;
            resultat = C.resultatCalcul();
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 20 (102/5)");

        }
    }
}
