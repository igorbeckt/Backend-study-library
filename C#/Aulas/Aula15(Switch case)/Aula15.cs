using System;

class Aula15{
    static void Main(){

        int tempo=0;
        char escolha;
        
        Console.WriteLine("Belo Horizonete/MG a Vitória/ES");
        Console.WriteLine("Escolha o transporte:[a]Avião | [c]carro | [o]Ônibus");

        escolha=char.Parse(Console.ReadLine());
        
        switch(escolha){
            case 'a':
            case 'A':
                tempo=50;
                break;
            case 'c':
            case 'C':
                tempo=480;
                break;
            case 'o':
            case 'O':
               tempo=660;
               break;
            default:
            tempo=-1;
            break;       

        }

        if(tempo<0){
            Console.WriteLine("Transporte indisponivel");
        }else{
            Console.WriteLine("Para o transporte escolhido o tempo é: {0} minutos",tempo);
        }
    } 
}