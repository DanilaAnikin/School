# Problém stabilního manželství (Stable Marriage Problem)

## Popis programu

Program řeší problém stabilního párování pomocí Gale-Shapleyho algoritmu. Cílem je spárovat n žen a n mužů na základě jejich preferencí tak, aby výsledné párování bylo stabilní.

## Průběh programu

1. Program načte počet párů n
2. Načte preference každé ženy (seřazený seznam mužů podle preference)
3. Načte preference každého muže (seřazený seznam žen podle preference)
4. Spustí Gale-Shapleyho algoritmus (ženy nabízejí mužům)
5. Vypíše výsledné párování

## Gale-Shapleyho algoritmus

**Princip:**
- Volné ženy postupně nabízejí sňatek mužům podle svých preferencí
- Muž přijme nabídku, pokud je volný, nebo si vybere mezi současnou partnerkou a novou nabídkou
- Odmítnuté ženy zkusí dalšího muže ze svého seznamu
- Proces pokračuje, dokud nejsou všichni spárováni

**Výsledek:** Stabilní párování - neexistuje dvojice (muž, žena), kteří by oba raději byli spolu než se svými současnými partnery.

## Vstup

- První řádek: počet párů n
- Dalších n řádků: preference každé ženy (čísla mužů 1 až n)
- Dalších n řádků: preference každého muže (čísla žen 1 až n)

## Výstup

Pro každou ženu (1 až n) vypíše číslo jejího partnera.

## Spuštění

```bash
dotnet run --project StabilniManzelstvi/StabilniManzelstvi.csproj < input.txt
```
