# Minimax - Hra Nim

## Popis programu

Program implementuje hru Nim, kde proti sobě hraje počítač a lidský hráč. Počítač používá minimax algoritmus s alfa-beta prořezáváním k výběru optimálního tahu.

## Pravidla hry Nim

- Na stole leží N hromádek sirek
- Hráči se střídají po tazích
- V každém tahu smí hráč vzít **1-2 sirky** z **jedné** hromádky
- **Vyhrává ten, který už nemá co odebrat** (Misère Nim)

## Průběh algoritmu minimax

### Základní princip

Algoritmus rekurzivně prohledává strom všech možných tahů:

1. **Maximalizující hráč (počítač)** - snaží se maximalizovat své skóre
2. **Minimalizující hráč (člověk)** - snaží se minimalizovat skóre počítače
3. Pro každý možný tah simuluje všechny následné tahy až do konce hry nebo maximální hloubky

### Terminální stavy

- Když už nejsou žádné sirky, hráč na tahu **vyhrává** (+1000/-1000 bodů)
- Algoritmus propaguje toto skóre zpět stromem

### Ohodnocovací funkce (bonus)

Když nedosáhneme listu stromu (depth = 0):

- Používá **Nim-sum** (XOR všech hromádek)
- Nim-sum = 0 → špatná pozice (-10/+10 bodů)
- Nim-sum ≠ 0 → dobrá pozice (+10/-10 bodů)

### Alfa-beta prořezávání (bonus)

Optimalizace, která ořezává větve stromu:

- **Alpha** - nejlepší skóre pro maximalizujícího hráče
- **Beta** - nejlepší skóre pro minimalizujícího hráče
- Pokud `beta ≤ alpha`, větev se neprohledává dál (úspora výpočtů)

### Výběr nejlepšího tahu

1. Na nejvyšší úrovni rekurze projde všechny možné tahy
2. Pro každý tah zavolá minimax a získá jeho ohodnocení
3. Vybere tah s nejvyšším skóre
4. Aktualizuje `bestPile` a `matchesToRemove`

## Nastavení hry

V `Main()` metodě lze nastavit:

```csharp
var initialPiles = new List<int> { 3, 4, 5 }; // počáteční hromádky
bool botStarts = true; // kdo začíná
```

## Spuštění

```bash
cd Minimax
dotnet run
```

## Příklad hry

```
Aktuální stav hry:
3 4 5
Počítač bere 2 sirky z hromádky 1

Aktuální stav hry:
3 2 5
Z které hromádky chcete brát? (0 1 2 )
```
