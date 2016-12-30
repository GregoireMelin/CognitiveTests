using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InterfacesGraphiques
{
    public partial class Menu : Form
    {
        #region Variables Globales
        public static string ongletCourant = "";
        #endregion

        public Menu()
        {
            InitializeComponent();

            this.Width = Program.tailleFenetre[0];
            this.Height = Program.tailleFenetre[1];

            titreResultatsLB.Text = "Résultats de : " + Program.U.prenom + " " + Program.U.nom;

            changementOnglet("menu");
        }

        #region Gestion Onglet

        private void changementOnglet(string nouvelOnglet)
        {
            // si l'onglet sélectionné n'est pas le même que le courant
            if (nouvelOnglet != ongletCourant)
            {
                // réglage des tailles
                int sideWidth = sidePB.Width;
                int nouvelOngletWidth = sideWidth + 10;

                // réglage de l'onglet courant (taille & pannel associé)
                switch (ongletCourant)
                {
                    case "menu":
                        menuPB.Width = sideWidth;
                        menuP.Hide();
                        break;
                    case "resultats":
                        resultatsPB.Width = sideWidth;
                        resultatsP.Hide();
                        break;
                    case "aide":
                        aidePB.Width = sideWidth;
                        aideP.Hide();
                        break;
                    default:
                        break;
                }

                switch (nouvelOnglet)
                {
                    case "menu":
                        menuPB.Width = nouvelOngletWidth;
                        menuP.Show();
                        choixDifficulteCB.SelectedIndex = 0;
                        choixTestCB.SelectedIndex = 0;
                        continuerVersTestBtn.Focus();
                        break;
                    case "resultats":
                        resultatsPB.Width = nouvelOngletWidth;
                        enregistrerResultatsBtn.Enabled = resultatAChaqueTest();
                        test1ResLB.Text = afficherResultatTest(1);
                        test2ResLB.Text = afficherResultatTest(2);
                        test3ResLB.Text = afficherResultatTest(3);
                        test4ResLB.Text = afficherResultatTest(4);
                        test5ResLB.Text = afficherResultatTest(5);
                        totalResLB.Text = afficherResultatTotal();


                        resultatsP.Show();
                        break;
                    case "aide":
                        aidePB.Width = nouvelOngletWidth;
                        aideP.Show();
                        break;
                }



                ongletCourant = nouvelOnglet;
            }
        }

        private void menuPB_Click(object sender, EventArgs e)
        {
            changementOnglet("menu");
        }

        private void menuLB_Click(object sender, EventArgs e)
        {
            changementOnglet("menu");
        }

        private void resultatsPB_Click(object sender, EventArgs e)
        {
            changementOnglet("resultats");
        }

        private void resultatsLB_Click(object sender, EventArgs e)
        {
            changementOnglet("resultats");
        }

        private void aidePB_Click(object sender, EventArgs e)
        {
            changementOnglet("aide");
        }

        private void aideLB_Click(object sender, EventArgs e)
        {
            changementOnglet("aide");
        }

        #endregion

        #region Gestion Test Choisi
        private void continuerVersTestBtn_Click(object sender, EventArgs e)
        {
            Program.testEnCours = choixTestCB.SelectedIndex;
            Program.difficulte = choixDifficulteCB.SelectedItem.ToString();

            Program.Test = new Test1PerceptionEtMemoireAssociative();

            switch (choixTestCB.SelectedIndex + 1)
            {
                case 2:
                    Program.Test = new Test2AttentionEtConcentration();
                    break;
                case 3:
                    Program.Test = new Test3CalculMental();
                    break;
                case 4:
                    Program.Test = new Test4ProblemesMathematiques();
                    break;
                case 5:
                    Program.Test = new Test5ProblemesPhysiques();
                    break;
                default:
                    break;
            }

            Program.changerDeForme(this, Program.Test);
        }
        #endregion

        #region Gestion Quitter
        private void quitterPB_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void quitterLB_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Program.applicationFermee)
            {
                if (MessageBox.Show("Souhaitez vous quitter le programme ?", "Message de confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Program.applicationFermee = true;
                    Application.Exit();
                }
                else e.Cancel = true;
            }
        }
        #endregion

        public int calculerResultatTotal()
        {
            int ptsTotaux = -1;
            int nbTestsEffectues = 0;


            foreach (int resultat in Program.U.resultatsParTests)
            {
                if (resultat != -1)
                {
                    ptsTotaux += resultat;
                    nbTestsEffectues++;
                }
            }

            if (nbTestsEffectues != 0) return ptsTotaux + 1;
            else return -1;
        }

        public string afficherResultatTotal()
        {
            double ptsTotaux = calculerResultatTotal();

            if (ptsTotaux != -1)
            {
                int nbTestsEffectues = 0;
                foreach (double resultat in Program.U.resultatsParTests) if (resultat != -1) nbTestsEffectues++;
                return (ptsTotaux / nbTestsEffectues) + "%";
            }

            else return "Aucune donnée";
        }

        public string afficherResultatTest(int numTest)
        {
            if (Program.U.resultatsParTests[numTest - 1] != -1) return Program.U.resultatsParTests[numTest - 1] + "%";
            else return "Aucune donnée";
        }

        public bool resultatAChaqueTest()
        {
            for (int i = 0; i < 5; i++) if (Program.U.resultatsParTests[i] == -1) return false;

            return true;
        }

        private void Menu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // correspond bouton entrer
            {
                if (menuP.Visible) continuerVersTestBtn.PerformClick();
            }
        }

        private void enregistrerResultatsBtn_Click(object sender, EventArgs e)
        {
            Program.U.resultatFinal = calculerResultatTotal();

            Program.enregistrerResultat();
        }
    }
}
