using BusWebApi;
using BusWebApi.Handlers;
using BusWebApi.HostedService;
using BusWebApi.Managers;
using BusWebApi.DataAccessLayer;
using BusWebApi.Services;
using BusWebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR().AddAzureSignalR();

builder.Services.AddHostedService<HostService>();

// Dependencies
builder.Services.AddTransient<ISignalRMessageSenderService, SignalRMessageSenderService>();
builder.Services.AddTransient<IStorageAccessor, RedisStorageAccessor>();
builder.Services.AddTransient<IDatabaseDataChangeHandler, RedisDataChangeHandler>();
builder.Services.AddTransient<IDifferenceTrackerService, DifferenceTrackerService>();
builder.Services.AddTransient<IEventManager, EventManager>();
builder.Services.AddTransient<IOfferManager, OfferManager>();
builder.Services.AddTransient<IStorageOriginator, StorageOriginator>();
builder.Services.AddTransient<IStorageMemento, StorageMemento>();
builder.Services.AddTransient<IStorageCaretaker, StorageCaretaker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseFileServer();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<SignalRHub>("/events");
});

app.MapControllers();

app.Run();
