using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EditorHTML
{
    public static class Editor
    {
        public static void Show() 
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("MODO EDITOR");
            Console.WriteLine("-------------");
            Start();
        }

        public static void Start() 
        {
            var text = new StringBuilder();

            do
            {
                text.Append(Console.ReadLine());
                text.Append(Environment.NewLine);
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            Console.WriteLine("-------------");
            Console.WriteLine("Deseja salvar o arquivo?");

            Console.WriteLine("Digite: true para (sim) \nDigite: false para (não)");

            var resultado = Console.ReadLine();

            if (resultado == "sim") 
            {
                Save(text);
            }
            else
            {
                Menu.Show();
            }
                
        }

        public static void Save(StringBuilder text)
        {
            Console.Clear();
            Console.WriteLine("Qual caminho para salvar o arquivo?");
            var path = Console.ReadLine();

            try
            {
                using (var file = new StreamWriter(path))
                {
                    file.Write(text);
                }

                Console.WriteLine($"Arquivo {path} salvo com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar o arquivo: {ex.Message}");
            }

        }
    }
}
