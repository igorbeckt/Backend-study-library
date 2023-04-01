using System;

class Aula15{
    static void Main(){

        //Exemplo2
        int[] n=new int[5];
        int[] num=new int[3]{55,77,99};
        /*             ou
        int[] num={55,77,99}; */

        n[0]=111;
        n[1]=222;
        n[2]=333;
        n[3]=444;
        n[4]=555;

        Console.WriteLine(num[2]);
    } 
}