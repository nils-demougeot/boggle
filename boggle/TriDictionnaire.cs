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
}

