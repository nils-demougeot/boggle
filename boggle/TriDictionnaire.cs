using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class TriDictionnaire
{
    #region Tri Rapide
    /// <summary>
    /// Tri le tableau dictionnaire passé en paramètre par ordre alphabétique et par dichotomie (Tri Quick Sort / Tri Rapide) et partition.
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
    static void TriFusion(string[] tab)
    {
        if (tab.Length > 1)
        {
            int milieu = tab.Length / 2;
            string[] gauche = new string[milieu];
            string[] droite = new string[tab.Length - milieu];

            for (int i = 0; i < milieu; i++)
            {
                gauche[i] = tab[i];
            }

            for (int i = milieu; i < tab.Length; i++)
            {
                droite[i - milieu] = tab[i];
            }

            TriFusion(gauche);
            TriFusion(droite);

            Fusion(tab, gauche, droite);

        }
    }

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

