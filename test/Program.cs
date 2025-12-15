using System;
using System.Collections.Generic;
using System.IO;

class Program {
    // Smery pohybu krale (8 smeru)
    static int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
    static int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

    // Vstupni bod programu - nacte vsechny soubory a zpracuje je
    static void Main(string[] args) {
        string[] soubory = Directory.GetFiles("vstupni_soubory", "*.txt");
        Array.Sort(soubory);

        foreach (string soubor in soubory) {
            Console.WriteLine($"=== {Path.GetFileName(soubor)} ===");
            
            ZpracujSoubor(soubor);
            Console.WriteLine();
        }
    }

    // Parsuje vstupni soubor a spusti hledani cesty
    static void ZpracujSoubor(string cesta) {
        string[] radky = File.ReadAllLines(cesta);
        int index = 0;
        int pocetPrekazek = int.Parse(radky[index++]);
        bool[,] prekazky = new bool[8, 8];

        for (int i = 0; i < pocetPrekazek; i++) {
            string[] souradnice = radky[index++].Split(' ');
            
            int x = int.Parse(souradnice[0]);
            int y = int.Parse(souradnice[1]);
            
            prekazky[x, y] = true;
        }

        string[] startParts = radky[index++].Split(' ');
        
        int startX = int.Parse(startParts[0]);
        int startY = int.Parse(startParts[1]);
        
        string[] cilParts = radky[index++].Split(' ');
        int cilX = int.Parse(cilParts[0]);
        
        
        int cilY = int.Parse(cilParts[1]);
        int vysledek = NajdiNejkratsiCestu(prekazky, startX, startY, cilX, cilY);

        if (vysledek == -1) {
            Console.WriteLine("Do cile se kralem nejde dostat.");
        } else {
            Console.WriteLine(vysledek);
        }
    }

    // BFS algoritmus pro nalezeni nejkratsi cesty krale
    static int NajdiNejkratsiCestu(bool[,] prekazky, int startX, int startY, int cilX, int cilY) {
        // Uz jsme v cili
        if (startX == cilX && startY == cilY) {
            return 0; // JOOOO VYHRAAAAAAAA!!!
        }

        
        // Start nebo cil jsou na prekazce
        if (prekazky[startX, startY] || prekazky[cilX, cilY]) {
            return -1;
        }

        bool[,] navstiveno = new bool[8, 8];
        Queue<(int x, int y, int kroky)> fronta = new Queue<(int, int, int)>();
        fronta.Enqueue((startX, startY, 0));
        navstiveno[startX, startY] = true;
        

        while (fronta.Count > 0) {
            var (x, y, kroky) = fronta.Dequeue();
            
            for (int i = 0; i < 8; i++) {
                int novyX = x + dx[i];
                int novyY = y + dy[i];
                
                if (novyX >= 0 && novyX < 8 && novyY >= 0 && novyY < 8 && !prekazky[novyX, novyY] && !navstiveno[novyX, novyY]) {
                    // TADY JE CIIIIL
                    if (novyX == cilX && novyY == cilY) {
                        return kroky + 1;
                    }
                    navstiveno[novyX, novyY] = true;
                    fronta.Enqueue((novyX, novyY, kroky + 1));
                }
            }
        
        }
        
        

        return -1; // Cesta do cile neexistuje
    }
}
