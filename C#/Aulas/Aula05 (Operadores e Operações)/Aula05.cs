using System;

class Aula05{

    static void Main(){

        // exemplo1
        int res=10+5;

        Console.WriteLine(res); 

        // exemplo2
        int res1=(10+5)*2;

        Console.WriteLine(res1);

        // exemplo3
        int res2=10+5*2;

        Console.WriteLine(res2); 

        // exemplo4
        bool  res3=10<5; // 10 é menor que 5?

        Console.WriteLine(res3); // Falso

        //exemplo5
        bool  res4=10>5; // 10 é maior que 5?

        Console.WriteLine(res4); // True

        //exemplo6
        bool  res5=10!=5; // 10 é diferente 5?

        Console.WriteLine(res5); // True

        //exemplo7
        bool  res6=10!=10; // 10 é diferente 10?

        Console.WriteLine(res6); // False

        //exemplo8
        int num=10;

        num++;//num+=1;//num=num+1; (As 3 operações dão o mesmo resultado)

        Console.WriteLine(num);

        //exemplo9

        bool res7=(5>3)|(10<5);
        // | = OR  / OU

        Console.WriteLine(res7);

        //exemplo9

        bool res8=(5>3)&(10<5);
        // & = AND / E

        Console.WriteLine(res8);
    }



}