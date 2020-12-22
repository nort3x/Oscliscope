using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using md.Objects;

namespace md
{
    
   

    
    public class Com
    {

        private SerialPort s;
        private ConcurrentQueue<Packet> pool;
        private bool run;
        public Com(String portname, int baudRate)
        {
            s = new SerialPort(portname,baudRate);
            s.DtrEnable = true;
            pool = new ConcurrentQueue<Packet>();
            s.Open();
            run = true;

            new Thread(() =>
            {
                while (run)
                {
                    try
                    {
                        pool.Enqueue(readPacket(s));
                    }
                    catch (Exception e)
                    {
                        Console.Error.Write(e);
                    }

                }
            }).Start();
        }



        private Packet readPacket(SerialPort sp)
        {
            Packet p =  new Packet();
            int buff;
            int UPTO = 1000000;
            while (true)
            {
                for (int i = 0; i < UPTO; i++)
                {
                    buff = sp.ReadByte();
                    if (buff == 9);
                        goto CAPA;
                }
                throw new Exception("REACHED the UPP limit of nonsense");
                
                CAPA:
                buff = sp.ReadByte();
                if (buff != 10)
                    continue;
                else
                {
                    p.te =  Convert.ToDouble(sp.ReadLine());
                    buff = sp.ReadByte();
                    if(buff !=11)
                        continue;

                    List<int> tempbuff = new List<int>();
                    while (true)
                    {
                        tempbuff.Add(Convert.ToInt16(sp.ReadLine()));
                        buff = sp.ReadByte();
                        if (buff == 14)
                        {
                            p.data = tempbuff.ToArray();
                            break;
                        }
                            
                    }

                    return p;
                }
            }
        }
        
        
        
    }
}