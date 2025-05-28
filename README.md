# Simple E-Commerce Project

This is a basic e-commerce project built with **ASP.NET Core** and **C#**, following **RESTful API** principles and the **Onion Architecture**. It utilizes **generic types** for flexibility and reusability.

## Features
- RESTful API for managing e-commerce entities.
- Clean and maintainable codebase using Onion Architecture.
- Reusable components thanks to generic programming.

## Technologies Used
- **Backend**: ASP.NET Core, C#
- **Database**: SQL Server
- **Architecture**: Onion Architecture

## Getting Started
### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or later)
- A database setup (e.g., SQL Server)

### Installation
1. **Clone the repository**:
   ```bash
   git clone https://github.com/TinaKorsandi/E-commerce.git
   cd e-commerce
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Configure the database**:
   Update the `appsettings.json` file with your database connection string.

4. **Apply migrations**:
   ```bash
   dotnet ef database update
   ```

5. **Run the application**:
   ```bash
   dotnet run
   ```

   Access the API at `http://localhost:5143/api`.

## Future Plans
- Add a simple, user-friendly UI.
- Implement user authentication and authorization.
- Include shopping cart functionality.

## License
This project is licensed under the MIT License.
