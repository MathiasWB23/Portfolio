using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Runtime.InteropServices;

namespace B2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * Opgave beskrivelse:
             * Create a program where the user tries to guess a randomly generated number.
             * 
             * Requirements: 
             * Generate a random number between 1 and 100. 
             * Prompt the user to guess the number. 
             * Provide feedback: 
             *      If the guess is too high, display: "Too high! Try again." 
             *      If the guess is too low, display: "Too low! Try again." 
             *      If the guess is correct, display: "Congratulations! You guessed it!" 
             * Allow the user to keep guessing until they find the correct number.
             */
            
            Random random = new Random(); // opretter random number generator
            int randomnumber = random.Next(1, 101); // random number generatoren er sat op med parametrene 1-100
            int score = 0;

            bool test = true;

            while (test) // Vi sætter koden i en while løkke for at tillade flere loop runs/bruger forsøg på at gætte rigtigt.
            {
                Console.WriteLine("Gæt hvilket tal jeg tænker på: ");
                int gæt = Convert.ToInt32(Console.ReadLine()); // brugerens gæt konverteres til en int.
                score++;

                if (gæt > randomnumber) // Hvis man gætter for højt
                {
                    Console.WriteLine("For højt");
                }
                else if (gæt < randomnumber) // hvis man gætter for lavt
                {
                    Console.WriteLine("For lavt");
                }
                else // Hvis man gætter rigtigt
                {
                    Console.WriteLine("Du gættede rigtigt!");
                    Console.WriteLine($"Dette var din score: {score}");

                    Console.WriteLine("Spillet er slut. Tak for at spille!");
                    test = false;
                }
            }
        }
    }
}
