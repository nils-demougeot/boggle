using boggle;
using System;
using System.Runtime.CompilerServices;

class Program
{
    public static void Main(string[] args)
    {
        NuageDeMots.TestDisplay();

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
        Joueur j1 = new Joueur(jeu.SaisirNom(1));
        Joueur j2 = new Joueur(jeu.SaisirNom(2));
        Joueur[] joueurs = new Joueur[2] { j1, j2 };
        jeu.Joueurs = joueurs;
        TimeSpan duree = TimeSpan.FromSeconds(dureeEnMin * 60);
        DateTime debut = DateTime.Now;
        Random r = new Random();
        Console.Write("Taille du plateau : ");
        int taille = Convert.ToInt32(Console.ReadLine());
        Console.Write("Langue (\"fr\" ou \"en\") : ");
        Dictionnaire dico = new Dictionnaire(Console.ReadLine());
        List<string> motsTrouves = new List<string>();

        for (int i = 0; i < dureeEnMin; i++)
        {
            
            jeu.Joueurs[(i % 2)].Mots = new List<string>();
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
                // Console.Clear();
                // Console.ForegroundColor = ConsoleColor.DarkGreen;
                // Console.WriteLine(asciiArt);
                // Console.ResetColor(); 
                Console.Write("C'est au tour de ");
                if (i % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                { Console.ForegroundColor = ConsoleColor.Red; }
                Console.WriteLine(jeu.Joueurs[(i % 2)].Nom);
                Console.ResetColor();
                TimeSpan tempsRestant = jeu.Minuteur(dureeManche, debutManche);
                Console.WriteLine("Temps restant : " + tempsRestant.Minutes + ":" + tempsRestant.Seconds);
                Console.WriteLine("Score : " + jeu.Joueurs[(i % 2)].Score + "\n");
                Console.WriteLine(plateau.toString());
                string mot = Console.ReadLine().ToUpper();
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



        /*
        NuageDeMots.TestDisplay();
        Dictionary<string, int> motsTrouvesFrequence = new Dictionary<string, int>();

        foreach (var item in items)
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
        */


    }
}