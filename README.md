📄 Disponível em: [Inglês/English](README.en.md)

# FIAP Cloud Games

### Projeto criado para desafio Tech Challenge da Pós Graduação Arquitetura de Sistemas .NET com Azure da [FIAP](https://www.fiap.com.br/).

O objetivo desta fase é desenvolver uma solução baseada em uma API REST em .NET 8 para gerenciar uma loja virtual de games.

---
## 🔧 Ferramentas
Esta seção descreve as ferramentas utilizadas ao longo do desenvolvimento do projeto:

- **[Egon.io](https://egon.io/):** Elaboração do *Domain Storytelling*, facilitando o mapeamento das interações entre os atores e elementos do sistema.
- **[Miro](https://miro.com/):** Plataforma visual colaborativa usada para o mapeamento de eventos com *Event Storming* e planejamento inicial da solução.
- **[Notion](https://www.notion.so/):** Documentação interna, centralizando informações importantes sobre padronização de desenvolvimento para o projeto.
- **[GitHub](https://github.com/):** Sistema de controle de versão para hospedagem do código-fonte e colaboração entre os integrantes.
- **[Github Projects](https://docs.github.com/pt/issues/planning-and-tracking-with-projects/learning-about-projects/about-projects):** Gestão do fluxo de trabalho através de um quadro Kanban com integração com as issues do projeto no Github.
- **[Visual Studio](https://visualstudio.microsoft.com/pt-br/) e [Jetbrains Rider](https://www.jetbrains.com/rider/):** bientes de desenvolvimento utilizados pelos membros da equipe para codificação em .NET 8.
- **[Docker Desktop]():** Utilizado para containerizar a aplicação .NET, facilitando a criação de ambientes reprodutíveis, independentes do sistema operacional dos desenvolvedores.

---

## 📄 Documentação
- EventStorming e Domain Storytelling do projeto: https://miro.com/app/board/uXjVIjBiF7Q=/?share_link_id=826571685187

---

## 🤝 Contribuidores

- Eduarda Matias - [LinkedIn](https://www.linkedin.com/in/eduarda-matias/) e [Github](https://github.com/eduardamatias)
- Gabriel Longas – [LinkedIn](https://www.linkedin.com/in/gabriellongas/) e [Github](https://github.com/gabriellongas)
- Matheus Soares - [LinkedIn](https://www.linkedin.com/in/matheus-soares-camacho-947859209/) e [Github](https://github.com/MatFoxDie)
- Pedro Luperini - [LinkedIn](https://www.linkedin.com/in/pedro-luperini-piza/) e [Github](https://github.com/BRPeekz)
- Rafaela Nascimento - [LinkedIn](https://www.linkedin.com/in/rafaela-nasc/) e [Github](https://github.com/RafaelaNasciment)

---
## ⚙️ Como Executar

Para executar o projeto, primeiramente é necessário ter o Docker instalado na máquina.
É possível instalar o docker através da página de instalação do [Docker Desktop](https://docs.docker.com/desktop/setup/install/windows-install/).

Com o Docker instalado, vá na pasta raiz do projeto, onde possui o arquivo *docker-compose.yml* e execute o comando abaixo em um terminal:

```
docker-compose up -d
```
Isto fará a criação de um container de SQL Server já pronto para ser utilizado pelo projeto.
Caso queira conectar ao banco de dados instanciado no Docker, basta apenas utilizar a connection string com o o nome *"DefaultConnection"* dentro do arquivo  [`appsettings.json`](src/FIAP.CloudGames.API/appsettings.json).