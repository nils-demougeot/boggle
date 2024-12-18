using boggle;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;

class Program
{
    public static void Main(string[] args)
    {
        #region Bannière ASCII art
        string asciiArt =
"██████╗  ██████╗  ██████╗  ██████╗ ██╗     ███████╗\n" +
"██╔══██╗██╔═══██╗██╔════╝ ██╔════╝ ██║     ██╔════╝\n" +
"██████╔╝██║   ██║██║  ███╗██║  ███╗██║     █████╗\n" +
"██╔══██╗██║   ██║██║   ██║██║   ██║██║     ██╔══╝  \n" +
"██████╔╝╚██████╔╝╚██████╔╝╚██████╔╝███████╗███████╗ \n" +
"╚═════╝  ╚═════╝  ╚═════╝  ╚═════╝ ╚══════╝╚══════╝ \n" +
"                                                     \n" +
"███╗   ██╗██╗██╗     ███████╗    ██╗  ██╗   ████████╗ ██████╗ ███╗   ███╗\n" +
"████╗  ██║██║██║     ██╔════╝    ╚██╗██╔╝   ╚══██╔══╝██╔═══██╗████╗ ████║\n" +
"██╔██╗ ██║██║██║     ███████╗     ╚███╔╝       ██║   ██║   ██║██╔████╔██║\n" +
"██║╚██╗██║██║██║     ╚════██║     ██╔██╗       ██║   ██║   ██║██║╚██╔╝██║\n" +
"██║ ╚████║██║███████╗███████║    ██╔╝ ██╗      ██║   ╚██████╔╝██║ ╚═╝ ██║\n" +
"╚═╝  ╚═══╝╚═╝╚══════╝╚══════╝    ╚═╝  ╚═╝      ╚═╝    ╚═════╝ ╚═╝     ╚═╝\n";

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(asciiArt);
        Console.ResetColor();
        #endregion

        #region Lecture nombre de manches
        Console.Write("\nNombre de manche par joueur ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("(3 si laissé vide)");
        Console.ResetColor();
        Console.Write(" : ");

        Console.ForegroundColor = ConsoleColor.Yellow;

        int nbTours = 3*2;
        string entreeNbTours;
        bool estValideNbTours = false;
        while (!estValideNbTours)
        {
            entreeNbTours = Console.ReadLine();
            Console.ResetColor();
            if (entreeNbTours == "")
            {
                nbTours = 3*2;
                estValideNbTours = true;
            }
            else if (int.TryParse(entreeNbTours, out nbTours) && nbTours >= 1)
            {
                estValideNbTours = true;
                nbTours = nbTours * 2;
            }
            else
            {
                Console.WriteLine("Entrée invalide. Veuillez réessayer.");
                Console.Write("Nombre de manche par joueur (3 si laissé vide) : ");
            }
        }
        #endregion

        #region Lecture noms joueurs et IA
        bool vsIA = false;
        Jeu jeu = new Jeu(nbTours * 60);
        Console.WriteLine();
        Joueur j1 = new Joueur(jeu.SaisirNom(1));
        Console.WriteLine();
        Joueur j2 = new Joueur(jeu.SaisirNom(2));

        int difficulte = 1;
        if (j2.Nom.ToUpper() == "IA")
        {
            vsIA = true;
            Console.Write("\nNiveau de difficulté de l'IA [1, 2, 3] ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("(1 si laissé vide)");
            Console.ResetColor();
            Console.Write(" : ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            
            string entreeDifficulte;
            bool estValideDifficulte = false;
            while (!estValideDifficulte)
            {
                entreeDifficulte = Console.ReadLine();
                Console.ResetColor();
                if (entreeDifficulte == "")
                {
                    difficulte = 1;
                    estValideDifficulte = true;
                }
                else if (int.TryParse(entreeDifficulte, out difficulte) && difficulte >= 1 && difficulte <= 3)
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
        #endregion

        #region Lecture taille plateau
        Console.Write("\nTaille du plateau ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("(6 si laissé vide)");
        Console.ResetColor();
        Console.Write(" : ");
        Console.ForegroundColor = ConsoleColor.Yellow;

        int taille = 6;
        string entreeTaille;
        bool estValideTaille = false;
        while (!estValideTaille)
        {
            entreeTaille = Console.ReadLine();
            Console.ResetColor();
            if (entreeTaille == "")
            {
                taille = 6;
                estValideTaille = true;
            }
            else if (int.TryParse(entreeTaille, out taille) && taille > 1)
            {
                estValideTaille = true;
            }
            else
            {
                Console.WriteLine("Entrée invalide. Veuillez réessayer.");
                Console.Write("Taille du plateau (6 si laissé vide) : ");
            }
        }
        #endregion

        #region Lecture langue dictionnaire
        Console.Write("\nLangue \"fr\" ou \"en\" ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("(\"fr\" si laissé vide)");
        Console.ResetColor();
        Console.Write(" : ");

        string langue = "fr";
        bool estValideLangue = false;
        Console.ForegroundColor = ConsoleColor.Yellow;
        while (!estValideLangue)
        {
            langue = Console.ReadLine();
            Console.ResetColor();
            if (langue == "")
            {
                langue = "fr";
                estValideLangue = true;
            }
            else if (langue.ToLower() == "fr" || langue.ToLower() == "en")
            {
                estValideLangue = true;
            }
            else
            {
                Console.WriteLine("Entrée invalide. Veuillez réessayer.");
                Console.Write("Langue (\"fr\" ou \"en\") : ");
            }
        }
        #endregion

        #region Creation de la partie et execution des manches successives
        Dictionnaire dico = new Dictionnaire(langue.ToLower());
        List<string> motsTrouves = new List<string>();
        De[,] des = new De[taille, taille];

        for (int x = 0; x < taille; x++)
        {
            for (int y = 0; y < taille; y++)
            {
                des[x, y] = new De(r, dico);
            }
        }
        for (int i = 0; i < nbTours; i++)
        {
            if (i%2 == 0)
            {
                Console.WriteLine("\n\n||────────────── TOUR " + Convert.ToInt32((i)/2+1) + " ──────────────||");
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\n|───────── À ");
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
            Console.WriteLine(" de jouer ─────────|");
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
                Console.Write(tempsRestant.Minutes + ":" + tempsRestant.Seconds + "\t"); 
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(jeu.BarreProgression(tempsRestant, dureeManche));
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
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(" + ");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" "+res+" ");
                    Console.ResetColor();

                    jeu.UpdateScore((i % 2), mot);
                } else
                {
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(" - ");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" " + res + " ");
                    Console.ResetColor();
                }
            }
            
        }
        #endregion

        #region Affichage des scores
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
        Console.WriteLine(" pour afficher les nuages de mots");
        Console.ResetColor();
        Console.ReadLine();
        #endregion

        #region Appel methode création nuage de mots
        Dictionary<string, int> motsTrouvesFrequenceJ1 = new Dictionary<string, int>();
        foreach (var mot in jeu.Joueurs[0].Mots)
        {
            if (motsTrouvesFrequenceJ1.ContainsKey(mot))
            {
                motsTrouvesFrequenceJ1[mot]++;
            }
            else
            {
                motsTrouvesFrequenceJ1[mot] = 1;
            }
        }

        Dictionary<string, int> motsTrouvesFrequenceJ2 = new Dictionary<string, int>();
        foreach (var mot in jeu.Joueurs[1].Mots)
        {
            if (motsTrouvesFrequenceJ2.ContainsKey(mot))
            {
                motsTrouvesFrequenceJ2[mot]++;
            }
            else
            {
                motsTrouvesFrequenceJ2[mot] = 1;
            }
        }

        NuageDeMots.Affichage(motsTrouvesFrequenceJ1, motsTrouvesFrequenceJ2);
        #endregion

        Console.ReadLine();

    }
}