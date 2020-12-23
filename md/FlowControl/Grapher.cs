using System.Collections.Concurrent;
using GnuPlotSharp;
using md.Objects;

namespace md.FlowControl
{
    public class Grapher
    {
        private bool td, fd;
        private ConcurrentQueue<Packet> pool;
        public Grapher(ref ConcurrentQueue<Packet> pool)
        {
            td = false;
            fd = false;
            this.pool = pool;
        }

        public void toggleTimeDomain()
        {
            if (td)
            {
            }
            else
            {
                
            }
        }
        public void end()
        {
            
        }
    }
}