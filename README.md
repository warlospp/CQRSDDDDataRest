# Proyecto PagosCQRSDDDDataRest

## Descripción

Este proyecto implementa un sistema de gestión de pagos utilizando una arquitectura basada en CQRS (Command Query Responsibility Segregation) y DDD (Domain-Driven Design). La solución está desarrollada en .NET y utiliza patrones modernos para el manejo de datos, validación de dominio y comunicación con una base de datos PostgreSQL a través de servicios REST.

---

## Características principales

- Entidades y Value Objects para modelar el dominio de pagos (`Pago`, `Monto`, `MetodoPago`).
- Validaciones de negocio encapsuladas en el dominio.
- DTOs para desacoplar la capa de persistencia y la de dominio.
- Deserialización robusta con manejo de formatos numéricos y strings.
- Servicio de acceso a datos `PostgresPagoService` que consume una API REST.
- Mapeo entre DTOs y entidades de dominio.
- Arquitectura limpia y escalable basada en DDD y CQRS.

---

## Tecnologías

- .NET 7 (o versión que uses)
- C#
- System.Text.Json para serialización/deserialización JSON
- PostgreSQL (acceso vía API REST)
- ASP.NET Core Web API
- Inyección de dependencias con `IOptions` y `HttpClient`

---

## Notas importantes

- La deserialización de pagos se realiza desde un JSON que contiene un array de objetos.
- El DTO `PagoDto` usa `Monto` como string para evitar problemas con formatos numéricos en JSON.
- El mapeo convierte el DTO a la entidad de dominio validando las reglas de negocio.
- Se recomienda revisar el JSON recibido para asegurar la correcta deserialización.

---

## Contribuciones

Las contribuciones son bienvenidas. Por favor abre un issue o un pull request para sugerir mejoras o reportar bugs.

---

## Licencia

Este proyecto está bajo la licencia MIT. Consulta el archivo LICENSE para más detalles.