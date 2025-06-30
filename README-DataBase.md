
# Zurich Interview – Backend (.NET + SQL Server)

## 🧱 Migraciones y base de datos

Este proyecto utiliza **Entity Framework Core** con SQL Server. A continuación se explican los comandos necesarios para crear y actualizar la base de datos.

### 📦 Requisitos previos

- .NET SDK 8.0+
- SQL Server local o remoto
- EF Core CLI instalado globalmente:

```bash
dotnet tool install --global dotnet-ef
```

> Puedes verificar la versión instalada con:
> ```bash
> dotnet ef --version
> ```

---

### ⚙️ Configuración de conexión

Asegúrate de tener una cadena de conexión válida en el archivo:

📄 `src/ZurichInterview.Api/appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ZurichInterviewDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

### 🛠️ Crear una migración

```bash
dotnet ef migrations add NombreDeLaMigracion \
  --project src/ZurichInterview.Infrastructure \
  --startup-project src/ZurichInterview.Api
```

Ejemplo:

```bash
dotnet ef migrations add InitialCreate \
  --project src/ZurichInterview.Infrastructure \
  --startup-project src/ZurichInterview.Api
```

---

### 🗃️ Aplicar las migraciones a la base de datos

```bash
dotnet ef database update \
  --project src/ZurichInterview.Infrastructure \
  --startup-project src/ZurichInterview.Api
```

---

### 🧪 ¿Problemas?

Si EF no puede crear el `DbContext` en tiempo de diseño, asegúrate de tener el archivo:

📄 `src/ZurichInterview.Infrastructure/Persistence/DesignTimeDbContextFactory.cs`

Implementando la interfaz `IDesignTimeDbContextFactory<AppDbContext>` con la ruta correcta al `appsettings.json`.

---

✅ ¡Base de datos lista! Ahora puedes continuar con los casos de uso y controladores.
