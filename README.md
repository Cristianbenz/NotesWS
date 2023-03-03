# NotesApi
Web service basado en la creacion de notas, tambien con funcionalidad de autenticacion de usuario con Bearer JWT.

[![Run in Postman](https://run.pstmn.io/button.svg)](https://god.gw.postman.com/run-collection/23086126-b74bf7a9-9254-456d-b868-9d11241c892c?action=collection%2Ffork&collection-url=entityId%3D23086126-b74bf7a9-9254-456d-b868-9d11241c892c%26entityType%3Dcollection%26workspaceId%3D1d9080cd-207d-4792-a278-e966b4a13ef4)

## Endpoints

### Autenticacion
- POST /api/Auth/SignUp
    - Descripcion: Genera un nuevo usuario.
    - Body: {Email: string, Name: string, Password: string}
- POST /api/Auth/SignIn
    - Descripcion: Autentifica al usuario con email y contraseña.
    - Body: {Email: string, Name: string, Password: string, ConfirmPassword: string}

### Notas
- GET /api/Note/GetAll/{userId}
    - Descripcion: Obtiene todas las notas de un usuario especifico
    - Parametro: id de usuario (int)
    - Authenticacion: requiere autenticacion con Bearer JWT
- POST /api/Note/CreateNote
    - Descripcion: Crea una nueva nota
    - Authenticacion: requiere autenticacion con Bearer JWT
    - Body: {Title: string, Text: string, UserId: int}
- PUT /api/Note/EditNote/{noteId}
    - Descripcion: Edita una nota especifica 
    - Authenticacion: requiere autenticacion con Bearer JWT
    - Body: {Title: string, Text: string}

## Tecnologias
- C#
- .NET
- Entity Framework
- Bearer JWT
- xUnit
