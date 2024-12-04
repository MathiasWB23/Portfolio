using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Opgave_3.Elektronik_Lager_AS
{
    internal class Program
    {

            class Vare
            {
                public int Varenummer { get; set; }
                public string Varenavn { get; set; }
                public int Antal { get; set; }
                public string Placering { get; set; }
                
                public Vare(int varenummer, string varenavn, int antal, string placering)
                {
                    Varenummer = varenummer;
                    Varenavn = varenavn;
                    Antal = antal;
                    Placering = placering;
                }

                public override string ToString()
                {
                    return $"Varenummer: {Varenummer}, Varenavn: {Varenavn}, Antal: {Antal}, Placering: {Placering}";
                }
            }
        
        static void Main(string[] args)
        {
            List<Vare> lagerbeholdning = new List<Vare>();

            bool kører = true;
            while (kører)
            {
                Console.WriteLine("Elektronik Lager A/S : lager program");
                Console.WriteLine("tast 1 : tilføj vare");
                Console.WriteLine("tast 2 : rediger lager beholdningen");
                Console.WriteLine("tast 3 : fjerne vare");
                Console.WriteLine("tast 4 : se oversigt over lager beholdningen");
                Console.WriteLine("tast 5 : luk og gem lager");
                Console.WriteLine("tast 6 : clear");

                Console.Write("indtast : ");
                string uservalg = Console.ReadLine();


                if (int.TryParse(uservalg, out int menuvalg))
                {
                    switch (menuvalg)
                    {
                        case 1: //tilføj vare menu
                            Console.WriteLine("indtast varenummer : ");
                            int varenummer = int.Parse(Console.ReadLine());

                            Console.WriteLine("indtast varenavn : ");
                            string varenavn = Console.ReadLine();

                            Console.WriteLine("indtast antal : ");
                            int vareantal = int.Parse(Console.ReadLine());

                            Console.WriteLine("indtast vareplacering : ");
                            string vareplacering = Console.ReadLine();

                            Vare nyVare = new Vare(varenummer, varenavn, vareantal, vareplacering);


                            lagerbeholdning.Add(nyVare);
                            Console.WriteLine("du har tilføjet : " + nyVare.ToString());
                            break;

                        case 2: //rediger lager beholdning
                            Console.WriteLine("indtast varenummer på den vare, du ønsker at redigere : ");
                            int redigerVarenummer = int.Parse(Console.ReadLine());

                            Vare vareAtRedigere = lagerbeholdning.Find(v => v.Varenummer == redigerVarenummer); 
                            //med det gemte varenummer, som vi ønsker at tilgå listens index og ændre i det gemte varenummers objekt. Så bruger vi varenummeret til at finde indexs objektet gemt under varenummeret.
                            //(v => v.Varenummer == redigerVarenummer); Lambda expression under Annoymuous functions. Det vil sige at input => expression; 

                            if (vareAtRedigere != null)
                            {
                                Console.WriteLine("indtast opdateret varenummer : "); //bruger input, hvor der vælges hvilken vare der skal ændres i
                                vareAtRedigere.Varenummer = int.Parse(Console.ReadLine()); /*variablen vareatredigere, er defineret af det readline input fra brugeren.
                                Som den bruger en .find funktion til at søge i listen lagerbeholdning. Det skal den finde heri, er varenummer variablen fra den kender fra case 1 input.
                                Således henviser vi til at den indtastede input, der ændres til int / heltal via Parse. Således kan vi finde og tilgå listens index for varenummer eks. 8517.
                                Når vi så finder og tilgår indexet ønskede objekt, kan vi ændre værdien varenummer.
                                */

                                Console.WriteLine("indtast opdateret varenavn : ");
                                vareAtRedigere.Varenavn = Console.ReadLine(); //som ovenstående, finder vi varenavnet i samme indtastede objekt i listens index, som vi kan manipulere og gemme igen.

                                Console.WriteLine("indtast opdateret antal : ");
                                vareAtRedigere.Antal = int.Parse(Console.ReadLine()); //som ovenstående, finder vi vareantal i samme indtastede objekt i listens index, som vi kan manipulere og gemme igen.

                                Console.WriteLine("indtast opdateret placering : ");
                                vareAtRedigere.Placering = Console.ReadLine(); ////som ovenstående, finder vi varenplacering i samme indtastede objekt i listens index, som vi kan manipulere og gemme igen.

                                Console.WriteLine("Varen er nu opdateret : " + vareAtRedigere.ToString());
                                //nu er listen opdateret og således er vores lagerbeholdning også ændret og opdateret
                            }

                            else
                            {
                                Console.WriteLine("vare ikke fundet, er du sikker på det er korrekt indtastet?");
                            }

                            break;

                        case 3: //fjerne vare
                            Console.WriteLine("indtast varenummer på den vare, du ønsker at fjerne : ");
                            int fjernVaren = int.Parse(Console.ReadLine()); //variablen fjernVaren oprettes, som en separat variabel, for at separere list index indholdet

                            int IndexAtFjerne = lagerbeholdning.FindIndex(v => v.Varenummer == fjernVaren);
                            //vi benytter denne gang findIndex, for at finde et objekt i listen lagerbeholdning som matcher den indtastede værdi i variabel fjernVaren.

                            if (IndexAtFjerne != -1) //hvis denne så findes, jamen så sletter vi index objektet der forbindes med varenummer.
                            {
                                lagerbeholdning.RemoveAt(IndexAtFjerne); //removeAt er en lignende funktion som FindIndex, hvor den fjerner det indhold i den referede list objekt.
                                Console.WriteLine("varenummer : " + fjernVaren + "fjernet");
                            }

                            else
                            {
                                Console.WriteLine("varenummer ikke fundet, stavede du korrekt? Eller måske varen allerede er fjernet.");
                                //tilfælde af fejl indtastning. Mulighed for måske bedre bruger dummy proofing.
                            }

                            break;

                        case 4: //se lager beholdning
                            if (lagerbeholdning.Count > 0)
                            {
                                Console.WriteLine("lagerbeholdning : ");
                                foreach (var vare in lagerbeholdning)
                                {
                                    Console.WriteLine(vare.ToString());
                                }
                            }

                            else
                            {
                                Console.WriteLine("Lageret er tomt.");
                            }

                            break;

                        case 5: //lige pt. lukker den blot programmet, her vil det være muligt at tilføje en gem index indholds feature i videreudvikling. Evt. gem til .txt fil.
                            Console.WriteLine("Lukker programmet. Tak for idag :)");
                            kører = false;
                            break;

                        case 6:
                            Console.Clear(); //console.Clear bruges måske til at clear forrige inputs, så konsollen bliver holdt clean
                            break;

                        default:
                            Console.WriteLine("ugyldigt valg, prøv igen");
                            break;

                    }
                }

                else
                {
                    Console.WriteLine("ugyldigt input, prøv igen");
                }
                
            }
        }
    }
}
