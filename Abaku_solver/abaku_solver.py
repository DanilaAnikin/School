#!/usr/bin/env python3
"""
Abaku Solver - Řešič počtářské hry Abaku

Najde všechny podpříklady v řadě zadaných čísel a spočítá jejich skóre.
Povolené operace: +, -, *, /, ^2, ^3
"""


def is_valid_number(s):
    """Kontroluje, zda je řetězec platné číslo (ne nula, ne leading zero kromě samotné nuly)"""
    if not s or s == '0':
        return False
    if len(s) > 1 and s[0] == '0':
        return False
    return True


def try_binary_operation(left_str, op):
    """Zkusí binární operaci mezi číslicemi v řetězci"""
    results = []

    # Projdeme všechny možné pozice rozdělení
    for i in range(1, len(left_str)):
        a_str = left_str[:i]
        b_str = left_str[i:]

        # Kontrola platnosti čísel
        if not is_valid_number(a_str) or not is_valid_number(b_str):
            continue

        a = int(a_str)
        b = int(b_str)

        # Nula nesmí být samostatný člen
        if a == 0 or b == 0:
            continue

        result = None
        op_symbol = op

        try:
            if op == '+':
                result = a + b
            elif op == '-':
                result = a - b
            elif op == '*':
                result = a * b
            elif op == '/':
                if b != 0 and a % b == 0:  # Pouze celočíselné dělení
                    result = a // b
        except:
            continue

        if result is not None and result != 0:  # Výsledek nesmí být nula
            results.append((f"{a}{op_symbol}{b}={result}", result, a_str, b_str))

    return results


def try_unary_operation(left_str, power):
    """Zkusí unární operaci (mocnina) na čísle"""
    if not is_valid_number(left_str):
        return None

    num = int(left_str)

    # Nula nesmí být samostatný člen
    if num == 0:
        return None

    result = num ** power

    # Výsledek nesmí být nula
    if result == 0:
        return None

    return (f"{num}^{power}={result}", result, left_str)


def find_examples_in_substring(substring, start_pos):
    """Najde všechny možné příklady v daném podřetězci"""
    examples = []

    # Projdeme všechny možné pozice rozdělení na levou a pravou část
    for split_pos in range(1, len(substring)):
        left = substring[:split_pos]
        right = substring[split_pos:]

        if not is_valid_number(right):
            continue

        right_num = int(right)

        # Zkusíme unární operace na levé části
        for power in [2, 3]:
            result = try_unary_operation(left, power)
            if result and result[1] == right_num:
                expr, _, left_str = result
                # Vypočítáme skóre pro tento příklad
                score = sum(int(d) for d in substring)
                examples.append((expr, score, start_pos, start_pos + len(substring)))

        # Zkusíme binární operace
        for op in ['+', '-', '*', '/']:
            results = try_binary_operation(left, op)
            for expr, result_num, a_str, b_str in results:
                if result_num == right_num:
                    # Vypočítáme skóre pro tento příklad
                    score = sum(int(d) for d in substring)
                    examples.append((expr, score, start_pos, start_pos + len(substring)))

    return examples


def solve_abaku(input_str):
    """Hlavní funkce pro řešení Abaku příkladu"""
    # Odstranění mezer
    input_str = input_str.replace(' ', '')

    # Kontrola validity vstupu
    if not input_str.isdigit():
        return [], 0

    found_examples = []
    used_ranges = set()  # Množina použitých rozsahů (start, end)

    # Projdeme všechny možné délky podřetězců (od nejdelších)
    for length in range(len(input_str), 1, -1):
        for start in range(len(input_str) - length + 1):
            substring = input_str[start:start + length]
            range_key = (start, start + length)

            # Pokud jsme tento rozsah už použili, přeskočíme
            if range_key in used_ranges:
                continue

            # Najdeme příklady v tomto podřetězci
            examples = find_examples_in_substring(substring, start)

            # Pokud najdeme alespoň jeden příklad, vybereme první a označíme rozsah jako použitý
            if examples:
                found_examples.append(examples[0])
                used_ranges.add(range_key)

    # Seřadíme příklady podle pozice
    found_examples.sort(key=lambda x: (x[2], x[3]))

    # Vypočítáme celkové skóre
    total_score = sum(ex[1] for ex in found_examples)

    return found_examples, total_score


def main():
    """Hlavní funkce programu"""
    print("=== Abaku Solver ===\n")

    # Vstup od uživatele
    input_str = input("Zadejte Abaku příklad (číslo bez mezer): ").strip()

    # Řešení
    examples, total_score = solve_abaku(input_str)

    # Výstup
    print("\nVýsledky:")
    if not examples:
        print("Nebyly nalezeny žádné podpříklady.")
    else:
        for expr, score, start, end in examples:
            print(expr)
        print(f"skóre: {total_score}")


if __name__ == "__main__":
    main()
