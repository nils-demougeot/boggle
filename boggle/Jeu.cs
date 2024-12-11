using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class Jeu
{
    string[] joueurs;
    int[] scores;
    int minuteur;
    int manche;
    string path = "../../../docs/Lettres.txt";

    public Jeu(int _minuteur)
    {
        this.minuteur = _minuteur;
        this.manche = 60;
        this.joueurs = new string[2];
        this.scores = new int[2] { 0, 0 };
    }

    public string[] Joueurs
    {
        get { return this.joueurs; }
        set { this.joueurs = value; }
    }

    public int[] Scores
    {
        get { return this.scores; }
        set { this.scores = value; }
    }

    public string SaisirNom(int j)
    {
        Console.Write("Nom du joueur " + j + " : ");
        string saisie = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(saisie))
        {
            Console.Write("Saisie incorrecte, veuillez réessayer : ");
            saisie = Console.ReadLine();
        }
        return saisie;
    }

    public TimeSpan Minuteur(TimeSpan duree, DateTime debut)
    {
        TimeSpan tempsEcoule = DateTime.Now - debut;
        TimeSpan tempsRestant = duree - tempsEcoule;
        return tempsRestant;
    }

    public void UpdateScore(int j, string mot)
    {
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
                        this.scores[j] += int.Parse(parties[1]);
                    }
                }
            }
        }
    }
}