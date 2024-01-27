using Microsoft.AspNetCore.SignalR;
using RealTimeChatSignlarRWebApi.Data;
using RealTimeChatSignlarRWebApi.Models;

namespace RealTimeChatSignlarRWebApi.Hubs;

public class ChatHub : Hub
{
    private readonly SharedDb _sharedDb;

    public ChatHub(
        SharedDb sharedDb
        )
    {
        _sharedDb = sharedDb;
    }

    public async Task JoinChat(UserConnection connection) => await Clients.All.SendAsync("ReceivedMessage", "admin", $"{connection.UserName} has Joined");

    public async Task JoinSpecificChatRoom(UserConnection connection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);
        _sharedDb.connections[Context.ConnectionId] = connection;
        await Clients.Group(connection.ChatRoom).SendAsync("Message Received", connection.UserName, $"{connection.UserName} has joined the {connection.ChatRoom}");
    }

    public async Task SendMessage(string message)
    {
        if (_sharedDb.connections.TryGetValue(Context.ConnectionId, out UserConnection connection))
        {
            await Clients.Group(connection.ChatRoom).SendAsync("ReceivedSpecificMessage",connection.UserName,message);
        }
    }


}
