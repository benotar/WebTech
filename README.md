# Web Tech

This repository contains a simple fullstack web-application.

## Description

This project is a full-stack web application built using:

- **Backend:** ASP.NET Core
- **Frontend:** React + Typescript

The backend provides a set of RESTful API endpoints, while the frontend consumes these APIs.

## Features

- CRUD operations for managing data
- Docker-compose orchestration
- Client-side routing using React Router
- State management using Zustand

To run this project locally, you need to have the following installed:

- [Docker](https://www.docker.com/products/docker-desktop/)
- [.NET SDK](https://dotnet.microsoft.com/en-us/download/)

## Getting Started

Follow these instructions to get the project up and running on your local machine.

### Setting Up Custom Domains

To test the web application with custom domains on your local machine, modify the `hosts` file to associate domain names with IP addresses.

### Editing the hosts File on Windows

1. Open Notepad as Administrator:
   - Search for "Notepad" in the Start menu.
   - Right-click on "Notepad" and select **Run as administrator**.

2. Open the `hosts` file:
   - In Notepad, click on **File > Open**.
   - Navigate to `C:\Windows\System32\drivers\etc`.
   - Change the file type from "Text Documents (.txt)" to "All Files (.*)".
   - Select the `hosts` file and click **Open**.

3. Add the following custom entries:

```plaintext
127.0.0.1 bg-local.com
127.0.0.1 api.bg-local.net
```

### Clone the Repository

```bash
git clone https://github.com/benotar/TZ.git
cd TZ/src/WebTech/WebTech.Backend
```

### Set JWT Token Secret

Check the contents of the buildhind.txt file. Copy the JWT token key and use it for the build process. Here's an example key you can use for testing:

```plaintext
MegaSecretKeyForDragonsAndWizards456
```

### Build the docker-compose file with data in buildhind.txt

Build the Docker Compose setup using the provided credentials and JWT secret:

```bash
docker-compose build --build-arg POSTGRES_DB_USERNAME=admin --build-arg POSTGRES_DB_PASSWORD=admin --build-arg JWT_SECRET=<your secret key>
```

### Start PostgreSQL with Docker-Compose

Start the PostgreSQL container:

```bash
docker-compose up postgres
```

### Restore Backend Dependencies

Navigate to the Web API project and restore the required dependencies:

```bash
cd TZ\src\WebTech\WebTech.Backend\WebTech\WebTech.WebApi
dotnet restore
```

### Apply Database Migrations

Move to the persistence project folder and apply the migrations:

```bash
cd TZ/src/WebTech/WebTech.Backend/WebTech/WebTech.Peristence
dotnet ef database update -s ../WebTech.WebApi/WebTech.WebApi.csproj
```

### Shut Down PostgreSQL and Run the Full Project

After successfully applying the migrations, stop the PostgreSQL container and run the entire project:

```bash
cd TZ/src/WebTech/WebTech.Backend
docker-compose down
docker-compose up
```

### Go to [local web-page](http://bg-local.com:3000/) and test

## Contact
If you have any questions, create issue, or contact me using [Instagram](https://www.instagram.com/benotar_) or Telegram (@benotaar).
