using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgave_1_Dynamisk_quizgenerator_easy_medium_diff
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*opgave beskrivelse:
             * Lav et program, der opretter en dynamisk quiz ud fra en liste af spørgsmål og svar.
             * Brugeren skal kunne svare på spørgsmålene, og programmet skal give feedback på hvert svar.
             * Krav:
             * Opret mindst fem spørgsmål med korrekte svar, gemt i arrays eller lister.
             * Brug en for- eller while-loop til at præsentere hvert spørgsmål et ad gangen.
             * Implementer en funktion, der tjekker, om svaret er korrekt, og giver feedback (rigtigt eller forkert)
             */
            Console.WriteLine("velkommen til din quizgenerator");
            Console.WriteLine("du vil få givet 5 spørgsmål og få feedback efter du har givet dine svar. Held og lykke!");
            string[] sprg = { "nr.1: Hvad er 1+2?", "nr.2: Hvad elsker aber?", "nr.3: Hvad er den amerikanske præsidents efternavn?", "nr.4: Hvor kommer thomas fra?", "nr.5: Hvilken farve er et æble?"};
            string[] korrekt = { "3", "banan", "trump", "klokkerholm", "rød" };
            int score = 0;

            for (int i = 0; i < sprg.Length; i++)
            {
                Console.WriteLine(sprg[i]);
                Console.WriteLine("indtast dit svar : ");
                string brugersvar = Console.ReadLine().ToLower();

                if (brugersvar == korrekt[i].ToLower())
                {
                    Console.WriteLine("korrekt");
                    score++;
                }
                else
                {
                    Console.WriteLine("forkert");
                }
            }

            Console.WriteLine($"quizzen er gennemført, du fik {score} ud af {sprg.Length} rigtige");
        }
    }
}
