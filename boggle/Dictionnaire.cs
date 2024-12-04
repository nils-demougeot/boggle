using System;
using System.IO;
using System.Collections.Generic;

public class Dictionnaire
{
    private string[] mots;
    private string langue;
    private int longueurMotMax;
    //dictionnaires de dictionnaires
    public Dictionnaire(string langue)
    {
        this.langue = langue;

        string adresseDictionnaire = "docs/MotsPossiblesFR.txt";
        if (langue == "fr")
        {
            adresseDictionnaire = "docs/MotsPossiblesFR.txt";
        }
        else if (langue == "en")
        {
            adresseDictionnaire = "docs/MotsPossiblesEN.txt";
        }

        this.longueurMotMax = 30; // a automatiser

        string texte = File.ReadAllText(adresseDictionnaire);
        this.mots = new string[texte.Split(' ').Length - texte.Split("  ").Length + 1]; //pas fifou
        int precedentZero = 0;
        int compteur = 0;
        for (int i = 0; i < texte.Length; i++)
        {
            if (compteur == 0 && texte[i] == ' ')
            { //premier elem dictionnaire
                this.mots[0] = texte.Substring(0, i);
                precedentZero = i;
                compteur++;
            }
            else if (texte[i] == ' ' && precedentZero != i - 1)
            { //cas general
                this.mots[compteur] = texte.Substring(precedentZero + 1, i - precedentZero - 1);
                precedentZero = i;
                compteur++;
            }
            else if (i == texte.Length - 1)
            { //dernier elem dictionnaire
                this.mots[compteur] = texte.Substring(precedentZero + 1);
            }
        }

    }

    public string toString()
    {
        string chaineRetour = "";
        //Nombre de mots par longueur
        chaineRetour += "Nombre de mots ...\n";
        for (int taille = 1; taille <= longueurMotMax; taille++)
        {
            int nbMots = 0;
            for (int i = 0; i < mots.Length; i++)
            {
                if (this.mots[i] != null && this.mots[i].Length == taille) //changer si taille tab correcte
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



        //tableau de tableau plutot que dictionnaire
    


        
    }    
}
