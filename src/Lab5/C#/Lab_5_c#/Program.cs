using System;
using TGeom;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Testing Base Class ---");
        TGeomProgression gp1 = new TGeomProgression(2.0, 3.0);
        gp1.Output();
        Console.WriteLine($"4th term: {gp1.GetNthTerm(4)}");
        Console.WriteLine($"Sum of first 4 terms: {gp1.GetSum(4)}");

        Console.WriteLine("\n--- Testing Overloads ---");
        TGeomProgression gp2 = new TGeomProgression(3.0, 2.0);
        TGeomProgression gp3 = gp1 + gp2;
        gp3.Output();

        Console.WriteLine("\n--- Testing Derived Class ---");
        TGeomProgressionM gpM = new TGeomProgressionM(2.0, 3.0);

        Console.WriteLine($"Is 54 a member of gpM? {gpM.IsMember(54.0)}");
        Console.WriteLine($"Is 50 a member of gpM? {gpM.IsMember(50.0)}");

        int[] seq1 = { 2, 4, 8, 16 };
        int[] seq2 = { 2, 4, 9, 16 };
        Console.WriteLine($"Is {{2, 4, 8, 16}} a geometric sequence? {gpM.IsGeometricSequence(seq1)}");
        Console.WriteLine($"Is {{2, 4, 9, 16}} a geometric sequence? {gpM.IsGeometricSequence(seq2)}");
    }
}