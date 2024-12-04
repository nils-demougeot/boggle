using System;
using System.Collections.Generic;

public class Joueur
{
    string nom;
    int score;
    List<string> mots;

    public Joueur(string _nom)
    {
        this.nom = _nom;
        this.score = 0;
        this.mots = new List<string>();
    }

    public string Nom
    {
        get { return this.nom; }
        set { this.nom = value; }
    }

    public int Score
    {
        get { return this.score; }
        set { this.score = value; }
    }

    public List<string> Mots
    {
        get { return this.mots; }
        set { this.mots = value; }
    }

    public bool Contain(string mot) //qui teste si le mot passé appartient déjà aux mots trouvés par le joueur pendant la partie
    {
        bool exist = false;
        for (int i = 0; i < this.mots.Count; i++)
        {
            if (mot == this.mots[i])
            {
                exist = true;
            }
        }
        return exist;
    }

    public void Add_Mot(string mot) //ajoute le mot dans la liste des mots déjà trouvés par le joueur au cours de la partie en modifiant le nombre d’occurrences si nécessaire
    {
        mots.Add(mot);
    }

    public string toString() //qui retourne une chaîne de caractères qui décrit un joueur.
    {
        return "Joueur " + nom + " : " + score + " points";
    }

}
