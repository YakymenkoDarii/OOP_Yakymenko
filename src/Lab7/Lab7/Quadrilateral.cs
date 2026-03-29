namespace Lab7_OOP
{
    /// <summary>
    /// Клас, що представляє опуклий чотирикутник.
    /// Реалізує обидва інтерфейси: задання через сторони і через координати вершин.
    /// </summary>
    public class Quadrilateral : IQuadrilateralBySides, IQuadrilateralByCoords
    {
        // ─── Поля для режиму "сторони" ────────────────────────────────────────
        private double _a, _b, _c, _d;   // сторони

        // діагоналі — обчислюються тільки в координатному режимі
        private double _d1, _d2;

        // ─── Поля для режиму "координати" ─────────────────────────────────────
        private Point _pA, _pB, _pC, _pD;

        // ─── Прапорець поточного режиму ───────────────────────────────────────
        private bool _usesCoords = false;

        // ══════════════════════════════════════════════════════════════════════
        //  Реалізація IQuadrilateralBySides
        // ══════════════════════════════════════════════════════════════════════

        /// <inheritdoc/>
        public void SetSides(double a, double b, double c, double d)
        {
            if (a <= 0 || b <= 0 || c <= 0 || d <= 0)
                throw new ArgumentException("Усі сторони повинні бути додатними.");

            _a = a; _b = b; _c = c; _d = d;
            _d1 = 0; _d2 = 0; // не використовуються в цьому режимі
            _usesCoords = false;
        }

        // ══════════════════════════════════════════════════════════════════════
        //  Реалізація IQuadrilateralByCoords
        // ══════════════════════════════════════════════════════════════════════

        /// <inheritdoc/>
        public void SetVertices(Point a, Point b, Point c, Point d)
        {
            _pA = a; _pB = b; _pC = c; _pD = d;
            _usesCoords = true;

            // Перераховуємо сторони і діагоналі з координат
            _a  = Distance(a, b);
            _b  = Distance(b, c);
            _c  = Distance(c, d);
            _d  = Distance(d, a);
            _d1 = Distance(a, c);
            _d2 = Distance(b, d);
        }

        // ══════════════════════════════════════════════════════════════════════
        //  Загальні методи (однакова логіка для обох режимів)
        // ══════════════════════════════════════════════════════════════════════

        /// <summary>Периметр = сума чотирьох сторін.</summary>
        public double GetPerimeter() => _a + _b + _c + _d;

        /// <summary>
        /// Площа:
        ///  - координатний режим → формула Гауса (шнурівки)
        ///  - режим сторін → формула Брахмагупти для вписаного чотирикутника:
        ///      S = √((s−a)(s−b)(s−c)(s−d)),  де s = (a+b+c+d)/2
        /// </summary>
        public double GetArea()
        {
            if (_usesCoords)
                return ShoelaceArea();

            // Формула Брахмагупти (максимальна площа при заданих сторонах)
            double s = (_a + _b + _c + _d) / 2.0;
            double expr = (s - _a) * (s - _b) * (s - _c) * (s - _d);
            if (expr < 0) expr = 0;
            return Math.Sqrt(expr);
        }

        /// <summary>Визначення типу опуклого чотирикутника.</summary>
        public string GetQuadrilateralType()
        {
            const double Eps = 1e-9;

            bool allEq = Approx(_a, _b, Eps) && Approx(_b, _c, Eps) && Approx(_c, _d, Eps);
            bool acEq  = Approx(_a, _c, Eps); // протилежні сторони a = c
            bool bdEq  = Approx(_b, _d, Eps); // протилежні сторони b = d

            if (_usesCoords)
            {
                // Маємо діагоналі — можна розрізнити всі типи
                bool diagsEqual = Approx(_d1, _d2, Eps);
                bool diagsPerp  = DiagonalsArePerpendicular(Eps);

                if (allEq && diagsEqual)          return "Квадрат";
                if (allEq && diagsPerp)           return "Ромб";
                if (acEq && bdEq && diagsEqual)   return "Прямокутник";
                if (acEq && bdEq)                 return "Паралелограм";
                if (allEq)                        return "Ромб";
                if (IsTrapezoid(Eps))             return "Трапеція";
                return "Довільний чотирикутник";
            }
            else
            {
                // Тільки сторони — неможливо розрізнити квадрат/ромб або прямокутник/паралелограм
                if (allEq)        return "Ромб (можливо Квадрат)";
                if (acEq && bdEq) return "Паралелограм (можливо Прямокутник)";
                return "Довільний чотирикутник";
            }
        }

        // ══════════════════════════════════════════════════════════════════════
        //  Допоміжні методи
        // ══════════════════════════════════════════════════════════════════════

        private static double Distance(Point p1, Point p2)
            => Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));

        /// <summary>Формула шнурівки Гауса для площі.</summary>
        private double ShoelaceArea()
        {
            Point[] pts = { _pA, _pB, _pC, _pD };
            double area = 0;
            for (int i = 0; i < pts.Length; i++)
            {
                int j = (i + 1) % pts.Length;
                area += pts[i].X * pts[j].Y;
                area -= pts[j].X * pts[i].Y;
            }
            return Math.Abs(area) / 2.0;
        }

        private static bool Approx(double x, double y, double eps)
            => Math.Abs(x - y) < eps;

        /// <summary>Перевірка перпендикулярності діагоналей через скалярний добуток.</summary>
        private bool DiagonalsArePerpendicular(double eps)
        {
            double acX = _pC.X - _pA.X, acY = _pC.Y - _pA.Y;
            double bdX = _pD.X - _pB.X, bdY = _pD.Y - _pB.Y;
            return Approx(acX * bdX + acY * bdY, 0, eps * 1e6);
        }

        /// <summary>Трапеція — рівно одна пара паралельних сторін.</summary>
        private bool IsTrapezoid(double eps)
        {
            bool ab_cd = SidesParallel(_pA, _pB, _pD, _pC, eps); // AB ∥ DC
            bool ad_bc = SidesParallel(_pA, _pD, _pB, _pC, eps); // AD ∥ BC
            return ab_cd ^ ad_bc;
        }

        private static bool SidesParallel(Point p1, Point p2, Point p3, Point p4, double eps)
        {
            double v1x = p2.X - p1.X, v1y = p2.Y - p1.Y;
            double v2x = p4.X - p3.X, v2y = p4.Y - p3.Y;
            return Approx(v1x * v2y - v1y * v2x, 0, eps * 1e6);
        }

        public override string ToString()
        {
            string mode = _usesCoords
                ? $"Вершини: A{_pA}, B{_pB}, C{_pC}, D{_pD}"
                : $"Сторони: a={_a:F4}, b={_b:F4}, c={_c:F4}, d={_d:F4}";

            return $"{mode}\n" +
                   $"  Периметр : {GetPerimeter():F4}\n" +
                   $"  Площа    : {GetArea():F4}\n" +
                   $"  Тип      : {GetQuadrilateralType()}";
        }
    }
}
