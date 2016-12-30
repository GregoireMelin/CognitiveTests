using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using Classes;

namespace InterfacesGraphiques
{
    static class Program
    {
        #region Variables Globales
        public static Utilisateur U = new Utilisateur();

        public static Form Menu;

        public static Form Test;
        public static int testEnCours;
        public static string difficulte;

        public static int[] tailleConnexion = new int[2] { 830, 450 };
        public static int[] tailleFenetre = new int[2] { 1024, 700 };

        public static bool applicationFermee = false;

        //Fonction pour avoir int random
        private static readonly Random nbRandom = new Random();
        private static readonly object syncLock = new object();
        public static int obtenirNombreAleatoire(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return nbRandom.Next(min, max);
            }
        }

        public static string cheminFichierUtilisateurs = "listeUtilisateurs.txt";
        #endregion

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Connexion());
        }

        public static void changerDeForme(Form actuel, Form nouveau)
        {
            actuel.Hide();
            nouveau.Show();

            if (actuel.WindowState == FormWindowState.Maximized) nouveau.WindowState = FormWindowState.Maximized;
            else
            {
                nouveau.Width = actuel.Width;
                nouveau.Height = actuel.Height;
                nouveau.Left = actuel.Left;
                nouveau.Top = actuel.Top;
                nouveau.WindowState = FormWindowState.Normal;
            }
        }

        public static void enregistrerResultat() // serialise un plateau de jeu
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Utilisateur));
            string nomPrenomUtilisateur = U.nom + "_" + U.prenom;
            string chemin = nomPrenomUtilisateur + "_resultatTestESA.xml";
            StreamWriter sw = new StreamWriter(chemin, false);
            serializer.Serialize(sw, U);
            sw.Close();

            StreamWriter fichier = new StreamWriter(cheminFichierUtilisateurs);
            fichier.WriteLine(nomPrenomUtilisateur);
            fichier.Close();
        }

        public static List<Question> chargerQuestions(string chemin)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Question>));
            StreamReader sr = new StreamReader(chemin);
            List<Question> LQ = (List<Question>)deserializer.Deserialize(sr);
            sr.Close();
            return LQ;
        }
    }
}
