using System;

namespace OOP_functions
{
    /// <summary>
    /// Lineární funkce f(x) = ax + b
    /// </summary>
    public class LinearFunction : MathFunction, IDerivable, IInvertible
    {
        private readonly double a;
        private readonly double b;

        public override string Description => $"f(x) = {FormatCoefficient(a)}x {FormatConstant(b)}";

        public LinearFunction(double a, double b) : base("Lineární funkce")
        {
            this.a = a;
            this.b = b;
            Domain = Interval.Real;
            Range = Interval.Real;
        }

        public override double Calculate(double x)
        {
            return a * x + b;
        }

        public string GetDerivativeDescription()
        {
            return $"f'(x) = {a}";
        }

        public string GetInverseDescription()
        {
            if (a == 0)
                return "Inverzní funkce neexistuje (a = 0)";

            double inverseA = 1.0 / a;
            double inverseB = -b / a;
            return $"f⁻¹(x) = {FormatCoefficient(inverseA)}x {FormatConstant(inverseB)}";
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
