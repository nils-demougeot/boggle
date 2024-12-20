using System;
using System.Collections.Generic;
using System.IO;


namespace boggle
{ 
    public class De
    {
        char[] lettres;
        char faceVisible;
        string path;
        Dictionnaire dico;

        public char FaceVisible
        {
            get { return this.faceVisible; }
            set { this.faceVisible = value; }
        }

        public De(Random r, Dictionnaire _dico, int taille)
        {
            this.dico = _dico;
            this.path = $"../../../docs/Lettres{ this.dico.Langue.ToUpper() }.txt";

            this.lettres = new char[6];
            string texte = File.ReadAllText(this.path);
            Dictionary<int, char> res = new Dictionary<int, char>();
            int somme = 0;
            string[] lignes = texte.Split('\n');
            foreach (string ligne in lignes)
            {
                string[] parties = ligne.Split(';');
                if (parties.Length >= 3)
                {
                    char lettre = parties[0][0];
                    somme += int.Parse(parties[^1]);
                    res.Add(somme, parties[0][0]);
                }
            }
            var list = new List<KeyValuePair<int, char>>(res);
            int[] nbLettres = new int[26];
            for (int  i = 0; i < nbLettres.Length; i++)
            {
                nbLettres[i] = 0;
            }
            for (int j = 0; j < 6; j++)
            {
                int index = r.Next(0, 100);
                for (int k = 0; k < res.Count; k++)
                {
                    if (index < list[k].Key && nbLettres[k] <= (int)(list[k].Value * taille))
                    {
                        this.lettres[j] = res[list[k].Key];
                        nbLettres[k]++;
                        break;
                    }
                }
            }
        }


        #region Lancer un Dé
        /// <summary>
        /// Sélectionne une face aléatoire du dé, c'est à dire, une lettre aléatoire dans le tableau de lettre que représente le dé
        /// </summary>
        /// <param name="r">Instance de la classe Random qui nous permettra d'avoir un nombre aléatoire entre 0 et la longueur du tableau/dé avec r.Next()</param> 
        public void LancerDe(Random r)
        {
            this.faceVisible = this.lettres[r.Next(0, this.lettres.Length)];
        }
        #endregion

        #region toString
        /// <summary>
        /// Fonction qui décrit le dé : toutes les lettres qu'il contient sur chaque "face" et sa face visible / sa lettre active
        /// </summary>
        /// <returns>Retourne une chaine de caractère décrivant le dé sous cette forme : Lettres : A, B, C, D, E, F Face visible : B</returns>
        public string toString()
        {
            string listeLettres = "";
            for (int i = 0; i < this.lettres.Length; i++)
            {
                listeLettres += this.lettres[i] + ", ";
            }
            return "Lettres : " + listeLettres + "\nFace visible : " + this.faceVisible;
        }
        #endregion
    }
}