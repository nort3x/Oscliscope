using System;

namespace md.FlowControl
{
    public class Main
    {
        private Com com;
        private Grapher grapher;
        private const String banner = "h help \t\t show this banner\n" +
                                      "q exit \t\t exit program" +
                                      "" +
                                      "" +
                                      "";
        public Main(Com comobject)
        {
            com = comobject;
            grapher = new Grapher(ref com.getPool());
        }

        public void Run()
        {
            String cmd;
            while (true)
            {
                cmd = Console.In.ReadLine();


                if (cmd == "help" || cmd == "h")
                {
                    Console.Write(banner + "\n");
                }
                
                if (cmd == "exit" || cmd == "q")
                {
                    com.end();
                    grapher.end();
                    break;
                }
            }
        }
    }
}