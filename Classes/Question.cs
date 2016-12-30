using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Classes
{
    [Serializable]
    public class Question
    {
        //ATTRIBUTS
        public string phrase { get; set; }
        public List<string> reponses { get; set; }
        public string bonneReponse { get; set; }

        //CONSTRUCTEURS
        public Question()
        { phrase = ""; bonneReponse = ""; reponses = new List<string>(); }
        public Question(string Phrase, string BonneReponse)
        { phrase = Phrase; bonneReponse = BonneReponse; }
        public Question(string Phrase, string BonneReponse, List<string> Reponses)
            : this(Phrase, BonneReponse)
        { reponses = Reponses; }


    }
}

