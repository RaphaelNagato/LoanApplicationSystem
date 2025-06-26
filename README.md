# Loan Application System

This is a Blazor-based web application for managing loan applications.

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/)

## Setup

1.  **Clone the repository:**
    ```bash
    git clone <repository-url>
    cd LoanApplicationSystem
    ```

2.  **Configure the database connection:**
    -   Open `LoanApplicationSystem/appsettings.json`.
    -   Add a `ConnectionStrings` section with your PostgreSQL connection string:
        ```json
        "ConnectionStrings": {
          "DbConnection": "server=localhost;username=<your-username>;password=<your-password>;database=loanappsystem;Port=5432;SslMode=Prefer;MinPoolSize=1;"
        },
        "AllowedHosts": "*"
        ```
    -   **Important:** Make sure to add a comma after the `Logging` section to ensure the JSON remains valid.

3.  **Apply database migrations:**
    -   The application is configured to automatically apply pending Entity Framework migrations on startup, so no manual database commands are required.

## Running the Application

### From the Command Line

1.  **Run the application:**
    -   Navigate to the `LoanApplicationSystem` directory.
    -   Run the following command:
        ```bash
        dotnet run --project LoanApplicationSystem
        ```

2.  **Access the application:**
    -   Open your web browser and navigate to the URL specified in the console output (e.g., `https://localhost:5001`).

### Using Visual Studio

1.  **Open the solution:**
    -   Open the `LoanApplicationSystem.sln` file in Visual Studio.
2.  **Run the application:**
    -   Press `F5` or the green "Run" button to build and start the application. Visual Studio will automatically handle restoring dependencies.
3.  **Access the application:**
    -   Your default web browser should open automatically to the application's URL.

## Project Structure

-   **Core:** Contains the domain entities, DTOs, and specifications.
-   **Infrastructure:** Handles data access, services, and dependency injection.
-   **LoanApplicationSystem:** The main Blazor web application project.