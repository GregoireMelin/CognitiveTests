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
    public partial class Test1PerceptionEtMemoireAssociative : Form
    {
        #region Variables Globales
        public List<Forme2D> formesDeBase;
        public List<string> formes;

        public int numeroQuestion;
        public int nbBonnesReponses;
        int nbFormesTotales;
        public string typeFormeTest;
        public Color couleurFormeTest;

        public string[,] reponses;
        public int[] nbTypeFormes;
        int nbFormesTest;

        SolidBrush brosse;
        #endregion

        public Test1PerceptionEtMemoireAssociative()
        {
            InitializeComponent();

            // reglage difficulte
            if (Program.difficulte == "Mode normal") testTimer.Interval = 4000;
            else testTimer.Interval = 2000;

            // chargement formes de bases une fois pour toute
            formesDeBase = chargerFormes();
            formes = new List<string> { "rb", "rj", "cb", "cj" };

            // initialisation et lancement des tests
            numeroQuestion = 0;
            nbBonnesReponses = 0;


            consigneP.Hide();
            testTLP.Hide();
            finP.Hide();
            aideP.Show();
        }

        // Fonction pour créer 40 formes différentes, selon [rectangle,cercle], [bleu, jaune], [0-9] (= 2*2*10 = 40)
        public List<Forme2D> chargerFormes()
        {
            List<Forme2D> formes = new List<Forme2D>();

            // 4 mêmes boucles ? => ordonner les formes par [type/couleur]
            for (int i = 0; i < 10; i++) formes.Add(new Forme2D("rectangle", 0, i.ToString(), Color.Blue)); // rectangles bleus
            for (int i = 0; i < 10; i++) formes.Add(new Forme2D("rectangle", 0, i.ToString(), Color.Yellow)); // rectangles jaunes
            for (int i = 0; i < 10; i++) formes.Add(new Forme2D("cercle", 0, i.ToString(), Color.Blue)); // cercles bleus
            for (int i = 0; i < 10; i++) formes.Add(new Forme2D("cercle", 0, i.ToString(), Color.Yellow)); // cercles jaunes

            return formes;
        }

        #region Deroulement d'un test
        #region Lancement d'un nouveau test
        public void nouveauTest()
        {
            numQuestionLB.Text = (++numeroQuestion).ToString();
            nbFormesTotales += nbFormesTest;
            scoreLB.Text = nbBonnesReponses.ToString() + "/" + nbFormesTotales.ToString();

            nbFormesTest = 0;

            reponsesP.Hide();
            testTLP.Hide();
            aideP.Hide();
            consigneP.Show();

            nbTypeFormes = new int[4]{0,0,0,0};
            reponses = new string[4, 2];
            string formeARetenir = formes[Program.obtenirNombreAleatoire(0, 4)];

            consigneLB.Text = consigne(formeARetenir);
            definirFormeTest(formeARetenir);
        }

        public string consigne(string elementARetenir)
        {
            string preConsigne = "Mémorisez les valeurs de tous les ";
            string postConsigne = "";

            switch (elementARetenir)
            {
                case "rj":
                    postConsigne = "rectangles jaunes.";
                    break;
                case "rb":
                    postConsigne = "rectangles bleus.";
                    break;
                case "cj":
                    postConsigne = "cercles jaunes.";
                    break;
                case "cb":
                    postConsigne = "cercles bleus.";
                    break;
            }

            return preConsigne + postConsigne;
        }

        public void definirFormeTest(string elementARetenir)
        {
            switch (elementARetenir[0])
            {
                case 'r':
                    typeFormeTest = "rectangle";
                    break;
                case 'c':
                    typeFormeTest = "cercle";
                    break;
            }

            switch (elementARetenir[1])
            {
                case 'b':
                    couleurFormeTest = Color.Blue;
                    break;
                case 'j':
                    couleurFormeTest = Color.Yellow;
                    break;
            }
        }

        private void lancerTestBtn_Click(object sender, EventArgs e)
        {
            reponsesP.Hide();
            consigneP.Hide();
            testTLP.Show();

            if (numeroQuestion == 1) remplirTLP();
            else testTLP.Refresh();

            testTimer.Start();
        }
        #endregion

        #region Affichage du tableau de formes
        private void remplirTLP()
        {
            for (int i = 1; i < testTLP.RowCount; i += 2)
            {
                for (int j = 0; j < testTLP.ColumnCount; j++)
                {
                    var control = testTLP.GetControlFromPosition(j, i);
                    testTLP.Controls.Remove(control);

                    Panel panelCell = new Panel();
                    panelCell.Dock = DockStyle.Fill;
                    testTLP.Controls.Add(panelCell, j, i);
                    panelCell.Paint += new PaintEventHandler(panel_Paint);
                }
            }
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Panel sp = sender as Panel;
            Graphics g = e.Graphics;

            int indexForme = 0;

            do { indexForme = Program.obtenirNombreAleatoire(0, 40); } while (nbTypeFormes[indexForme / 10] >= 3); // recherche d'une forme 
            nbTypeFormes[indexForme / 10]++; // actualisation du nb de ce type de forme dans listeAleatoire

            Forme2D F = formesDeBase[indexForme].clone();
            F.x_position = 0;
            F.y_position = 0;

            if (F.type == typeFormeTest && F.couleur.ToString() == couleurFormeTest.ToString())
            {
                reponses[nbFormesTest, 0] = definirLettreAssociee(sp.TabIndex - 12);
                reponses[nbFormesTest, 1] = F.chiffre;
                nbFormesTest++;
            }


            brosse = new SolidBrush(F.couleur);
            Rectangle forme = new Rectangle(F.x_position, F.y_position, sp.Width, sp.Height);
            if (F.type == "rectangle") g.FillRectangle(brosse, forme);
            else g.FillEllipse(brosse, forme);


            Font f = new Font("Arial", 12, FontStyle.Bold);
            RectangleF zone = new Rectangle(F.x_position, F.y_position, sp.Width, sp.Height);
            StringFormat format = new StringFormat();
            brosse = new SolidBrush(Color.Black);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            g.DrawString(F.chiffre.ToString(), f, brosse, zone, format);

        }

        public string definirLettreAssociee(int indexForme)
        {
            string lettreAssociee = "";

            switch (indexForme + 1)
            {
                case 1:
                    lettreAssociee = "A";
                    break;
                case 2:
                    lettreAssociee = "B";
                    break;
                case 3:
                    lettreAssociee = "C";
                    break;
                case 4:
                    lettreAssociee = "D";
                    break;
                case 5:
                    lettreAssociee = "E";
                    break;
                case 6:
                    lettreAssociee = "F";
                    break;
                case 7:
                    lettreAssociee = "G";
                    break;
                case 8:
                    lettreAssociee = "H";
                    break;
                case 9:
                    lettreAssociee = "I";
                    break;
                case 10:
                    lettreAssociee = "J";
                    break;
                case 11:
                    lettreAssociee = "K";
                    break;
                case 12:
                    lettreAssociee = "L";
                    break;
            }

            return lettreAssociee;
        }
        #endregion

        #region Mise en place du questionnaire de verification
        private void testTimer_Tick(object sender, EventArgs e)
        {
            testTimer.Stop();
            testTLP.Hide();
            consigneP.Hide();

            reponsesP.Show();
            creerReponses();
            toutValiderBtn.Enabled = true;
            toutValiderBtn.Focus();
        }

        private void toutValiderBtn_Click(object sender, EventArgs e)
        {
            #region Forme1
            if (compteur1NUD.Value.ToString() == reponses[0, 1])
            {
                typeReponse1LB.Text = "Juste";
                typeReponse1LB.ForeColor = Color.YellowGreen;
                nbBonnesReponses++;
            }
            else
            {
                typeReponse1LB.Text = "Faux : " + reponses[0, 1];
                typeReponse1LB.ForeColor = Color.Red;
            }

            typeReponse1LB.Show();
            #endregion

            #region Forme2
            if (compteur2NUD.Value.ToString() == reponses[1, 1])
            {
                typeReponse2LB.Text = "Juste";
                typeReponse2LB.ForeColor = Color.YellowGreen;
                nbBonnesReponses++;
            }
            else
            {
                typeReponse2LB.Text = "Faux : " + reponses[1, 1];
                typeReponse2LB.ForeColor = Color.Red;
            }

            typeReponse2LB.Show();
            #endregion

            #region Forme3
            if (compteur3NUD.Value.ToString() == reponses[2, 1])
            {
                typeReponse3LB.Text = "Juste";
                typeReponse3LB.ForeColor = Color.YellowGreen;
                nbBonnesReponses++;
            }
            else
            {
                typeReponse3LB.Text = "Faux : " + reponses[2, 1];
                typeReponse3LB.ForeColor = Color.Red;
            }
            typeReponse3LB.Show();
            #endregion

            #region Forme4
            if (nbFormesTest == 4)
            {
                if (compteur4NUD.Value.ToString() == reponses[3, 1])
                {
                    typeReponse4LB.Text = "Juste";
                    typeReponse4LB.ForeColor = Color.YellowGreen;
                    nbBonnesReponses++;
                }
                else
                {
                    typeReponse4LB.Text = "Faux : " + reponses[3, 1];
                    typeReponse4LB.ForeColor = Color.Red;
                }

                typeReponse4LB.Show();
            }
            #endregion

            toutValiderBtn.Enabled = false;

            testSuivantBtn.Enabled = true;
        }

        public void creerReponses()
        {
            lettreForme1LB.Text = reponses[0, 0];
            lettreForme2LB.Text = reponses[1, 0];
            lettreForme3LB.Text = reponses[2, 0];

            compteur1NUD.Value = 0;
            compteur2NUD.Value = 0;
            compteur3NUD.Value = 0;

            typeReponse1LB.Hide();
            typeReponse2LB.Hide();
            typeReponse3LB.Hide();

            if (nbFormesTest == 4)
            {
                lettreForme4LB.Text = reponses[3, 0];
                compteur4NUD.Value = 0;
                afficherReponseQuatre(true);
            }
            else afficherReponseQuatre(false);
        }

        public void afficherReponseQuatre(bool afficher)
        {
            lettreForme4LB.Visible = afficher;
            compteur4NUD.Visible = afficher;
            typeReponse4LB.Hide();
        }

        private void testSuivantBtn_Click(object sender, EventArgs e)
        {
            if (numeroQuestion != 10)
            {
                testSuivantBtn.Enabled = false;
                nouveauTest();
            }
            else
            {
                reponsesP.Hide();
                finP.Show();
                retourMenuBtn.Focus();
                nbFormesTotales += nbFormesTest;
                Program.U.resultatsParTests[0] = nbBonnesReponses * 100 / nbFormesTotales;
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

        private void Test1PerceptionEtMemoireAssociative_FormClosing(object sender, FormClosingEventArgs e)
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

        private void Test1PerceptionEtMemoireAssociative_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // correspond bouton entrer
            {
                if (reponsesP.Visible)
                {
                    if(toutValiderBtn.Enabled) toutValiderBtn.PerformClick();
                    else if (testSuivantBtn.Enabled) testSuivantBtn.PerformClick();
                }
                else if (consigneP.Visible) lancerTestBtn.PerformClick();
            }
        }
                  
        private void commencerTestBtn_Click(object sender, EventArgs e)
        {
            aideP.Hide();
            nouveauTest();
        }        
    }
}
