using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stopwatch
{
    public static class Menu
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("S = segundos => 10s = 10 segundos");
            Console.WriteLine("M = minutos => 1m = 1 minuto");
            Console.WriteLine("0s = sair");
            Console.WriteLine("Quanto tempo deseja contar?");

            string data = Console.ReadLine().ToLower();
            char type = char.Parse(data.Substring(data.Length - 1, 1));
            int time = int.Parse(data.Substring(0, data.Length - 1));
            int multiplier = 1;

            if (type == 'm')
                multiplier = 60;

            if (time == '0')
                System.Environment.Exit(0);

            Operation.PreStart(time * multiplier);
        }
    }
}
