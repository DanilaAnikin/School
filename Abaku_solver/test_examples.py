#!/usr/bin/env python3
"""
Testovací příklady pro Abaku Solver
"""

from abaku_solver import solve_abaku


def test_example(input_str, expected_score=None):
    """Testuje jeden příklad"""
    print(f"\n{'='*50}")
    print(f"Vstup: {input_str}")
    print(f"{'='*50}")

    examples, total_score = solve_abaku(input_str)

    if examples:
        for expr, score, start, end in examples:
            print(expr)
        print(f"skóre: {total_score}")

        if expected_score is not None:
            if total_score == expected_score:
                print(f"✓ Správně! (očekávané skóre: {expected_score})")
            else:
                print(f"✗ Chyba! Očekávané skóre: {expected_score}, získané: {total_score}")
    else:
        print("Nebyly nalezeny žádné podpříklady.")


def main():
    """Hlavní testovací funkce"""
    print("=== Abaku Solver - Testovací příklady ===")

    # Testovací příklad z zadání
    test_example("9817", expected_score=59)

    # Další testovací příklady
    test_example("248")   # 2*4=8
    test_example("12345")
    test_example("111")
    test_example("2816")  # 2^3=8, 8*2=16
    test_example("936")   # 9/3=6
    test_example("525")   # 5^2=25


if __name__ == "__main__":
    main()
