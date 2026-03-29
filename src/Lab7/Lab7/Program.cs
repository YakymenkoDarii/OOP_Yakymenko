using Lab7_OOP;

Console.OutputEncoding = System.Text.Encoding.UTF8;

bool running = true;
while (running)
{
    Console.Clear();
    PrintHeader();

    // ── Крок 1: вибір способу задання ────────────────────────────────────────
    Console.WriteLine("  Як задати чотирикутник?");
    Console.WriteLine("  [1] Довжинами сторін");
    Console.WriteLine("  [2] Координатами вершин");
    Console.WriteLine("  [0] Вийти");
    Console.WriteLine();
    Console.Write("  Ваш вибір: ");

    string? inputMode = Console.ReadLine()?.Trim();
    if (inputMode == "0") break;

    Quadrilateral quad = new();

    if (inputMode == "1")
    {
        // ── Ввід сторін ────────────────────────────────────────────────────
        Console.WriteLine();
        Console.WriteLine("  Введіть довжини чотирьох сторін чотирикутника:");
        Console.WriteLine("  (усі значення мають бути додатними числами)");
        Console.WriteLine();

        double a  = ReadPositive("  Сторона a    = ");
        double b  = ReadPositive("  Сторона b    = ");
        double c  = ReadPositive("  Сторона c    = ");
        double d  = ReadPositive("  Сторона d    = ");

        try
        {
            quad.SetSides(a, b, c, d);
        }
        catch (ArgumentException ex)
        {
            PrintError(ex.Message);
            Pause();
            continue;
        }
    }
    else if (inputMode == "2")
    {
        // ── Ввід координат ─────────────────────────────────────────────────
        Console.WriteLine();
        Console.WriteLine("  Введіть координати чотирьох вершин A, B, C, D");
        Console.WriteLine("  (x і y через пробіл, наприклад: 3 4.5)");
        Console.WriteLine();

        Point pA = ReadPoint("  A");
        Point pB = ReadPoint("  B");
        Point pC = ReadPoint("  C");
        Point pD = ReadPoint("  D");

        quad.SetVertices(pA, pB, pC, pD);
    }
    else
    {
        PrintError("Невідомий вибір. Спробуйте знову.");
        Pause();
        continue;
    }

    // ── Крок 2: вибір операції ────────────────────────────────────────────
    bool operationLoop = true;
    while (operationLoop)
    {
        Console.WriteLine();
        PrintSeparator();
        Console.WriteLine("  Оберіть операцію:");
        Console.WriteLine("  [1] Обчислити периметр");
        Console.WriteLine("  [2] Обчислити площу");
        Console.WriteLine("  [3] Визначити тип");
        Console.WriteLine("  [4] Показати всі результати");
        Console.WriteLine("  [0] Повернутися до головного меню");
        Console.WriteLine();
        Console.Write("  Ваш вибір: ");

        string? op = Console.ReadLine()?.Trim();
        Console.WriteLine();

        switch (op)
        {
            case "1":
                PrintResult("Периметр", $"{quad.GetPerimeter():F4}");
                break;
            case "2":
                PrintResult("Площа", $"{quad.GetArea():F4}");
                break;
            case "3":
                PrintResult("Тип чотирикутника", quad.GetQuadrilateralType());
                break;
            case "4":
                Console.WriteLine("  ┌──────────────────────────────────────┐");
                Console.WriteLine($"  │  Периметр          : {quad.GetPerimeter(),12:F4}  │");
                Console.WriteLine($"  │  Площа             : {quad.GetArea(),12:F4}  │");
                Console.WriteLine($"  │  Тип               : {quad.GetQuadrilateralType(),-13}│");
                Console.WriteLine("  └──────────────────────────────────────┘");
                break;
            case "0":
                operationLoop = false;
                break;
            default:
                PrintError("Невідомий вибір. Спробуйте знову.");
                break;
        }
    }
}

Console.Clear();
Console.WriteLine();
Console.WriteLine("  До побачення!");
Console.WriteLine();

// ── Допоміжні функції ─────────────────────────────────────────────────────────

static void PrintHeader()
{
    Console.WriteLine("  ╔══════════════════════════════════════════════╗");
    Console.WriteLine("  ║      Лаб. №7 — Опуклий чотирикутник         ║");
    Console.WriteLine("  ╚══════════════════════════════════════════════╝");
    Console.WriteLine();
}

static void PrintSeparator()
    => Console.WriteLine("  ────────────────────────────────────────────────");

static void PrintResult(string label, string value)
    => Console.WriteLine($"  ✔  {label}: {value}");

static void PrintError(string msg)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n  ✘  {msg}");
    Console.ResetColor();
}

static void Pause()
{
    Console.WriteLine("\n  Натисніть Enter, щоб продовжити...");
    Console.ReadLine();
}

static double ReadPositive(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string? raw = Console.ReadLine()?.Replace(',', '.');
        if (double.TryParse(raw,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out double val) && val > 0)
            return val;

        PrintError("Введіть додатнє число (наприклад: 5 або 3.14).");
        Console.ResetColor();
    }
}

static Point ReadPoint(string label)
{
    while (true)
    {
        Console.Write($"{label} (x y): ");
        string[]? parts = Console.ReadLine()
            ?.Replace(',', '.')
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts?.Length == 2
            && double.TryParse(parts[0], System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double x)
            && double.TryParse(parts[1], System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double y))
            return new Point(x, y);

        PrintError("Введіть два числа через пробіл (наприклад: 3 4.5).");
        Console.ResetColor();
    }
}
