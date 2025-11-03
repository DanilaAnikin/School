# Two Beasts in Labyrinth

## Popis programu

Rozšířená verze programu Beast in Labyrinth - simuluje pohyb DVOU bestií v jednom bludišti současně.

## Klíčové rozdíly oproti jednoduchému Beast in Labyrinth

1. **Více bestií** - Program podporuje 2 nebo více bestií v bludišti
2. **Interakce mezi bestiemi** - Bestie se vzájemně považují za překážky
3. **Sekvenční pohyb** - Bestie se pohybují postupně (v pořadí, v jakém byly nalezeny na mapě)

## Algoritmus

Každá bestie se pohybuje podle pravidla pravé ruky, stejně jako v původní verzi:

1. Pokud zmizela stěna po pravici → otočí se vpravo
2. Pokud může jít rovně (a není tam druhá bestie) → jde rovně
3. Pokud nemůže jít rovně, ale není stěna vpravo (ani druhá bestie) → otočí se vpravo
4. Jinak → otočí se vlevo

Při rozhodování musí bestie brát v úvahu pozice ostatních bestií jako neprostupné překážky.

## Vstup

- Šířka a výška bludiště
- Mapa bludiště s dvěma (nebo více) bestiemi označenými směrovými symboly (`^`, `v`, `<`, `>`)

## Výstup

Po každém z 20 kroků program vypíše číslo kroku a aktuální stav bludiště s pozicemi obou bestií.
