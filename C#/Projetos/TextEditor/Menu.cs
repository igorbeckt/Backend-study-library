using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    public static class Menu
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine("1 - abrir arquivo");
            Console.WriteLine("2 - criar novo arquivo");
            Console.WriteLine("0 - sair");
            short option = short.Parse(Console.ReadLine());

            switch (option)
            {
                case 0: System.Environment.Exit(0); break;
                case 1: Operation.Abrir(); break;
                case 2: Operation.Editar(); break;
                default: Menu.Show(); break;
            }
        }
    }
}
