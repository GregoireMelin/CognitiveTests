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
    public partial class Connexion : Form
    {
        public Connexion()
        {
            InitializeComponent();
            this.Width = Program.tailleConnexion[0];
            this.Height = Program.tailleConnexion[1];
        }

        private void continuerBtn_Click(object sender, EventArgs e)
        {
            if ((nomTB.Text != "" && prenomTB.Text != "") || System.IO.File.Exists(nomTB.Text + "_" + prenomTB.Text + "_resultatTestESA.xml"))
            {
                Utilisateur.nbUtilisateur = nbUtilisateurs();

                Program.U.nom = nomTB.Text;
                Program.U.prenom = prenomTB.Text;


                Program.Menu = new Menu();                
                Program.Menu.Show();
                this.Hide();
            }

            else consigneLB.ForeColor = Color.Red;
        }

        private int nbUtilisateurs()
        {
            int nbUtilisateurs = 0;
            string line;
 
            System.IO.StreamReader file = new System.IO.StreamReader(Program.cheminFichierUtilisateurs);
            while ((line = file.ReadLine()) != null) nbUtilisateurs++;
            file.Close();

            return nbUtilisateurs;
        }
    }
}
