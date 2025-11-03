# Beast in Labyrinth

Simulace příšery v bludišti, která se pohybuje podle pravidla pravé ruky (right-hand rule).

## Zadání

V bludišti reprezentovaném maticí políček se nachází příšera. Příšera je v každém kroku na jednom políčku a je otočená jedním ze čtyř možných směrů - nahoru, doprava, dolů nebo doleva.

V každém kroku výpočtu příšera udělá jeden tah, možné tahy jsou:
- Otočit se doprava
- Otočit se doleva
- Postupit kupředu (přejít na sousední políčko ve směru otočení)

Na začátku má příšera po pravé ruce zeď a pohybuje se tak, aby pravou rukou stále sledovala zeď.

### Vstupní formát

- První řádek: šířka bludiště
- Druhý řádek: výška bludiště
- Následující řádky: mapa bludiště

Znaky mapy:
- `X` - zeď
- `.` - volné políčko
- `^`, `>`, `v`, `<` - volné políčko s příšerou otočenou nahoru, doprava, dolů nebo doleva

### Výstupní formát

Program vypíše 20 kroků simulace. Po každém tahu vypíše:
- Číslo kroku (např. "1. krok")
- Mapu bludiště s aktuální pozicí a orientací příšery
- Prázdný řádek

## Implementace

### Struktura kódu (C#)

Program je implementován v C# s využitím OOP principů:

1. **Direction** - Statická třída pro práci se směry
   - Obsahuje konstanty pro 4 směry (UP, RIGHT, DOWN, LEFT)
   - Metody pro otáčení doprava/doleva
   - Mapování směrů na symboly a vektory pohybu

2. **Beast** - Třída reprezentující příšeru
   - Uchovává pozici (řádek, sloupec)
   - Uchovává aktuální směr otočení
   - Metody pro pohyb a otáčení

3. **Labyrinth** - Třída reprezentující bludiště
   - Uchovává mapu bludiště jako 2D pole
   - Obsahuje příšeru
   - Implementuje algoritmus pravé ruky se sledováním stavu
   - Vypisuje aktuální stav bludiště

### Algoritmus pravé ruky

Implementovaný algoritmus se sledováním stavu:
1. Pokud měla příšera zeď napravo a teď zmizela (otevření) → otoč se doprava
2. Jinak pokud můžeš jít dopředu → jdi dopředu
3. Jinak pokud napravo není zeď → otoč se doprava
4. Jinak → otoč se doleva

Algoritmus sleduje, zda byla v předchozím kroku zeď napravo. Když zeď zmizí (objeví se otevření), příšera se okamžitě otočí doprava, aby následovala zeď podle pravidla pravé ruky.

## Použití

### Kompilace a spuštění

```bash
# Kompilace
dotnet build

# Spuštění
dotnet run < input.txt

# Nebo přímé spuštění zkompilované aplikace
./bin/Debug/net8.0/BeastApp < input.txt
```

### Příklad vstupu (input.txt)

```
10
6
XXXXXXXXXX
X....X...X
X....X...X
X.X..X.X.X
X.X.>..X.X
XXXXXXXXXX
```

### Příklad výstupu

```
1. krok
XXXXXXXXXX
X....X...X
X....X...X
X.X..X.X.X
X.X..>.X.X
XXXXXXXXXX

2. krok
XXXXXXXXXX
X....X...X
X....X...X
X.X..X.X.X
X.X...>X.X
XXXXXXXXXX

...
```

## Požadavky

- .NET 6.0 nebo novější
- Žádné externí závislosti

## Struktura projektu

```
Beast_in_Labyrinth/
├── Program.cs           # Hlavní soubor s implementací
├── BeastApp.csproj      # .NET projekt
├── input.txt            # Příklad vstupního souboru
└── README.md            # Dokumentace
```

## Autor

Vytvořeno pomocí Claude Code
Datum: 2025-11-03
