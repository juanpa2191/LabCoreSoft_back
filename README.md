# LabCoreSoft - Patient Management System

Sistema de gestión de pacientes para un centro médico, desarrollado con .NET 9 y Arquitectura Limpia.

## Tecnologías

- **Backend**: .NET 9 Web API
- **Base de Datos**: SQL Server
- **ORM**: Entity Framework Core
- **Arquitectura**: Clean Architecture
- **Patrón CQRS**: MediatR
- **Documentación**: Swagger
- **Pruebas**: xUnit

## Prerrequisitos

- .NET 9 SDK
- SQL Server (o LocalDB)

## Instalación

1. Clona el repositorio:
   ```
   git clone <url-del-repositorio>
   cd LabCoreSoft/back
   ```

2. Restaura los paquetes:
   ```
   dotnet restore
   ```

3. Configura la base de datos:
   - Actualiza la cadena de conexión en `src/Presentation/appsettings.json` si es necesario.
   - Ejecuta las migraciones:
     ```
     dotnet ef database update --project src/Infrastructure/LabCoreSoft.Infrastructure.csproj --startup-project src/Presentation/LabCoreSoft.Presentation.csproj
     ```

4. Ejecuta las pruebas:
   ```
   dotnet test
   ```

## Ejecución

Ejecuta la aplicación:
```
dotnet run --project src/Presentation/LabCoreSoft.Presentation.csproj
```

La aplicación estará disponible en `https://localhost:5001` (o el puerto configurado), y Swagger en `https://localhost:5001/swagger`.

## Endpoints de la API

- **POST /api/patients**: Registrar un nuevo paciente.
- **POST /api/patients/query**: Listar y buscar pacientes con paginación y filtros.
- **PUT /api/patients/{id}**: Actualizar un paciente.
- **DELETE /api/patients/{id}**: Eliminar un paciente (eliminación suave).

## Pruebas

Importa la colección de Postman desde `postman_collection.json` para probar los endpoints.

## Estructura del Proyecto

- `src/Domain`: Entidades y reglas de negocio.
- `src/Application`: Casos de uso, comandos y consultas.
- `src/Infrastructure`: Implementación de repositorios y base de datos.
- `src/Presentation`: Controladores de API y configuración.
- `tests/UnitTests`: Pruebas unitarias.