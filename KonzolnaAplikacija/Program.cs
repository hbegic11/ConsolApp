using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Student
    {
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public string? BrojIndeksa { get; set; }
        public List<int> Ocjene { get; set; } = new List<int>();
        public double Prosjek => Ocjene.Count > 0 ? Ocjene.Average() : 0.0;
    }

    class Program
    {
        static List<Student> studenti = new List<Student>();

        static void Main(string[] args)
        {
            int izbor = -1;
            while (true)
            {
                Console.Clear();
                PrikaziMeni();

                string? unos = Console.ReadLine();
                if (!int.TryParse(unos, out izbor) || izbor < 0 || izbor > 6)
                {
                    Console.WriteLine("Pogrešan unos, unesite broj od 0 do 6.");
                    Console.WriteLine("Pritisnite enter za nastavak...");
                    Console.ReadLine();
                    continue;
                }

                switch (izbor)
                {
                    case 1:
                        DodajStudenta();
                        break;
                    case 2:
                        DodajOcjenu();
                        break;
                    case 3:
                        PrikaziSveStudente();
                        break;
                    case 4:
                        PrikaziProsjek();
                        break;
                    case 5:
                        ObrisiStudenta();
                        break;
                    case 6:
                        PretraziStudenta();
                        break;
                    case 0:
                        Console.WriteLine("\nPritisni enter za izlaz...");
                        Console.ReadLine();
                        return;
                }

                Console.WriteLine("Pritisnite enter za nastavak...");
                Console.ReadLine();
            }
        }

        static void PrikaziMeni()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("     SISTEM ZA UPRAVLJANJE OCJENAMA");
            Console.WriteLine("===========================================");
            Console.WriteLine();
            Console.WriteLine("1. Dodaj novog studenta");
            Console.WriteLine("2. Dodaj ocjenu studentu");
            Console.WriteLine("3. Prikazi sve studente");
            Console.WriteLine("4. Prikazi prosjecne ocjene");
            Console.WriteLine("5. Obrisi studenta");
            Console.WriteLine("6. Pretrazi po indeksu");
            Console.WriteLine("0. Izlaz iz programa");
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Ukupno studenata: {studenti.Count} | Prosječna ocjena: {IzracunajUkupniProsjek():0.00}");
            Console.WriteLine("--------------------------------------------");
            Console.Write("Unesite izbor (0-6): ");
        }

        static void DodajStudenta()
        {
            Console.Write("Ime: ");
            string? ime = Console.ReadLine();

            Console.Write("Prezime: ");
            string? prezime = Console.ReadLine();

            Console.Write("Broj indeksa: ");
            string? indeks = Console.ReadLine();

            if (!SadrziSamoSlova(ime) || !SadrziSamoSlova(prezime))
            {
                Console.WriteLine("Ime i prezime mogu sadržavati samo slova, razmake ili crtice.");
                return;
            }

            if (!ValidanIndeks(indeks))
            {
                Console.WriteLine("Broj indeksa nije validan. Dozvoljeni su samo brojevi, crtice i slova na kraju.");
                return;
            }

            if (studenti.Any(s => s.BrojIndeksa == indeks))
            {
                Console.WriteLine("Student sa ovim brojem indeksa već postoji.");
                return;
            }

            studenti.Add(new Student
            {
                Ime = ime,
                Prezime = prezime,
                BrojIndeksa = indeks
            });

            Console.WriteLine("Student uspješno dodan.");
        }

        static bool SadrziSamoSlova(string? tekst)
        {
            return !string.IsNullOrWhiteSpace(tekst) && tekst.All(c => char.IsLetter(c) || c == ' ' || c == '-');
        }

        static bool ValidanIndeks(string? indeks)
        {
            if (string.IsNullOrWhiteSpace(indeks))
                return false;
            if (indeks.Contains(" "))
                return false;
            if (!char.IsDigit(indeks[0]))
                return false;
            return indeks.All(c => char.IsLetterOrDigit(c) || c == '-');
        }

        static void DodajOcjenu()
        {
            Console.Write("Unesite broj indeksa studenta: ");
            string? indeks = Console.ReadLine();

            Student? student = studenti.FirstOrDefault(s => s.BrojIndeksa == indeks);
            if (student == null)
            {
                Console.WriteLine("Student sa ovim brojem indeksa ne postoji.");
                return;
            }

            Console.Write("Unesite ocjenu (5-10): ");
            if (int.TryParse(Console.ReadLine(), out int ocjena))
            {
                if (ocjena < 5 || ocjena > 10)
                {
                    Console.WriteLine("Ocjena mora biti između 5 i 10.");
                    return;
                }
                student.Ocjene.Add(ocjena);
                Console.WriteLine("Ocjena uspješno dodana.");
            }
            else
            {
                Console.WriteLine("Pogrešan unos. Unesite broj između 5 i 10.");
            }
        }

        static void PrikaziSveStudente()
        {
            if (studenti.Count == 0)
            {
                Console.WriteLine("Nema unešenih studenata.");
                return;
            }

            Console.WriteLine("\n--- Lista studenata: ---");
            foreach (var student in studenti)
            {
                Console.WriteLine($"{student.Ime} {student.Prezime} | {student.BrojIndeksa} | Ocjene: {string.Join(", ", student.Ocjene)}");
            }
            Console.WriteLine("--------------------------------------------");
        }

        static void PrikaziProsjek()
        {
            if (studenti.Count == 0)
            {
                Console.WriteLine("Nema unešenih studenata.");
                return;
            }

            Console.WriteLine("\n--- Prosječne ocjene studenata: ---");
            foreach (var student in studenti)
            {
                Console.WriteLine($"{student.Ime} {student.Prezime} | {student.BrojIndeksa} | Prosjek: {student.Prosjek:0.00}");
            }
            Console.WriteLine("--------------------------------------------");
        }

        static void ObrisiStudenta()
        {
            Console.Write("Unesite broj indeksa studenta koji želite obrisati: ");
            string? indeks = Console.ReadLine();

            Student? student = studenti.FirstOrDefault(s => s.BrojIndeksa == indeks);
            if (student != null)
            {
                studenti.Remove(student);
                Console.WriteLine("Student uspješno obrisan.");
            }
            else
            {
                Console.WriteLine("Student sa ovim brojem indeksa ne postoji.");
            }
        }

        static void PretraziStudenta()
        {
            Console.Write("Unesite broj indeksa studenta: ");
            string? indeks = Console.ReadLine();

            Student? student = studenti.FirstOrDefault(s => s.BrojIndeksa == indeks);
            if (student != null)
            {
                Console.WriteLine($"{student.Ime} {student.Prezime} | Ocjene: {string.Join(", ", student.Ocjene)} | Prosjek: {student.Prosjek:0.00}");
            }
            else
            {
                Console.WriteLine("Student nije pronađen.");
            }
        }

        static double IzracunajUkupniProsjek()
        {
            var sveOcjene = studenti.SelectMany(s => s.Ocjene);
            return sveOcjene.Any() ? sveOcjene.Average() : 0.0;
        }
    }
}