using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;


public class Dictionnaire
{
    private string[] mots;
    private string langue;
    public Dictionnaire(string langue)
    {
        this.langue = langue;

        string adresseDictionnaire = "../../../docs/MotsPossiblesFR.txt";
        if (langue == "fr")
        {
            adresseDictionnaire = "../../../docs/MotsPossiblesFR.txt";
        }
        else if (langue == "en")
        {
            adresseDictionnaire = "../../../docs/MotsPossiblesEN.txt";
        }

        StreamReader lecteur = new StreamReader(adresseDictionnaire);

        try
        {
            string fichier = lecteur.ReadToEnd();
            this.mots = fichier.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        } finally
        {
            lecteur.Close();
        }

        TriDictionnaire.TriRapideDico(this.mots, 0, this.mots.Length - 1);
    }

    #region Accès écriture/lecture des attributs
    public string[] Mots
    {
        get { return this.mots; }
    }
    #endregion

    #region Description Dictionnaire [toString()]
    /// <summary>
    /// Fonction qui décrit le dictionnaire courant : nombre de mots par longueur, nombre de mots par lettre et langue. 
    /// </summary>
    /// <returns>Retourne une chaine de caractère mise en forme décrivant le dictionnaire</returns>
    public string toString()
    {
        string chaineRetour = "";
        //Nombre de mots par longueur
        chaineRetour += "Nombre de mots ...\n";
        for (int taille = 1; taille < mots.Length;taille++)
        {
            int nbMots = 0;
            for (int i = 0; i < mots.Length; i++)
            {
                if (this.mots[i].Length == taille)
                {
                    nbMots++;
                }
            }
            if (nbMots > 0) { chaineRetour += "- De longueur " + taille + " : " + nbMots + "\n"; }
        }

        //Nombre de mots par lettre
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        chaineRetour += "\nNombre de mots ...\n";
        for (int lettre = 0; lettre < alphabet.Length; lettre++)
        {
            int nbMots = 0;
            for (int i = 0; i < mots.Length; i++)
            {
                if (this.mots[i] != null && char.ToUpper(this.mots[i][0]) == alphabet[lettre])
                {
                    nbMots++;
                }
            }
            if (nbMots > 0) { chaineRetour += "- Commençant par " + alphabet[lettre] + " : " + nbMots + "\n"; }
        }

        //Langue
        chaineRetour += "\nLangue : " + this.langue + "\n";

        return chaineRetour;
    }
    #endregion

    #region Recherche récursive dans dictionnaire [RechDichoRecursif()]
    /// <summary>
    /// Recherche de manière récusrive si un mot est présent ou non dans le dictionnaire courrant
    /// </summary>
    /// <param name="mot">Mot à rechercher [DOIT ETRE EN MAJUSCULE]</param>
    /// <param name="gauche">Indice gauche de la recherche dans le dictionnaire</param>
    /// <param name="droite">Indice droit de la recherche dans le dictionnaire</param>
    /// <returns>True si le mot est dans le dictionnaire, False sinon</returns>
    public bool RechDichoRecursif(string mot, int gauche, int droite)
    {

        if (gauche > droite)
        {
            return false;
        }

        int milieu = (gauche + droite) / 2;

        if (this.mots[milieu] == mot)
        {
            return true;
        }
        else if (string.Compare(mot, this.mots[milieu]) < 0)
        {
            return RechDichoRecursif(mot, gauche, milieu-1);
        }
        else
        {
            return RechDichoRecursif(mot, milieu + 1, droite);
        }
    }
    #endregion
}

