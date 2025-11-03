using System;

namespace OOP_functions
{
    /// <summary>
    /// Kvadratická funkce f(x) = ax² + bx + c
    /// </summary>
    public class QuadraticFunction : MathFunction, IDerivable
    {
        private readonly double a;
        private readonly double b;
        private readonly double c;
        private readonly double vertexX; // x-ová souřadnice vrcholu
        private readonly double vertexY; // y-ová souřadnice vrcholu

        public override string Description => $"f(x) = {FormatCoefficient(a)}x² {FormatTerm(b, "x")} {FormatConstant(c)}";

        public QuadraticFunction(double a, double b, double c) : base("Kvadratická funkce")
        {
            if (a == 0)
                throw new ArgumentException("Koeficient a nesmí být 0 pro kvadratickou funkci");

            this.a = a;
            this.b = b;
            this.c = c;

            // Výpočet vrcholu paraboly: x = -b/(2a), y = f(x)
            vertexX = -b / (2 * a);
            vertexY = a * vertexX * vertexX + b * vertexX + c;

            Domain = Interval.Real;

            // Obor hodnot závisí na orientaci paraboly
            if (a > 0)
            {
                // Parabola otevřená nahoru: [vertexY, ∞)
                Range = new Interval(vertexY, double.PositiveInfinity, true, false);
            }
            else
            {
                // Parabola otevřená dolů: (-∞, vertexY]
                Range = new Interval(double.NegativeInfinity, vertexY, false, true);
            }
        }

        public override double Calculate(double x)
        {
            return a * x * x + b * x + c;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            string orientation = a > 0 ? "otevřená nahoru" : "otevřená dolů";
            Console.WriteLine($"  Funkce má parabolický průběh {orientation} s vrcholem V = [{vertexX:F2}, {vertexY:F2}]");
        }

        public string GetDerivativeDescription()
        {
            // Derivace: f'(x) = 2ax + b
            double derivA = 2 * a;
            return $"f'(x) = {FormatCoefficient(derivA)}x {FormatConstant(b)}";
        }

        private string FormatCoefficient(double coefficient)
        {
            if (coefficient == 1)
                return "";
            if (coefficient == -1)
                return "-";
            return coefficient.ToString();
        }

        private string FormatTerm(double coefficient, string variable)
        {
            if (coefficient == 0)
                return "";
            if (coefficient > 0)
                return $"+ {(coefficient == 1 ? "" : coefficient.ToString())}{variable}";
            return $"- {(Math.Abs(coefficient) == 1 ? "" : Math.Abs(coefficient).ToString())}{variable}";
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
