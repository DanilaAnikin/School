using System.IO;
using System.Text;
using System.Globalization;

namespace PraceSTextovymiSoubory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* // <-- stačí když jednu ze dvou * smažete a celá studijní část se zakomentuje (a naopak)
            #region Studijní část
            // 1. OTEVŘENÍ SOUBORU
            // Relativní cesta: @"..\vstupy\1.txt"
            // Absolutní cesta: @"C:\Users\...\vstup.txt"

            // 2. ČTENÍ ZE SOUBORU
            // File - pro menší soubory
            string text = File.ReadAllText("data.txt");
            string[] lines = File.ReadAllLines("data.txt");

            // StreamReader - pro větší soubory
            using (StreamReader sr = new StreamReader(@"vstupy\0_vstup.txt"))
            {
                char prvniZnak = (char)sr.Read();
                string zbytekPrvniRadky = sr.ReadLine();
                string zbytekSouboru = sr.ReadToEnd();
            }

            // 3. ZÁPIS DO SOUBORU
            // File
            File.WriteAllText("vystup.txt", "Ahoj světe!\nToto je nový soubor.");
            File.AppendAllText("vystup.txt", "Přidáváme další řádku.\n");
            string[] radky = { "První řádek", "Druhý řádek", "Třetí řádek" };
            File.WriteAllLines("vystup.txt", radky);
            string[] dalsiRadky = { "Čtvrtý řádek", "Pátý řádek" };
            File.AppendAllLines("vystup.txt", dalsiRadky);

            // StreamWriter
            using (StreamWriter sw = new StreamWriter("vystup.txt", false)) // false -> PŘEPISOVÁNÍ, true -> PŘIPISOVÁNÍ
            {
                sw.Write("Ahoj");
                sw.WriteLine("Toto je nová řádka");
            }

            // 4. OŠETŘENÍ VÝJIMEK
            try
            {
                using (StreamReader sr = new StreamReader(@"vstupy\0_vstup.txt"))
                {
                    // čtení
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Soubor nebyl nalezen: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Nemáš oprávnění: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Chyba vstupu/výstupu: " + ex.Message);
            }
            catch
            {
                Console.WriteLine("Nastala nějaká chyba");
            }
            #endregion
            /**/

            #region Praktická část

            // (10b) 1. Jaký je počet znaků v souboru 1.txt a jaký v 2.txt?
            // Zkontrolujte s VS Code a vysvětlete rozdíly.
            using (StreamWriter sw = new StreamWriter(@"../vstupni_soubory/2.txt"))
            {
                sw.WriteLine("Ahoj \tsvěte!\n");
            }

            string soubor1 = File.ReadAllText(@"../vstupni_soubory/1.txt");
            string soubor2 = File.ReadAllText(@"../vstupni_soubory/2.txt");

            Console.WriteLine($"Počet znaků v 1.txt: {soubor1.Length}");
            Console.WriteLine($"Počet znaků v 2.txt: {soubor2.Length}");

            Console.WriteLine("\nZnaky v 1.txt:");
            for (int i = 0; i < soubor1.Length; i++)
            {
                Console.WriteLine($"[{i}]: '{soubor1[i]}' (ASCII: {(int)soubor1[i]})");
            }

            Console.WriteLine("\nZnaky v 2.txt:");
            for (int i = 0; i < soubor2.Length; i++)
            {
                Console.WriteLine($"[{i}]: '{soubor2[i]}' (ASCII: {(int)soubor2[i]})");
            }

            /* ODPOVĚĎ:
             * 1.txt: 13 znaků ("Ahoj\tsvěte!\n")
             * 2.txt: 16 znaků ("Ahoj \tsvěte!\n\r\n")
             *
             * Rozdíly:
             * - 2.txt má mezeru po "Ahoj" (+1 znak)
             * - Windows použije \r\n místo jen \n (+1 znak)
             * - WriteLine() přidá další \r\n k již existujícímu \n v řetězci (+2 znaky)
             */


            // (10b) 2. Jaký je počet znaků v souboru 1.txt, když pomineme bílé znaky?
            string text1 = File.ReadAllText(@"../vstupni_soubory/1.txt");
            int pocetNebilichZnaku = 0;
            foreach (char c in text1)
            {
                if (!char.IsWhiteSpace(c))
                {
                    pocetNebilichZnaku++;
                }
            }
            Console.WriteLine($"Počet nebílých znaků v souboru 1.txt: {pocetNebilichZnaku}");
            // Odpověď: 11 znaků


            // (5b) 3. Jaké znaky (vypište jako integery) jsou použity pro oddělení řádků v souboru 3.txt?
            // Porovnejte s 4.txt a 5.txt. Jakým znakům odpovídají v ASCII tabulce?
            using (StreamWriter sw = new StreamWriter(@"../vstupni_soubory/4.txt"))
            {
                sw.WriteLine("1");
                sw.WriteLine("2");
                sw.WriteLine("3");
            }
            using (StreamWriter sw = new StreamWriter(@"../vstupni_soubory/5.txt"))
            {
                sw.Write("1\n2\n3");
            }

            // ODPOVĚĎ:
            // 3.txt a 4.txt: \r\n (CR LF) = znaky 13 a 10 (Windows)
            // 5.txt: \n (LF) = znak 10 (Unix/Linux)
            // WriteLine() používá Environment.NewLine (na Windows \r\n)



            // (10b) 4. Kolik slov má soubor 6.txt?
            // Za slovo považujme neprázdnou souvislou posloupnost nebílých znaků oddělené bílými.
            string text6 = File.ReadAllText(@"../vstupni_soubory/6.txt");
            string[] slova6 = text6.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
            int pocetSlov = slova6.Length;
            Console.WriteLine($"Počet slov v souboru 6.txt: {pocetSlov}");
            // Odpověď: 52 slov


            // (15b) 5. Zapište do souboru 7.txt slovo "řeřicha". Povedlo se?
            // Vypište obsah souboru do konzole. V čem je u konzole problém a jak ho spravit?
            // Jaké kódování používá C#? Kolik bytů na znak?

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            using (StreamWriter sw = new StreamWriter(@"../vstupni_soubory/7.txt", false, System.Text.Encoding.UTF8))
            {
                sw.Write("řeřicha");
            }

            string obsahSouboru7 = File.ReadAllText(@"../vstupni_soubory/7.txt", System.Text.Encoding.UTF8);
            Console.WriteLine($"Obsah souboru 7.txt: {obsahSouboru7}");

            // ODPOVĚĎ:
            // C# používá UTF-16 v paměti (2 byty na znak, speciální znaky 4 byty)
            // StreamWriter defaultně používá UTF-8 (ASCII 1 byte, české znaky 2 byty)
            // Problém konzole: Windows používá CP-852/CP-1250 - řešení: Console.OutputEncoding = UTF8



            // (25b) 6. Vypište četnosti jednotlivých slov v souboru 8.txt do souboru 9.txt ve formátu slovo:četnost na samostatný řádek.
            // Slova očistěte od diakritiky a převeďte na malá písmena.

            static string RemoveDiacritics(string text)
            {
                string normalized = text.Normalize(NormalizationForm.FormD);
                StringBuilder sb = new StringBuilder();
                foreach (char c in normalized)
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                        sb.Append(c);
                }
                return sb.ToString().Normalize(NormalizationForm.FormC);
            }

            string text8 = File.ReadAllText(@"../vstupni_soubory/8.txt");
            string[] words = text8.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> slova = new Dictionary<string, int>();

            foreach (string word in words)
            {
                string cleanWord = word.Trim('.', ',', '!', '?', ';', ':', '"', '\'', '(', ')');
                cleanWord = cleanWord.ToLower();
                cleanWord = RemoveDiacritics(cleanWord);

                if (!string.IsNullOrWhiteSpace(cleanWord))
                {
                    if (slova.ContainsKey(cleanWord))
                    {
                        slova[cleanWord]++;
                    }
                    else
                    {
                        slova[cleanWord] = 1;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(@"../vstupni_soubory/9.txt"))
            {
                foreach (var pair in slova)
                {
                    sw.WriteLine($"{pair.Key}:{pair.Value}");
                }
            }


            // (+15b) Bonus: Vypište četnosti jednotlivých znaků abecedy (malá a velká písmena) v souboru 8.txt do konzole.
            string textBonus = File.ReadAllText(@"../vstupni_soubory/8.txt");
            Dictionary<char, int> cetnostiZnaku = new Dictionary<char, int>();

            foreach (char znak in textBonus)
            {
                if (char.IsLetter(znak))
                {
                    if (cetnostiZnaku.ContainsKey(znak))
                    {
                        cetnostiZnaku[znak]++;
                    }
                    else
                    {
                        cetnostiZnaku[znak] = 1;
                    }
                }
            }

            Console.WriteLine("Četnosti písmen v souboru 8.txt:");
            foreach (var pismeno in cetnostiZnaku)
            {
                Console.WriteLine($"{pismeno.Key}: {pismeno.Value}");
            }

            #endregion
        }
    }
}
