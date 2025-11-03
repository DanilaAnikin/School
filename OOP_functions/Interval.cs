namespace OOP_functions
{
    /// <summary>
    /// Reprezentuje interval pro definiční obor nebo obor hodnot funkce
    /// </summary>
    public struct Interval
    {
        public double LowerBound { get; }
        public double UpperBound { get; }
        public bool IsLowerClosed { get; }
        public bool IsUpperClosed { get; }

        public Interval(double lowerBound, double upperBound, bool isLowerClosed = true, bool isUpperClosed = true)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            IsLowerClosed = isLowerClosed;
            IsUpperClosed = isUpperClosed;
        }

        /// <summary>
        /// Vytvoří interval celých reálných čísel (-∞, ∞)
        /// </summary>
        public static Interval Real => new Interval(double.NegativeInfinity, double.PositiveInfinity, false, false);

        /// <summary>
        /// Vytvoří interval [0, ∞)
        /// </summary>
        public static Interval NonNegative => new Interval(0, double.PositiveInfinity, true, false);

        /// <summary>
        /// Vytvoří interval (0, ∞)
        /// </summary>
        public static Interval Positive => new Interval(0, double.PositiveInfinity, false, false);

        /// <summary>
        /// Vytvoří interval (-∞, a) ∪ (a, ∞) - bez bodu a
        /// </summary>
        public static Interval RealExcept(double value)
        {
            return new Interval(double.NegativeInfinity, double.PositiveInfinity, false, false);
        }

        /// <summary>
        /// Vypíše interval v matematickém formátu
        /// </summary>
        public override string ToString()
        {
            string lower = double.IsNegativeInfinity(LowerBound) ? "-∞" : LowerBound.ToString();
            string upper = double.IsPositiveInfinity(UpperBound) ? "∞" : UpperBound.ToString();

            char leftBracket = IsLowerClosed && !double.IsInfinity(LowerBound) ? '[' : '(';
            char rightBracket = IsUpperClosed && !double.IsInfinity(UpperBound) ? ']' : ')';

            return $"{leftBracket}{lower}, {upper}{rightBracket}";
        }

        /// <summary>
        /// Zkontroluje, zda je hodnota v intervalu
        /// </summary>
        public bool Contains(double value)
        {
            bool aboveLower = IsLowerClosed ? value >= LowerBound : value > LowerBound;
            bool belowUpper = IsUpperClosed ? value <= UpperBound : value < UpperBound;
            return aboveLower && belowUpper;
        }
    }
}
