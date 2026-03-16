using System;

namespace TGeom
{
    public class TGeomProgression
    {
        protected double b1;
    protected double q;

    public TGeomProgression()
    {
        b1 = 1.0;
        q = 1.0;
    }

    public TGeomProgression(double firstTerm, double ratio)
    {
        b1 = firstTerm;
        q = ratio;
    }

    public TGeomProgression(TGeomProgression other)
    {
        b1 = other.b1;
        q = other.q;
    }

    public virtual void Input()
    {
        Console.Write("Enter the first term (b1): ");
        b1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter the common ratio (q): ");
        q = Convert.ToDouble(Console.ReadLine());
    }

    public virtual void Output()
    {
        Console.WriteLine($"Geometric Progression: b1 = {b1}, q = {q}");
    }

    public double GetNthTerm(int n)
    {
        return b1 * Math.Pow(q, n - 1);
    }

    public double GetSum(int n)
    {
        if (Math.Abs(q - 1.0) < 1e-9)
        {
            return b1 * n;
        }
        return b1 * (Math.Pow(q, n) - 1) / (q - 1);
    }

    public static TGeomProgression operator +(TGeomProgression a, TGeomProgression b)
    {
        return new TGeomProgression(a.b1 + b.b1, a.q + b.q);
    }

    public static TGeomProgression operator -(TGeomProgression a, TGeomProgression b)
    {
        return new TGeomProgression(a.b1 - b.b1, a.q - b.q);
    }
}
}
