using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class Jeu
{
    Joueur[] joueurs;
    int minuteur;
    int manche;
    string path = "../../../docs/Lettres.txt";

    public Jeu(int _minuteur)
    {
        this.minuteur = _minuteur;
        this.manche = 60;
        this.joueurs = new Joueur[2];
    }

    public Joueur[] Joueurs
    {
        get { return this.joueurs; }
        set { this.joueurs = value; }
    }

    #region Saisie Nom
    /// <summary>
    /// Demande à l'utilisateur de saisir son nom de joueur dans la console et enregistre cette chaine de caractère
    /// </summary>
    /// <param name="j">Le numéro du joueur (1 ou 2)</param>
    /// <returns>Retourne le nom saisi dans la console par le joueur</returns>
    public string SaisirNom(int j)
    {
        Console.Write("Nom du joueur " + j);
        if (j == 1)
        {
            Console.Write(" : "); 
            Console.ForegroundColor = ConsoleColor.Blue;
        } else
        {
            Console.Write(" : ");
            Console.ForegroundColor = ConsoleColor.Red;
        }
        string saisie = Console.ReadLine();
        Console.ResetColor();

        while (string.IsNullOrWhiteSpace(saisie))
        {
            Console.Write("Saisie incorrecte, veuillez réessayer : ");
            if (j == 1)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            saisie = Console.ReadLine();
            Console.ResetColor();
        }
        return saisie;
    }
    #endregion

    #region Minuteur
    /// <summary>
    /// Lance un minuteur en fonction de la durée voulue et de l'heure de départ
    /// </summary>
    /// <param name="duree">Durée du minuteur</param>
    /// <param name="debut">Heure de début du minuteur</param>
    /// <returns>Retourne le temps restant, de type TimeSpan</returns>
    public TimeSpan Minuteur(TimeSpan duree, DateTime debut)
    {
        TimeSpan tempsEcoule = DateTime.Now - debut;
        TimeSpan tempsRestant = duree - tempsEcoule;
        return tempsRestant;
    }
    #endregion

    #region Mise à jour du score
    /// <summary>
    /// Met à jour le score du joueur en fonction du mot entré
    /// </summary>
    /// <param name="j">Numéro du joueur (0 étant le premier joueur et 1 le second)</param>
    /// <param name="mot">Mot entré par le joueur, précédemment vérifié</param>
    public void UpdateScore(int j, string mot)
    {
        mot = mot.ToUpper();
        string texte = File.ReadAllText(this.path);
        string[] lignes = texte.Split('\n');
        for (int i = 0; i < mot.Length; i++)
        {
            foreach (string ligne in lignes)
            {
                string[] parties = ligne.Split(';');
                if (parties.Length >= 3)
                {
                    if (parties[0][0] == mot[i])
                    {
                        this.joueurs[j].Score += int.Parse(parties[1]);
                    }
                }
            }
        }
    }
    #endregion
}