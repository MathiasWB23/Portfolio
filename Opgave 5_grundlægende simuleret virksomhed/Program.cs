using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Opgave_5_grundlægende_simuleret_virksomhed
{
    internal class Program
    {
        class HR 
            /*for at holde styr på vores medarbejder, fyringer og ansættelser, så opretter vi en list (OOP).
               for at vi kan arbejde med den dynamisk med formål at kunne ændre, slette og tilfæje listens objekter.
            Vi kalder classen HR, som en simulation af en HR (human ressources) afdeling.
            HR afdelingen adminstrere de ansatte medarbejderes informationer, navn, stilling, løn og ansættelsesstatus.
            Når vi opretter hver medarbejder somn et objekt i HR classen, så kan vi dynamisk tilføje, -
            ændre og slette medarbejdernes informationer i form af objekter i en objekt-orienteret programmering (OOP).

            Jeg bruger i praksis en enkapsulering af objektets indhold, enkapsuleringen af Navn, Stilling, Løn og Status, som enkapsuleres i eks. objektet Thomas, se linje 64
            */
        {
            public string Navn { get; set; } //vi opretter variablen og dets indhold skal vi først fylde ud senere, get og set
            public string Stilling { get; set; } //get gør det muligt at lave en get request til at kunne tilgå nogle variablers indhold/værdier, der ellers ikke kan tilgås samt at kunne ændre dem. Set gør at vi kan ændre værdien indenfor nogle properties/parametre. Nu hvor det er public kan de altid tilgås, men hvis de var private kan de kun tilgås via GET og SET.
            public int Løn { get; set; }
            public string Status { get; set; }
            public HR(string navn, string stilling, int løn, string status = "ansat") 
                //HR classen skal fyldes af objekter der består af Navn, Stilling, Løn og Status hvis værdier/content sættes til at være variblerne defineret i ()
                //når objekter oprettes, så sættes deres status automatisk til at være "ansat"
            {
                Navn = navn;
                Stilling = stilling;
                Løn = løn;
                Status = status;
            }
        }

        class Vare
            //vi opretter en class list (OOP), for at kunne arbejde dynamisk med vores lager, så kan vi ændre, slette og tilføje lagerets varer som objekter.
        {
            public int Varenummer { get; set; }
            public string Varenavn { get; set; }
            public int Antal { get; set; }
            public int Pris { get; set; }
            public Vare(int varenummer, string varenavn, int antal, int pris)
            {
                Varenummer = varenummer;
                Varenavn = varenavn;
                Antal = antal;
                Pris = pris;
            }
        }
        static void Main(string[] args)
        {
            List<HR> medarbejdere = new List<HR>();

            medarbejdere.Add(new HR("Thomas", "Manager", 30000));
            medarbejdere.Add(new HR("Dag", "programmør", 25000));

            List<Vare> lagerbeholdning = new List<Vare>();

            lagerbeholdning.Add(new Vare(1, "lade kabel", 33, 100));
            lagerbeholdning.Add(new Vare(2, "switch2960", 6, 7500));
            lagerbeholdning.Add(new Vare(3, "router1911", 13, 9999));

            bool kører = true;
            while (kører)
            {
                Console.WriteLine("Velkommen til Thomas er dum A/S, du hørte rigtigt du kan investere i Thomas fremtid");
                Console.WriteLine("Afdelinger: HR, Lager, Økonomi");
                Console.WriteLine("Login : ");
                String loginPass = Console.ReadLine();
                

                if (loginPass == "HR") //HR afdeling
                {
                    Console.WriteLine("velkommen til HR du har følgende muligheder : ");
                    Console.WriteLine("tast 1 for at se medarbejder listen");
                    Console.WriteLine("tast 2 for at ansætte medarbejdere");
                    Console.WriteLine("tast 3 for at fyre medarbejdere");
                    Console.WriteLine("tast 0 for at lukke");
                    Console.Write("Indtast dit valg : ");
                    string uservalg = Console.ReadLine(); //vi gemmer brugens valg i en variabel, så vi kan bestemme noget koden der associeres med variablen.

                    if (int.TryParse(uservalg, out int menu))
                    {
                        switch (menu)
                        {
                            case 1: //Hvis uservalg var 1, så vises medarbejderlisten, som også løbende opdateres.
                                Console.WriteLine("du er nu i medarbejderlisten");

                                foreach (var medarbejder in medarbejdere)
                                {
                                    Console.WriteLine($"Navn: {medarbejder.Navn}, Stilling: {medarbejder.Stilling}, Løn: {medarbejder.Løn}, Ansættelsesstatus: {medarbejder.Status}");
                                }
                                break;

                            case 2: //Hvis uservalg var 2, så kan vi ansætte en ny medarbejder
                                Console.WriteLine("ansættelses funktion, indtast oplysninger om den nye medarbejder");
                                Console.WriteLine("indtast Navn : ");
                                string navn = Console.ReadLine();

                                Console.WriteLine("indtast Stilling : ");
                                string stilling = Console.ReadLine();

                                Console.WriteLine("indtast Løn : ");
                                int løn = int.Parse(Console.ReadLine());

                                HR nyMedarbejder = new HR(navn, stilling, løn);
                                medarbejdere.Add(nyMedarbejder);
                                Console.WriteLine("medarbejder tilføjet");
                                break;

                            case 3: //Hvis uservalg var 3, så kan vi komme ind i en fyrings funktion
                                Console.WriteLine("Fyrings funktion, indtast deres navn : ");
                                string Fyring = Console.ReadLine();

                                int indexAtFjerne = medarbejdere.FindIndex(v => v.Navn == Fyring);

                                if (indexAtFjerne != -1) //hvis denne så findes, jamen så sletter vi index objektet der forbindes med varenummer.
                                {
                                    medarbejdere.RemoveAt(indexAtFjerne); //removeAt er en lignende funktion som FindIndex, hvor den fjerner det indhold i den referede list objekt.
                                    Console.WriteLine("navn : " + Fyring + "fjernet");
                                }
                                break;

                            case 0: //Hvis uservalg var 0, så skal vi lave en funktion der lukker
                                Environment.Exit(0); //lukke funktion, der lukker det enviornment som metoden er en del af. Det vil sige vores namespace enviroment.
                                break;

                        }
                    }
                }

                else if (loginPass == "Lager")
                {
                    Console.WriteLine("Velkommen til lageret, du har følgende muligheder : ");
                    Console.WriteLine("Tast 1 for at se lagerbeholdningen");
                    Console.WriteLine("Tast 2 for at tilføje en vare til lageret");
                    Console.Write("Indtast dit valg : ");
                    string lagerValg = Console.ReadLine(); //vi gemmer uservalget i Lager delen af koden

                    if (int.TryParse(lagerValg, out int lagerMenu))
                    {
                        switch (lagerMenu)
                        {
                            case 1: //Hvis lagervalg er 1, vises lager status
                                Console.WriteLine("Lagerbeholdning:");
                                foreach (var vare in lagerbeholdning)
                                {
                                    Console.WriteLine($"Varenavn: {vare.Varenavn}, Antal: {vare.Antal}, Pris: {vare.Pris}");
                                }
                                break;

                            case 2: //Hvis lagervalg er 2, så kan man tilføje nye varer
                                Console.WriteLine("Indtast Varenummer : ");
                                int varenummer = int.Parse(Console.ReadLine());

                                Console.WriteLine("Indtast Varenavn : ");
                                string varenavn = Console.ReadLine();

                                Console.WriteLine("Indtast Antal : ");
                                if (int.TryParse(Console.ReadLine(), out int antal))
                                {
                                    Console.WriteLine("Indtast Pris : ");
                                    if (int.TryParse(Console.ReadLine(), out int pris))
                                    {
                                        Vare nyVare = new Vare(varenummer, varenavn, antal, pris);
                                        lagerbeholdning.Add(nyVare);
                                        Console.WriteLine("Vare tilføjet til lageret!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Ugyldig pris værdi, prøv igen.");
                                    }
                                }

                                else
                                {
                                    Console.WriteLine("Ugyldigt antal værdi, prøv igen.");
                                }
                                break;


                            default:
                                Console.WriteLine("Ugyldigt valg, prøv igen.");
                                break;
                        }
                    }
                }

                else if (loginPass == "Økonomi")
                {
                    Console.WriteLine("Velkommen til Økonomi");
                    Console.WriteLine("her kan du prøve en simuleret fremtid for virksomheden over de næste 3 kvartaler, dine medarbejdere og varer har indvirkning på simulationen");

                    Console.WriteLine("indtast nuværende omsætning : ");
                    int omsætning = int.Parse(Console.ReadLine());

                    Console.WriteLine("Er du klar, til din virksomheds simulering?");
                    Console.WriteLine("Y/N");
                    string UserØkoValg = Console.ReadLine();


                    //loading bar funktion
                    if (UserØkoValg == "Y")
                    {
                        Console.Write("Loading: ");

                        for (int i = 0; i < 30; i++) // 30 er længden af loading baren
                        {
                            Console.Write("#");
                            Thread.Sleep(50); //delay til loading
                        }

                        Console.WriteLine("\nDone!");
                    }
                    SimulateEconomicImpact(medarbejdere, lagerbeholdning, 3);
                }
            }
        }

        //Koden nedenfor er AI genereret 
        static void SimulateEconomicImpact(List<HR> medarbejdere, List<Vare> lagerbeholdning, int periods)
        {
            Random rand = new Random();

            for (int period = 1; period <= periods; period++)
            {
                Console.WriteLine($"\n--- Period {period} ---");

                // Her kan vi justere medarbejdernes løn
                foreach (var employee in medarbejdere)
                {
                    double salaryChange = GenerateRandomPercentage(rand);
                    int adjustedSalary = (int)(employee.Løn * (1 + salaryChange));
                    Console.WriteLine($"Employee: {employee.Navn} | Original Løn: {employee.Løn} | New Løn: {adjustedSalary}");
                    employee.Løn = adjustedSalary; // Update salary
                }

                // Her kan vi justere varens priser
                foreach (var product in lagerbeholdning)
                {
                    double priceChange = GenerateRandomPercentage(rand);
                    int adjustedPrice = (int)(product.Pris * (1 + priceChange));
                    Console.WriteLine($"Product: {product.Varenavn} | Original Pris: {product.Pris} | New Pris: {adjustedPrice}");
                    product.Pris = adjustedPrice; // opdater prisen
                }
            }
        }

        // Metode til at genere en random værdi, indenfor en ramme på 5% positiv eller negativ
        static double GenerateRandomPercentage(Random rand)
        {
            return (rand.NextDouble() * 0.10) - 0.05; // Produces value mellem -0.05 og +0.05
        }
    }
}


