using System;

class Aula24{
    
    static void Main(){
        int num=50;
        dobrar(ref num);
        Console.WriteLine(num);
    }
    
    //Passagem por referência
    static void dobrar(ref int valor){
        valor*=2;
    }

    //Passagem por valor
    static void dobrar1(int valor){
        valor*=2;
    }
}