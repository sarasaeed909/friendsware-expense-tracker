using System.Text.Json;

namespace ExpenseTrackerAPI.Middlewares
{
    public class InputSanitisationMiddleware
    {
        private readonly RequestDelegate _next;

        public InputSanitisationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Only sanitize POST and PUT requests (where data is sent)
            if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
            {
                context.Request.EnableBuffering();
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;

                if (!string.IsNullOrEmpty(body))
                {
                    var jsonDocument = JsonDocument.Parse(body);
                    var sanitizedBody = SanitizeJsonElement(jsonDocument.RootElement);
                    var newBody = JsonSerializer.Serialize(sanitizedBody);
                    var byteArray = System.Text.Encoding.UTF8.GetBytes(newBody);
                    context.Request.Body = new MemoryStream(byteArray);
                }
            }

            await _next(context);
        }

        private object? SanitizeJsonElement(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    var sanitizedObject = new Dictionary<string, object?>();
                    foreach (var property in element.EnumerateObject())
                    {
                        sanitizedObject[property.Name] = SanitizeJsonElement(property.Value);
                    }
                    return sanitizedObject;

                case JsonValueKind.String:
                    var stringValue = element.GetString();
                    // Trim whitespace from strings
                    return stringValue?.Trim();

                case JsonValueKind.Array:
                    var sanitizedArray = new List<object?>();
                    foreach (var item in element.EnumerateArray())
                    {
                        sanitizedArray.Add(SanitizeJsonElement(item));
                    }
                    return sanitizedArray;

                default:
                    return JsonSerializer.Deserialize<object>(element.GetRawText());
            }
        }
    }
}