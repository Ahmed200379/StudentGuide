# StudentGuide

## 📌 Overview
StudentGuide is a comprehensive academic guide platform designed to assist students in managing their studies efficiently. The system offers features such as subject registration, material repository, student activities, and a chatbot for guidance on lectures, sections, and technologies.

## 🚀 Features
- 📚 **Subject Registration** – Easily enroll in courses and manage your academic journey.
- 🗂 **Material Repository** – Store and access academic materials like lecture notes and research papers.
- 🤖 **AI Chatbot** – Get instant answers about sections, lectures, and technologies.
- 🎯 **Student Activities** – Stay updated with extracurricular and academic activities.

## 🛠 Tech Stack
- **Backend:** .NET Core RESTful API
- **Architecture:** 3-Tier Architecture, Repository Pattern, Unit of Work
- **Authentication:** JWT (JSON Web Token)
- **Database:** SQL Server, Entity Framework (EF) Core ORM
- **Frontend:** (To be added if applicable)

## 🔧 Setup and Installation
### 1️⃣ Prerequisites
Ensure you have the following installed:
- .NET SDK 8.0
- SQL Server
- Visual Studio / VS Code
- Postman (optional for API testing)

### 2️⃣ Clone the Repository
```sh
 git clone https://github.com/Ahmed200379/StudentGuide.git
 cd StudentGuide
```

### 3️⃣ Configure Database Connection
Modify the `appsettings.json` file in the `StudentGuide.API` project to set up your SQL Server connection.

```json
"ConnectionStrings": {
  "connection": "Server=YOUR_SERVER;Database=StudentGuideDB;Trusted_Connection=True;"
}
```

### 4️⃣ Run Migrations & Update Database
```sh
 dotnet ef migrations add InitialCreate
 dotnet ef database update
```

### 5️⃣ Run the Project
```sh
 dotnet run --project StudentGuide.API
```

## 🔍 API Endpoints
### 📌 Materials
- **Get All Materials** – `GET /api/Material/GetAllMaterials`
- **Get Material by Name** – `GET /api/Material/GetMaterialByName?name=Physics`
- **Add New Material** – `POST /api/Material/AddNewMaterial`

## 🤝 Contributing
Feel free to fork the repo and submit pull requests for new features or bug fixes.

## 📜 License
This project is licensed under the **MIT License**.

---
### 👨‍💻 Developed by **Ahmed Elsayed Mohamed Hamed**
🔗 [GitHub Profile](https://github.com/Ahmed200379)

