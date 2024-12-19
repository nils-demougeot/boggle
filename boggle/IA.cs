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
        List<string> motsValides = new List<string>();

        TimeSpan dureeProcess = TimeSpan.FromSeconds((int)(6/this.plateau.Taille) + 1);
        DateTime debutProcess = DateTime.Now;

        // Couleurs pour la "case" clignotante
        ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.DarkYellow };
        int colorIndex = 0;

        // Positionnement initial
        int bottomLine = Console.WindowHeight - 1; // Derni�re ligne visible
        int boxPositionX = 1; // Position horizontale de la case
        int textPositionX = 4; // Position horizontale du mot

        Random random = new Random();
        
        while (this.jeu.Minuteur(dureeProcess, debutProcess) > TimeSpan.Zero) 
        {
            int r = random.Next(dico.Mots.Length);
            string mot = dico.Mots[r];
            if (this.plateau.Test_Plateau(mot, this.jeu.Joueurs[1]) == "Mot valide")
            {
                motsValides.Add(mot);
            }
            else { mots.Add(mot); }
        }

        TimeSpan dureeIA = TimeSpan.FromSeconds(60);
        DateTime debutIA = DateTime.Now;
        int index = 0;
        while (jeu.Minuteur(dureeIA, debutIA) > TimeSpan.Zero)
        {
            
            TimeSpan dureeRecherche = TimeSpan.FromSeconds((int)(20/difficulte));
            DateTime debutRecherche = DateTime.Now;

            
            
            while (this.jeu.Minuteur(dureeRecherche, debutRecherche) > TimeSpan.Zero) 
            { 
                Console.SetCursorPosition(0, bottomLine); // Va tout en bas de la console
                Console.Write(new string(' ', Console.WindowWidth)); // Efface la ligne
                

                // Affiche la case clignotante
                Console.SetCursorPosition(boxPositionX, bottomLine);
                Console.BackgroundColor = colors[colorIndex]; // Change la couleur de fond
                Console.Write("  "); // Dessine un espace coloré
                Console.ResetColor();

                // Affiche le mot à droite
                Console.SetCursorPosition(textPositionX, bottomLine);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("L'IA teste les mots... ");
                Console.ForegroundColor = colors[colorIndex];
                
                Console.Write(mots[random.Next(mots.Count)]);
                Console.ResetColor();
                // Mise à jour des indices
                colorIndex = (colorIndex + 1) % colors.Length; // Cycle des couleurs

                Random afficherCeMot = new Random();
                Thread.Sleep(100);
            }

            Console.SetCursorPosition(0, bottomLine); // Va tout en bas de la console
            Console.Write(new string(' ', Console.WindowWidth));
            string mot = motsValides[index];
            this.jeu.UpdateScore(1, mot);
            

            Console.WriteLine();

            Console.Write("C'est au tour de ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(jeu.Joueurs[1].Nom);
            Console.ResetColor();

            TimeSpan tempsRestant = jeu.Minuteur(dureeIA, debutIA);
            Console.Write("Temps restant : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(tempsRestant.Minutes + ":" + tempsRestant.Seconds + "\t");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(jeu.BarreProgression(tempsRestant, dureeIA));
            Console.ResetColor();
            Console.Write("Score : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(jeu.Joueurs[1].Score + "\n");
            Console.ResetColor();
            Console.WriteLine(plateau.toString());


            Console.Write("-> ");
            Console.ForegroundColor = ConsoleColor.Yellow;

            Thread.Sleep(500);
            for (int i = 0; i < mot.Length; i++)
            {
                TimeSpan dureeTyping = TimeSpan.FromSeconds(0.05 + random.NextDouble() * (0.4 - 0.05));
                DateTime debutTyping = DateTime.Now;
                Console.Write(mot[i]);
                while (this.jeu.Minuteur(dureeTyping, debutTyping) > TimeSpan.Zero) { }
            }
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("Mot Valide\n\n");
            index++;




            
        }
            
    }
}