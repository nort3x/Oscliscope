﻿  using System;
  using System.Diagnostics;
  using System.IO.Ports;
  using System.Linq;
  using System.Timers;
  using GLib;
  using md.Analyzer;
  using md.FlowControl;
  using md.Objects;
  using Thread = System.Threading.Thread;

  namespace md
  {
      class Program
      {
          private const String SerialName = "/dev/ttyACM0";

          static void Main(string[] args)
          {

              // int[] data = new int[500];
              // for (int i = 0; i < 500; i++)
              // {
              //     data[i] = (int)(Math.Sin(2 * Math.PI*i/500)*1024);
              // }
              // //Packet populater = tester.populater.getPacket(500);
              // // Packet populater = new Packet();
              // // populater.data = data;
              // // populater.te = 1000000;
              // var f= DFT.getDFTOfTimePacket(populater);
              //
              Begin:
              try
              {

                  Com c = new Com(SerialName, 115200);
                  new Main(c).Run();

              }
              catch
              {
                  Console.WriteLine("cannot establish serial connection");
                  Thread.Sleep(5000);
                  goto Begin;
              }
          }
      }
  }