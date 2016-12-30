using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Classes;


namespace InterfacesGraphiques
{
    public partial class Test3CalculMental : Form
    {
        #region Variables Globales
        public List<Calcul> listeCalculs; 
        public string operateur;

        public int numeroQuestion;
        public int nbBonnesReponses;

        public bool testFacile;
        #endregion

        public Test3CalculMental()
        {
            InitializeComponent();

            numeroQuestion = 0;
            nbBonnesReponses = 0;

            testFacile = (Program.difficulte == "Mode normal") ? true : false;

            choixOperationP.Hide();
            calculP.Hide();
            finP.Hide();
            aideP.Show();
        }

        #region Deroulement d'un test
        #region Initialisation Operateurs et Calculs
        public void lancerTest(string typeCalcul)
        {
            choixOperationP.Hide();

            operateur = typeCalcul;

            initialiserCalculs();

            passerAuCalculSuivant();
        }

        public void initialiserCalculs()
        {
            listeCalculs = new List<Calcul>();
            int termeDeDroite = 0;
            int termeDeGauche = 0;

            for (int i = 0; i < 10; i++)
            {
                termeDeDroite = 0;
                termeDeGauche = 0;

                switch (operateur)
                {
                    case "+":
                        termeDeGauche = Program.obtenirNombreAleatoire(100, 1000);
                        termeDeDroite = Program.obtenirNombreAleatoire(100, 1000);
                        break;
                    case "-":
                        termeDeGauche = Program.obtenirNombreAleatoire(100, 1000);
                        do
                        {
                            termeDeDroite = Program.obtenirNombreAleatoire(10, 1000);
                        }
                        while (termeDeDroite > termeDeGauche);
                        break;
                    case "*":
                        termeDeGauche = Program.obtenirNombreAleatoire(2, 100);
                        termeDeDroite = Program.obtenirNombreAleatoire(2, 10);
                        break;
                    case "/":
                        termeDeGauche = Program.obtenirNombreAleatoire(10, 1000);
                        termeDeDroite = Program.obtenirNombreAleatoire(2, 10);
                        break;
                }

                listeCalculs.Add(new Calcul(operateur, termeDeGauche, termeDeDroite));
            }
        }

        private void lancerAdditionBtn_Click(object sender, EventArgs e)
        {
            lancerTest("+");
        }

        private void lancerSoustractionBtn_Click(object sender, EventArgs e)
        {
            lancerTest("-");
        }

        private void lancerMultiplicationBtn_Click(object sender, EventArgs e)
        {
            lancerTest("*");
        }

        private void lancerDivisionBtn_Click(object sender, EventArgs e)
        {
            titreCalculLB.Text = "Résolvez cette opération (résultat division entière) :";
            lancerTest("/");
        }


        #endregion

        #region Affichage Calcul
        public void passerAuCalculSuivant()
        {
            numQuestionLB.Text = (++numeroQuestion).ToString();
            scoreLB.Text = nbBonnesReponses.ToString() + "/" + numeroQuestion.ToString();

            calculP.Show();

            afficherCalculEnCours();

            if (!testFacile) tempsReponseTimer.Start();
        }

        public void afficherCalculEnCours()
        {
            Calcul calculEnCours = listeCalculs[numeroQuestion - 1];

            termeGaucheLB.Text = calculEnCours.termeGauche.ToString();
            termeDroitLB.Text = calculEnCours.termeDroit.ToString();
            operateurLB.Text = calculEnCours.operateur;
            termeGaucheLB.Refresh();
            termeDroitLB.Refresh();

            reponseNUD.Value = 0;

            typeReponseLB.Hide();

            validerCalculBtn.Enabled = true;
            calculSuivantBtn.Enabled = false;
            calculSuivantBtn.Visible = false;
        }


        private void tempsReponseTimer_Tick(object sender, EventArgs e)
        {
            validerCalculBtn.PerformClick();
        }


        #endregion

        #region Gestion fin calcul
        private void validerCalculBtn_Click(object sender, EventArgs e)
        {
            tempsReponseTimer.Stop();
            validerCalculBtn.Enabled = false;
            calculSuivantBtn.Enabled = true;
            calculSuivantBtn.Visible = true;
            typeReponseLB.Show();

            switch (listeCalculs[numeroQuestion - 1].verificationReponseUtilisateur((int)reponseNUD.Value))
            {
                case true:
                    typeReponseLB.Text = "Juste !";
                    typeReponseLB.ForeColor = Color.YellowGreen;
                    nbBonnesReponses++;
                    break;
                case false:
                    typeReponseLB.Text = "Faux : " + listeCalculs[numeroQuestion - 1].resultatCalcul();
                    typeReponseLB.ForeColor = Color.Red;
                    break;
            }
        }

        private void calculSuivantBtn_Click(object sender, EventArgs e)
        {
            if (numeroQuestion != 10)
            {
                calculSuivantBtn.Visible = false;
                calculSuivantBtn.Enabled = false;
                typeReponseLB.Visible = false;

                passerAuCalculSuivant();
            }

            else
            {
                calculP.Hide();
                finP.Show();
            }
        }
        #endregion
        #endregion

        #region Gestion Retour Menu
        private void menuLB_Click(object sender, EventArgs e)
        {
            retourMenu();
        }

        private void menuPB_Click(object sender, EventArgs e)
        {
            retourMenu();
        }

        private void retourMenuBtn_Click(object sender, EventArgs e)
        {
            Program.U.resultatsParTests[2] = nbBonnesReponses * 10;
            retourMenu();
        }

        private void retourMenu()
        {
            Program.changerDeForme(this, Program.Menu);
        }
        #endregion

        #region Gestion Quitter
        private void quitterLB_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void quitterPB_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Test3CalculMental_FormClosing(object sender, FormClosingEventArgs e)
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

        private void commencerTestBtn_Click(object sender, EventArgs e)
        {
            aideP.Hide();
            choixOperationP.Show();
        }
    }

}
