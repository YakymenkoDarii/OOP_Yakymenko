using System;

namespace TGeom
{
    public class TGeomProgressionM : TGeomProgression
    {
        public TGeomProgressionM() : base() { }
    public TGeomProgressionM(double firstTerm, double ratio) : base(firstTerm, ratio) { }
    public TGeomProgressionM(TGeomProgression other) : base(other) { }

    public bool IsGeometricSequence(int[] sequence)
    {
        if (sequence.Length < 2) return true;

        double ratio = (double)sequence[1] / sequence[0];
        for (int i = 2; i < sequence.Length; i++)
        {
            if (Math.Abs((double)sequence[i] / sequence[i - 1] - ratio) > 1e-9)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsMember(double number)
    {
        if (Math.Abs(b1) < 1e-9) return Math.Abs(number) < 1e-9;
        if (Math.Abs(number) < 1e-9) return false;

        double ratio = number / b1;

        if (q > 0 && ratio < 0) return false;
        if (Math.Abs(q - 1) < 1e-9) return Math.Abs(number - b1) < 1e-9;
        if (Math.Abs(q) < 1e-9) return Math.Abs(number - b1) < 1e-9;

        double nMinus1 = Math.Log(Math.Abs(ratio)) / Math.Log(Math.Abs(q));

        if (nMinus1 < -1e-9) return false;
        return Math.Abs(nMinus1 - Math.Round(nMinus1)) < 1e-9;
    }
}
}
