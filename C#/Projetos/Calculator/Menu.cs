using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
     public static class Menu
    {
        public static void Show()
        {
            Console.Clear();

            Console.WriteLine("O que você deseja calcular?");
            Console.WriteLine("1 - soma");
            Console.WriteLine("2 - subtração");
            Console.WriteLine("3 - divisão");
            Console.WriteLine("4 - multiplicação");
            Console.WriteLine("5 - sair");

            Console.WriteLine("----------------------");
            Console.WriteLine("Selecione uma opção:");
            short resultado = short.Parse(Console.ReadLine());

            switch (resultado)
            {
                case 1: Operation.Soma(); break;
                case 2: Operation.Subtracao(); break;
                case 3: Operation.Divisao(); break;
                case 4: Operation.Multiplicacao(); break;
                case 5: System.Environment.Exit(0); break;
                default: Menu.Show(); break;
            }
        }
     }
}
