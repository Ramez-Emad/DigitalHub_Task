# DigitalHub Task API

A **Course Enrollment API** built with **ASP.NET Core (.NET 10)** using Clean Architecture. It allows users to enroll in courses and automatically logs every enrollment action using a background audit system.

---

## 📁 Project Structure

The solution is split into **4 projects**, each with a specific responsibility:

```
DigitalHub_Task/
│
├── DigitalHub_Task.API/
│   ├── Controllers/
│   │   └── EnrollmentController.cs
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── Program.cs
│
├── DigitalHub_Task.Application/
│   ├── CQRS/Commands/
│   │   ├── AuditLog/
│   │   │   ├── AuditLogCommand.cs
│   │   │   └── AuditLogCommandHandler.cs
│   │   └── Enrollment/
│   │       ├── EnrollCommand.cs
│   │       ├── EnrollCommandHandler.cs
│   │       ├── EnrollmentCreatedEvent.cs
│   │       └── EnrollmentCreatedEventHandler.cs
│   ├── DTOs/Enrollment/
│   │   ├── EnrollRequest.cs
│   │   └── EnrollResponse.cs
│   ├── Interfaces/
│   │   ├── IAuditLogQueue.cs
│   │   └── Repositories/
│   │       ├── IAuditLogRepository.cs
│   │       └── IEnrollmentRepository.cs
│   └
├── DigitalHub_Task.Domain/
│   └── Entities/
│       ├── AuditLog.cs
│       ├── Course.cs
│       ├── Enrollment.cs
│       └── User.cs
│
└── DigitalHub_Task.Infrastructure/
    ├── BackgroundServices/
    │   ├── AuditLogQueue.cs
    │   └── AuditLogWorker.cs
    ├── Data/
    │   └── AppDbContext.cs
    ├── Migrations/
    └── Repositories/
        ├── AuditLogRepository.cs
        └── EnrollmentRepository.cs
```

---

## 🔍 How the Audit System Works

The audit system automatically records a log entry every time a user enrolls in a course. Here's how it flows step by step:

```
User sends POST /enroll
        │
        ▼
EnrollCommandHandler
  → Saves the enrollment to the database
  → Publishes an EnrollmentCreatedEvent
        │
        ▼
EnrollmentCreatedEventHandler
  → Receives the event
  → Puts an audit entry into the AuditLogQueue (in-memory queue)
        │
        ▼
AuditLogWorker  (runs in the background, always listening)
  → Picks up the entry from the queue
  → Sends an AuditLogCommand to save it to the database
        │
        ▼
AuditLogCommandHandler
  → Calls AuditLogRepository
  → Writes the final record to the AuditLogs table in SQL Server
```

**Why this design?**
- The enrollment response is returned to the user **immediately** — audit logging happens in the background without slowing down the API.
- The `AuditLogQueue` decouples the enrollment flow from the logging concern.
- The `AuditLogWorker` is a hosted background service that runs for the lifetime of the app.

---

## 🚀 How to Run the Project Locally

### ✅ Prerequisites

Before you start, make sure you have these installed:

| Tool | Download |
|------|----------|
| **Git** | https://git-scm.com/downloads |
| **.NET 10 SDK** | https://dotnet.microsoft.com/download |
| **SQL Server** (or SQL Server Express) | https://www.microsoft.com/en-us/sql-server/sql-server-downloads |
| **Visual Studio 2022** (recommended) or VS Code | https://visualstudio.microsoft.com/ |

---

### Step 1 — Clone the repository

Open a terminal and run:

```bash
git clone https://github.com/Ramez-Emad/DigitalHub_Task.git
cd DigitalHub_Task
```

---

### Step 2 — Set up the database connection

Open the file:
```
DigitalHub_Task.API/appsettings.json
```

Find the `ConnectionStrings` section and update it with your SQL Server details:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=DigitalHubDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> 💡 If you're using SQL Server Express, change `Server=localhost` to `Server=localhost\SQLEXPRESS`

---


### Step 3 — Run the project

**Option A — Using Visual Studio:**
1. Open `DigitalHub_Task.API.slnx` in Visual Studio 2022
2. Right-click `DigitalHub_Task.API` → "Set as Startup Project"
3. Press **F5** or click the green ▶ Run button

**Option B — Using the terminal:**
```bash
cd DigitalHub_Task.API
dotnet run
```

---

### Step 4 — Test the API

Once running, open your browser and go to:

```
https://localhost:{port}/swagger
```

You'll see the **Swagger UI** where you can test the API endpoints.

> The port number is shown in the terminal when the app starts (e.g., `Now listening on: https://localhost:7234`)

#### Example: Enroll a user in a course

Send a `POST` request to `/api/enrollment` with this body:

```json
{
  "userId": 1,
  "courseId": 1
}
```

After a successful enrollment, check your `AuditLogs` table in the database — a new record should have been created automatically in the background!

---

## 🛠️ Tech Stack

- **ASP.NET Core (.NET 10)** — Web framework
- **Entity Framework Core** — Database ORM
- **SQL Server** — Database
- **MediatR** — CQRS pattern (commands, queries, events)
- **Swagger / OpenAPI** — API documentation & testing UI
- **Clean Architecture** — Separation of concerns across 4 layers
