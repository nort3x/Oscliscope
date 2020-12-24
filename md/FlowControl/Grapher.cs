using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;

using md.Objects;

namespace md.FlowControl
{
    public class Grapher
    {
        private static int id = 0;
        private Packet p;
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
            double dt = p.te / p.data.Length;
            for (int i = 0; i < p.data.Length; i++)
            {
                s.Append(i*dt).Append("\t").Append(p.data[i]).Append("\n");
            }
            
            File.WriteAllText("time"+id+".dat",s.ToString());
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