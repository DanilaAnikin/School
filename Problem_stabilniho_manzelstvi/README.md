# Problém stabilního manželství (Stable Marriage Problem)

## Popis problému

Máme omezený prostor, kde žije n můžů a n žen připravených vejít do manželství. Cílem je najít takové párování, kde každý pár (muž-žena) bude stabilní.

Každý z můžů a žen vytvoří seznam příslušníků opačného pohlaví dle svých preferencí.

**Nestabilní pár** by byl takový, kde:
- Aspoň jeden z manželů touží po někom, koho má na seznamu s vyšší preferencí než svého současného partnera
- A ten někdo po něm také touží víc než po svém současném partnerovi
- Pak se obě současná manželství rozpadnou a tyto dvě hrdličky budou spolu

Cílem je najít řešení **bez takových třaskavých situací**.

## Gale-Shapleyho algoritmus

Algoritmus funguje následovně:

1. Všechny ženy nabídnou sňatek mužům, které mají na své top pozici
2. Oslovení muži si z nabídek vyberou tu nejlepší volbu dle svých preferencích a ostatní odmítnou
3. Všechny odmítnuté ženy pokračují dál další nejlepší volbou dle svých preferencí

Tento postup se opakuje, dokud nejsou všichni spárováni.

### Vlastnosti algoritmu

- **Časová složitost**: O(n²)
- **Konečnost**: Algoritmus vždy skončí
- **Úplnost**: Vždy najde řešení
- **Správnost**: Najde stabilní párování

Lloyd Shapley a Alvin Roth za tento algoritmus získali v roce 2012 **Nobelovu cenu za ekonomii**.

## Formát vstupu

```
n
preference_ženy_1
preference_ženy_2
...
preference_ženy_n
preference_muže_1
preference_muže_2
...
preference_muže_n
```

Kde každá řádka preferencí obsahuje n čísel (od 1 do n) seřazených od nejlepší volby po nejhorší.

### Příklad vstupu

```
4
1 3 2 4
4 3 1 2
1 4 3 2
1 4 3 2
3 2 4 1
3 2 4 1
2 3 1 4
4 3 2 1
```

## Formát výstupu

Pro každou ženu (od 1 do n) se vypíše číslo muže, se kterým je spárovaná.

### Příklad výstupu

```
2
3
1
4
```

To znamená:
- Žena 1 je spárovaná s mužem 2
- Žena 2 je spárovaná s mužem 3
- Žena 3 je spárovaná s mužem 1
- Žena 4 je spárovaná s mužem 4

## Použití

### Build projektu

```bash
dotnet build StabilniManzelstvi/StabilniManzelstvi.csproj
```

### Spuštění

```bash
dotnet run --project StabilniManzelstvi/StabilniManzelstvi.csproj < input.txt
```

Nebo interaktivně:

```bash
dotnet run --project StabilniManzelstvi/StabilniManzelstvi.csproj
```

## Reálné aplikace

- Přiřazování lékařů do nemocnic
- Přidělování studentů na univerzity
- Matching na seznamovacích aplikacích
- Přidělování dárců orgánů příjemcům

## Reference

- [YouTube video s animací](https://www.youtube.com/watch?v=Qcv1IqHWAzg)
- [Studijní materiály](https://fiveable.me/methods-of-mathematics-calculus-statistics-and-combinatorics/unit-12/stable-marriage-problem/study-guide/QYJWEf2NDRyL8C2v)
- [Důkaz správnosti (CZ)](https://ksvi.mff.cuni.cz/~dvorak/vyuka/UIN009/StabManz.pdf)
- [Gale-Shapley algoritmus (Wikipedia)](https://en.wikipedia.org/wiki/Gale%E2%80%93Shapley_algorithm)
