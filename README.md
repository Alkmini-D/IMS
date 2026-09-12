# IMS — Inventory Management System

Final project for the **Coding Factory 8** program (Athens University of Economics and Business — ΟΠΑ).

IMS is a full-stack web application for managing **inventories** and **assemblies**. Users can register, manage stock, define assemblies composed of inventories, produce and sell them, and generate reports on the state of the inventory.

---

## Features

- **Authentication & Authorization** with ASP.NET Core Identity (register, login, logout, account management, email confirmation).
- **Inventory management** — create, edit, view, and search inventories by name.
- **Assembly management** — define assemblies composed of inventories; produce, sell, and delete assemblies.
- **Transaction tracking** — purchase, produce, and sell transactions for both inventories and assemblies, with search.
- **Business rule validation** — e.g. verifying enough inventory exists before producing an assembly.
- **Blazor routing** with a mixed static/interactive rendering model (see [Render Modes](#render-modes) below).

---

## Tech Stack

- **.NET / ASP.NET Core** (C#)
- **Blazor Web App** — Interactive Server rendering, Static SSR for Identity pages
- **Entity Framework Core** with **Microsoft SQL Server**
- **ASP.NET Core Identity** (Entity Framework stores, cookie authentication)
- **Clean / Layered Architecture** — Domain / Use Cases / Plugins / UI

---

## How the Assignment Requirements Are Met

| Requirement | Where it is implemented |
|---|---|
| Domain Model (DDD) | `IMS.CoreBusiness` — `Inventory`, `Assembly`, `InventoryTransaction`, `AssemblyTransaction` |
| Model-First database | EF Core migrations in `IMS.Plugins.EFCore` |
| Repository layer | `IMS.Plugins.EFCore` implements interfaces defined in `IMS.UseCases.PluginInterfaces` |
| Service / Use Case layer | Use cases in `IMS.UseCases` (e.g. `IAddInventoryUseCase`, `IProduceAssemblyUseCase`, `ISellAssemblyUseCase`) |
| Presentation layer | `IMS_WebApp` — Blazor Web App with Server-Side Rendering (Razor) |
| Authentication (backend) | ASP.NET Core Identity, `SignInManager`, `UserManager` |
| Authorization (frontend) | `<AuthorizeView>`, `[Authorize]`, `/Account/*` Identity pages |

---

## Project Layout

```
IMS/
├── IMS.CoreBusiness/          # Domain entities: Inventory, Assembly, Transactions
├── IMS.UseCases/              # Business logic + plugin (repository) interfaces
│   ├── PluginInterfaces/      # Repository contracts
│   ├── Inventories/           # Inventory use cases
│   ├── Assemblies/            # Assembly use cases
│   └── Reports/               # Transaction search / reporting use cases
├── IMS.Plugins.EFCore/        # EF Core DbContext + repository implementations
├── IMS_WebApp/                # Blazor Web App (UI + composition root)
│   ├── Components/
│   │   ├── Account/           # Identity pages (static SSR)
│   │   ├── Layout/            # MainLayout, NavMenu
│   │   └── Pages/             # Inventory / Assembly / Reports pages
│   ├── Data/                  # ApplicationDbContext, ApplicationUser
│   └── Program.cs             # DI, middleware, endpoints
└── IMS.slnx                   # Solution file
```

---

## Domain Model (short overview)

- `Inventory` — a stock item with a name, quantity, and price.
- `Assembly` — a product composed of one or more inventories.
- `InventoryTransaction` — purchase / produce / sell events on inventories.
- `AssemblyTransaction` — produce / sell events on assemblies.
- `ApplicationUser` — extends `IdentityUser` for authentication.

The database is generated from this model using **EF Core migrations** (Model-First approach).

---

## Build & Deployment

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (8.0 or later)
- **SQL Server** (LocalDB, Express, or a full instance)
- An IDE: **Visual Studio 2022+**, **Rider**, or **VS Code**
- EF Core CLI tools (optional, for migrations):

  ```bash
  dotnet tool install --global dotnet-ef
  ```

### 1. Clone the repository

```bash
git clone https://github.com/Alkmini-D/IMS.git
cd IMS
```

### 2. Configure connection strings

Edit `IMS_WebApp/appsettings.json` (or `appsettings.Development.json`) and set:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=IMS_Identity;Trusted_Connection=True;MultipleActiveResultSets=true",
    "InventoryManagement_new": "Server=(localdb)\\mssqllocaldb;Database=InventoryManagement;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

| Connection string | Used for |
|---|---|
| `DefaultConnection` | ASP.NET Core Identity tables (users, tokens, etc.) |
| `InventoryManagement_new` | Inventory / Assembly / Transaction data |

### 3. Create the database (Model-First)

Apply EF Core migrations:

```bash
dotnet ef database update --project IMS.Plugins.EFCore --startup-project IMS_WebApp
dotnet ef database update --project IMS_WebApp --startup-project IMS_WebApp
```

> If the above commands fail on a clean clone, the project may rely on `EnsureCreated()`. In that case, uncomment the `EnsureDeleted()` / `EnsureCreated()` block in `IMS_WebApp/Program.cs`, run the app once, and re-comment the block.

### 4. Build the solution

```bash
dotnet build IMS.slnx -c Release
```

### 5. Run the application

```bash
dotnet run --project IMS_WebApp -c Release
```

Navigate to the HTTPS URL printed in the console. Register a new user; during development, the confirmation link is written to the console by the no-op email sender (`IdentityNoOpEmailSender`). Log in and start using the app.

### 6. Publish for deployment

Deploy to **IIS**, **Azure App Service**, or any ASP.NET Core–compatible host:

```bash
dotnet publish IMS_WebApp -c Release -o ./publish
```

Then:

1. Deploy the contents of `./publish` to your host.
2. On IIS, ensure the [ASP.NET Core Hosting Bundle](https://dotnet.microsoft.com/download/dotnet) is installed.
3. Provide production connection strings via environment variables (`ConnectionStrings__DefaultConnection`, `ConnectionStrings__InventoryManagement_new`) or `appsettings.Production.json`.

---

## Render Modes

This app uses a **mixed Blazor render-mode strategy**:

- Most pages use **InteractiveServer** so `@onclick` handlers, forms, and interactive components work.
- The **Identity pages** (`/Account/*`) must render as **Static SSR**, because they require `HttpContext` to set authentication cookies through `SignInManager`.

`App.razor` therefore selects the render mode **conditionally** based on the requested path, and `AccountLayout.razor` forces a full page reload when the mode changes.

> ⚠️ When modifying the render mode setup:
> - Do **not** force `InteractiveServer` globally on `<Routes />` without excluding `/Account` paths — login and registration will break with a `NullReferenceException` on `HttpContext`.
> - Do **not** add `@rendermode InteractiveServer` to layouts or components with `RenderFragment` parameters — this triggers compiler error **RZ1041**.
> - Apply the render mode **once**, conditionally, in `App.razor`.

---

## Author

**Alkmini-D** — [github.com/Alkmini-D](https://github.com/Alkmini-D)
