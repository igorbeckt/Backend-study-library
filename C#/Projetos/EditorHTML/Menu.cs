using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorHTML
{
    public static class Menu
    {
        public static void Show()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;

            DrawScreen();
            WriteOptions();

            var option = short.Parse(Console.ReadLine());
            HandleMenuOption(option);
        }

        public static void DrawScreen() 
        {
            LineDraw();

            for (int lines = 0; lines <= 10; lines++) 
            {
                Console.Write("|");
                for (int i = 0; i <= 30; i++)
                    Console.Write(" ");

                Console.Write("|");
                Console.Write("\n");
            }

            LineDraw();
        }


        public static void LineDraw()
        {
            Console.Write("+");
            for (int i = 0; i <= 30; i++)
                Console.Write("-");

            Console.Write("+");
            Console.Write("\n");
        }
    
        public static void WriteOptions()
        {
            Console.SetCursorPosition(3, 1);
            Console.WriteLine("Editor HTML");
            Console.SetCursorPosition(3, 2);
            Console.WriteLine("============");
            Console.SetCursorPosition(3, 3);
            Console.WriteLine("Selecione uma opção abaixo");
            Console.SetCursorPosition(3, 5);
            Console.WriteLine("1 - Novo arquivo");
            Console.SetCursorPosition(3, 6);
            Console.WriteLine("2 - Abrir");
            Console.SetCursorPosition(3, 7);
            Console.WriteLine("0 - Sair");
            Console.SetCursorPosition(3, 8);
            Console.WriteLine("Opção: ");
            Console.SetCursorPosition(3, 9);
        }
    
    
        public static void HandleMenuOption(short option)
        {
            switch(option)
            {
                case 1: Editor.Show(); break;
                case 2: OpenText.Open(); break;
                case 0: {
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                }
                default: Show(); break;
            }
        }
    }
}
