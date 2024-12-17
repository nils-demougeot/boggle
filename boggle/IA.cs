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
        Random random = new Random();
        int r = random.Next(dico.Mots.Length);
        if (plateau.Test_Plateau(dico.Mots[r], this.jeu.Joueurs[1]))
        {
            this.jeu.UpdateScore(jeu.Mots[r], 1);
            TimeSpan duree = TimeSpan.FromSeconds(15 / difficulte);
            DateTime debut = DateTime.Now;
            while (this.jeu.Minuteur(duree, debut) > TimeSpan.Zero) { }
        }
    }
}