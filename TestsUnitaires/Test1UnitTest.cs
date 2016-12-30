using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterfacesGraphiques;
using System.Windows.Forms;

namespace TestsUnitaires
{
    /// <summary>
    /// Test unitaires sur le Test 1 (Perception et memorisation)
    /// </summary>
    [TestClass]
    public class Test1UnitTest
    {
        public Test1UnitTest()
        {
            F1 = new Test1PerceptionEtMemoireAssociative();
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
        public Test1PerceptionEtMemoireAssociative F1;
        #endregion

        [TestMethod]
        public void chargerFormes_nombre()
        {
            int valeur = 40;
            int resultat = F1.chargerFormes().Count();
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 40 (BDD de 40 formes)");
        }

        [TestMethod]
        public void consigne_texte()
        {
            string valeur = "Mémorisez les valeurs de tous les rectangles jaunes.";
            string resultat = F1.consigne("rj");
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 'Mémorisez les valeurs de tous les rectangles jaunes.'");
        }

        [TestMethod]
        public void definirLettreAsoociee_texte()
        {
            string valeur = "B";
            string resultat = F1.definirLettreAssociee(1);
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 'B'");
        }
    }
}
