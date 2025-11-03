using System;
using System.Collections.Generic;
using System.Linq;

// Abstraktní základní třída pro všechny postavy
abstract class Character
{
    public string Name { get; protected set; }
    public int Health { get; protected set; }
    public int Power { get; set; }

    public Character(string name, int health, int power)
    {
        Name = name;
        Health = health;
        Power = power;
    }

    public abstract void Attack(Character target);

    public virtual void TakeDamage(int amount, Character? attacker = null)
    {
        Health -= amount;
        if (Health < 0) Health = 0;
    }

    public bool IsAlive => Health > 0;

    public override string ToString()
    {
        return $"{this.GetType().Name} {Name} ({Health}/{Power})";
    }
}

// Čaroděj - odráží poloviční poškození zpět na útočníka
class Wizard : Character
{
    private const int BASE_HEALTH = 80;
    private const int BASE_POWER = 25;

    public Wizard(string name) : base(name, BASE_HEALTH, BASE_POWER)
    {
    }

    public override void Attack(Character target)
    {
        Console.WriteLine($"{Name}: Abrakadabra! Magická rána!");
        target.TakeDamage(Power, this);
    }

    public override void TakeDamage(int amount, Character? attacker = null)
    {
        base.TakeDamage(amount, attacker);

        // Odraz polovičního poškození zpět na útočníka
        if (attacker != null && attacker.IsAlive)
        {
            int reflectedDamage = amount / 2;
            Console.WriteLine($"  -> {Name} odráží {reflectedDamage} poškození zpět na {attacker.Name}!");
            attacker.TakeDamage(reflectedDamage, null); // Neodrážíme rekurzivně
        }
    }
}

// Bojovník
class Warrior : Character
{
    private const int BASE_HEALTH = 120;
    private const int BASE_POWER = 20;

    public Warrior(string name) : base(name, BASE_HEALTH, BASE_POWER)
    {
    }

    public override void Attack(Character target)
    {
        Console.WriteLine($"{Name}: Za slávu! Mečem vpřed!");
        target.TakeDamage(Power, this);
    }
}

// Lučištník
class Archer : Character
{
    private const int BASE_HEALTH = 90;
    private const int BASE_POWER = 22;

    public Archer(string name) : base(name, BASE_HEALTH, BASE_POWER)
    {
    }

    public override void Attack(Character target)
    {
        Console.WriteLine($"{Name}: Šíp letí! Na cíl!");
        target.TakeDamage(Power, this);
    }
}

class Program
{
    static Random random = new Random();

    static void Main(string[] args)
    {
        Console.WriteLine("=== BITVA ARMÁD ===\n");

        // Vytvoření dvou armád
        List<Character> army1 = CreateArmy("Armáda Severu");
        List<Character> army2 = CreateArmy("Armáda Jihu");

        Console.WriteLine("\n--- Armáda 1 ---");
        PrintArmy(army1);

        Console.WriteLine("\n--- Armáda 2 ---");
        PrintArmy(army2);

        // Simulace bitvy
        int round = 1;
        while (army1.Count > 0 && army2.Count > 0)
        {
            Console.WriteLine($"\n\n=== KOLO {round} ===");

            // Bojují postavy na stejných indexech
            int battleCount = Math.Min(army1.Count, army2.Count);

            for (int i = 0; i < battleCount; i++)
            {
                Character char1 = army1[i];
                Character char2 = army2[i];

                Console.WriteLine($"\n[Souboj {i + 1}] {char1} vs {char2}");

                // Char1 útočí na char2
                char1.Attack(char2);

                // Pokud char2 zahynul, char1 získává +1 Power
                if (!char2.IsAlive)
                {
                    Console.WriteLine($"  -> {char2.Name} byl poražen!");
                    char1.Power++;
                    Console.WriteLine($"  -> {char1.Name} zvyšuje sílu na {char1.Power}!");
                }
                else if (char2.IsAlive)
                {
                    // Char2 útočí na char1 (pokud ještě žije)
                    char2.Attack(char1);

                    // Pokud char1 zahynul, char2 získává +1 Power
                    if (!char1.IsAlive)
                    {
                        Console.WriteLine($"  -> {char1.Name} byl poražen!");
                        char2.Power++;
                        Console.WriteLine($"  -> {char2.Name} zvyšuje sílu na {char2.Power}!");
                    }
                }
            }

            // Odstranění mrtvých postav z armád
            army1.RemoveAll(c => !c.IsAlive);
            army2.RemoveAll(c => !c.IsAlive);

            Console.WriteLine($"\n--- Stav po kole {round} ---");
            Console.WriteLine($"Armáda 1: {army1.Count} bojovníků");
            Console.WriteLine($"Armáda 2: {army2.Count} bojovníků");

            round++;
        }

        // Vyhlášení vítěze
        Console.WriteLine("\n\n=== KONEC BITVY ===");
        if (army1.Count > 0)
        {
            Console.WriteLine("🏆 Vítězí Armáda 1!");
            Console.WriteLine("\nPřeživší bojovníci:");
            PrintArmy(army1);
        }
        else if (army2.Count > 0)
        {
            Console.WriteLine("🏆 Vítězí Armáda 2!");
            Console.WriteLine("\nPřeživší bojovníci:");
            PrintArmy(army2);
        }
        else
        {
            Console.WriteLine("⚔️ Remíza! Obě armády padly!");
        }
    }

    static List<Character> CreateArmy(string armyPrefix)
    {
        List<Character> army = new List<Character>();

        // Náhodný počet čarodějů (1-3)
        int wizardCount = random.Next(1, 4);

        // Náhodný počet bojovníků (2-5)
        int warriorCount = random.Next(2, 6);

        // Zbytek jsou lučištníci (aby bylo celkem 10)
        int archerCount = 10 - wizardCount - warriorCount;

        // Vytvoření čarodějů
        for (int i = 0; i < wizardCount; i++)
        {
            army.Add(new Wizard($"{armyPrefix}_Wizard{i + 1}"));
        }

        // Vytvoření bojovníků
        for (int i = 0; i < warriorCount; i++)
        {
            army.Add(new Warrior($"{armyPrefix}_Warrior{i + 1}"));
        }

        // Vytvoření lučištníků
        for (int i = 0; i < archerCount; i++)
        {
            army.Add(new Archer($"{armyPrefix}_Archer{i + 1}"));
        }

        // Zamíchání armády pro náhodné rozložení
        return army.OrderBy(x => random.Next()).ToList();
    }

    static void PrintArmy(List<Character> army)
    {
        for (int i = 0; i < army.Count; i++)
        {
            Console.WriteLine($"  [{i}] {army[i]}");
        }
    }
}
