using System;
using System.Security.Cryptography;
using md.Objects;

namespace md.tester
{
    public class populater
    {
        public static Packet getPacket(int samples_count)
        {
            
            Packet p = new Packet();
            Random r = new Random();

            p.te = r.NextDouble() * 350;
            p.data = new int[samples_count];
            for (int i = 0; i < samples_count; i++)
            {
                p.data[i] = r.Next(0, 1024);
            }

            return p;
        } 
       
    }
}