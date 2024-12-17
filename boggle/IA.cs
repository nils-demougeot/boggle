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
        TimeSpan dureeIA = TimeSpan.FromSeconds(60);
        DateTime debutIA = DateTime.Now;
        while (jeu.Minuteur(dureeIA, debutIA) > TimeSpan.Zero)
        {
            Random random = new Random();
            int r = random.Next(dico.Mots.Length);
            string mot = dico.Mots[r];
            if (plateau.Test_Plateau(mot, this.jeu.Joueurs[1]) == "Mot valide")
            {
                this.jeu.UpdateScore(1, mot);
                TimeSpan duree = TimeSpan.FromSeconds(5 / difficulte);
                DateTime debut = DateTime.Now;







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
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(mot);
                Console.ResetColor();
                Console.WriteLine("Mot Valide");








                while (this.jeu.Minuteur(duree, debut) > TimeSpan.Zero) { }



            }
        }
            
    }
}