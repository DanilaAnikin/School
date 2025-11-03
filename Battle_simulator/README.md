# Battle Simulator

## Popis programu

Program simuluje RPG souboj mezi dvěma armádami. Každá armáda obsahuje 10 postav různých typů (Wizard, Warrior, Archer), které proti sobě bojují, dokud jedna z armád není kompletně poražena.

## Entity

Program obsahuje tři typy postav:

- **Wizard** (Čaroděj)
  - Health: 80, Power: 25
  - Speciální schopnost: Odráží 50% přijatého poškození zpět na útočníka
  - Bojová hláška: "Abrakadabra! Magická rána!"

- **Warrior** (Bojovník)
  - Health: 120, Power: 20
  - Bojová hláška: "Za slávu! Mečem vpřed!"

- **Archer** (Lučištník)
  - Health: 90, Power: 22
  - Bojová hláška: "Šíp letí! Na cíl!"

## Průběh programu

### 1. Inicializace armád

- Program vytvoří dvě armády, každá s 10 postavami
- Složení každé armády je náhodné:
  - 1-3 Wizardů
  - 2-5 Warriors
  - Zbytek jsou Archeři (celkem vždy 10 postav)
- Postavy jsou v armádě náhodně zamíchány

### 2. Simulace kola

Každé kolo probíhá následovně:

1. **Spárování soupeřů**: Postavy na stejných indexech (0 vs 0, 1 vs 1, atd.) bojují proti sobě
2. **Útok**:
   - První postava zaútočí na druhou:
     - Vyvolá bojovou hlášku
     - Zavolá `TakeDamage(Power)` na cíl
   - Pokud druhá postava přežila, zaútočí zpět stejným způsobem
3. **Wizard specialita**: Když Wizard dostane poškození, automaticky odrazí 50% zpět na útočníka
4. **Zvýšení síly**: Pokud postava zabije protivníka, zvýší si Power o 1
5. **Úklid**: Na konci kola se z obou armád odstraní všechny mrtvé postavy

### 3. Konec bitvy

- Bitva pokračuje, dokud jedna z armád nemá alespoň jednu živou postavu
- Program vyhlásí vítěznou armádu a vypíše přeživší bojovníky

## Algoritmus útoku a obrany

### Attack(Character target)
- Každá postava má vlastní bojovou hlášku
- Zavolá `target.TakeDamage(this.Power, this)`

### TakeDamage(int amount, Character attacker)
- **Základní verze** (Warrior, Archer):
  - `Health -= amount`
- **Wizard specialita**:
  - Nejprve přijme plné poškození
  - Pak odrazí 50% poškození zpět na útočníka
  - Odražené poškození se už dále neodráží (zamezení nekonečné smyčky)

### IsAlive
- Vrací `true`, pokud `Health > 0`

## Spuštění

```bash
cd Battle_simulator
dotnet run
```

## Příklad výstupu

```
=== BITVA ARMÁD ===

--- Armáda 1 ---
  [0] Warrior Armáda_Severu_Warrior1 (120/20)
  [1] Wizard Armáda_Severu_Wizard1 (80/25)
  ...

=== KOLO 1 ===

[Souboj 1] Warrior Armáda_Severu_Warrior1 (120/20) vs Wizard Armáda_Jihu_Wizard1 (80/25)
Armáda_Severu_Warrior1: Za slávu! Mečem vpřed!
  -> Armáda_Jihu_Wizard1 odráží 10 poškození zpět na Armáda_Severu_Warrior1!
Armáda_Jihu_Wizard1: Abrakadabra! Magická rána!

--- Stav po kole 1 ---
Armáda 1: 10 bojovníků
Armáda 2: 9 bojovníků

...

=== KONEC BITVY ===
Vítězí Armáda 1!
```
