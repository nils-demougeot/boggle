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

    #region Accès écriture/lecture des attributs
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
    #endregion

    #region Vérifier si le mot a déjà été trouvé
    /// <summary>
    /// Fonction vérifiant si le mot passé en paramètre n'a pas déjà été trouvé pendant la manche, 
    /// c-a-d s'il n'est pas deja présent dans la liste "Mots" du joueur
    /// </summary>
    /// <param name="mot">Mot à vérifier</param>
    /// <returns>Retourne True si le mot a déjà été trouvé, False sinon</returns>
    public bool Contains(string mot)
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
    #endregion

    #region Ajouter un mot aux mots déjà trouvés
    /// <summary>
    /// Ajoute le mot en paramètre à la liste de mots deja trouvés
    /// </summary>
    /// <param name="mot">Mot à ajouter</param>
    public void AddMot(string mot)
    {
        mots.Add(mot);
    }
    #endregion

    #region Description du joueur
    /// <summary>
    /// Fonction décrivant le joueur en fonction de son nom et de son score
    /// </summary>
    /// <returns>Retourne une chaine de caractère décrivant le joueur sous cette forme : "Michel : 50 points"</returns>
    public string toString()
    {
        return nom + " : " + score + " points";
    }
    #endregion

}
