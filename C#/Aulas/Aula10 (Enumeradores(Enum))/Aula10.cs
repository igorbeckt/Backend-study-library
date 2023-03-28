using System;

class Aula10{

    enum DiasSemana{Domingo,Segunda,Terça,Quarta,Quinta,Sexta,Sábado};
    static void Main(){

        //Exemplo1
        DiasSemana ds = DiasSemana.Domingo;

        Console.WriteLine(ds);

        //Exemplo2
        DiasSemana ds1 = (DiasSemana)3;

        Console.WriteLine(ds1);

        //Exemplo3
        int ds2=(int)DiasSemana.Sexta;

        Console.WriteLine(ds2);

        
    }
}