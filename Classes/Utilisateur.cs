using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Classes
{
    [Serializable]
    public class Utilisateur
    {
        public static int nbUtilisateur = 0;

        //ATTRIBUTS
        public string nom { get; set; }
        public string prenom { get; set; }
        public int id { get; set; }
        public int resultatFinal { get; set; }
        public int[] resultatsParTests { get; set; }

        //CONSTRUCTEURS
        public Utilisateur()
        { nom = "Anonymous"; prenom = "Guest"; id = nbUtilisateur++; resultatFinal = -1; resultatsParTests = new int[5] { -1, -1, -1, -1, -1 }; }
        public Utilisateur(string Nom, string Prenom)
        : this()
        { nom = Nom; prenom = Prenom; }
    }
}

