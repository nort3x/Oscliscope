using System;

namespace md.FlowControl
{
    public class Main
    {
        private Com com;
        private Grapher grapher;
        private const String banner = "h help \t\t show this banner\n" +
                                      "q exit \t\t exit program\n" +
                                      "t time \t\t start probing time domain signal\n" +
                                      "f freq \t\t start probing frequency domain singanl\n" +
                                      "qos \t\t checking quality of serial connection\n"+
                                      "command <command2send> \t\t send command to Arduino\n";
        public Main(Com comobject)
        {
            com = comobject;
            grapher = new Grapher(ref com.getPool()); // for first channel
        }

        public void Run()
        {
            
            Console.WriteLine(banner);
            String cmd;
            while (true)
            {
                Console.Write("#> ");
                cmd = Console.In.ReadLine();


                if (cmd == "help" || cmd == "h")
                {
                    Console.Write(banner + "\n");
                }
                
                else if (cmd == "exit" || cmd == "q")
                {
                    com.end();
                    grapher.end();
                    break;
                }
                
                else if (cmd == "time")
                {
                    grapher.toggleTimeDomain();
                }
                
                else if (cmd == "freq")
                {
                    grapher.toggleFreqDomain();
                }
                
                else if (cmd.Contains("command"))
                {
                    cmd = cmd.Replace("command ", "");
                    com.sendCommand(cmd);
                }
                else if (cmd == "qos")
                {
                    Console.Write("Number of malformed received packets: "+com.getNumberOferrs()+"\n");
                }
            }
        }
    }
}