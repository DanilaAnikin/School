# OOP Functions - Knihovna matematických funkcí

## Popis programu

Interaktivní konzolová aplikace implementující knihovnu matematických funkcí pomocí objektově orientovaného programování.

## Architektura

Program využívá hierarchii tříd:

- **MathFunction** - abstraktní základní třída pro všechny funkce
- **IDerivable** - rozhraní pro funkce s derivací
- **IInvertible** - rozhraní pro funkce s inverzní funkcí
- **Interval** - třída reprezentující definiční obor / obor hodnot

## Implementované funkce

1. **LinearFunction** - Lineární funkce `f(x) = ax + b`
   - Má derivaci i inverzní funkci

2. **AbsoluteLinearFunction** - Lineární funkce s absolutní hodnotou `f(x) = |ax + b|`
   - Má derivaci (po částech)

3. **QuadraticFunction** - Kvadratická funkce `f(x) = ax² + bx + c`
   - Má derivaci
   - Vypočítává vrchol paraboly

4. **RationalLinearFunction** - Lineární lomená funkce `f(x) = (ax + b) / (cx + d)`
   - Má derivaci
   - Detekuje asymptoty

## Průběh programu

1. Program zobrazí seznam dostupných funkcí
2. Uživatel vybere funkci zadáním čísla
3. Uživatel zadá hodnotu x
4. Program vypočítá a zobrazí:
   - Hodnotu f(x)
   - Definiční obor
   - Derivaci (pokud existuje)
   - Inverzní funkci (pokud existuje)
   - Další vlastnosti (např. vrchol paraboly, asymptoty)

5. Uživatel může pokračovat s další funkcí nebo ukončit program
