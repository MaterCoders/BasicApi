# BasicApi

A simple ASP.NET Core Web API project with Products CRUD operations.

## Features

- ✅ Full CRUD operations for Products
- ✅ RESTful API endpoints
- ✅ Input validation
- ✅ Error handling
- ✅ Swagger/OpenAPI documentation
- ✅ Search functionality
- ✅ In-memory data storage

## API Endpoints

### Products Controller

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/products` | Get all products |
| GET | `/api/products/{id}` | Get product by ID |
| POST | `/api/products` | Create new product |
| PUT | `/api/products/{id}` | Update entire product |
| PATCH | `/api/products/{id}` | Partially update product |
| DELETE | `/api/products/{id}` | Delete product |
| GET | `/api/products/search?name={term}` | Search products by name |

### Weather Forecast (Sample)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/weatherforecast` | Get weather forecast data |

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- PowerShell (Windows) or Terminal (macOS/Linux)

### Running the Application

1. Clone or download the project
2. Navigate to the project directory:
   ```powershell
   cd BasicApi
   ```
3. Run the application:
   ```powershell
   dotnet run
   ```
4. Open your browser and navigate to:
   - API: `https://localhost:7xxx`
   - Swagger UI: `https://localhost:7xxx/swagger`

### Sample Product Data

The API comes with 3 sample products:
- Laptop - Gaming laptop ($999.99)
- Mouse - Wireless mouse ($29.99)
- Keyboard - Mechanical keyboard ($79.99)

## Project Structure

```
BasicApi/
├── Controllers/
│   └── ProductsController.cs    # Products CRUD controller
├── Models/
│   ├── Product.cs              # Product entity model
│   └── ProductDto.cs           # Data transfer objects
├── Properties/
│   └── launchSettings.json     # Launch configuration
├── appsettings.json            # Application settings
├── appsettings.Development.json # Development settings
├── Program.cs                  # Application entry point
├── BasicApi.csproj            # Project file
├── BasicApi.http              # HTTP test file
└── README.md                  # This file
```

## Technologies Used

- **ASP.NET Core 8.0** - Web framework
- **Swagger/OpenAPI** - API documentation
- **Newtonsoft.Json** - JSON serialization
- **System.ComponentModel.DataAnnotations** - Model validation

## Development

### Building the Project

```powershell
dotnet build
```

### Running Tests

```powershell
dotnet test
```

### Adding Database Support

This project currently uses in-memory storage. To add database support:

1. Install Entity Framework Core:
   ```powershell
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   ```

2. Create a DbContext and configure it in `Program.cs`

3. Update the controller to use the DbContext instead of static lists

## Contributing

1. Fork the project
2. Create a feature branch
3. Make your changes
4. Test your changes
5. Submit a pull request

## License

This project is for educational purposes.
