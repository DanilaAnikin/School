# Abaku Solver

## Popis programu

Program řeší počtářskou hru Abaku - najde všechny podpříklady v řadě zadaných čísel a spočítá jejich skóre. Abaku je počtářská hra, která trénuje používání aritmetických operací (podobně jako Scrabble, ale s čísly).

## Pravidla

### Povolené operace
- Binární: `+`, `-`, `*`, `/` (pouze celočíselné dělení)
- Unární: `^2`, `^3`

### Omezení
- Nula nesmí být samostatný člen příkladu ani celý výsledek
- Pokud je ve stejné řadě více příkladů, bere se jen jeden

### Hodnocení
Za každý funkční podpříklad se spočítá skóre: sečtou se hodnoty použitých cifer

## Průběh algoritmu

Algoritmus postupuje systematicky od nejdelších podřetězců k nejkratším:

### 1. Generování kandidátů

Pro vstupní číslo `9817`:
- Projde všechny délky podřetězců (4, 3, 2)
- Pro každou délku projde všechny možné pozice
- Např. pro délku 4: `9817`, pro délku 3: `981`, `817`, pro délku 2: `98`, `81`, `17`

### 2. Testování příkladů v každém podřetězci

Pro každý podřetězec (např. `981`):

**a) Zkouší unární operace:**
- Rozdělí na levou a pravou část: `9|81`, `98|1`
- Testuje, zda `levá_část^2 = pravá_část` nebo `levá_část^3 = pravá_část`
- Pro `9|81`: zkouší `9^2 = 81` ✓ (nalezeno!)

**b) Zkouší binární operace:**
- Pro každé rozdělení testuje `+`, `-`, `*`, `/`
- Pro `9|81`: zkouší `9+8=1?`, `9-8=1?`, atd.

### 3. Validace

Pro každý nalezený příklad kontroluje:
- Žádná část není nula
- Výsledek není nula
- Čísla nemají vedoucí nuly (např. `081` není platné)

### 4. Výběr příkladů

- Pro každou "řadu" (souvislý podřetězec) vybere **maximálně jeden** příklad
- Uchovává si, které rozsahy již byly použity
- Např. pokud našel `9^2=81` v rozsahu `[0,3)`, nebude hledat další příklady v tomto rozsahu

### 5. Výpočet skóre

- Pro každý nalezený příklad sečte hodnoty všech použitých cifer
- Např. pro `9^2=81` je skóre `9+8+1 = 18`
- Celkové skóre je součet všech dílčích skóre

## Vstup

Jedno číslo bez mezer mezi ciframi (např. `9817`)

## Výstup

- Seznam všech nalezených podpříkladů
- Celkové skóre

### Příklad

**Vstup:**
```
9817
```

**Výstup:**
```
9^2=81
9+8=17
8-1=7
skóre: 59
```

**Vysvětlení:**
- `9^2=81` - rozsah [0,3), skóre: 9+8+1 = 18
- `9+8=17` - rozsah [0,4), skóre: 9+8+1+7 = 25
- `8-1=7` - rozsah [1,4), skóre: 8+1+7 = 16
- Celkem: 18 + 25 + 16 = 59

## Spuštění

```bash
cd Abaku_solver
python3 abaku_solver.py
```
