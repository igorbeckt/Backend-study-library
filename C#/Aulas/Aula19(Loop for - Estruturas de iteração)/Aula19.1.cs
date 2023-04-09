using System;

class Aula19{

    //Exemplo2-for com vetor
    static void Main(){    

        int[] num=new int[10];

        for(int i=0;i<num.Length;i++){
            num[i]=0;
        }

        for(int i=0;i<10;i++){
            Console.WriteLine("Valor de num na pos{0}: {1}",i,num[i]);
        }               
    } 
}