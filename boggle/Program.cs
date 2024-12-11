using System;
using System.Runtime.CompilerServices;

class Program
{
    public static void Main(string[] args)
    {
        string asciiArt =
" ___                 _      \n" +
"| . > ___  ___  ___ | | ___ \n" +
"| . \\/ . \\/ . |/ . || |/ ._>\n" +
"|___/\\___/\\_. |\\_. ||_|\\___.\n" +
"          <___'<___'        \n" +
" _          _ _  _  _       _     ___             \n" +
"| |_  _ _  | \\ |<_>| | ___ < >   |_ _| ___ ._ _ _ \n" +
"| . \\| | | |   || || |<_-< /.\\/   | | / . \\| ' ' |\n" +
"|___/`_. | | \\_||_||_|/__/ \\_/\\   |_| \\___/|_|_|_|\n" +
"     <___'                                         \n\n";

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(asciiArt);
        Console.ResetColor();
        Console.Write("Nombre de manche par joueur : ");
        int dureeEnMin = Convert.ToInt32(Console.ReadLine()) * 2;
        Jeu jeu = new Jeu(dureeEnMin * 60);
        string[] joueurs = new string[2];
        joueurs[0] = jeu.SaisirNom(1);
        joueurs[1] = jeu.SaisirNom(2);
        jeu.Joueurs = joueurs;
        TimeSpan duree = TimeSpan.FromSeconds(dureeEnMin * 60);
        DateTime debut = DateTime.Now;
        Random r = new Random();
        Console.Write("Taille du plateau : ");
        int taille = Convert.ToInt32(Console.ReadLine());
        Console.Write("Langue (\"fr\" ou \"en\") : ");
        Dictionnaire dico = new Dictionnaire(Console.ReadLine());
        

        for (int i = 0; i < dureeEnMin; i++)
        {
            
            TimeSpan dureeManche = TimeSpan.FromSeconds(60);
            DateTime debutManche = DateTime.Now;
            De[,] des = new De[taille, taille];
            for (int x = 0; x < taille; x++)
            {
                for (int y = 0; y < taille; y++)
                {
                    des[x, y] = new De(r);
                }
            }
            Plateau plateau = new Plateau(des, dico);
            while (jeu.Minuteur(dureeManche, debutManche) > TimeSpan.Zero)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(asciiArt);
                Console.ResetColor(); Console.Write("C'et au tour de ");
                if (i % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                { Console.ForegroundColor = ConsoleColor.Red; }
                Console.WriteLine(jeu.Joueurs[(i % 2)]);
                Console.ResetColor();
                TimeSpan tempsRestant = jeu.Minuteur(dureeManche, debutManche);
                Console.WriteLine("Temps restant : " + tempsRestant.Minutes + ":" + tempsRestant.Seconds);
                Console.WriteLine("Score : " + jeu.Scores[i % 2] + "\n");
                Console.WriteLine(plateau.toString());
                string mot = plateau.Test_Plateau(Console.ReadLine());
                if (mot == "")
                {
                    Console.Write("Incorrect");
                }
                else
                {
                    Console.Write("Correct");
                }
                jeu.UpdateScore((i % 2), mot);
                Console.ReadLine();
            }
        }
        Console.Clear();
        Console.WriteLine("Score de " + joueurs[0] + " : " + jeu.Scores[0] + "\n");
        Console.WriteLine("Score de " + joueurs[1] + " : " + jeu.Scores[1] + "\n");
        int gagnant = Array.IndexOf(jeu.Scores, Math.Max(jeu.Scores[0], jeu.Scores[1]));
        Console.WriteLine(joueurs[gagnant] + " a gagné !");
        Console.ReadLine();
    }
}