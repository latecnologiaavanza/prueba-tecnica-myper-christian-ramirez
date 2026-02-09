# 🏢 Sistema de Gestión de Trabajadores - MYPER CORE

Solución integral para el mantenimiento de legajos de personal, desarrollada como evaluación técnica para el cargo de **Analista Programador .NET Core**. El sistema destaca por una arquitectura limpia, validaciones robustas y una interfaz de usuario optimizada con procesos asíncronos.

## 🔗 Recursos del Diseño

* **Prototipo Funcional (Figma):** [Diseño y Dimensiones UI](https://www.figma.com/design/ytXjSQx1H4dCZwbCgnKyJf/Untitled?node-id=0-1&t=dr5tnZCwVDTAL5wc-1)

---

## 🚀 Flujo de Ejecución (App en Real)

Cuando se inicia la aplicación, el flujo sigue este orden lógico:

1. **Inicialización:** El `Program.cs` configura la cultura `es-PE`, inyecta las dependencias (Servicios/Repositorios) y levanta el middleware de Entity Framework.
2. **Carga Principal (Index):** Al acceder, el `TrabajadorController` llama al servicio, el cual ejecuta el **Procedimiento Almacenado** `sp_ListarTrabajadores` en SQL Server para poblar la tabla.
3. **Registro/Edición (Modales):** No hay recargas de página. Al hacer clic en "Nuevo" o "Editar", se levanta un modal de Bootstrap que carga una **Vista Parcial**.
4. **Persistencia Asíncrona:** El formulario se envía vía **AJAX**.
* Si hay errores de validación (ej. menor de 18 años o DNI duplicado), el servidor devuelve un error `400` y el modal permanece abierto mostrando los mensajes en rojo.
* Si es exitoso, el servidor devuelve un string `"Success"`, el script cierra el modal y refresca la vista principal.



---

## 🛠️ Stack Tecnológico

* **Framework:** .NET 8.0 (C#)
* **Patrón Arquitectónico:** MVC + Service Pattern + Repository Pattern
* **Base de Datos:** SQL Server (Scripts incluidos).
* **Validaciones:** Data Annotations + Custom Attributes.
* **Frontend:** HTML5, CSS3, Bootstrap 5.3, jQuery (AJAX).

---

## 📋 Reglas de Negocio e Integridad de Datos

* **Validación de Identidad:** El sistema valida que el `NumeroDocumento` sea único. Si se intenta duplicar, el `TrabajadorService` lanza una `InvalidOperationException` que se captura en el Controller.
* **Restricción de Edad:** Se implementó el atributo `[MayorEdad]`. No se permite guardar registros si la fecha de nacimiento no cumple los **18 años** al día de hoy.
* **Manejo de Imágenes:** * Las fotos se guardan físicamente en el servidor (`wwwroot/fotos`).
* Se renombran con `Guid.NewGuid()` para evitar sobrescritura.
* Se asigna `/fotos/default.png` automáticamente si el usuario no proporciona una.



---

## ⚙️ Guía de Instalación (Paso a Paso)

### 1. Preparación de la Base de Datos

Ubicación del script: `/Database/Script_Inicial_Trabajadores.sql`

1. Abra **SQL Server Management Studio**.
2. Cree la base de datos `TrabajadoresPrueba` o simplemente ejecute el script completo.
3. Asegúrese de que el SP `sp_ListarTrabajadores` se haya creado correctamente.

### 2. Configuración de la Solución

Edite el archivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=TrabajadoresPrueba;Integrated Security=True;TrustServerCertificate=True;"
}

```

### 3. Ejecución

* **Visual Studio:** Presione `F5` o el botón "Play".
* **Consola:**
```bash
dotnet restore
dotnet build
dotnet run --project PruebaTecnicaMyper

```



---

## 🧪 Pruebas Unitarias

Se incluyó un proyecto de tests para garantizar que la lógica de negocio no se rompa ante cambios:

* **Mocks:** Uso de `Moq` para simular el repositorio y el entorno web.
* **Escenarios:** Validación de DNI duplicado, asignación de foto por defecto y éxito en la actualización.

Para ejecutar los tests:

```bash
dotnet test

```

---

## 📂 Estructura del Repositorio

```text
├── PruebaTecnicaMyper.sln
├── /PruebaTecnicaMyper (Proyecto Web)
│   ├── /Attributes      # Validaciones personalizadas
│   ├── /Controllers     # Lógica de navegación
│   ├── /Data            # Contexto de DB
│   ├── /Database        # Scripts SQL
│   ├── /Repositories    # Capa de datos
│   ├── /Services        # Lógica de negocio (CORE)
│   └── /wwwroot         # Archivos estáticos y fotos
└── /PruebaTecnicaMyper.Tests (XUnit)

```

**Postulante:** Christian Raul Ramirez Escalante  
**Fecha:** Febrero 2026

---