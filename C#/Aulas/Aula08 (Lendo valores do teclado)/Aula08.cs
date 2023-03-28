using System;

class Aula08{

    static void Main(){
        
        //exemplo1
        string nome;

        Console.Write("Digite seu nome: ");
        nome=Console.ReadLine();        
        Console.WriteLine("Nome digitado: {0}", nome);

        //exemplo2
        int v1,v2,soma;

        Console.Write("Digite o primeiro valor: ");
        v1=int.Parse(Console.ReadLine());
        Console.Write("Digite o segundo valor: ");
        v2=Convert.ToInt32(Console.ReadLine());
        soma=v1+v2;
        Console.WriteLine("A soma de {0} mais {1} é igual a {2}",v1,v2,soma);

    }
}