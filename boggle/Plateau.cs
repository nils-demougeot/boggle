using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;

public class Plateau
{
    int taille;
    De[,] des;
    char[,] facesVisibles;
    List<string> motsTrouves;
    Dictionnaire dico;

    public Plateau(De[,] _des, Dictionnaire _dico)
    {
        this.des = _des;
        this.dico = _dico;
        this.taille = this.des.GetLength(0);
        this.facesVisibles = new char[taille, taille];
        this.motsTrouves = new List<string> { };
        for (int i = 0; i < taille; i++)
        {
            for (int j = 0; j < taille; j++)
            {
                facesVisibles[i, j] = des[i, j].FaceVisible;
            }
        }
    }

    public string toString()
    {
        string output = "";
        for (int i = 0; i < this.taille; i++)
        {
            for (int j = 0; j < this.taille; j++)
            {
                Console.Write(this.facesVisibles[i, j] + " ");
            }
            Console.WriteLine();
        }
        return output;
    }

    public string Test_Plateau(string mot)
    {
        if (string.IsNullOrWhiteSpace(mot) || mot.Length < 2 || motsTrouves.Contains(mot))
        {
            return "";
        }

        mot = mot.ToUpper();
        if (!this.dico.RechDichoRecursif(mot, 0, (this.dico.Mots.Length - 1)))
        {
            return "";
        }
        bool[,] visited = new bool[taille, taille];

        for (int i = 0; i < taille; i++)
        {
            for (int j = 0; j < taille; j++)
            {
                if (facesVisibles[i, j] == mot[0])
                {
                    if (RechercherMot(i, j, mot, 0, visited))
                    {
                        this.motsTrouves.Add(mot);
                        return mot;
                    }
                }
            }
        }
        return "";
    }

    private bool RechercherMot(int x, int y, string mot, int index, bool[,] visited)
    {
        // Condition de victoire : tout le mot a été trouvé
        if (index == mot.Length)
            return true;

        // Vérifier les limites du plateau
        if (x < 0 || y < 0 || x >= taille || y >= taille)
            return false;

        // Vérifier si la case a déjà été utilisée ou si elle ne correspond pas au caractère attendu
        if (visited[x, y] || facesVisibles[x, y] != mot[index])
            return false;

        // Marquer la case comme visitée
        visited[x, y] = true;

        // Directions adjacentes : haut, bas, gauche, droite et diagonales
        int[] dx = { -1, -1, -1, 0, 1, 1, 1, 0 };
        int[] dy = { -1, 0, 1, 1, 1, 0, -1, -1 };

        // Vérifier toutes les directions
        for (int dir = 0; dir < 8; dir++)
        {
            if (RechercherMot(x + dx[dir], y + dy[dir], mot, index + 1, visited))
                return true;
        }

        // Annuler la visite 
        visited[x, y] = false;

        return false;
    }
}