namespace Lab7_OOP
{
    /// <summary>
    /// Інтерфейс для чотирикутника, заданого довжинами сторін.
    /// </summary>
    public interface IQuadrilateralBySides
    {
        /// <summary>Встановити довжини чотирьох сторін (a, b, c, d).</summary>
        void SetSides(double a, double b, double c, double d);

        /// <summary>Обчислити периметр.</summary>
        double GetPerimeter();

        /// <summary>Обчислити площу.</summary>
        double GetArea();

        /// <summary>Визначити тип чотирикутника.</summary>
        string GetQuadrilateralType();
    }
}
