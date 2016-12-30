using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Classes
{
    public abstract class Forme
    {
        //ATTRIBUTS
        public int x_position { get; set; }
        public int y_position { get; set; }

        //CONSTRUCTEURS
        public Forme()
        { x_position = 200; y_position = 80; }
        public Forme(int X_position, int Y_position)
        { x_position = X_position; y_position = Y_position; }

        //METHODES
        public override string ToString()
        {
            return "forme";
        }
    }
}

