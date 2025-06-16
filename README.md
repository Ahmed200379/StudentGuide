# StudentGuide

## 📌 Overview

**StudentGuide** is a comprehensive academic guide platform designed to assist students in managing their studies efficiently. The system offers features such as subject registration, material repository, student activities, and a chatbot for guidance on lectures, sections, and technologies.

## 🚀 Features

* 📚 **Subject Registration** – Easily enroll in courses and manage your academic journey.
* 📗 **Course Management** – Add, update, search, recommend, and paginate through courses.
* 🗂 **Material Repository** – Store and access academic materials like lecture notes and research papers.
* 📄 **Document Management** – Upload, view, edit, and delete academic documents.
* 💳 **Payment System** – Create and manage electronic student payments.
* 👨‍💼 **Admin Panel** – Manage users, messages, department registration messages, and oversee the system.
* 🤖 **AI Chatbot** – Get instant answers about sections, lectures, and technologies.
* 🎯 **Student Activities** – Stay updated with extracurricular and academic activities.
* 📊 **Results Management** – View student results, import from Excel, and search by student.

## 🛠 Tech Stack

* **Backend:** .NET Core 8 RESTful API
* **Architecture:** 3-Tier Architecture, Repository Pattern, Unit of Work
* **Authentication:** JWT (JSON Web Token)
* **Database:** SQL Server, Entity Framework Core ORM
* **Frontend:** (To be added)

## 🔧 Setup and Installation

### 1️⃣ Prerequisites

Make sure you have:

* .NET SDK 8.0
* SQL Server
* Visual Studio / VS Code
* Postman (optional)

### 2️⃣ Clone the Repository

```bash
git clone https://github.com/Ahmed200379/StudentGuide.git
cd StudentGuide
```

### 3️⃣ Configure Database Connection

Edit `appsettings.json` in `StudentGuide.API` project:

```json
"ConnectionStrings": {
  "connection": "Server=YOUR_SERVER;Database=StudentGuideDB;Trusted_Connection=True;"
}
```

### 4️⃣ Run Migrations & Update Database

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5️⃣ Run the Project

```bash
dotnet run --project StudentGuide.API
```

## 🔍 API Endpoints

### 📄 Documents

* Get All Documents – `GET /api/Document/GetAll`
* Get Document by ID – `GET /api/Document/{id}`
* Add Document – `POST /api/Document`
* Update Document – `PUT /api/Document`
* Delete Document – `DELETE /api/Document/{id}`

### 📚 Materials

* Get All Materials – `GET /api/Material/GetAllMaterials`
* Get Material by Name – `GET /api/Material/GetMaterialByName?name=Physics`
* Add New Material – `POST /api/Material/AddNewMaterial`

### 📗 Courses

* Add Course – `POST /api/Course/DashBoard/AddCourse`
* Update Course – `PUT /api/Course/DashBoard/UpdateCourse`
* Search Courses – `GET /api/Course/Search`
* Delete Course – `DELETE /api/Course/Dashboard/DeleteCourse/{code}`
* Get All Courses (Paged) – `GET /api/Course/DashBoard/GetAllCoursesInPagnation/{page}/{countPerPage}`
* Get All Courses – `GET /api/Course/DashBoard/GetAllCourses`
* Get Course by ID – `GET /api/Course/{id}`
* Get Available Courses – `GET /api/Course/GetAllAvalibleCourses/{code}`
* Get Recommended Courses – `GET /api/Course/GetAllRecommendationCourses/{code}`

### 💳 Payment

* Create Payment – `POST /api/Payment/CreatePayment`

### 👨‍💼 Admin

* Add Admin – `POST /api/Admin/AddAdmin`
* Send Messages – `POST /api/Admin/SendMessages`
* Send Department Registration Messages – `POST /api/Admin/SendDepartmentRegisterationMessages`

### 🧑‍🎓 Students

* Add New Student – `POST /api/Student/DashBoard/AddNewStudent`
* Update Student – `PUT /api/Student/DashBoard/UpdateStudent`
* Get All Students – `GET /api/Student/DashBoard/GetAllStudents`
* Get Student by Code – `GET /api/Student/DashBoard/GetStudentByIdForAdmin/{code}`
* Enroll in Courses – `POST /api/Student/EnrollCourses`

### 📈 Results

* Add Result – `POST /api/Result/AddResult`
* Add Results via Excel – `POST /api/Result/AddResultWithExcel`
* Get All Results – `GET /api/Result/GetAllResultsForAllStudents`
* Get Results for Specific Student – `POST /api/Result/GetAllResultForSpecificStudent`

### 👤 Account

* Register – `POST /api/Account/Register`
* Login – `POST /api/Account/Login`
* Forget Password – `POST /api/Account/ForgetPassword`
* Reset Password – `POST /api/Account/ResetPassword`
* Check Code – `POST /api/Account/CheckCode`

### 📘 Swagger UI

Explore all endpoints: [https://studentguideapi.runasp.net/swagger](https://studentguideapi.runasp.net/swagger)

## 🤝 Contributing

Feel free to fork the repo and submit pull requests for new features or bug fixes.

## 📜 License

This project is licensed under the MIT License.

## 👨‍💻 Developed by

**Ahmed Elsayed Mohamed Hamed**
[GitHub Profile](https://github.com/Ahmed200379)
