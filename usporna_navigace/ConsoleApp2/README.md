# Úsporná navigace

## Popis programu

Program hledá nejkratší cestu v grafu měst a silnic s omezením - lze použít maximálně 1 placenou silnici.

## Průběh programu

1. Program načte počet měst a silnic
2. Načte informace o silnicích (která města spojují, délka, zda je placená)
3. Načte počáteční a cílové město
4. Spustí modifikovaný Dijkstrův algoritmus s omezením na max 1 placenou silnici
5. Rekonstruuje a vypíše nejkratší nalezenou cestu
6. Vypíše celkovou vzdálenost cesty

## Algoritmus

Program používá **modifikovaný Dijkstrův algoritmus**:

- Stav grafu obsahuje: (město, počet_použitých_placených_silnic)
- Udržuje vzdálenosti pro každý stav: dist[město][0 nebo 1]
- Při přechodu po placené silnici zvýší počítadlo placených silnic
- Pokud by se překročil limit 1 placené silnice, přechod není povolen
- Na konci vybere nejkratší cestu do cíle (s 0 nebo 1 placenou silnicí)

## Vstup

- První řádek: M (počet měst) a S (počet silnic)
- Dalších S řádků: město1 město2 délka placená (0/1)
- Poslední řádek: start cíl

Města jsou číslována od 0.

## Výstup

- Cesta ve formátu: `město1 -> město2 -> ... -> městoN`
- `vzdálenost: X`
- Nebo "Cesta neexistuje." pokud neexistuje cesta splňující omezení

## Spuštění

```bash
dotnet run --project ConsoleApp2/ConsoleApp2.csproj < input.txt
``` 
