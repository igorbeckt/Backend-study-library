<h1 align="center">Cronômetro</h1>
<p align="center"><i>O projeto "Stopwatch" implementa um cronômetro (stopwatch) de console simples para medir o tempo decorrido entre eventos.</i></p>

## Funcionalidades

- **Contagem de Tempo**: O cronômetro permite que o usuário informe um tempo de contagem, podendo ser em segundos ou minutos. Por exemplo:
S = segundos => 10s = 10 segundos
M = minutos => 1m = 1 minuto
- **Início e Contagem**: Após o usuário informar o tempo de contagem, o cronômetro exibirá uma contagem regressiva para iniciar a medição do tempo. A contagem será exibida em três etapas: "Ready...", "Set...", "GO...!".
- **Medição do Tempo**: Após a contagem regressiva, o cronômetro iniciará a medição do tempo. O tempo será exibido em segundos, incrementando a cada segundo que passa.
- **Finalização e Retorno ao Menu**: Quando o tempo programado for atingido, o cronômetro exibirá a mensagem "finished stopwatch" e aguardará por 2,5 segundos antes de retornar ao menu inicial.
- **Sair**: O usuário pode digitar "0s" para sair do cronômetro e encerrar a aplicação.

## Como Usar

1. Execute a aplicação e o menu inicial será exibido.
   
2. Digite o tempo de contagem desejado, seguindo o formato S ou M (por exemplo, 10s ou 1m) e pressione Enter.
   
3. O cronômetro exibirá a contagem regressiva e iniciará a medição do tempo após a mensagem "GO...!".
  
4. O tempo decorrido será exibido em segundos, e o cronômetro continuará contando até que o tempo programado seja atingido.
   
5. Quando o tempo programado for alcançado, o cronômetro exibirá "finished stopwatch" antes de retornar ao menu inicial.
    
6. Para sair do cronômetro, digite "0s" no menu principal e pressione Enter.


```ruby
S = segundos => 10s = 10 segundos
M = minutos => 1m = 1 minuto
0s = sair
Quanto tempo deseja contar?
---------------------------------------
Resposta: 1M
---------------------------------------
Ready...
Set...
Go...!
(Cronômetro começa a contar)
```



## Requisitos do Sistema

- .NET Core 3.1 ou superior
  
 ### Linguagem de programação
<p display="inline-block">
  <img width="48" src="https://www.freeiconspng.com/uploads/c-logo-icon-18.png" alt="csharp-logo"/>
</p>
                                                                                                  
### Ferramentas de desenvolvimento

<p display="inline-block">
  <img width="48" src="https://static.wikia.nocookie.net/logopedia/images/e/ec/Microsoft_Visual_Studio_2022.svg" alt="vs-logo"/>
  
  <img width="48" src="https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Visual_Studio_Code_1.35_icon.svg/2048px-Visual_Studio_Code_1.35_icon.svg.png" alt="vscode-logo"/>
</p>


## Autor

Nome: Igor Nascimento                                                                                                                           
Email: igorbeckt@hotmail.com
