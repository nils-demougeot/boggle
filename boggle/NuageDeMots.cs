using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;



namespace boggle
{
    public class NuageDeMots
    {
        /// <summary>
        /// Génère et sauvegarde une image représentant le nuage des mots (les mots les plus trouvés durant la partie en grand)
        /// </summary>
        /// <param name="associationMotFrequence">Dictionnaire contenant tous les mots uniques trouvés par le joueur associés au nombre de fois que ce mot a été trouvé</param>
        /// <param name="cheminSauvegarde">Adresse où le fichier image du nuage de mot sera sauvegardé</param>
        /// <param name="numJoueur">Numéro du joueur (joueur 1 ou joueur 2)</param>
        /// <param name="largeurImage">Défini la largeur en pixel de l'image</param>
        /// <param name="hauteurImage">Défini la hauteur en pixel de l'image</param>
        /// <exception cref="ArgumentException">Si le joueur n'a trouvé aucun mot pendant la partie, on n'affiche pas de nuage de mot</exception>
        public void CreerNuageDeMots(Dictionary<string, int> associationMotFrequence, string cheminSauvegarde, int numJoueur, int largeurImage = 800, int hauteurImage = 600)
        {
            if (associationMotFrequence == null || !associationMotFrequence.Any())
            {
                throw new ArgumentException("Auncun mot trouvé par le joueur");
            }

            using var bitmap = new SKBitmap(largeurImage, hauteurImage);
            using var canvas = new SKCanvas(bitmap);

            if (numJoueur == 1)
            {
                canvas.Clear(new SKColor(222, 235, 255));
            } else
            {
                canvas.Clear(new SKColor(255, 220, 220));
            }

            Random random = new Random();
            int maxFrequency = associationMotFrequence.Values.Max();
            int minFontSize = 15;
            int maxFontSize = 75;

            var sortedWords = associationMotFrequence.OrderByDescending(kvp => kvp.Value);

            foreach (var wordEntry in sortedWords)
            {
                string word = wordEntry.Key;
                int frequency = wordEntry.Value;

                int fontSize = minFontSize + (frequency * (maxFontSize - minFontSize) / maxFrequency);

                int x = random.Next(0, largeurImage - fontSize * word.Length);
                int y = random.Next(fontSize, hauteurImage - fontSize);

                using var paint = new SKPaint
                {
                    Color = new SKColor((byte)random.Next(180), (byte)random.Next(180), (byte)random.Next(180)), //peut etre blanc donc prblm
                    TextSize = fontSize,
                    IsAntialias = true,
                    Typeface = SKTypeface.FromFamilyName("Arial")
                };

                canvas.DrawText(word, x, y, paint);
            }

            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = System.IO.File.OpenWrite(cheminSauvegarde);
            data.SaveTo(stream);

            Console.WriteLine("Nuage de mot du joueur "+ numJoueur + " sauvegardé à /bin/debug/net6.0/"+ cheminSauvegarde);
        }
        

        static public void Affichage(Dictionary<string, int> motsTrouvesJ1, Dictionary<string, int> motsTrouvesJ2)
        {
            if (motsTrouvesJ1.Count != 0)
            {
                NuageDeMots NuageJoueur1 = new NuageDeMots();
                NuageJoueur1.CreerNuageDeMots(motsTrouvesJ1, "NuageJoueur1.png", 1);

                Process.Start(new ProcessStartInfo("NuageJoueur1.png") { UseShellExecute = true });
            } else
            {
                Console.WriteLine("Aucun mot n'a été entré par le Joueur 1, pas de nuage de mots");
            }

            if (motsTrouvesJ2.Count != 0)
            {
                NuageDeMots NuageJoueur2 = new NuageDeMots();
                NuageJoueur2.CreerNuageDeMots(motsTrouvesJ2, "NuageJoueur2.png", 2);

                Process.Start(new ProcessStartInfo("NuageJoueur2.png") { UseShellExecute = true });
            }
            else
            {
                Console.WriteLine("Aucun mot n'a été entré par le Joueur 2, pas de nuage de mots");
            }

        }
    }
}

