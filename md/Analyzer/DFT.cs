using System;
using System.Numerics;
using md.Objects;

namespace md.Analyzer
{
    public class DFT
    {
        public static FrequencyDomainPacket getDFTOfTimePacket(Packet p)
        {
            // asume delta_t = 1
           //  find FC then use affine transform on time

           FourierCoffAtom[] atomlist = new FourierCoffAtom[p.data.Length/2];

           Complex Xi;
           for (int i = 0; i < p.data.Length/2; i++) // for each frequancy
           {
               Xi = new Complex(0,0);
               for (int j = 0; j < p.data.Length-1; j++)
               {
                   Xi += 0.0049*p.data[j]*Complex.Exp(new Complex(0,2*Math.PI*i*j/p.data.Length));
               }
               atomlist[i].Amp = 2 * Xi.Magnitude;
               atomlist[i].Phase = Xi.Phase;
               atomlist[i].Frequency = (i / p.te) * 1000000; // because its micro (sorry for gunshot)
           }
           return new FrequencyDomainPacket(ref atomlist);
        }
    }
}