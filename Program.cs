using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

class Program
{
    const string ServiceBusConnectionString = "Endpoint=sb://arquitecturaar.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=liz2Rdnrrk71JmuXcI3xyWnanI36FTj2h+ASbNqRNXI="; // Reemplaza con tu cadena de conexión
    const string QueueNameDietas = "dietas";

    static async Task Main()
    {
        Console.WriteLine("Sistema de Dietas: Esperando recetas médicas...");

        await RecibirRecetasAsync();

        Console.WriteLine("Presiona cualquier tecla para salir.");
        Console.ReadKey();
    }

    static async Task RecibirRecetasAsync()
    {
        var client = new QueueClient(ServiceBusConnectionString, QueueNameDietas);

        RegisterOnMessageHandlerAndReceiveMessages(client);

        Console.ReadKey();

        await client.CloseAsync();
    }

    static void RegisterOnMessageHandlerAndReceiveMessages(QueueClient queueClient)
    {
        var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
        {
            MaxConcurrentCalls = 1,
            AutoComplete = false
        };

        queueClient.RegisterMessageHandler(async (message, token) =>
        {
            var recetaMedica = JsonConvert.DeserializeObject<MensajeRecetaMedica>(Encoding.UTF8.GetString(message.Body));

            // Procesar la receta médica en el sistema de dietas
            ProcesarRecetaEnDietas(recetaMedica);

            Console.WriteLine($"Receta procesada en Dietas: {recetaMedica}");

            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }, messageHandlerOptions);
    }

    static void ProcesarRecetaEnDietas(MensajeRecetaMedica receta)
    {
        // Lógica de procesamiento de receta en el sistema de dietas
        // ...

        Console.WriteLine($"Procesando receta en Dietas para el paciente: {receta.Paciente}");
    }

    static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
    {
        Console.WriteLine($"Mensaje de error: {exceptionReceivedEventArgs.Exception.Message}");
        return Task.CompletedTask;
    }
}

class MensajeRecetaMedica
{
    public string Paciente { get; set; }
    public string Medicamento { get; set; }
    public string Dosis { get; set; }
    public string FechaEmision { get; set; }
}
