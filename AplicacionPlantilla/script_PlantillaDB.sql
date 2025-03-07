﻿USE [master]
GO
/****** Object:  Database [ProyectoJunta]    Script Date: 6/3/2025 22:26:28 ******/
	 
/****** Descripción de la Base de Datos y Flujo de Proceso
Este script de SQL crea la base de datos ProyectoJunta y establece una serie de configuraciones para optimizar el rendimiento, la integridad de los datos y la eficiencia de las consultas dentro del sistema. A continuación, se detalla cómo la base de datos está estructurada y cómo los reclutadores pueden entender el flujo del proceso en su contexto de implementación.

1. Creación de la Base de Datos ProyectoJunta
El proceso comienza con la creación de una base de datos llamada ProyectoJunta, la cual tiene la siguiente configuración:

Ubicación del archivo de datos: Se encuentra en C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ProyectoJunta.mdf, y su tamaño inicial es de 8192KB, con un crecimiento automático de 65536KB.
Ubicación del archivo de registro: El archivo de log, ubicado en C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ProyectoJunta_log.ldf, tiene un tamaño inicial de 8192KB y crece a 65536KB.
Compatibilidad: El nivel de compatibilidad de la base de datos se establece en 160, lo que garantiza que las características de SQL Server estén alineadas con las últimas versiones del producto.
2. Configuración de Opciones de la Base de Datos
Se aplican varias configuraciones para optimizar el rendimiento y la seguridad:

Desactivación de opciones de autoclosed y auto-shrink: Se garantiza que la base de datos no se cierre automáticamente, ni se reduzca su tamaño de forma automática, lo cual ayuda a mantener un rendimiento estable.
Actualización automática de estadísticas: Las estadísticas se actualizan automáticamente para asegurar que las consultas se optimicen en función de los datos más recientes.
Optimización de transacciones: Se habilita el query store, que almacena y optimiza las consultas ejecutadas en la base de datos, mejorando el rendimiento y la gestión de consultas repetitivas.
3. Creación de la Tabla plantillas
Dentro de la base de datos ProyectoJunta, se crea la tabla plantillas, que almacena información sobre plantillas que podrían usarse en el aplicativo. Esta tabla tiene la siguiente estructura:

Columnas:

IDPlantillas: Es una columna de tipo int que sirve como clave primaria, incrementándose automáticamente.
nombre: Almacena el nombre de la plantilla. Esta columna es de tipo nvarchar y tiene un límite de 255 caracteres.
contenido: Contiene el contenido de la plantilla y puede almacenar grandes cantidades de texto, ya que es de tipo nvarchar(max).
CreadoEn: Almacena la fecha y hora de creación de la plantilla. Se asigna automáticamente la fecha y hora actual por defecto mediante getdate().
Restricciones y optimizaciones:

La clave primaria IDPlantillas asegura la unicidad de cada plantilla.
Se han habilitado índices y bloqueos de fila/página para garantizar que las operaciones de lectura y escritura sean rápidas y eficientes.
4. Configuración de los Parámetros de Recuperación
Se establece un modelo de recuperación FULL, lo que significa que se realizará un registro completo de transacciones y se podrán restaurar los datos a un punto específico en el tiempo en caso de fallos. Además, la opción TARGET_RECOVERY_TIME se establece en 60 segundos, lo que optimiza la velocidad de recuperación tras un fallo.

5. Integración con el Sistema de Consultas (Query Store)
El Query Store está habilitado con configuración avanzada para:

Modo de operación: Lectura/escritura.
Política de limpieza: Las consultas obsoletas se eliminarán después de 30 días.
Tamaño máximo de almacenamiento: 1000MB.
Captura de estadísticas de espera: Habilitada para facilitar el análisis de consultas lentas.
Esto asegura que el sistema pueda adaptarse y evolucionar en función de los patrones de uso de la base de datos, mejorando así la eficiencia de las consultas y el análisis de rendimiento.

Propósito en el Flujo del Aplicativo
Este conjunto de configuraciones y tablas está diseñado para optimizar el manejo de plantillas dentro del aplicativo ProyectoJunta. La base de datos está configurada para soportar un flujo de trabajo de alta disponibilidad y rendimiento, permitiendo a los usuarios crear, almacenar y recuperar plantillas de forma eficiente y sin interrupciones.





 ******/
CREATE DATABASE [ProyectoJunta]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProyectoJunta', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ProyectoJunta.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProyectoJunta_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ProyectoJunta_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ProyectoJunta] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProyectoJunta].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProyectoJunta] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProyectoJunta] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProyectoJunta] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProyectoJunta] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProyectoJunta] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProyectoJunta] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProyectoJunta] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProyectoJunta] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProyectoJunta] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProyectoJunta] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProyectoJunta] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProyectoJunta] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProyectoJunta] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProyectoJunta] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProyectoJunta] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProyectoJunta] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProyectoJunta] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProyectoJunta] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProyectoJunta] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProyectoJunta] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProyectoJunta] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProyectoJunta] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProyectoJunta] SET RECOVERY FULL 
GO
ALTER DATABASE [ProyectoJunta] SET  MULTI_USER 
GO
ALTER DATABASE [ProyectoJunta] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProyectoJunta] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProyectoJunta] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProyectoJunta] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProyectoJunta] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProyectoJunta] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProyectoJunta', N'ON'
GO
ALTER DATABASE [ProyectoJunta] SET QUERY_STORE = ON
GO
ALTER DATABASE [ProyectoJunta] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ProyectoJunta]
GO
/****** Object:  Table [dbo].[plantillas]    Script Date: 6/3/2025 22:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[plantillas](
	[IDPlantillas] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](255) NOT NULL,
	[contenido] [nvarchar](max) NOT NULL,
	[CreadoEn] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[IDPlantillas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[plantillas] ADD  DEFAULT (getdate()) FOR [CreadoEn]
GO
USE [master]
GO
ALTER DATABASE [ProyectoJunta] SET  READ_WRITE 
GO
