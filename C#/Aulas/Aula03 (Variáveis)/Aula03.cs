using System;

class Aula03{
    static void Main(){
        byte n1=10; // 0 e 255
        int num=-10;
        char letra='c';
        float valor=5.3f;
        string nome="Bruno"; 

        var aux=10;
        var aux1="Igor";
        var aux2=nome;

        Console.WriteLine("tenho "+ aux + " balas!"); 

        int num1,num2,res;

        num1=10;
        num2=2;
        res= num1 * num2;

        Console.WriteLine("A multiplicação de " + num1 + " vezes " + num2 + " é igual a: " + res);
    }
}