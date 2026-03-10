# Proyecto_Restaurante — Nota sobre flujo de compra y configuración de Stripe

Resumen breve
------------
Se ha modificado el flujo de compra para que las órdenes y el envío de correos se generen únicamente después de la confirmación de pago por Stripe. Se han eliminado del UI y del controlador las opciones para:

- Finalizar compra sin pasar por Stripe (botón "Pagar y finalizar compra").
- Enviar un correo de prueba manual desde la vista de checkout.

Flujo actual de compra
---------------------
1. El usuario añade productos al carrito.
2. Desde `Checkout` (vista `Views/Platillos/Checkout.cshtml`) el usuario pulsa "Pagar con tarjeta (Stripe)".
3. La aplicación crea una sesión de Stripe Checkout (`CreateStripeSession`) y redirige al usuario a Stripe.
4. El usuario completa (o simula) el pago en Stripe. En modo test puedes usar tarjetas de prueba (ej. `4242 4242 4242 4242`).
5. Stripe redirige al endpoint `StripeSuccess` del controlador `PlatillosController` con `session_id`.
6. La aplicación obtiene la sesión (`sessionService.GetAsync(session_id)`) y comprueba `session.PaymentStatus == "paid"`.
7. Si está pagado, la aplicación crea la orden en la base de datos y envía el correo de confirmación al comprador (si se dispone de email en sesión).

Archivos clave
-------------
- `Controllers/PlatillosController.cs`
  - `CreateStripeSession` — crea la sesión de Stripe Checkout.
  - `StripeSuccess` — valida la sesión y crea la orden / envía correo.
  - `CheckoutPost` — ahora redirige a Checkout con un mensaje (ya no crea ordenes sin pago).

- `Views/Platillos/Checkout.cshtml` — muestra únicamente el botón para iniciar Stripe Checkout.

Configuración de Stripe
-----------------------
Recomendación: nunca guardar las claves secretas en el repositorio.

Opciones para configurar la `SecretKey` (clave secreta) y `PublishableKey`:

1. Variables de entorno (producción recomendado):

   - `STRIPE_SECRET_KEY` — clave secreta (ej. `sk_test_...`).
   - Puedes exportarla en el servidor o configurarla en el servicio de hosting.

2. `dotnet user-secrets` (desarrollo local):

   - En la carpeta del proyecto donde está el archivo `.csproj` ejecuta:
     ```bash
     dotnet user-secrets init
     dotnet user-secrets set "Stripe:SecretKey" "sk_test_xxx"
     dotnet user-secrets set "Stripe:PublishableKey" "pk_test_xxx"
     ```

3. `appsettings.json` (NO RECOMENDADO para secretos):

   - Si decides usarlo en local, asegúrate de no incluir claves reales en el control de versiones.

Formato esperado
---------------
- `SecretKey` debe tener formato `sk_test_...` o `sk_live_...`.
- `PublishableKey` debe ser `pk_test_...` o `pk_live_...`.

Pruebas locales con Stripe
--------------------------
- Usa las claves de prueba proporcionadas por tu cuenta de Stripe Dashboard (Developers → API keys).
- Durante la sesión de Checkout en modo test, utiliza tarjetas de prueba:
  - Número: `4242 4242 4242 4242` — cualquier MM/AA válido y CVC (ej. `12/34`, `123`).
- Verifica que después del pago el endpoint `StripeSuccess` crea la orden y limpia el carrito.

Correo (SMTP)
-------------
- El envío de correos sigue ocurriendo en `StripeSuccess` tras la creación de la orden.
- Configuración SMTP: revisa `appsettings.json` o gestiona las credenciales con `user-secrets`/variables de entorno.

Notas operativas y seguridad
---------------------------
- No dejes claves en el repositorio público. Si hay claves comprometidas, revócalas desde el Dashboard de Stripe.
- Revisa que la clave usada no esté revocada y pertenezca a la cuenta correcta.
- Considera agregar verificación adicional (logs) y manejo de errores para fallos en el envío de correo.

Pasos sugeridos luego de estos cambios
--------------------------------------
1. Hacer commit con un mensaje claro y push:
   ```bash
   git add .
   git commit -m "Forzar pago por Stripe; eliminar checkout sin pago y envío de prueba de correo"
   git push origin <tu-rama>
   ```
2. Probar el flujo completo en entorno local usando claves de prueba.
3. En producción, establecer `STRIPE_SECRET_KEY` y `SMTP` mediante variables de entorno seguras.

Contacto
-------
Si necesitas, puedo agregar una sección de troubleshooting o ejemplos de llamadas a la API de Stripe para depuración.
