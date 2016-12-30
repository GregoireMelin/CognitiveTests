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
    public partial class Test4ProblemesMathematiques : Form
    {
        #region Variables Globales
        public int numeroQuestion;
        public int nbBonnesReponses;
        public List<Question> questionsMathematiques;
        public Question questionEnCours;

        //Fonction pour avoir int random
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }
        #endregion

        public Test4ProblemesMathematiques()
        {
            InitializeComponent();            

            // chargement des questions une fois pour toute
            questionsMathematiques = Program.chargerQuestions((Program.difficulte == "Mode normal") ? "questionsMathsFaciles.xml" : "questionsMathsDifficiles.xml");

            // initialisations et lancement des tests
            testP.Show();
            finP.Hide();

            numeroQuestion = 0;
            nbBonnesReponses = 0;

            testP.Hide();
            finP.Hide();
            aideP.Show();
        }

        #region Deroulement d'un test
        #region Lancement d'un nouveau test
        public void nouveauTest()
        {
            numQuestionLB.Text = (++numeroQuestion).ToString();
            scoreLB.Text = nbBonnesReponses.ToString() + "/" + (numeroQuestion - 1).ToString();
            choisirProchaineQuestion();
            afficherQuestion();
        }

        public void choisirProchaineQuestion()
        {
            int indexQuestion = GetRandomNumber(0, questionsMathematiques.Count());
            questionEnCours = questionsMathematiques[indexQuestion];
            questionsMathematiques.Remove(questionEnCours);
        }

        public void afficherQuestion()
        {
            questionLB.Text = questionEnCours.phrase;
            rep1Btn.Text = questionEnCours.reponses[0];
            rep2Btn.Text = questionEnCours.reponses[1];
            rep3Btn.Text = questionEnCours.reponses[2];
            rep4Btn.Text = questionEnCours.reponses[3];

            autoriserReponse(true);

            choixReponseLB.Focus();
        }
        #endregion

        #region Verifcation reponse
        public void verifierQuestion(int numReponse)
        {
            #region Afficher bonne reponse en vert
            switch (numeroBonneReponse())
            {
                case 1:
                    rep1Btn.BackColor = Color.YellowGreen;
                    break;
                case 2:
                    rep2Btn.BackColor = Color.YellowGreen;
                    break;
                case 3:
                    rep3Btn.BackColor = Color.YellowGreen;
                    break;
                case 4:
                    rep4Btn.BackColor = Color.YellowGreen;
                    break;
            }
            #endregion

            #region Si réponse juste
            if (questionEnCours.reponses[numReponse - 1] == questionEnCours.bonneReponse)
            {
                nbBonnesReponses++;
                evaluationReponseTB.Text = "Réponse Juste !";
                evaluationReponseTB.ForeColor = Color.YellowGreen;
            }
            #endregion

            #region Si réponse fausse
            else
            {
                evaluationReponseTB.Text = "Réponse Fausse !";
                evaluationReponseTB.ForeColor = Color.Red;

                switch (numReponse)
                {
                    case 1:
                        rep1Btn.BackColor = Color.Red;
                        break;
                    case 2:
                        rep2Btn.BackColor = Color.Red;
                        break;
                    case 3:
                        rep3Btn.BackColor = Color.Red;
                        break;
                    case 4:
                        rep4Btn.BackColor = Color.Red;
                        break;
                }
            }
            #endregion

            autoriserReponse(false);

            evaluationReponseTB.Show();
            questionSuivanteBtn.Show();
            questionSuivanteBtn.Focus();
        }

        public int numeroBonneReponse()
        {
            int numero = 0;

            for (int i = 0; i < 4; i++)
            {
                if (questionEnCours.reponses[i] == questionEnCours.bonneReponse)
                {
                    numero = i + 1;
                    break;
                }
            }

            return numero;
        }

        private void rep1Btn_Click(object sender, EventArgs e)
        {
            verifierQuestion(1);
        }

        private void rep2Btn_Click(object sender, EventArgs e)
        {
            verifierQuestion(2);
        }

        private void rep3Btn_Click(object sender, EventArgs e)
        {
            verifierQuestion(3);
        }

        private void rep4Btn_Click(object sender, EventArgs e)
        {
            verifierQuestion(4);
        }

        public void autoriserReponse(bool autoriser)
        {
            rep1Btn.Enabled = autoriser;
            rep2Btn.Enabled = autoriser;
            rep3Btn.Enabled = autoriser;
            rep4Btn.Enabled = autoriser;
        }
        #endregion

        #region Passage question suivante
        private void questionSuivante_Click(object sender, EventArgs e)
        {
            if (numeroQuestion == 10)
            {
                scoreLB.Text = nbBonnesReponses.ToString() + "/" + (numeroQuestion).ToString();
                testP.Hide();
                finP.Show();
                retourMenuBtn.Focus();
                Program.U.resultatsParTests[3] = nbBonnesReponses * 10;
            }
            else
            {
                rep1Btn.BackColor = Color.LightBlue;
                rep2Btn.BackColor = Color.LightBlue;
                rep3Btn.BackColor = Color.LightBlue;
                rep4Btn.BackColor = Color.LightBlue;

                evaluationReponseTB.Hide();
                questionSuivanteBtn.Hide();
                nouveauTest();
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

        private void Test4ProblemesMathematiques_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Gestion Entree Utilisateur
        //Donne à l'utilisateur la possibilité de valider son résultat avec la touche "Entrée"
        private void Test4ProblemesMathematiques_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // correspond bouton entrer
            {
                if (finP.Visible) retourMenuBtn.PerformClick();
                else
                {
                    if (testP.Visible && questionSuivanteBtn.Visible) questionSuivanteBtn.PerformClick();
                }
            }
        }
        #endregion

        private void commencerTestBtn_Click(object sender, EventArgs e)
        {
            aideP.Hide();
            testP.Show();
            nouveauTest();
        }
    }
}
