# College Management System

A robust College Management web application built with **ASP.NET Core MVC** to streamline student course enrollment and administrative management. The system features secure role-based access control (RBAC), allowing students to manage their course loads while giving instructors full administrative oversight of the student body.

## 🚀 Features

### Student Portal (User Role)
* **Secure Login & Registration:** Managed via ASP.NET Core Identity.
* **Course Enrollment:** Students can browse and enroll in the specific courses they wish to take.

### Instructor Portal (Admin Role)
* **Student Management:** Instructors have administrative privileges to view the student registry and permanently delete student records from the database.
* **Access Control:** Protected routes ensure only users with the `Admin` role can perform destructive actions.

## 💻 Tech Stack

* **Language:** C#
* **Framework:** ASP.NET Core MVC
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core (EF Core)
* **Authentication/Authorization:** ASP.NET Core Identity

## 🛠️ Getting Started

Follow these steps to set up the project locally on your machine.

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download) (Version matches your project, e.g., .NET 6, 7, or 8)
* [Visual Studio 2022](https://visualstudio.microsoft.com/) or JetBrains Rider
* [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) or SQL Server Express

### Installation & Setup

1. **Clone the repository**
   ```bash
   git clone [https://github.com/3bd-allah/College-Management-System.git](https://github.com/3bd-allah/College-Management-System.git)
   cd CollegeManagementSystem
