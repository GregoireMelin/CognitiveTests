using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Classes
{
    public class Calcul
    {
        //ATTRIBUT
        public string operateur { get; set; }
        public int termeGauche { get; set; }
        public int termeDroit { get; set; }

        //CONSTRUCTEURS
        public Calcul(string Operateur, int TermeGauche, int TermeDroit)
        { operateur = Operateur; termeGauche = TermeGauche; termeDroit = TermeDroit;}

        //METHODES
        public int resultatCalcul()
        {
            int resultat = 0;

            switch (operateur)
            {
                case "+":
                    resultat = termeGauche + termeDroit;
                    break;
                case "-":
                    resultat = termeGauche - termeDroit;
                    break;
                case "*":
                    resultat = termeGauche * termeDroit;
                    break;
                case "/":
                    resultat = termeGauche / termeDroit;
                    break;
            }

            return resultat;
        }

        public bool verificationReponseUtilisateur(int reponse)
        {
            return reponse == resultatCalcul();
        }
    }
}
