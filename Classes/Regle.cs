using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Classes
{
    public class Regle
    {
        public string regleBouton1 { get; set; }
        public string regleBouton2 { get; set; }

        public Regle(string RegleBouton1, string RegleBouton2)
        { regleBouton1 = RegleBouton1; regleBouton2 = RegleBouton2; }

        public override string ToString()
        {
            string finCouleur = "la même couleur.";
            string finForme = "la même forme.";
            string finPoints = "le même nombre de points.";

            string finBouton1 = "";
            switch (regleBouton1)
            {
                case "couleurs":
                    finBouton1 = finCouleur;
                    break;
                case "formes":
                    finBouton1 = finForme;
                    break;
                case "points":
                    finBouton1 = finPoints;
                    break;
            }

            string finBouton2 = "";
            switch (regleBouton2)
            {
                case "couleurs":
                    finBouton2 = finCouleur;
                    break;
                case "formes":
                    finBouton2 = finForme;
                    break;
                case "points":
                    finBouton2 = finPoints;
                    break;
            }


            string consigne = "Appuyez sur le bouton 1 si deux objets consécutifs ont " + finBouton1;
            consigne += "\nAppuyez sur le bouton 2 si deux objets consécutifs ont " + finBouton2;
            consigne += "\nAppuyez sur le bouton 3 dans tous les autres cas.";

            return consigne;
        }

    }
}
