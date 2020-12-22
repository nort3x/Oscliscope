using System;
using System.Text;

namespace md.Objects
{
    public struct FourierCoffAtom
    {
        public double Frequency;
        public double Amp;
        public double Phase;

        public override string ToString()
        {
            return new StringBuilder().Append(Math.Round(Amp,1)).Append(" Cos(2Pi "+Math.Round(Frequency,1)+" + "+Math.Round(Phase,1)+")").ToString();
        }
    }
}