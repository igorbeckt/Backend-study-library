using System;

class Aula12{
    static void Main(){

        //Exemplo1
        int note=80;
        string resultado="Reprovado";

        if(note >= 60){
            resultado="Aprovado";
        }

        Console.WriteLine("Resultado:{0}",resultado);


    } 

}