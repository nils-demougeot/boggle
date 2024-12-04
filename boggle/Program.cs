using System;

class Program
{
    public static void Main(string[] args)
    {
        // *************************** TEST CLASSE DICTIONNAIRE ***************************
        // Dictionnaire dico = new Dictionnaire("fr");
        // Console.WriteLine(dico.toString());

        // *************************** TEST CLASSE DE ***************************
        // Random r = new Random();
        // De de = new De(r);
        // Console.WriteLine(de.toString());

        // *************************** TEST CLASSE PLATEAU ***************************
        /*Random r = new Random();
        int taille = 4;
        De[,] des = new De[taille, taille];
        for (int i = 0; i < taille; i++)
        {
            for (int j = 0; j < taille; j++)
            {
                des[i, j] = new De(r);
            }
        }
        Plateau plateau = new Plateau(des);
        while (true)
        {
            Console.Clear();
            Console.WriteLine(plateau.toString());
            Console.WriteLine(plateau.Test_Plateau(Console.ReadLine()) + "\n");
            Console.ReadLine();
        }*/

        // ************************* TEST CLASSE JEU ***************************
        int dureeEnMin = 6;
        Jeu jeu = new Jeu(dureeEnMin * 60);
        jeu.J1 = jeu.SaisirNom(1);
        jeu.J2 = jeu.SaisirNom(2);
        TimeSpan duree = TimeSpan.FromSeconds(dureeEnMin * 60);
        DateTime debut = DateTime.Now;
        jeu.AffichageMinuteur(duree, debut);

    }
}