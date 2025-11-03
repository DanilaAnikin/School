using System;

namespace OOP_functions
{
    /// <summary>
    /// Lineární funkce s absolutní hodnotou f(x) = |ax + b|
    /// </summary>
    public class AbsoluteLinearFunction : MathFunction, IDerivable
    {
        private readonly double a;
        private readonly double b;

        public override string Description => $"f(x) = |{FormatCoefficient(a)}x {FormatConstant(b)}|";

        public AbsoluteLinearFunction(double a, double b) : base("Lineární funkce s absolutní hodnotou")
        {
            this.a = a;
            this.b = b;
            Domain = Interval.Real;
            Range = Interval.NonNegative; // Výsledek je vždy >= 0
        }

        public override double Calculate(double x)
        {
            return Math.Abs(a * x + b);
        }

        public string GetDerivativeDescription()
        {
            // Derivace není definována v bodě, kde ax + b = 0
            // Pro x < -b/a: f'(x) = -a (pokud a > 0)
            // Pro x > -b/a: f'(x) = a (pokud a > 0)
            return $"f'(x) = {a} pro x > {-b/a:F2}, f'(x) = {-a} pro x < {-b/a:F2}";
        }

        private string FormatCoefficient(double coefficient)
        {
            if (coefficient == 1)
                return "";
            if (coefficient == -1)
                return "-";
            return coefficient.ToString();
        }

        private string FormatConstant(double constant)
        {
            if (constant == 0)
                return "";
            if (constant > 0)
                return $"+ {constant}";
            return $"- {Math.Abs(constant)}";
        }
    }
}
