using System;

namespace OOP_functions
{
    /// <summary>
    /// Abstraktní základní třída pro všechny matematické funkce
    /// </summary>
    public abstract class MathFunction
    {
        /// <summary>
        /// Název funkce (např. "Lineární funkce")
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Popis funkce s konkrétními hodnotami parametrů (např. "f(x) = 2x + 3")
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Definiční obor funkce
        /// </summary>
        public Interval Domain { get; protected set; }

        /// <summary>
        /// Obor hodnot funkce
        /// </summary>
        public Interval Range { get; protected set; }

        /// <summary>
        /// Vypočítá funkční hodnotu pro zadané x
        /// </summary>
        public abstract double Calculate(double x);

        /// <summary>
        /// Vypíše informace o funkci
        /// </summary>
        public virtual void PrintInfo()
        {
            Console.WriteLine($"{Name}: {Description} na D(f) = {Domain}");
        }

        /// <summary>
        /// Vypíše rozšiřující vlastnosti funkce (derivace, inverze)
        /// </summary>
        public virtual void PrintExtendedProperties()
        {
            if (this is IDerivable derivable)
            {
                Console.WriteLine($"Derivace: {derivable.GetDerivativeDescription()}");
            }

            if (this is IInvertible invertible)
            {
                Console.WriteLine($"Inverzní funkce: {invertible.GetInverseDescription()}");
            }
        }

        protected MathFunction(string name)
        {
            Name = name;
        }
    }
}
