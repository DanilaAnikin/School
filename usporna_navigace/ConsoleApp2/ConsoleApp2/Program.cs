using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        try
        {
            // Načteme si počet měst a silnic
            string[] firstLine = Console.ReadLine().Split(' ');
            if (firstLine.Length != 2)
            {
                Console.WriteLine("Neplatný vstup.");
                return;
            }

            int M = int.Parse(firstLine[0]);
            int S = int.Parse(firstLine[1]);

            if (M <= 0)
            {
                Console.WriteLine("Neplatný vstup.");
                return;
            }

            if (S < 0)
            {
                Console.WriteLine("Neplatný vstup.");
                return;
            }

            // Graf jako seznam sousedů
            List<(int neighbor, int length, int paid)>[] graph = new List<(int, int, int)>[M];
            for (int i = 0; i < M; i++)
            {
                graph[i] = new List<(int, int, int)>();
            }

            // Načteme si silnice
            for (int i = 0; i < S; i++)
            {
                string[] roadLine = Console.ReadLine().Split(' ');
                if (roadLine.Length != 4)
                {
                    Console.WriteLine("Neplatný vstup.");
                    return;
                }

                int city1 = int.Parse(roadLine[0]);
                int city2 = int.Parse(roadLine[1]);
                int length = int.Parse(roadLine[2]);
                int paid = int.Parse(roadLine[3]);

                if (city1 < 0 || city1 >= M || city2 < 0 || city2 >= M)
                {
                    Console.WriteLine("Neplatný vstup.");
                    return;
                }

                if (length <= 0)
                {
                    Console.WriteLine("Neplatný vstup.");
                    return;
                }

                if (paid != 0 && paid != 1)
                {
                    Console.WriteLine("Neplatný vstup.");
                    return;
                }

                graph[city1].Add((city2, length, paid));
                graph[city2].Add((city1, length, paid));
            }

            // Načteme si počáteční a cílové město
            string[] lastLine = Console.ReadLine().Split(' ');
            if (lastLine.Length != 2)
            {
                Console.WriteLine("Neplatný vstup.");
                return;
            }

            int start = int.Parse(lastLine[0]);
            int end = int.Parse(lastLine[1]);

            if (start < 0 || start >= M || end < 0 || end >= M)
            {
                Console.WriteLine("Neplatný vstup.");
                return;
            }

            // Dijkstra s omezením na max 1 placenou silnici
            // Stav: (město, počet_použitých_placených_silnic)
            int[,] dist = new int[M, 2]; // dist[město, počet_placených_silnic]
            (int city, int paidCount)[,] parent = new (int, int)[M, 2];
            bool[,] visited = new bool[M, 2];

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    dist[i, j] = int.MaxValue;
                    parent[i, j] = (-1, -1);
                    visited[i, j] = false;
                }
            }

            dist[start, 0] = 0;

            // Nejmenší složitost - O(n²)
            for (int iter = 0; iter < M * 2; iter++)
            {
                // Najdeme si nenavštívený uzel s nejmenší vzdáleností
                int minDist = int.MaxValue;
                int minCity = -1;
                int minPaid = -1;

                for (int city = 0; city < M; city++)
                {
                    for (int paid = 0; paid <= 1; paid++)
                    {
                        if (!visited[city, paid] && dist[city, paid] < minDist)
                        {
                            minDist = dist[city, paid];
                            minCity = city;
                            minPaid = paid;
                        }
                    }
                }

                if (minCity == -1) break;

                visited[minCity, minPaid] = true;

                foreach (var (neighbor, length, isPaid) in graph[minCity])
                {
                    int newPaidCount = minPaid + isPaid;
                    if (newPaidCount <= 1)
                    {
                        int newDist = dist[minCity, minPaid] + length;
                        if (newDist < dist[neighbor, newPaidCount])
                        {
                            dist[neighbor, newPaidCount] = newDist;
                            parent[neighbor, newPaidCount] = (minCity, minPaid);
                        }
                    }
                }
            }

            // Najdeme si nejlepší cestu do cíle
            int finalDist = Math.Min(dist[end, 0], dist[end, 1]);
            int finalPaid = dist[end, 0] <= dist[end, 1] ? 0 : 1;

            if (finalDist == int.MaxValue)
            {
                Console.WriteLine("Cesta neexistuje.");
                return;
            }

            // Rekonstrukce cesty
            List<int> path = new List<int>();
            int current = end;
            int currentPaid = finalPaid;

            while (current != -1)
            {
                path.Add(current);
                var (prevCity, prevPaid) = parent[current, currentPaid];
                current = prevCity;
                currentPaid = prevPaid;
            }
            path.Reverse();

            // Výpis
            Console.WriteLine(string.Join(" -> ", path));
            Console.WriteLine($"vzdálenost: {finalDist}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Neplatný vstup.");
        }
        catch (Exception)
        {
            Console.WriteLine("Neplatný vstup.");
        }
    }
}
