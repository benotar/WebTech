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
- [.NET SKD](https://dotnet.microsoft.com/en-us/download/)

## Getting Started

Follow these instructions to get the project up and running on your local machine.

### Setting Up Custom Domains
To test web applications with custom domains on your local machine, you need to edit the hosts file. This will associate domain names with IP addresses.

### Steps to Edit the hosts File on Windows

Open Notepad as Administrator:
Search for "Notepad" in the Start menu.
Right-click on "Notepad" and select Run as administrator.

### Open the hosts file:

In Notepad, click on File > Open.
Navigate to C:\Windows\System32\drivers\etc.
Change the file type from "Text Documents (.txt)" to "All Files (.*)".
Select the hosts file and click Open.

### Add custom entries:

```
127.0.0.1 bg-local.com
127.0.0.1 api.bg-local.net
```

### Clone the Repository

```bash
git clone https://github.com/benotar/TZ.git
cd TZ/src/WebTech/WebTech.Backend
```

### Check the Contents of buildhind.txt

Copy the data from the file and paste your key for the jwt token. My test key = MySuperSecretKeyForTestTZ—-111—-222—-333

### Build the docker-compose file with data in buildhind.txt

```bash
docker-compose build --build-arg POSTGRES_DB_USERNAME=admin --build-arg POSTGRES_DB_PASSWORD=admin --build-arg JWT_SECRET=<your secret key>
```

### Start the postgres with docker-compose to apply migrations

```bash
docker-compose up postgres
```

### Apply migrations to work with the database

Dependencies may need to be restored

```bash
cd TZ/src/WebTech/WebTech.Backend/WebTech
```

Optional:
=====================
```bash
dotnet restore
```
=====================

```bash
dotnet ef database update -s ../WebTech.WebApi/WebTech.WebApi.csproj
```

### After successfully applying the migrations, run the project

```bash
cd TZ/src/WebTech/WebTech.Backend
docker-compose up
```

### Go to [local web-page](http://bg-local.com:3000/) and test

## Contact
If you have any questions, create issue, or contact me using [Instagram](https://www.instagram.com/benotar_) or Telegram (@benotaar)