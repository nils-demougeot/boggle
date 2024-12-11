using System;
using System.Collections.Generic;
using System.IO;

public class De
{
    char[] lettres;
    char faceVisible;
    string path = "../../../docs/Lettres.txt";

    public char FaceVisible
    {
        get { return this.faceVisible; }
        set { this.faceVisible = value; }
    }

    public De(Random r)
    {
        this.lettres = new char[6];
        string texte = File.ReadAllText(this.path);
        Dictionary<int, char> res = new Dictionary<int, char>();
        int somme = 0;
        string[] lignes = texte.Split('\n');
        foreach (string ligne in lignes)
        {
            string[] parties = ligne.Split(';');
            if (parties.Length >= 3)
            {
                char lettre = parties[0][0];
                somme += int.Parse(parties[^1]);
                res.Add(somme, parties[0][0]);
            }
        }
        var list = new List<KeyValuePair<int, char>>(res);
        for (int j = 0; j < 6; j++)
        {
            int index = r.Next(0, 100);
            for (int k = 0; k < res.Count; k++)
            {
                if (index < list[k].Key)
                {
                    this.lettres[j] = res[list[k].Key];
                    break;
                }
            }
        }
        Lance(r);
    }

    public void Lance(Random r)
    {
        this.faceVisible = this.lettres[r.Next(0, this.lettres.Length)];
    }
    public string toString()
    {
        string listeLettres = "";
        for (int i = 0; i < this.lettres.Length; i++)
        {
            listeLettres += this.lettres[i] + ", ";
        }
        return "Lettres : " + listeLettres + "\nFace visible : " + this.faceVisible;
    }
}