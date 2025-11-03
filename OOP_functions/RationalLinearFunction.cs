using System;

namespace OOP_functions
{
    /// <summary>
    /// Lineární lomená funkce f(x) = (ax + b) / (cx + d)
    /// </summary>
    public class RationalLinearFunction : MathFunction, IDerivable
    {
        private readonly double a;
        private readonly double b;
        private readonly double c;
        private readonly double d;
        private readonly double asymptoteX; // Vertikální asymptota

        public override string Description
        {
            get
            {
                string numerator = $"{FormatCoefficient(a)}x {FormatConstant(b)}";
                string denominator = $"{FormatCoefficient(c)}x {FormatConstant(d)}";
                return $"f(x) = ({numerator}) / ({denominator})";
            }
        }

        public RationalLinearFunction(double a, double b, double c, double d) : base("Lineární lomená funkce")
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;

            // Výpočet vertikální asymptoty: cx + d = 0 => x = -d/c
            asymptoteX = -d / c;

            Domain = Interval.RealExcept(asymptoteX);
            Range = Interval.RealExcept(a / c); // Horizontální asymptota y = a/c
        }

        public override double Calculate(double x)
        {
            double denominator = c * x + d;
            if (Math.Abs(denominator) < 1e-10)
                return double.NaN; // Nedefinováno

            return (a * x + b) / denominator;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"  Funkce má hyperbolický průběh s vertikální asymptotou x = {asymptoteX:F2}");
        }

        public string GetDerivativeDescription()
        {
            // Derivace: f'(x) = (ad - bc) / (cx + d)²
            double numerator = a * d - b * c;
            return $"f'(x) = {numerator} / ({FormatCoefficient(c)}x {FormatConstant(d)})²";
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
