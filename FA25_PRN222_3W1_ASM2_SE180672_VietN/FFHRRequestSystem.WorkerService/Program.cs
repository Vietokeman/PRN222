using FFHRRequestSystem.Services.VietN;
using FFHRRequestSystem.WorkerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "FFHRRequestSystem_WorkerService";
});

builder.Services.AddSingleton<TicketProcessingVietNService>();
var host = builder.Build();
host.Run();
