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
    public partial class Test2AttentionEtConcentration : Form
    {
        #region Variables Globales
        public List<string> typesFormesDeBase;
        public List<Color> couleursDeBase;
        public List<int> nbPointsDeBase;
        public List<Forme2D> formesSerie;

        public List<Regle> listeRegles;
        public Regle regleSerie;
        public bool seriesFaciles;

        public int numeroSerie;
        public int numeroQuestion;
        
        public int nbBonnesReponses;

        public int[] reponses;

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

        SolidBrush brosse;
        #endregion

        public Test2AttentionEtConcentration()
        {
            InitializeComponent();

            // reglage difficulte
            seriesFaciles = (Program.difficulte == "Mode normal") ? true : false;                       

            // chargement des regles une fois pour toute
            listeRegles = chargerRegles();

            // initialisation et lancement des tests
            numeroSerie = 0;
            nbBonnesReponses = 0;

            consigneP.Hide();
            questionP.Hide();
            finP.Hide();
            aideP.Show();
        }

        // Fonction pour charger les differentes regles possibles
        public List<Regle> chargerRegles()
        {
            Regle r1 = new Regle("couleurs", "points");
            Regle r2 = new Regle("points", "couleurs");
            Regle r3 = new Regle("couleurs", "formes");
            Regle r4 = new Regle("formes", "couleurs");
            Regle r5 = new Regle("points", "formes");
            Regle r6 = new Regle("formes", "points");

            return new List<Regle> { r1, r2, r3, r4, r5, r6 };
        }

        // Fonction pour initaliser chaque caracteristique des formes possibles
        public void initialiserFormesDeBases()
        {
            // chargement formes de bases une fois pour toute
            typesFormesDeBase = new List<string> { "carre", "rond", "triangle" };
            couleursDeBase = new List<Color> { Color.Yellow, Color.Red, Color.Green, Color.Blue };
            nbPointsDeBase = new List<int> { 0, 1, 2, 3, 4 };
        }

        #region Deroulement d'une serie
        #region Lancement d'une nouvelle serie
        public void nouvelleSerie()
        {
            numeroQuestion = 0;
            numSerieLB.Text = (++numeroSerie).ToString();

            consigneP.Show();

            definirRegleSerie();
            definirFormesSerie();
        }        

        public void definirRegleSerie()
        {
            if (!seriesFaciles || numeroSerie == 1)
            {
                regleSerie = listeRegles[GetRandomNumber(0, 6)];
            }

            consigneLB.Text = regleSerie.ToString();
        }

        public void definirFormesSerie()
        {
            formesSerie = new List<Forme2D>();
            string nouveauTypeForme = "";
            Color nouvelleCouleur = Color.Blue;
            int nouveauNbPoints = 0;

            reponses = new int[4];

            for (int i = 0; i < 5; i++)
            {
                initialiserFormesDeBases();

                nouveauTypeForme = typesFormesDeBase[GetRandomNumber(0, typesFormesDeBase.Count())];


                if (i > 0 && (nouveauTypeForme ==  formesSerie[i - 1].type))
                {
                    couleursDeBase.Remove(formesSerie[i - 1].couleur);
                    nbPointsDeBase.Remove(formesSerie[i - 1].nbPoints);
                    reponses[i - 1] = definirBonBouton("formes");
                }
                nouvelleCouleur = couleursDeBase[GetRandomNumber(0, couleursDeBase.Count())];

                if (i > 0 && (nouvelleCouleur.ToString() == formesSerie[i - 1].couleur.ToString()))
                {
                    nbPointsDeBase.Remove(formesSerie[i - 1].nbPoints);
                    reponses[i - 1] = definirBonBouton("couleurs");
                }
                nouveauNbPoints = nbPointsDeBase[GetRandomNumber(0, nbPointsDeBase.Count())];

                if (i > 0 && (nouveauNbPoints == formesSerie[i - 1].nbPoints))
                {
                    reponses[i - 1] = definirBonBouton("points");
                }

                if (i > 0 && reponses[i - 1] == 0) reponses[i - 1] = 3;

                Forme2D f = new Forme2D(nouveauTypeForme, nouveauNbPoints, "0", nouvelleCouleur);
                formesSerie.Add(f);
            }
        }

        public int definirBonBouton(string typeReponse)
        {
            if (typeReponse == regleSerie.regleBouton1) return 1;
            else if (typeReponse == regleSerie.regleBouton2) return 2;
            else return 3;
        }

        private void lancerTestBtn_Click(object sender, EventArgs e)
        {
            consigneP.Hide();
            questionP.Show();

            afficherQuestionSuivante();
        }
        #endregion

        #region Affichage des questions
        public void afficherQuestionSuivante()
        {
            numQuestionLB.Text = (++numeroQuestion).ToString();
            if (numeroQuestion > 1) scoreLB.Text = nbBonnesReponses.ToString() + "/" + ((numeroSerie - 1) * 4 + (numeroQuestion - 1)).ToString();
            formeP.Refresh();


            questionBtn1.Enabled = (numeroQuestion == 1) ? false : true;
            questionBtn2.Enabled = (numeroQuestion == 1) ? false : true;
            questionBtn3.Enabled = true;

            reinitialiserBoutons();
            typeReponseLB.Hide();

            if (!seriesFaciles) tempsReponseTimer.Start();
        }

        public void reinitialiserBoutons()
        {
            questionBtn1.BackColor = Color.LightBlue;
            questionBtn2.BackColor = Color.LightBlue;
            questionBtn3.BackColor = Color.LightBlue;
        }

        private void formeP_Paint(object sender, PaintEventArgs e)
        {
            Panel sp = sender as Panel;
            Graphics g = e.Graphics;

            Forme2D F = formesSerie[numeroQuestion - 1];
            F.x_position = 0;
            F.y_position = 0;

            brosse = new SolidBrush(F.couleur);

            Rectangle forme = new Rectangle(F.x_position, F.y_position, sp.Width, sp.Height);
            switch (formesSerie[numeroQuestion - 1].type)
            {
                case "carre":
                    g.FillRectangle(brosse, forme);
                    break;
                case "rond":
                    g.FillEllipse(brosse, forme);
                    break;
                case "triangle":
                    Point[] points = { new Point(sp.Width / 2, 0), new Point(sp.Width, sp.Height), new Point(0, sp.Height) };
                    g.FillPolygon(brosse, points);
                    break;
            }

            int taillePoints = 10;
            int espacementPoints = 15;
            brosse = new SolidBrush(Color.Black);
            switch (F.nbPoints)
            {
                case 1:
                    g.FillEllipse(brosse, new Rectangle(sp.Width / 2, sp.Height / 2, taillePoints, taillePoints));
                    break;
                case 2:
                    g.FillEllipse(brosse, new Rectangle(sp.Width / 2 - espacementPoints, sp.Height / 2, taillePoints, taillePoints));
                    g.FillEllipse(brosse, new Rectangle(sp.Width / 2 + espacementPoints, sp.Height / 2, taillePoints, taillePoints));
                    break;
                case 3:
                    g.FillEllipse(brosse, new Rectangle(sp.Width / 2, sp.Width / 2 - espacementPoints, taillePoints, taillePoints));
                    g.FillEllipse(brosse, new Rectangle(sp.Width / 2 + espacementPoints, sp.Height / 2 + espacementPoints, taillePoints, taillePoints));
                    g.FillEllipse(brosse, new Rectangle(sp.Width / 2 - espacementPoints, sp.Height / 2 + espacementPoints, taillePoints, taillePoints));
                    break;
                case 4:
                    g.FillEllipse(brosse, new Rectangle(sp.Width / 2 - espacementPoints, sp.Height / 2 - espacementPoints, taillePoints, taillePoints));
                    g.FillEllipse(brosse, new Rectangle(sp.Width / 2 + espacementPoints, sp.Height / 2 - espacementPoints, taillePoints, taillePoints));
                    g.FillEllipse(brosse, new Rectangle(sp.Width / 2 - espacementPoints, sp.Height / 2 + espacementPoints, taillePoints, taillePoints));
                    g.FillEllipse(brosse, new Rectangle(sp.Width / 2 + espacementPoints, sp.Height / 2 + espacementPoints, taillePoints, taillePoints));
                    break;
            }
        }

        private void tempsReponseTimer_Tick(object sender, EventArgs e)
        {
            verifierReponse(0);
        }
        #endregion

        #region Verification de la reponse
        public void verifierReponse(int numReponse)
        {
            if (!seriesFaciles) tempsReponseTimer.Stop();

            if (numeroQuestion > 1)
            {
                typeReponseLB.Show();

                switch (numReponse == reponses[numeroQuestion - 2])
                {
                    case true:
                        afficherBonneReponse(true, numReponse);
                        typeReponseLB.Text = "Réponse juste !";
                        typeReponseLB.ForeColor = Color.YellowGreen;
                        nbBonnesReponses++;
                        break;
                    case false:
                        afficherBonneReponse(true, reponses[numeroQuestion - 2]);
                        afficherBonneReponse(false, numReponse);                        
                        typeReponseLB.Text = "Réponse fausse !";
                        typeReponseLB.ForeColor = Color.Red;
                        break;
                }
            }

            questionBtn1.Enabled = false;
            questionBtn2.Enabled = false;
            questionBtn3.Enabled = false;

            tempsAffichageReponseTimer.Start();
        }

        private void questionBtn1_Click(object sender, EventArgs e)
        {
            verifierReponse(1);
        }

        private void questionBtn2_Click(object sender, EventArgs e)
        {
            verifierReponse(2);
        }

        private void questionBtn3_Click(object sender, EventArgs e)
        {
            verifierReponse(3);
        }

        public void afficherBonneReponse(bool reponseEstJuste, int numeroReponse)
        {
            Color couleurBouton = (reponseEstJuste) ? Color.YellowGreen : Color.Red;
            switch (numeroReponse)
            {
                case 1:
                    questionBtn1.BackColor = couleurBouton;
                    break;
                case 2:
                    questionBtn2.BackColor = couleurBouton;
                    break;
                case 3:
                    questionBtn3.BackColor = couleurBouton;
                    break;
            }
        }

        private void tempsAffichageReponseTimer_Tick(object sender, EventArgs e)
        {
            tempsAffichageReponseTimer.Stop();

            if (numeroQuestion < 5) afficherQuestionSuivante();
            else gererFinSerie();
        }
        #endregion

        #region Gestion fin d'une serie
        public void gererFinSerie()
        {
            questionBtn1.Enabled = false;
            questionBtn2.Enabled = false;
            questionBtn3.Enabled = false;

            prochaineSerieBtn.Visible = true;
            prochaineSerieBtn.Enabled = true;

            scoreLB.Text = nbBonnesReponses.ToString() + "/" + ((numeroSerie - 1) * 4 + (numeroQuestion - 1)).ToString();
        }

        private void prochaineSerieBtn_Click(object sender, EventArgs e)
        {
            questionP.Hide();

            if (numeroSerie < 3)
            {
                prochaineSerieBtn.Visible = false;
                prochaineSerieBtn.Enabled = false;

                nouvelleSerie();
            }
            else finP.Show();
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
            Program.U.resultatsParTests[1] = nbBonnesReponses * 100 / 12;
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

        private void Test2AttentionEtConcentration_FormClosing(object sender, FormClosingEventArgs e)
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
            nouvelleSerie();
        }
    }
}
