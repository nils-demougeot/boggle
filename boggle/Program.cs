using boggle;
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
        Console.ForegroundColor = ConsoleColor.Yellow;
        int nbTours = Convert.ToInt32(Console.ReadLine()) * 2;
        Console.ResetColor();
        Jeu jeu = new Jeu(nbTours * 60);
        Joueur j1 = new Joueur(jeu.SaisirNom(1));
        Joueur j2 = new Joueur(jeu.SaisirNom(2));
        Joueur[] joueurs = new Joueur[2] { j1, j2 };
        jeu.Joueurs = joueurs;
        TimeSpan duree = TimeSpan.FromSeconds(nbTours * 60);
        DateTime debut = DateTime.Now;
        Random r = new Random();
        Console.Write("Taille du plateau : ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        int taille = Convert.ToInt32(Console.ReadLine());
        Console.ResetColor();
        Console.Write("Langue (\"fr\" ou \"en\") : ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Dictionnaire dico = new Dictionnaire(Console.ReadLine());
        Console.ResetColor();
        List<string> motsTrouves = new List<string>();
        De[,] des = new De[taille, taille];
        for (int x = 0; x < taille; x++)
        {
            for (int y = 0; y < taille; y++)
            {
                des[x, y] = new De(r);
            }
        }
        for (int i = 0; i < nbTours; i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\n||----------- À ");
            if (i % 2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else
            { Console.ForegroundColor = ConsoleColor.Red; }
            Console.Write(jeu.Joueurs[(i % 2)].Nom);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" de jouer -----------||\n");
            Console.ResetColor();

            jeu.Joueurs[(i % 2)].Mots = new List<string>();
            TimeSpan dureeManche = TimeSpan.FromSeconds(60);
            DateTime debutManche = DateTime.Now;
            
            Plateau plateau = new Plateau(des, dico);
            while (jeu.Minuteur(dureeManche, debutManche) > TimeSpan.Zero)
            {
                // Console.Clear();
                // Console.ForegroundColor = ConsoleColor.DarkGreen;
                // Console.WriteLine(asciiArt);
                // Console.ResetColor(); 
                Console.WriteLine();
                Console.ResetColor();

                Console.Write("C'est au tour de ");
                if (i % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                { 
                    Console.ForegroundColor = ConsoleColor.Red; 
                }
                Console.WriteLine(jeu.Joueurs[(i % 2)].Nom);
                Console.ResetColor();

                TimeSpan tempsRestant = jeu.Minuteur(dureeManche, debutManche);
                Console.Write("Temps restant : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(tempsRestant.Minutes + ":" + tempsRestant.Seconds);
                Console.ResetColor();
                Console.Write("Score : ");
                Console.ForegroundColor = ConsoleColor.Yellow; 
                Console.WriteLine(jeu.Joueurs[(i % 2)].Score + "\n");
                Console.ResetColor();
                Console.WriteLine(plateau.toString());
                Console.ForegroundColor = ConsoleColor.Magenta;
                string mot = Console.ReadLine().ToUpper();
                Console.ResetColor();
                string res = plateau.Test_Plateau(mot, jeu.Joueurs[(i % 2)]);
                Console.WriteLine(res);
                if (res == "Mot valide")
                {
                    jeu.UpdateScore((i % 2), mot);
                }
                Console.ReadLine();
            }
            for (int j = 0; j < jeu.Joueurs[(i % 2)].Mots.Count; j++)
            {
                motsTrouves.Add(jeu.Joueurs[(i % 2)].Mots[j]);
            }
            
        }

        Console.Clear();
        Console.WriteLine(jeu.Joueurs[0].toString());
        Console.WriteLine(jeu.Joueurs[1].toString());
        int gagnant = Array.IndexOf(jeu.Joueurs, Math.Max(jeu.Joueurs[0].Score, jeu.Joueurs[1].Score));
        if (jeu.Joueurs[0].Score > jeu.Joueurs[1].Score)
        {
            Console.WriteLine(jeu.Joueurs[0].Nom + " a gagné !");
        }
        else if (jeu.Joueurs[0].Score < jeu.Joueurs[1].Score) 
        {
            Console.WriteLine(jeu.Joueurs[1].Nom + " a gagné !");
        }
        else
        {
            Console.WriteLine("Ex-aequo !");
        }
        Console.ReadLine();



        Dictionary<string, int> motsTrouvesFrequence = new Dictionary<string, int>();
        foreach (var item in motsTrouves)
        {
            if (motsTrouvesFrequence.ContainsKey(item))
            {
                motsTrouvesFrequence[item]++;
            }
            else
            {
                motsTrouvesFrequence[item] = 1;
            }
        }

        NuageDeMots.Affichage(motsTrouvesFrequence);



    }
}