using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SistemaDietas.Application;
using SistemaDietas.Domain;
using SistemaDietas.Infraestructure;
using System;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private const string ServiceBusConnectionString = "Endpoint=sb://arquitecturaar.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=liz2Rdnrrk71JmuXcI3xyWnanI36FTj2h+ASbNqRNXI=";
    private const string QueueNameDietas = "dietas";

    static async Task Main()
    {
        Console.WriteLine("Sistema de Dietas: Esperando recetas médicas...");

        var dietasService = new DietasService();
        var dietasApplicationService = new DietasApplicationService(dietasService);

        var queueClient = new QueueClient(ServiceBusConnectionString, QueueNameDietas);
        var serviceBusReceiver = new ServiceBusReceiver(dietasApplicationService);

        await serviceBusReceiver.ProcesarMensajesAsync(queueClient);

        Console.WriteLine("Presiona cualquier tecla para salir.");
        Console.ReadKey();

        await queueClient.CloseAsync();
    }
}