using System;
using System.Collections.Generic;
using System.Linq;

class StabilniManzelstvi
{
    static void Main()
    {
        // Načtení vstupu
        int n = int.Parse(Console.ReadLine());

        // Preference žen (indexováno od 0, ale preference jsou čísla od 1 do n)
        int[][] womenPrefs = new int[n][];
        for (int i = 0; i < n; i++)
        {
            womenPrefs[i] = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x) - 1) // převod na 0-indexed
                .ToArray();
        }

        // Preference mužů (indexováno od 0, ale preference jsou čísla od 1 do n)
        int[][] menPrefs = new int[n][];
        for (int i = 0; i < n; i++)
        {
            menPrefs[i] = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x) - 1) // převod na 0-indexed
                .ToArray();
        }

        // Vytvoření ranking tabulky pro muže (pro rychlé porovnání preferencí)
        // menRanking[m][w] = pořadí ženy w v preferencích muže m (0 = nejlepší)
        int[][] menRanking = new int[n][];
        for (int m = 0; m < n; m++)
        {
            menRanking[m] = new int[n];
            for (int rank = 0; rank < n; rank++)
            {
                int woman = menPrefs[m][rank];
                menRanking[m][woman] = rank;
            }
        }

        // Inicializace stavů
        int[] womanPartner = new int[n]; // partner ženy (nebo -1 pokud není spárovaná)
        int[] manPartner = new int[n];   // partner muže (nebo -1 pokud není spárován)
        int[] womanNextProposal = new int[n]; // index dalšího muže, kterému žena nabídne sňatek

        for (int i = 0; i < n; i++)
        {
            womanPartner[i] = -1;
            manPartner[i] = -1;
            womanNextProposal[i] = 0;
        }

        // Gale-Shapley algoritmus (ženy nabízejí)
        Queue<int> freeWomen = new Queue<int>();
        for (int i = 0; i < n; i++)
        {
            freeWomen.Enqueue(i);
        }

        while (freeWomen.Count > 0)
        {
            int woman = freeWomen.Dequeue();

            // Pokud žena ještě má komu nabídnout
            if (womanNextProposal[woman] < n)
            {
                // Další muž v seznamu této ženy
                int man = womenPrefs[woman][womanNextProposal[woman]];
                womanNextProposal[woman]++;

                if (manPartner[man] == -1)
                {
                    // Muž je volný, zasnoubení
                    womanPartner[woman] = man;
                    manPartner[man] = woman;
                }
                else
                {
                    // Muž už má partnerku
                    int currentPartner = manPartner[man];

                    // Porovnání preferencí muže
                    if (menRanking[man][woman] < menRanking[man][currentPartner])
                    {
                        // Muž preferuje novou ženu
                        womanPartner[currentPartner] = -1;
                        freeWomen.Enqueue(currentPartner); // předchozí partnerka se stává volnou

                        womanPartner[woman] = man;
                        manPartner[man] = woman;
                    }
                    else
                    {
                        // Muž preferuje současnou partnerku, žena zůstává volná
                        freeWomen.Enqueue(woman);
                    }
                }
            }
        }

        // Výstup: pro každou ženu (1 až n) vypíšeme jejího partnera
        for (int w = 0; w < n; w++)
        {
            Console.WriteLine(womanPartner[w] + 1); // převod zpět na 1-indexed
        }
    }
}
