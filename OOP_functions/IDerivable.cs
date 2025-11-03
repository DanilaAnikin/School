namespace OOP_functions
{
    /// <summary>
    /// Rozhraní pro funkce, které lze derivovat
    /// </summary>
    public interface IDerivable
    {
        /// <summary>
        /// Vrátí textový popis derivace funkce
        /// </summary>
        string GetDerivativeDescription();
    }
}
