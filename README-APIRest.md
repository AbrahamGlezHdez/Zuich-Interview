# API REST - ZurichInterview

Este documento describe las API REST disponibles en el sistema, incluyendo sus endpoints, modelos, ejemplos y cómo usarlas.

---

## Descripción General

Esta API permite gestionar clientes, usuarios y pólizas de seguros. Está desarrollada con .NET 8 y usa Entity Framework Core para acceso a datos.

---

## Autenticación

Actualmente, la API no requiere autenticación (o detallar método de autenticación si se implementa).

---

## Endpoints

### 1. Pólizas (`/api/policies`)
| Método HTTP | Endpoint                        | Descripción                 | Parámetros                     | Respuesta                      |
|-------------|---------------------------------|-----------------------------|--------------------------------|--------------------------------|
| GET         | /api/policies                   | Obtener todas las pólizas   | Ninguno                        | Lista de pólizas (PolicyDto)   |
| GET         | /api/policies/{id}              | Obtener una póliza por ID   | id (int)                       | Póliza específica (PolicyDto)  |
| GET         | /api/policies/client/{clientId} | Obtener pólizas por cliente | clientId (int)                 | Lista de pólizas para cliente  |
| POST        | /api/policies                   | Crear una nueva póliza      | PolicyDto (JSON)               | Póliza creada (PolicyDto)      |
| PUT         | /api/policies/{id}              | Actualizar póliza existente | id (int), PolicyDto (JSON)     | Póliza actualizada (PolicyDto) |
| DELETE      | /api/policies/{id}              | Eliminar póliza             | id (int)                       | Ninguno                        |
| GET         | /api/policies/user/{usuarioId}  | Obtener pólizas por usuario | usuarioId (int)                | Lista de pólizas para usuario  |
| POST        | /api/policies/cancel            | Cancelar póliza por usuario | policyId (int), usuarioId(int) | Boolean (éxito o fallo)        |


#### Modelo PolicyDto (ejemplo)

```json
{
  "id": 1,
  "clientId": 1,
  "type": "Car",
  "startDate": "2024-01-01T00:00:00",
  "expirationDate": "2025-01-01T00:00:00",
  "amount": 1500.00,
  "status": "Active"
}
```

---

### 2. Clientes (`/api/clients`)

| Método | Endpoint             | Descripción                |
|--------|----------------------|----------------------------|
| GET    | `/api/clients`       | Obtiene todos los clientes |
| GET    | `/api/clients/{id}`  | Obtiene un cliente por ID  |
| POST   | `/api/clients`       | Crea un nuevo cliente      |
| PUT    | `/api/clients/{id}`  | Actualiza un cliente       |
| DELETE | `/api/clients/{id}`  | Elimina un cliente         |

#### Modelo ClientDto (ejemplo)

```json
{
  "id": 1,
  "identificationNumber": "1234567890",
  "name": "Juan",
  "middleName": "Carlos",
  "surName": "Pérez",
  "email": "juan.perez@email.com",
  "phone": "555-1234",
  "address": "Av. Reforma 123",
  "usuarioId": 10
}
```

---

### 3. Usuarios (`/api/users`)

| Método | Endpoint             | Descripción                |
|--------|----------------------|----------------------------|
| GET    | `/api/users`         | Obtiene todos los usuarios |
| GET    | `/api/users/{id}`    | Obtiene un usuario por ID  |
| POST   | `/api/users`         | Crea un nuevo usuario      |
| PUT    | `/api/users/{id}`    | Actualiza un usuario       |
| DELETE | `/api/users/{id}`    | Elimina un usuario         |

#### Modelo UsuarioDto (ejemplo)

```json
{
  "id": 10,
  "username": "juan123",
  "email": "juan123@email.com"  
}
```

---

## Respuestas comunes

| Código HTTP               | Descripción           |
|---------------------------|-----------------------|
| 200 OK                    | Operación exitosa     |
| 201 Created               | Recurso creado        |
| 204 No Content            | Eliminación exitosa   |
| 400 Bad Request           | Solicitud inválida    |
| 404 Not Found             | Recurso no encontrado |
| 500 Internal Server Error | Error del servidor    |

---

## Ejemplo de uso con curl

Obtener todas las pólizas:

```bash
curl -X GET http://localhost:5000/api/policies
```

Crear una póliza nueva:

```bash
curl -X POST http://localhost:5000/api/policies \
  -H "Content-Type: application/json" \
  -d '{
    "clientId": 1,
    "type": "Car",
    "startDate": "2024-01-01T00:00:00",
    "expirationDate": "2025-01-01T00:00:00",
    "amount": 1500.00,
    "status": "Active"
  }'
```
