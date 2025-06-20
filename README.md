
# RaftLab – External User Integration via ReqRes API

This solution demonstrates consuming an external API (https://reqres.in) using modern .NET Core techniques including HttpClientFactory, Polly for resiliency, and in-memory caching. The architecture is cleanly separated by responsibility, promoting maintainability and scalability.

## 🔧 Tech Stack

- .NET 8
- ASP.NET Core Web API
- HttpClientFactory
- Polly (Retry Policy)
- In-Memory Caching
- Configuration via appsettings.Development.json 
- Clean Architecture
- Swagger For Unit Testing

## 📁 Project Structure

```
RaftLabDemo/
│
├── Raftlab.Web/                    # ASP.NET Core Web API layer
│   └── Controllers/                # API controllers
│
├── Raftlab.ReqResPlugin/           # Models and DTOs (User, UserListResponse, etc.)
│
├── Raftlab.ReqResImplementation/   # API client that consumes ReqRes APIs using HttpClient
│
├── Raftlab.Service/                # Interface definitions and base response classes
│
├── Raftlab.GlobleService/          # Global services including in-memory cache service
│
├── RaftLab.sln                 # Visual Studio solution file
```

## 🚀 Features

- Fetch paginated list of users from ReqRes
- Fetch single user by ID
- In-memory caching for both API calls (expires in 45 seconds)
- Polly-based retry logic for transient faults (up to 3 retries)
- Configuration-driven base URL for API
- Dependency Injection across all layers
- Clear service segregation and separation of concerns

## 📥 Setup Instructions

1. **Clone the repo and navigate into it**

```bash
git clone https://github.com/konenkadri72/Raftlab-UserService.git
cd Raftlab.Web
```

2. **Restore dependencies**

```bash
dotnet restore
```

3. **Build the solution**

```bash
dotnet build
```

4. **Run the Web API**

```bash
cd Raftlab.Web
dotnet run
```

## 📦 API Endpoints

| Endpoint                      | Method | Description                  |
|------------------------------|--------|------------------------------|
| `/api/user/getAllUsers`      | GET    | Get all users (with pagination) |
| `/api/user/getUser`          | GET    | Get single user by ID        |

### Example

```bash
GET /api/user/getAllUsers?page=1
GET /api/user/getUser?userId=2
```

## ⚙️ Configuration

Update `appsettings.json`:

```json
{
  "ReqResBaseUrl": "https://reqres.in/api/"
}
```
## 🧠 Notes

- Retry policies are applied to the ReqResClientService via Polly.
- Caching only stores responses if the result is successful.
- Service registration follows Clean Architecture with extension methods per layer.


This solution is created for RaftLabs .NET developer assessment by Konen Kadri.
