namespace Lab7_OOP
{
    /// <summary>
    /// Точка у двовимірному просторі.
    /// </summary>
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}; {Y})";
    }

    /// <summary>
    /// Інтерфейс для чотирикутника, заданого координатами вершин.
    /// </summary>
    public interface IQuadrilateralByCoords
    {
        /// <summary>Встановити координати чотирьох вершин (у порядку обходу).</summary>
        void SetVertices(Point a, Point b, Point c, Point d);

        /// <summary>Обчислити периметр.</summary>
        double GetPerimeter();

        /// <summary>Обчислити площу.</summary>
        double GetArea();

        /// <summary>Визначити тип чотирикутника.</summary>
        string GetQuadrilateralType();
    }
}
