using System;

class Aula12{
    static void Main(){

        //Exemplo2
        int nota=0;
        string resultado1="Reprovado";

        nota=int.Parse(Console.ReadLine());

        if(nota >= 60){
            resultado1="Aprovado";
        }

        Console.WriteLine("Resultado1: {0}",resultado1);


    } 

}