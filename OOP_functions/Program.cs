using System;
using System.Collections.Generic;

namespace OOP_functions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== KNIHOVNA MATEMATICKÝCH FUNKCÍ ===\n");

            // Vytvoření seznamu různých funkcí
            List<MathFunction> functions = new List<MathFunction>
            {
                new LinearFunction(2, 3),                    // 1. f(x) = 2x + 3
                new LinearFunction(-1, 5),                   // 2. f(x) = -x + 5
                new AbsoluteLinearFunction(1, -4),           // 3. f(x) = |x - 4|
                new RationalLinearFunction(1, 2, 1, -3),     // 4. f(x) = (x + 2) / (x - 3)
                new QuadraticFunction(1, -4, 3),             // 5. f(x) = x² - 4x + 3
                new QuadraticFunction(-2, 4, 1)              // 6. f(x) = -2x² + 4x + 1
            };

            bool pokracovat = true;

            while (pokracovat)
            {
                Console.WriteLine("\nDostupné funkce:");
                Console.WriteLine(new string('-', 80));

                for (int i = 0; i < functions.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {functions[i].Name}: {functions[i].Description}");
                }

                Console.WriteLine(new string('-', 80));

                // Výběr funkce
                int vybranaFunkce = 0;
                bool platnyVstup = false;

                while (!platnyVstup)
                {
                    Console.Write($"\nZadejte číslo funkce (1-{functions.Count}): ");
                    string vstup = Console.ReadLine();

                    if (int.TryParse(vstup, out vybranaFunkce) && vybranaFunkce >= 1 && vybranaFunkce <= functions.Count)
                    {
                        platnyVstup = true;
                    }
                    else
                    {
                        Console.WriteLine($"Neplatný vstup! Zadejte číslo od 1 do {functions.Count}.");
                    }
                }

                // Zadání hodnoty x
                double x = 0;
                platnyVstup = false;

                while (!platnyVstup)
                {
                    Console.Write("Zadejte hodnotu x: ");
                    string vstup = Console.ReadLine();

                    if (double.TryParse(vstup, out x))
                    {
                        platnyVstup = true;
                    }
                    else
                    {
                        Console.WriteLine("Neplatný vstup! Zadejte číselnou hodnotu.");
                    }
                }

                Console.WriteLine("\n" + new string('=', 80));

                // Výpočet a zobrazení výsledku
                MathFunction selectedFunction = functions[vybranaFunkce - 1];
                selectedFunction.PrintInfo();

                double result = selectedFunction.Calculate(x);

                if (double.IsNaN(result))
                {
                    Console.WriteLine($"\nf({x}) = nedefinováno (dělení nulou)");
                }
                else
                {
                    Console.WriteLine($"\nf({x}) = {result}");
                }

                // Vypíšeme rozšiřující vlastnosti
                selectedFunction.PrintExtendedProperties();

                Console.WriteLine(new string('=', 80));

                // Zeptat se, zda chce pokračovat
                Console.Write("\nChcete vypočítat další funkci? (a/n): ");
                string odpoved = Console.ReadLine()?.ToLower();
                pokracovat = (odpoved == "a" || odpoved == "ano" || odpoved == "y" || odpoved == "yes");
            }

            Console.WriteLine("\n=== KONEC ===");
        }
    }
}
