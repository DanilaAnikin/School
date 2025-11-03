# Beast in Labyrinth

## Popis programu

Program simuluje pohyb "bestie" (tvora) v bludišti. Bestie se pohybuje podle pravidla pravé ruky - tedy drží se stále stěny po pravici.

## Průběh programu

1. Program načte bludiště ze vstupu (šířka, výška, mapa)
2. Najde pozici a směr bestie na mapě (značeno symboly `^`, `v`, `<`, `>`)
3. Provede 20 kroků simulace pohybu
4. Po každém kroku vypíše číslo kroku a aktuální stav bludiště

## Algoritmus pohybu

Bestie rozhoduje podle následujících pravidel:

1. Pokud zmizela stěna po pravici (byla tam v minulém kroku, teď ne) → otočí se vpravo
2. Pokud může jít rovně → jde rovně
3. Pokud nemůže jít rovně, ale není stěna vpravo → otočí se vpravo
4. Jinak → otočí se vlevo

Tento algoritmus zajišťuje, že bestie drží stěnu po pravé ruce a pomalu prozkoumává bludiště.

## Vstup

- Šířka bludiště
- Výška bludiště
- Mapa: `X` = stěna, `.` = volné pole, `^/v/</>`= bestie se směrem

## Výstup

Pro každý z 20 kroků: číslo kroku a aktuální stav bludiště s pozicí bestie.

## Spuštění

```bash
dotnet run < input.txt
```
