using boggle;
using System;
using System.Runtime.CompilerServices;

class Program
{
    public static void Main(string[] args)
    {

        /*string asciiArt =
" ___                 _      \n" +
"| . > ___  ___  ___ | | ___ \n" +
"| . \\/ . \\/ . |/ . || |/ ._>\n" +
"|___/\\___/\\_. |\\_. ||_|\\___.\n" +
"          <___'<___'        \n" +
" _          _ _  _  _       _     ___             \n" +
"| |_  _ _  | \\ |<_>| | ___ < >   |_ _| ___ ._ _ _ \n" +
"| . \\| | | |   || || |<_-< /.\\/   | | / . \\| ' ' |\n" +
"|___/`_. | | \\_||_||_|/__/ \\_/\\   |_| \\___/|_|_|_|\n" +
"     <___'                                         \n\n";*/

        string asciiArt =
"██████╗  ██████╗  ██████╗  ██████╗ ██╗     ███████╗\n" +
"██╔══██╗██╔═══██╗██╔════╝ ██╔════╝ ██║     ██╔════╝\n" +
"██████╔╝██║   ██║██║  ███╗██║  ███╗██║     █████╗\n" +
"██╔══██╗██║   ██║██║   ██║██║   ██║██║     ██╔══╝  \n" +
"██████╔╝╚██████╔╝╚██████╔╝╚██████╔╝███████╗███████╗ \n" +
"╚═════╝  ╚═════╝  ╚═════╝  ╚═════╝ ╚══════╝╚══════╝ \n" +
"                                                     \n" +
"███╗   ██╗██╗██╗     ███████╗    ██╗  ██╗    ████████╗ ██████╗ ███╗   ███╗\n" +
"████╗  ██║██║██║     ██╔════╝    ╚██╗██╔╝    ╚══██╔══╝██╔═══██╗████╗ ████║\n" +
"██╔██╗ ██║██║██║     ███████╗     ╚███╔╝        ██║   ██║   ██║██╔████╔██║\n" +
"██║╚██╗██║██║██║     ╚════██║     ██╔██╗        ██║   ██║   ██║██║╚██╔╝██║\n" +
"██║ ╚████║██║███████╗███████║    ██╔╝ ██╗       ██║   ╚██████╔╝██║ ╚═╝ ██║\n" +
"╚═╝  ╚═══╝╚═╝╚══════╝╚══════╝    ╚═╝  ╚═╝       ╚═╝    ╚═════╝ ╚═╝     ╚═╝\n";
                                                                          


        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(asciiArt);
        Console.ResetColor();
        Console.Write("Nombre de manche par joueur : ");
        Console.ForegroundColor = ConsoleColor.Yellow;

        int nbTours = 1;
        string entreeNbTours;
        bool estValideNbTours = false;
        while (!estValideNbTours)
        {
            entreeNbTours = Console.ReadLine();
            Console.ResetColor();
            if (entreeNbTours != null && int.TryParse(entreeNbTours, out nbTours) && nbTours >= 1)
            {
                estValideNbTours = true;
                nbTours = nbTours * 2;
            }
            else
            {
                Console.WriteLine("Entrée invalide. Veuillez réessayer.");
                Console.Write("Nombre de manche par joueur : ");
            }
        }

        bool vsIA = false;
        Jeu jeu = new Jeu(nbTours * 60);
        Joueur j1 = new Joueur(jeu.SaisirNom(1));
        Joueur j2 = new Joueur(jeu.SaisirNom(2));

        int difficulte = 1;
        if (j2.Nom.ToUpper() == "IA")
        {
            vsIA = true;
            Console.Write("Niveau de difficulté de l'IA [1, 2, 3] : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            string entreeDifficulte;
            bool estValideDifficulte = false;
            while (!estValideDifficulte)
            {
                entreeDifficulte = Console.ReadLine();
                Console.ResetColor();

                if (entreeDifficulte != null && int.TryParse(entreeDifficulte, out difficulte) && difficulte >= 1 && difficulte <= 3)
                {
                    estValideDifficulte = true;
                }
                else
                {
                    Console.WriteLine("Entrée invalide. Veuillez réessayer.");
                    Console.Write("Niveau de difficulté de l'IA [1, 2, 3] : ");
                }
            }
        }
        Joueur[] joueurs = new Joueur[2] { j1, j2 };
        jeu.Joueurs = joueurs;
        TimeSpan duree = TimeSpan.FromSeconds(nbTours * 60);
        DateTime debut = DateTime.Now;
        Random r = new Random();
        Console.Write("Taille du plateau : ");
        Console.ForegroundColor = ConsoleColor.Yellow;

        int taille = 6;
        string entreeTaille;
        bool estValideTaille = false;
        while (!estValideTaille)
        {
            entreeTaille = Console.ReadLine();
            Console.ResetColor();
            if (entreeTaille != null && int.TryParse(entreeTaille, out taille) && taille > 1)
            {
                estValideTaille = true;
            }
            else
            {
                Console.WriteLine("Entrée invalide. Veuillez réessayer.");
                Console.Write("Taille du plateau : ");
            }
        }

        
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
            if (i%2 == 0)
            {
                Console.WriteLine("\n\n||----------- TOUR " + Convert.ToInt32((i)/2+1) + " -----------||");
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\n||----------- À ");
            if (i % 2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else
            { 
                Console.ForegroundColor = ConsoleColor.Red; 
            }
            Console.Write(jeu.Joueurs[(i % 2)].Nom);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" de jouer -----------||");
            Console.ResetColor();

            jeu.Joueurs[(i % 2)].Mots = new List<string>();
            TimeSpan dureeManche = TimeSpan.FromSeconds(5);//modif
            DateTime debutManche = DateTime.Now;
            
            Plateau plateau = new Plateau(des, dico);
            if (vsIA && (i % 2) == 1)
            {
                IA ia = new IA(plateau, dico, jeu, difficulte);
                ia.Jouer();
                for (int j = 0; j < jeu.Joueurs[(i % 2)].Mots.Count; j++)
                {
                    motsTrouves.Add(jeu.Joueurs[(i % 2)].Mots[j]);
                }
                continue;
            }
            while (jeu.Minuteur(dureeManche, debutManche) > TimeSpan.Zero)
            {
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

                Console.Write("-> ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                string mot = Console.ReadLine().ToUpper().Trim();
                Console.ResetColor();
                string res = plateau.Test_Plateau(mot, jeu.Joueurs[(i % 2)]);
                
                
                if (res == "Mot valide")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(" "+res+" ");
                    Console.ResetColor();
                    Console.WriteLine();

                    jeu.UpdateScore((i % 2), mot);
                } else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(" " + res + " ");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
            for (int j = 0; j < jeu.Joueurs[(i % 2)].Mots.Count; j++)
            {
                motsTrouves.Add(jeu.Joueurs[(i % 2)].Mots[j]);
            }
            
        }

        Console.Clear();

        Console.WriteLine(jeu.Joueurs[0].toString());
        Console.WriteLine(jeu.Joueurs[1].toString());
        Console.WriteLine();
        int gagnant = Array.IndexOf(jeu.Joueurs, Math.Max(jeu.Joueurs[0].Score, jeu.Joueurs[1].Score));
        if (jeu.Joueurs[0].Score > jeu.Joueurs[1].Score)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(jeu.Joueurs[0].Nom);
            Console.ResetColor();

            Console.WriteLine(" a gagné !");
        }
        else if (jeu.Joueurs[0].Score < jeu.Joueurs[1].Score) 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(jeu.Joueurs[1].Nom);
            Console.ResetColor();

            Console.WriteLine(" a gagné !");
        }
        else
        {
            Console.WriteLine("Ex-aequo !");
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\nAppuyez sur ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("ENTREE");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" pour afficher le nuage de mots");
        Console.ResetColor();
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
        Console.ReadLine();


    }
}