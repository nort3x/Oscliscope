using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;
using md.Analyzer;
using md.Objects;

namespace md.FlowControl
{
    public class Grapher
    {
        private static int id = 0;
        private Packet p,qf;
        private bool td, fd;
        private ConcurrentQueue<Packet> pool;
        public Grapher(ref ConcurrentQueue<Packet> pool)
        {
            id++;
            td = false;
            fd = false;
            this.pool = pool;
        }

        private void writePacketTofile()
        {
            StringBuilder s = new StringBuilder();
            double dt = p.te / (p.data.Length*1000000);
            for (int i = 0; i < p.data.Length; i++)
            {
                s.Append(i*dt).Append("\t").Append(p.data[i]*0.00488).Append("\n");
            }
            
            File.WriteAllText("time"+id+".dat",s.ToString());
        }
        private void writeFPacketTofile(FrequencyDomainPacket fp)
        {
            StringBuilder s = new StringBuilder();
            for (int i = 1; i < fp.fca.Length; i++)
            {
                s.Append(fp.fca[i].Frequency).Append("\t").Append(fp.fca[i].Amp)
                    .Append('\t').Append(fp.fca[i].Phase).Append("\n");
            }
            
            File.WriteAllText("freq"+id+".dat",s.ToString());
        }


        

        public void toggleTimeDomain()
        {
            if (!td)
            {
                td = true;
                new Thread(() =>
                {
                    while (td)
                    {
                        if (pool.TryDequeue(out p))
                        {
                           writePacketTofile();
                           
                        };
                        
                    }
                    
                    
                }).Start();
            }
            else
            {
                td = false;
            }
        }

        public void toggleFreqDomain()
        {
            if (!fd)
            {
                fd = true;
                new Thread(() =>
                {
                    while (fd)
                    {
                        if (pool.TryPeek(out qf))
                        {
                            writeFPacketTofile(DFT.getDFTOfTimePacket(qf));    
                        }
                        
                    }
                    
                    
                }).Start();
            }
            else
            {
                fd = false;
            }
        }
        public void end()
        {
            fd = false;
            td = false;

        }
    }
}