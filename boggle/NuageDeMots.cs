using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//dotnet add package SkiaSharp



namespace boggle
{
    public class NuageDeMots
    {
        public void CreerNuageDeMots(Dictionary<string, int> wordFrequencies, string outputPath, int canvasWidth = 800, int canvasHeight = 600)
        {
            // Validate inputs
            if (wordFrequencies == null || !wordFrequencies.Any())
            {
                throw new ArgumentException("Word frequencies dictionary is empty or null.");
            }

            // Create a SkiaSharp surface
            using var bitmap = new SKBitmap(canvasWidth, canvasHeight);
            using var canvas = new SKCanvas(bitmap);

            // Clear the canvas with a white background
            canvas.Clear(SKColors.White);

            // Prepare variables
            Random random = new Random();
            int maxFrequency = wordFrequencies.Values.Max();
            int minFontSize = 15; // Minimum font size
            int maxFontSize = 75; // Maximum font size

            // Sort words by frequency (descending)
            var sortedWords = wordFrequencies.OrderByDescending(kvp => kvp.Value);

            foreach (var wordEntry in sortedWords)
            {
                string word = wordEntry.Key;
                int frequency = wordEntry.Value;

                // Calculate font size based on frequency
                int fontSize = minFontSize + (frequency * (maxFontSize - minFontSize) / maxFrequency);

                // Randomize position
                int x = random.Next(0, canvasWidth - fontSize * word.Length);
                int y = random.Next(fontSize, canvasHeight - fontSize);

                // Create paint for the word
                using var paint = new SKPaint
                {
                    Color = new SKColor((byte)random.Next(180), (byte)random.Next(180), (byte)random.Next(180)), //peut etre blanc donc prblm
                    TextSize = fontSize,
                    IsAntialias = true,
                    Typeface = SKTypeface.FromFamilyName("Arial")
                };

                // Draw the word on the canvas
                canvas.DrawText(word, x, y, paint);
            }

            // Save the canvas to an image file
            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = System.IO.File.OpenWrite(outputPath);
            data.SaveTo(stream);

            Console.Write("Nuage de mot sauvegardé à /bin/debug/net6.0/");
            Console.WriteLine(outputPath);
        }

        static public void Affichage(Dictionary<string, int> motsTrouvesFrequence)
        {

            string outputPath = "nuageDeMots.png"; // Chemin pour enregistrer l'image de sortie
            NuageDeMots generator = new NuageDeMots();
            generator.CreerNuageDeMots(motsTrouvesFrequence, outputPath);
            Process.Start(new ProcessStartInfo(outputPath) { UseShellExecute = true });
        }
    }
}

