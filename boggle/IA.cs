using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

public class IA
{
    Plateau plateau;
    Dictionnaire dico;
    Jeu jeu;
    int difficulte;

    public IA(Plateau _plateau, Dictionnaire _dico, Jeu _jeu, int _difficulte)
    {
        this.plateau = _plateau;
        this.jeu = _jeu;
        this.dico = _dico;
        this.difficulte = _difficulte;
    }

    public void Jouer()
    {
        List<string> mots = new List<string>();
        TimeSpan dureeIA = TimeSpan.FromSeconds(60);
        DateTime debutIA = DateTime.Now;
        while (jeu.Minuteur(dureeIA, debutIA) > TimeSpan.Zero)
        {
            Random random = new Random();
            int r = random.Next(dico.Mots.Length);
            string mot = dico.Mots[r];
            mots.Add(mot);
            if (plateau.Test_Plateau(mot, this.jeu.Joueurs[1]) == "Mot valide")
            {
                this.jeu.UpdateScore(1, mot);
                

                Console.WriteLine();
                Console.ResetColor();

                Console.Write("C'est au tour de ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(jeu.Joueurs[1].Nom);
                Console.ResetColor();

                TimeSpan tempsRestant = jeu.Minuteur(dureeIA, debutIA);
                Console.Write("Temps restant : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(tempsRestant.Minutes + ":" + tempsRestant.Seconds);
                Console.ResetColor();
                Console.Write("Score : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(jeu.Joueurs[1].Score + "\n");
                Console.ResetColor();
                Console.WriteLine(plateau.toString());

                Console.Write("-> ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                TimeSpan duree = TimeSpan.FromSeconds(15 / difficulte);
                DateTime debut = DateTime.Now;

                while (this.jeu.Minuteur(duree, debut) > TimeSpan.Zero) { }

                for (int i = 0; i < mot.Length; i++)
                {
                    TimeSpan dureeTyping = TimeSpan.FromSeconds(0.05 + random.NextDouble() * (0.4 - 0.05));
                    DateTime debutTyping = DateTime.Now;
                    Console.Write(mot[i]);
                    while (this.jeu.Minuteur(dureeTyping, debutTyping) > TimeSpan.Zero) { }
                }
                Console.WriteLine();
                Console.ResetColor();
                Console.WriteLine("Mot Valide");





            }
        }
            
    }
}