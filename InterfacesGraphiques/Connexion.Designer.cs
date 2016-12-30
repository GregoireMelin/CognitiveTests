namespace InterfacesGraphiques
{
    partial class Connexion
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Connexion));
            this.topPB = new System.Windows.Forms.PictureBox();
            this.topLB = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.consigneLB = new System.Windows.Forms.Label();
            this.prenomLB = new System.Windows.Forms.Label();
            this.nomLB = new System.Windows.Forms.Label();
            this.prenomTB = new System.Windows.Forms.TextBox();
            this.nomTB = new System.Windows.Forms.TextBox();
            this.continuerBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.topPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // topPB
            // 
            this.topPB.BackColor = System.Drawing.Color.DarkBlue;
            this.topPB.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPB.Location = new System.Drawing.Point(0, 0);
            this.topPB.Name = "topPB";
            this.topPB.Size = new System.Drawing.Size(814, 60);
            this.topPB.TabIndex = 0;
            this.topPB.TabStop = false;
            // 
            // topLB
            // 
            this.topLB.AutoSize = true;
            this.topLB.BackColor = System.Drawing.Color.DarkBlue;
            this.topLB.Font = new System.Drawing.Font("Arial Rounded MT Bold", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topLB.ForeColor = System.Drawing.Color.White;
            this.topLB.Location = new System.Drawing.Point(38, 12);
            this.topLB.Name = "topLB";
            this.topLB.Size = new System.Drawing.Size(738, 37);
            this.topLB.TabIndex = 1;
            this.topLB.Text = "Bienvenue dans le programme de test de l\'ESA";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.DarkBlue;
            this.pictureBox2.Location = new System.Drawing.Point(190, 120);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(450, 230);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // consigneLB
            // 
            this.consigneLB.AutoSize = true;
            this.consigneLB.BackColor = System.Drawing.Color.DarkBlue;
            this.consigneLB.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consigneLB.ForeColor = System.Drawing.Color.White;
            this.consigneLB.Location = new System.Drawing.Point(234, 146);
            this.consigneLB.Name = "consigneLB";
            this.consigneLB.Size = new System.Drawing.Size(362, 22);
            this.consigneLB.TabIndex = 4;
            this.consigneLB.Text = "Veuillez remplir les champs ci-dessous";
            // 
            // prenomLB
            // 
            this.prenomLB.AutoSize = true;
            this.prenomLB.BackColor = System.Drawing.Color.DarkBlue;
            this.prenomLB.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prenomLB.ForeColor = System.Drawing.Color.White;
            this.prenomLB.Location = new System.Drawing.Point(234, 200);
            this.prenomLB.Name = "prenomLB";
            this.prenomLB.Size = new System.Drawing.Size(92, 22);
            this.prenomLB.TabIndex = 5;
            this.prenomLB.Text = "Prénom :";
            // 
            // nomLB
            // 
            this.nomLB.AutoSize = true;
            this.nomLB.BackColor = System.Drawing.Color.DarkBlue;
            this.nomLB.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nomLB.ForeColor = System.Drawing.Color.White;
            this.nomLB.Location = new System.Drawing.Point(263, 243);
            this.nomLB.Name = "nomLB";
            this.nomLB.Size = new System.Drawing.Size(63, 22);
            this.nomLB.TabIndex = 6;
            this.nomLB.Text = "Nom :";
            // 
            // prenomTB
            // 
            this.prenomTB.Location = new System.Drawing.Point(332, 204);
            this.prenomTB.MaxLength = 40;
            this.prenomTB.Name = "prenomTB";
            this.prenomTB.Size = new System.Drawing.Size(264, 20);
            this.prenomTB.TabIndex = 7;
            // 
            // nomTB
            // 
            this.nomTB.Location = new System.Drawing.Point(332, 247);
            this.nomTB.MaxLength = 40;
            this.nomTB.Name = "nomTB";
            this.nomTB.Size = new System.Drawing.Size(264, 20);
            this.nomTB.TabIndex = 8;
            // 
            // continuerBtn
            // 
            this.continuerBtn.BackColor = System.Drawing.Color.Blue;
            this.continuerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continuerBtn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continuerBtn.ForeColor = System.Drawing.Color.White;
            this.continuerBtn.Location = new System.Drawing.Point(345, 295);
            this.continuerBtn.Name = "continuerBtn";
            this.continuerBtn.Size = new System.Drawing.Size(140, 40);
            this.continuerBtn.TabIndex = 10;
            this.continuerBtn.Text = "Continuer";
            this.continuerBtn.UseVisualStyleBackColor = false;
            this.continuerBtn.Click += new System.EventHandler(this.continuerBtn_Click);
            // 
            // Connexion
            // 
            this.AcceptButton = this.continuerBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 412);
            this.Controls.Add(this.continuerBtn);
            this.Controls.Add(this.nomTB);
            this.Controls.Add(this.prenomTB);
            this.Controls.Add(this.nomLB);
            this.Controls.Add(this.prenomLB);
            this.Controls.Add(this.consigneLB);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.topLB);
            this.Controls.Add(this.topPB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(830, 450);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(830, 450);
            this.Name = "Connexion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connexion";
            ((System.ComponentModel.ISupportInitialize)(this.topPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox topPB;
        private System.Windows.Forms.Label topLB;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label consigneLB;
        private System.Windows.Forms.Label prenomLB;
        private System.Windows.Forms.Label nomLB;
        private System.Windows.Forms.TextBox prenomTB;
        private System.Windows.Forms.TextBox nomTB;
        private System.Windows.Forms.Button continuerBtn;
    }
}

