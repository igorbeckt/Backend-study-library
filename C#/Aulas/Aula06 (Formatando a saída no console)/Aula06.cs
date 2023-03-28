using System;

class Aula06{

    static void Main(){

        //Exemplo1
        int n1,n2,n3;
        n1=10; n2=20; n3=30;

        Console.Write("EX1:n1={0}, n2={1}, n3+{2}" ,n1,n2,n3); //(n1 +" , " + n2 + " , " + n3)

        //Exemplo2
        int n4,n5,n6;
        n4=10; n5=20; n6=30;

        Console.Write("\nEX2: \nn4={0} \nn5={1} \nn6+{2}" ,n4,n5,n6); //\n quebra de linha

        //Exemplo3
        int n7,n8,n9;
        n7=10; n8=20; n9=30;

        Console.Write("\nEX3: \nn7=\t{0} \nn8=\t{1} \nn9=\t{2}" ,n7,n8,n9); //\t afasta o conteudo 

        //exemplo4    
        double valorCompra=5.50;
        double valorVenda;
        double lucro=0.1;
        string produto="Pastel";
        
        valorVenda=valorCompra+(valorCompra*lucro);
        
        Console.WriteLine("\nEx4:");
        Console.WriteLine("Produto......:{0,15}",produto); // :{(indice da variavel),(valor do espaçamento)}
        Console.WriteLine("Val.Compra...:{0,15:c}",valorCompra); // :c adiciona valor de moeda (R$)
        Console.WriteLine("Lucro........:{0,15:p}",lucro);       // :p adiciona porcentagem
        Console.WriteLine("Val.Venda....:{0,15:p}",valorVenda);

    }

}        