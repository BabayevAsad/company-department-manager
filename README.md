# **Company Department Manager 🏢👨‍💼📊**

## **📝 About Project**
The **Company Department Manager** is an **MVC-based** application built with **.NET** that enables organizations to **manage departments, workers, and expenses efficiently**. It features **secure authentication** with **JWT**, implements **soft delete mechanisms**, and maintains **one-to-many relationships** between departments, workers, and expenses.

## **📌 Features**

### 🏢 **Department, Worker & Expense Management**
- **CRUD operations** for managing **departments, workers, and expenses**.
- **One-to-Many Relationship**:
  - **Each Department** has multiple **Workers**.
  - **Each Department** has multiple **Expenses**.

### 🔄 **Soft Delete Implementation**
- Ensures **data integrity** by **marking records as deleted** instead of permanently removing them.
- When a **Department** is soft deleted, its **Workers** and **Expenses** are **also soft deleted**.

### 🔐 **Authentication & Authorization**
- Uses **JWT Authentication** for securing API endpoints.
- Implements **Role-Based Access Control (RBAC)** to restrict access to sensitive operations.

### 🚀 **Caching Mechanism**
- Improves performance by **caching frequently accessed data**.
- Utilizes **Memory Caching** to reduce redundant database queries.

### ⚡ **Database**
- Implements **Entity Framework Core (EF Core)** for database management.

## **📚 Working with Services**
This project follows a **Service Layer approach** to maintain clean and scalable code:

### **🛠 Service Layer Responsibilities**
1. **Handles business logic** before interacting with the database.
2. **Calls repositories** for database operations instead of directly modifying the database from controllers.
3. **Ensures maintainability and testability** by decoupling the application logic from the controllers.
   
### 🖥️ **MVC Architecture & Service-Based Approach**
- Follows **Model-View-Controller (MVC)** design principles for **separation of concerns**.
- Uses a **service-based architecture**, ensuring that:
  - **Controllers** handle HTTP requests and responses.
  - **Services** handle business logic and interact with the database.
  - **Repositories** abstract database interactions for cleaner code and easier testing.
 
 ### 📜 **Swagger API Documentation**
- Provides **interactive API documentation** via **Swagger**.
- Supports **Try-It-Out functionality**, making it easy to test endpoints.

## **🛠 Technologies Used**
1️⃣ **.NET (ASP.NET Core MVC)**  
2️⃣ **Entity Framework Core (EF Core) for ORM**  
3️⃣ **JWT Authentication for security**  
4️⃣ **FluentValidation for request validation**  
5️⃣ **LINQ for efficient database querying**  
6️⃣ **Dependency Injection for modularity**  
7️⃣ **Memory Caching for performance**  
8️⃣ **SQL Server / PostgreSQL (configurable database support)**  
9️⃣ **Soft Delete Handling**  
🔟 **Swagger for API documentation**  


## **🐛 Find a Bug? Want to Contribute?**
If you find any bugs, **submit an issue** or contribute to the project!

📧 **Contact:** [asad_babayev@outlook.com](mailto:asad_babayev@outlook.com)  
📌 **Submit an Issue:** Use the **Issues** tab above.  
🔄 **Contribute a PR:** If submitting a fix, please reference the related issue.
