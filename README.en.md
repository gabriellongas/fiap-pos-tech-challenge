📄 Available in: [Portuguese](README.md)

# FIAP Cloud Games

### Project created for the Tech Challenge of the Postgraduate Program in .NET Systems Architecture with Azure at [FIAP](https://www.fiap.com.br/).

The objective of this phase is to develop a solution based on a REST API using .NET 8 to manage a virtual game store.

---

## 🔧 Tools
This section outlines the main tools used during the development of the project:

- **[Egon.io](https://egon.io/):** Used for building *Domain Storytelling*, helping map the interactions between system actors and elements.
- **[Miro](https://miro.com/):** Visual collaboration platform used for *Event Storming* diagrams and initial solution planning.
- **[Notion](https://www.notion.so/):** Centralized documentation hub, containing important project guidelines and development standards.
- **[GitHub](https://github.com/):** Version control system for hosting the source code and enabling team collaboration.
- **[GitHub Projects](https://docs.github.com/en/issues/planning-and-tracking-with-projects/learning-about-projects/about-projects):** Kanban-style project board used to manage workflow and track issues throughout the project.
- **[Visual Studio](https://visualstudio.microsoft.com/)** & **[JetBrains Rider](https://www.jetbrains.com/rider/):** IDEs used by the development team for building the .NET 8 application.
- **[Docker Desktop](https://www.docker.com/products/docker-desktop/):** Used to containerize the .NET application, enabling the creation of reproducible environments regardless of the developer's operating system.

---

## 📄 Documentation
EventStorming and Domain Storytelling of the project: https://miro.com/app/board/uXjVIjBiF7Q=/?share_link_id=826571685187

---

## 🤝 Contributors

- Eduarda Matias - [LinkedIn](https://www.linkedin.com/in/eduarda-matias/) | [GitHub](https://github.com/eduardamatias)
- Gabriel Longas – [LinkedIn](https://www.linkedin.com/in/gabriellongas/) | [GitHub](https://github.com/gabriellongas)
- Matheus Soares - [LinkedIn](https://www.linkedin.com/in/matheus-soares-camacho-947859209/) | [GitHub](https://github.com/MatFoxDie)
- Pedro Luperini - [LinkedIn](https://www.linkedin.com/in/pedro-luperini-piza/) | [GitHub](https://github.com/BRPeekz)
- Rafaela Nascimento - [LinkedIn](https://www.linkedin.com/in/rafaela-nasc/) | [GitHub](https://github.com/RafaelaNasciment)

---

## ⚙️ How to Run
To run the project, Docker must be installed on your machine.
You can install Docker via the Docker Desktop installation page.

With Docker installed, go to the root folder of the project, where the docker-compose.yml file is located, and run the following command in a terminal:

docker-compose up -d
This will create a SQL Server container ready to be used by the project.
If you want to connect to the database instantiated in Docker, simply use the connection string named "DefaultConnection" found in the appsettings.json file.