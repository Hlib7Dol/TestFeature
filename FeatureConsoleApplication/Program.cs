// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using FeatureConsoleApplication;
using FeatureConsoleApplication.Managers;
using FeatureConsoleApplication.Dto;
using FeatureConsoleApplication.Mapper;

var connectionString = "Endpoint=https://testfeature.service.signalr.net;AccessKey=otQrayi9WoUMZBCtlTMnkDvin0cpPgsHFhufRDD/mOs=;Version=1.0;";

var _connectionStringTool = new ConnectionStringTool();

var (url, accessKey) = _connectionStringTool.ParseConnectionString(connectionString);
var hubUrl = _connectionStringTool.GetClientUrl(url, "SignalRHub");

// Uncomment in case starting server and game client at the same time
await Task.Delay(5000);

IEventManager eventManager = new EventManager();
IOfferManager offerManager = new OfferManager();

var activeEvents = await eventManager.GetActiveEventsAsync();
var activeOffers = await offerManager.GetActiveOffersAsync();

Console.WriteLine("Currently active offers and events:");

foreach (var activeEvent in activeEvents)
{
    Console.WriteLine($"Event: name: {activeEvent.Name}, type: {activeEvent.Event_Type}, starts at: {activeEvent.Starts_At}, expires at: {activeEvent.Expires_At}");
}

foreach (var activeOffer in activeOffers)
{
    Console.WriteLine($"Offer: name: {activeOffer.Name}, type: {activeOffer.Offer_Type}, starts at: {activeOffer.Starts_At}, expires at: {activeOffer.Expires_At}");
}

Console.WriteLine("--------------------");

var connection = new HubConnectionBuilder().WithUrl(hubUrl, options =>
{
    options.AccessTokenProvider = () =>
    {
        return Task.FromResult(_connectionStringTool.GenerateAccessToken(hubUrl, accessKey))!;
    };
}).Build();

connection.On("offer", (OfferDto obj) =>
{
    if (obj == null)
    {
        throw new ArgumentNullException(nameof(obj));
    }

    var offer = obj.ToOffer();

    Console.WriteLine($"Offer: name: {offer.Name}, type: {offer.Offer_Type}, starts at: {offer.Starts_At}, expires at: {offer.Expires_At}");
});

connection.On("event", (EventDto obj) =>
{
    if (obj == null)
    {
        throw new ArgumentNullException(nameof(obj));
    }

    var eventObj = obj.ToEvent();

    Console.WriteLine($"Event: name: {eventObj.Name}, type: {eventObj.Event_Type}, starts at: {eventObj.Starts_At}, expires at: {eventObj.Expires_At}");
});

await connection.StartAsync();

Console.ReadLine();

await connection.StopAsync();