
# API CleanArch + CQRS

Web API desenvolvida em C#, que implementa um CRUD básico para gerenciar informações da classe 'Blog'. 

Utiliza os princípios de Clean Architecture, sendo dividida em quatro camadas principais: API, Application, Domain e Infra (separação clara de responsabilidades para facilitar a manutenção e a escalabilidade do código).

Utiliza o Entity Framework Core para a interação com o banco de dados SQLite. 

Também foi implementado o padrão CQRS (Command Query Responsibility Segregation), separando as operações de leitura e escrita, otimizando a performance da API e permitindo o uso de múltiplos bancos de dados para diferentes propósitos, se necessário.

Faz uso das bibliotecas:
- AutoMapper: para mapear objetos de forma simplificada;
- FluentValidation: para validar os dados de entrada de maneira consistente e elegante. 


## Autores

- [@brunohmsato](https://www.github.com/brunohmsato)


## Instalação

- Clone o projeto em sua máquina;

- Abra o 'Console do Gerenciador de Pacotes';

- Selecione o Projeto Padrão como 'CQRS.Infra';

- Execute:

```bash
  update-database
```
    
