using FFHRRequestSystem.Services.VietN;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace FFHRRequestSystem.WorkerService
{
    /*
     sc create "vu627" binPath= "D:\PRN222\FA25_PRN222_3W1_ASM2_SE180672_VietN\FFHRRequestSystem.WorkerService\bin\Debug\net8.0\FFHRRequestSystem.WorkerService.exe"

    sc delete "vu627"
     */
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly TicketProcessingVietNService _service;

        public Worker(ILogger<Worker> logger, TicketProcessingVietNService service)
        {
            _logger = logger;
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //if (_logger.IsEnabled(LogLevel.Information))
                //{
                //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //}
                //await Task.Delay(1000, stoppingToken);

                this.WriteLogFile();

                await Task.Delay(3000, stoppingToken);

            }
        }

        private async Task WriteLogFile()
        {
            try
            {
                var items = await _service.GetAllAsync();

                var opion = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var content = JsonSerializer.Serialize(items, opion);

                var logFilePath = @"D:\DataLog_PRN222_3W1_FA25.txt";

                using (var file = File.Open(logFilePath, FileMode.Append, FileAccess.Write))
                {
                    using (var writer = new StreamWriter(file))
                    {
                        writer.WriteLineAsync(DateTime.Now.ToString() + ": " + content);
                        writer.FlushAsync();
                    }
                }
                ;
            }
            catch(Exception ex)
            {

            }
        }
    }
}
