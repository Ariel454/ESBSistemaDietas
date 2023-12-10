using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SistemaDietas.Application;
using SistemaDietas.Domain;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDietas.Infraestructure
{
    public class ServiceBusReceiver
    {
        private readonly DietasApplicationService _applicationService;

        public ServiceBusReceiver(DietasApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public async Task ProcesarMensajesAsync(QueueClient queueClient)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(async (message, token) =>
            {
                var recetaMedica = JsonConvert.DeserializeObject<RecetaMedica>(Encoding.UTF8.GetString(message.Body));

                // Procesar la receta médica en el sistema de dietas
                _applicationService.ProcesarRecetaEnDietas(recetaMedica);

                Console.WriteLine($"Receta procesada en Dietas: {recetaMedica}");

                await queueClient.CompleteAsync(message.SystemProperties.LockToken);
            }, messageHandlerOptions);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Mensaje de error: {exceptionReceivedEventArgs.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}
