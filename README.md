# Sales Date Prediction App
Este proyecto es una aplicación de predicción de fechas de ventas construida con :Net Core 8. A continuación, se detallan los pasos para configurar y ejecutar el proyecto en tu entorno local.

## Requisitos

- **.NET SDK**: 8.0
- **Visual Studio**: 2022 (versión 17.4 o superior)

## Instalación de Herramientas

### .NET SDK

Puedes descargar e instalar el .NET SDK 8.0 desde el siguiente enlace:
[.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)

### Visual Studio

Descarga e instala Visual Studio 2022 desde el siguiente enlace:
[Visual Studio 2022](https://visualstudio.microsoft.com/es/vs/)

Asegúrate de seleccionar la carga de trabajo **"Desarrollo de ASP.NET y web"** durante la instalación.

## Inicialización del Proyecto

Sigue estos pasos para inicializar el proyecto:

1. **Clona el repositorio**:

- git clone https://github.com/ingjuang/pruebaCodificoBack

- cd pruebaCodificoBack
  
- Configurar cadena de conexión a la base de datos en el archivo appsettings.json

2. **Restaura las dependencias**:
- dotnet restore

3. **Compila el proyecto**:
- dotnet build

4. **Ejecuta el proyecto**:
- cd SalesDatePredictionBack.Api
- dotnet run --urls="https://localhost:7048"

## Ejecución de Pruebas

Para ejecutar las pruebas unitarias, utiliza el siguiente comando:
- dotnet test

