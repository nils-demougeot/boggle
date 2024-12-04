using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

public class Jeu
{
    string j1;
    string j2;
    int scoreJ1;
    int scoreJ2;
    int minuteur;
    int manche;
    
    public Jeu(int _minuteur)
    {
        this.scoreJ1 = 0;
        this.scoreJ2 = 0;
        this.minuteur = _minuteur;
        this.manche = 60;
    }

    public string J1
    {
        get { return this.j1; }
        set { this.j1 = value; }
    }

    public string J2
    {
        get { return this.j2; }
        set { this.j2 = value; }
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

    public void AffichageMinuteur(TimeSpan duree, DateTime debut)
    {
        while (true)
        {
            TimeSpan tempsRestant = this.Minuteur(duree, debut);
            Console.SetCursorPosition(0, 20);
            Console.WriteLine(tempsRestant.Minutes + ":" + tempsRestant.Seconds);
            if (tempsRestant <= TimeSpan.Zero)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Temps écoulé !");
                break;
            }

            Thread.Sleep(1000);
        }
    }
}