using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class TriDictionnaire
{
    #region Tri Rapide
    /// <summary>
    /// Tri le tableau dictionnaire passé en paramètre par ordre alphabétique et par dichotomie (Tri Quick Sort / Tri Rapide).
    /// </summary>
    /// <param name="dictionnaire">"Dictionnaire" contenant des mots dans chaque case du tableau non trié</param>
    /// <param name="gauche">indice de départ dans le tableau pour le tri diviser pour régner (0 en général)</param>
    /// <param name="droite">indice de fin dans le tableau pour le tri diviser pour régner (dictionnaire.mots.Length en général)</param>
    public static void TriRapideDico(string[] dictionnaire, int gauche, int droite)
    {
        if (gauche < droite)
        {
            int positionPivot = Partition(dictionnaire, gauche, droite);
            TriRapideDico(dictionnaire, gauche, positionPivot - 1);
            TriRapideDico(dictionnaire, positionPivot + 1, droite);
        }
    }

    /// <summary>
    /// Partitionne le tableau : met les chaines avant le pivot dans l'ordre alphabétique à gauche et les chaines après le pivot dans l'ordre alphabétique à droite.
    /// </summary>
    /// <param name="dictionnaire">"Dictionnaire" contenant des mots dans chaque case du tableau non trié</param>
    /// <param name="gauche">indice de départ dans le sous tableau pour le tri</param>
    /// <param name="droite">indice de fin dans le sous tableau pour le tri</param>
    static int Partition(string[] dictionnaire, int gauche, int droite)
    {
        string pivot = dictionnaire[droite];
        int frontiereActuelle = gauche - 1;

        for (int j = gauche; j < droite; j++)
        {
            if (string.Compare(dictionnaire[j], pivot) < 0)
            {
                frontiereActuelle++;

                string temp = dictionnaire[frontiereActuelle];
                dictionnaire[frontiereActuelle] = dictionnaire[j];
                dictionnaire[j] = temp;
            }
        }

        string tempPivot = dictionnaire[frontiereActuelle + 1];
        dictionnaire[frontiereActuelle + 1] = dictionnaire[droite];
        dictionnaire[droite] = tempPivot;

        return frontiereActuelle + 1;
    }
    #endregion

    #region Tri Fusion
    /// <summary>
    /// Tri le tableau dictionnaire passé en paramètre par ordre alphabétique (Tri Fusion / Merge Sort).
    /// </summary>
    /// <param name="dictionnaire">Tableau non trié qu'il faut trier</param>
    static void TriFusion(string[] dictionnaire)
    {
        if (dictionnaire.Length > 1)
        {
            int milieu = dictionnaire.Length / 2;
            string[] gauche = new string[milieu];
            string[] droite = new string[dictionnaire.Length - milieu];

            for (int i = 0; i < milieu; i++)
            {
                gauche[i] = dictionnaire[i];
            }

            for (int i = milieu; i < dictionnaire.Length; i++)
            {
                droite[i - milieu] = dictionnaire[i];
            }

            TriFusion(gauche);
            TriFusion(droite);

            Fusion(dictionnaire, gauche, droite);

        }
    }

    /// <summary>
    /// Fusionne deux sous tableau triés.
    /// </summary>
    /// <param name="tab">Tableau principal non trié qu'il faut trier</param>
    /// <param name="gauche">Sous tableau gauche trié</param>
    /// <param name="droite">Sous tableau droit trié</param>
    static void Fusion(string[] tab, string[] gauche, string[] droite)
    {
        int i = 0;
        int j = 0;
        int k = 0;

        while (i < gauche.Length && j < droite.Length)
        {
            if (String.Compare(gauche[i], droite[j]) < 0)
            {
                tab[k] = gauche[i];
                i++;
            }
            else
            {
                tab[k] = droite[j];
                j++;
            }

            k++;
        }

        while (i < gauche.Length)
        {
            tab[k] = gauche[i];
            i++;
            k++;
        }

        while (j < droite.Length)
        {
            tab[k] = droite[j];
            j++;
            k++;
        }
    }
    #endregion
}

