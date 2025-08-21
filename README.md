# FirstAPI

A simple ASP.NET Core Web API for managing books, built with C# and EF Core.

---

## **Setup Instructions**

1. **Clone the repository**

```bash
git clone https://github.com/leafarpontes/first-csharp-api-books.git
cd FirstAPI
```

2. **Create local configuration**

- Copy the example settings file:

```text
appsettings.Development.json.example -> appsettings.Development.json
```

- Edit `appsettings.Development.json` and replace the `Server` value with your SQL Server instance.  
- Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=FirstAPI_data;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True"
}
```

> **Note:** Keep `appsettings.Development.json` ignored by Git. The `.example` file is safe to commit.

3. **Create the database**

- Make sure a database with the name you set in `Database` exists.  
- Or run EF Core migrations if applicable:

```bash
dotnet ef database update
```

4. **Run the API**

- In Visual Studio, press **F5** or click **Run**  
- Or in terminal:

```bash
dotnet run
```

5. **Test the endpoints**

- GET all books: `GET https://localhost:{PORT}/api/books`  
- POST a new book: `POST https://localhost:{PORT}/api/books` with JSON body:

```json
{
  "title": "1984",
  "author": "George Orwell",
  "yearPublished": "1949"
}
```

---

### **Tips**

- Make sure your **launch profile** is set to **Development** so `appsettings.Development.json` is loaded.  
- For local development, `Trusted_Connection=True` uses your Windows account — no password needed.
