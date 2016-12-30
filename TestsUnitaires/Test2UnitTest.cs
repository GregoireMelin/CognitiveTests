using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using InterfacesGraphiques;
using System.Windows.Forms;
using Classes;

namespace TestsUnitaires
{
    /// <summary>
    /// Test unitaires sur le Test 2 (Attention et concentration)
    /// </summary>
    [TestClass]
    public class Test2UnitTest
    {
        public Test2UnitTest()
        {
            F2 = new Test2AttentionEtConcentration();
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
        public Test2AttentionEtConcentration F2;
        #endregion

        [TestMethod]
        public void chargerRegles_nombre()
        {
            int valeur = 6;
            int resultat = F2.chargerRegles().Count();
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 6");
        }

        [TestMethod]
        public void intialiserFormesDeBase_parametres()
        {
            F2.initialiserFormesDeBases();

            int valeur = 3;
            int resultat = F2.typesFormesDeBase.Count();
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 3 (nb types formes)");

            valeur = 4;
            resultat = F2.couleursDeBase.Count();
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 4 (nb couleurs");

            valeur = 5;
            resultat = F2.nbPointsDeBase.Count();
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 5 (nb points)");


        }

        [TestMethod]
        public void definirBonBouton_nombre()
        {
            F2.regleSerie = new Regle("points", "formes");

            int valeur = 1;
            int resultat = F2.definirBonBouton("points");
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 1 (bouton 1)");
        }

        [TestMethod]
        public void definirFormesSerie_nombre()
        {
            F2.definirFormesSerie();

            int valeur = 5;
            int resultat = F2.formesSerie.Count();
            Assert.AreEqual(valeur, resultat, "La valeur doit être égale à 5 (5 formes dans la série)");

            for(int i = 1; i < 5; i++)
            {
                resultat = 0;

                if (F2.formesSerie[i].type == F2.formesSerie[i - 1].type) resultat++;
                if (F2.formesSerie[i].couleur.ToString() == F2.formesSerie[i - 1].couleur.ToString()) resultat++;
                if (F2.formesSerie[i].nbPoints == F2.formesSerie[i - 1].nbPoints) resultat++;
                
                Assert.IsTrue(( resultat == 0 || resultat == 1), "La valeur doit être égale à 0 ou 1 (0 ou 1 seule simulitude entre les formes)");
            }
        }
    }
}
