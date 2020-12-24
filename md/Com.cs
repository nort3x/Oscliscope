using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Xml;
using md.Objects;

namespace md
{
    
   

    
    public class Com
    {
        private int NumberOfErr = 0;

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
            int n;
            int UPTO = 1000000;
            while (true)
            {
                for (int i = 0; i < UPTO; i++)
                {
                    
                    if (s.ReadByte() == 9);        // begin flag
                        goto CAPA;
                }
                throw new Exception("REACHED the UPP limit of nonsense");
                
                CAPA:
                if (sp.ReadByte() == 15)
                {
                    try
                    {
                        p.te = Convert.ToDouble(sp.ReadLine()); // try to read field value te

                    }
                    catch (Exception e)
                    {
                        Console.Write("");
                        continue; // if couldnot convert just continue
                    }

                    if (s.ReadByte() == 15) // next field value n
                    {
                        try
                        {
                            n = Convert.ToInt16(s.ReadLine());
                        }
                        catch (Exception e)
                        {
                            NumberOfErr++;
                            continue;
                        }

                        if (s.ReadByte() == 15)
                        {
                            int[] data = new int[n];
                            int highval;
                            int lowval;
                            for (int i = 0; i < n; i++)
                            {
                                highval = s.ReadByte();
                                lowval = s.ReadByte(); // some checks for checking validity 
                                data[i] = (short)(((highval) & 0xFF) << 8 | (lowval) & 0xFF);
                            }

                            p.data = data;
                            if (s.ReadByte() == 14)
                            {
                                return p;
                            }
                        }
                    }
                }
            }
        }


        public ref ConcurrentQueue<Packet> getPool()
        {
            return ref pool;
        }

        public int getNumberOferrs()
        {
            return NumberOfErr;
        }

        public void end()
        {
            run = false;
        }
    }
}