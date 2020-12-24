using System;
using System.Collections.Generic;

namespace md.Objects
{
    public class FrequencyDomainPacket
    {
        public FourierCoffAtom[] fca;
        
        public FrequencyDomainPacket()
        {
            
        }

        public FrequencyDomainPacket(ref FourierCoffAtom[] fca)
        {
            if (fca.Length <= 1)
            {
                throw new Exception("UnSufficient Data samples");
            }
            this.fca = fca;
        }

        public FourierCoffAtom getDominantMode()
        {
            FourierCoffAtom mode = fca[1];
            for (int i = 2; i < fca.Length; i++)
            {
                if (fca[i].Amp > mode.Amp)
                {
                    mode = fca[i];
                }
            }
            return mode;
        }
        
        public void normalize(){
            FourierCoffAtom h = getDominantMode(fca);
            for(int i=1;i<fca.Length;i++){
                fca[i].Amp/=h.Amp;
            }
        }

        public FourierCoffAtom getDominantMode(FourierCoffAtom[] fca)
        {
            FourierCoffAtom mode = fca[1];
            for (int i = 2; i < fca.Length; i++)
            {
                if (fca[i].Amp > mode.Amp)
                {
                    mode = fca[i];
                }
            }
            return mode;
        }

        public FourierCoffAtom[] getDominantModes(int n)
        {
            FourierCoffAtom[] ans = new FourierCoffAtom[n];
            List<FourierCoffAtom> init_set = new List<FourierCoffAtom>(fca);
            for (int i = 0; i < n; i++)
            {
                ans[i] = getDominantMode(init_set.ToArray());
                init_set.Remove(ans[i]);
            }
            return ans;
        }
    }
}