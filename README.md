# ShiftSync

ShiftSync é uma aplicação de gerenciamento de horário de trabalho que permite aos usuários registrar suas entradas, saídas e pausas durante o dia. Utilizando a arquitetura limpa, o projeto visa oferecer uma solução robusta e escalável para o controle de jornada de trabalho.

## Funcionalidades

- **Autenticação JWT:** Protege os endpoints da API utilizando tokens JWT para garantir que apenas usuários autenticados possam acessar e realizar operações.
- **Registro de Horário de Entrada e Saída:** Permite que os usuários registrem o horário de entrada e saída de suas jornadas de trabalho.
- **Controle de Pausas:** Registra o início e o fim das pausas durante a jornada de trabalho.
- **Relatórios Mensais:** Gera relatórios mensais detalhados com as horas trabalhadas, horas extras e horas de pausa.
- **Envio de Relatórios por Email:** Envia relatórios mensais para o email do usuário com detalhes sobre o seu desempenho.

## Estrutura do Projeto

- **ShiftSync.Api:** Contém os controladores e configurações da API.
- **ShiftSync.Application:** Implementa a lógica de negócios e serviços.
- **ShiftSync.Core:** Define interfaces e modelos de dados.
- **ShiftSync.Infrastructure:** Gerencia o acesso aos dados e configurações de persistência.

## Configuração

1. **Clonar o Repositório**

    ```bash
    git clone https://github.com/Digowmarins/ShiftSync.git
    cd ShiftSync
    ```

2. **Instalar Dependências**

    ```bash
    dotnet restore
    ```

3. **Configurar a String de Conexão**

    Atualize o arquivo `appsettings.json` com sua string de conexão para o banco de dados.

4. **Executar Migrations**

    Execute as migrations para configurar o banco de dados.

    ```bash
    dotnet ef database update
    ```

5. **Iniciar a Aplicação**

    ```bash
    dotnet run
    ```

## Uso

1. **Autenticação**

    Obtenha um token JWT autenticando-se com suas credenciais.

2. **Endpoints**

    - `POST /api/timelog/checkin` - Registra o horário de entrada.
    - `POST /api/timelog/checkout` - Registra o horário de saída.
    - `POST /api/timelog/break/start` - Inicia uma pausa.
    - `POST /api/timelog/break/end` - Finaliza uma pausa.
    - `POST /api/reports/monthly` - Gera um relatório mensal e envia por email.

## Contribuição

Se você deseja contribuir para o projeto, por favor, faça um fork do repositório e envie um pull request com suas alterações.
