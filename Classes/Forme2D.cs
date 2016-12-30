using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace Classes
{
    public sealed class Forme2D : Forme
    {
        //ATTRIBUTS
        public string type { get; set; } // rectangle, cercle, carre, triangle
        public int nbPoints { get; set; }
        public string chiffre { get; set; }
        public Color couleur { get; set; }

        //CONSTRUCTEURS
        public Forme2D()
            : base()
        { type = "rectangle"; nbPoints = 0; chiffre = ""; couleur = Color.White; }
        public Forme2D(string Type, int NbPoints, string Chiffre, Color Couleur)
            : base()
        { type = Type; nbPoints = NbPoints; chiffre = Chiffre; couleur = Couleur; }
        public Forme2D(string Type, int NbPoints, string Chiffre, Color Couleur, int X_position, int Y_Position)
            : base(X_position, Y_Position)
        { type = Type; nbPoints = NbPoints; chiffre = Chiffre; couleur = Couleur; }

        //METHODES
        public override string ToString()
        {
            return "forme2D";
        }

        public Forme2D clone()
        {
            return new Forme2D(this.type, this.nbPoints, this.chiffre, this.couleur, this.x_position, this.y_position);
        }
    }
}

