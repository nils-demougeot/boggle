using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using System.Xml;

public class Plateau
{
    int taille;
    De[,] des;
    char[,] facesVisibles;
    Dictionnaire dico;

    public Plateau(De[,] _des, Dictionnaire _dico)
    {
        this.des = _des;
        this.dico = _dico;
        this.taille = this.des.GetLength(0);
        Random r = new Random();
        this.facesVisibles = new char[taille, taille];
        for (int i = 0; i < taille; i++)
        {
            for (int j = 0; j < taille; j++)
            {
                des[i, j].LancerDe(r);
                facesVisibles[i, j] = des[i, j].FaceVisible;
            }
        }
    }

    #region Description du plateau
    /// <summary>
    /// Renvoie le plateau actuel de telle sorte qu'il soit lisible, sous forme d'un carré de lettres
    /// </summary>
    /// <returns>Une chaine de caractère contenant un carré des lettres composant le plateau</returns>
    public string toString()
    {
        string output = "";
        for (int i = 0; i < this.taille; i++)
        {
            Console.Write("  ");
            for (int j = 0; j < this.taille; j++)
            {
                Console.Write(this.facesVisibles[i, j] + " ");
            }
            Console.WriteLine();
        }
        return output;
    }
    #endregion

    #region Test Plateau
    /// <summary>
    /// Fonction vérifiant si le mot entré est valide, c-a-d, si ce n'est pas juste un espace blanc, 
    /// s'il existe dans le dictionnaire, et s'il est bien présent sur le plateau selon les règles du boggle
    /// </summary>
    /// <param name="mot">Mot à vérifier</param>
    /// <param name="joueur">Joueur actuel</param>
    /// <returns>Retourne le mot passé en paramètre s'il est valide et une chaine de caractère vide sinon</returns>
    public string Test_Plateau(string mot, Joueur joueur)
    {
        mot = mot.ToUpper();
        string output = "Ce mot n'est pas présent sur le plateau";
        if (string.IsNullOrWhiteSpace(mot) || mot.Length < 2)
        {
            output = "Veuillez entrer un mot";
        }

        else if (!this.dico.RechDichoRecursif(mot, 0, (this.dico.Mots.Length - 1)))
        {
            output = "Ce mot n'appartient pas au dictionnaire";
        }

        else if (joueur.Contains(mot))
        {
            output = "Ce mot a déjà été entré";
        }

        else
        {
            bool[,] visited = new bool[taille, taille];

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (facesVisibles[i, j] == mot[0])
                    {
                        if (RechercherMot(i, j, mot, 0, visited))
                        {
                            joueur.AddMot(mot);
                            output = "Mot valide";
                        }
                    }
                }
            }
        }
        
        return output;
    }
    #endregion

    #region Recherche récursive du mot sur le plateau
    /// <summary>
    /// Fonction récursive vérifiant si le mot entré en paramètre est bien présent sur le plateau 
    /// en respectant les règles d'adjacence du boggle
    /// </summary>
    /// <param name="x">La position en x sur la plateau ectuellement vérifiée</param>
    /// <param name="y">La position en y sur le plateau actuellement vérifiée</param>
    /// <param name="mot">Le mot à vérifier sur le plateau</param>
    /// <param name="index">Le rang de la lettre du mot actuellement vérifiée</param>
    /// <param name="visited">Matrice de booléens de la taille du plateau indiquant pour chaque lettre si elle a déjà été visitée</param>
    /// <returns>True si le mot est présent sur le plateau, False sinon</returns>
    private bool RechercherMot(int x, int y, string mot, int index, bool[,] visited)
    {
        if (index == mot.Length)
        {
            return true;
        }
        if (x < 0 || y < 0 || x >= taille || y >= taille)
        {
            return false;
        }
        if (visited[x, y] || facesVisibles[x, y] != mot[index])
        {
            return false;
        }
        visited[x, y] = true;
        int[] dx = { -1, -1, -1, 0, 1, 1, 1, 0 };
        int[] dy = { -1, 0, 1, 1, 1, 0, -1, -1 };
        for (int dir = 0; dir < 8; dir++)
        {
            if (RechercherMot(x + dx[dir], y + dy[dir], mot, index + 1, visited))
            {
                return true;
            }
        }
        visited[x, y] = false;
        return false;
    }
    #endregion
}