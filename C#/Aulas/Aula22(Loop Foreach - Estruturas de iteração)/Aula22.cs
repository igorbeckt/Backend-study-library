using System;

class Aula20{

    static void Main(){

        int [] num=new int[5]{11,22,33,44,55};

        for(int i=0;i<num.Length;i++){
            Console.WriteLine(num[i]);

        } 

        // a diferença entre o for e o foreach, é que o foreach é mais simples de se estruturar.

        foreach(int n in num){
            Console.WriteLine(n);
        }

    } 
}