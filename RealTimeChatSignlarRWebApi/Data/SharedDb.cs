using RealTimeChatSignlarRWebApi.Models;
using System.Collections.Concurrent;

namespace RealTimeChatSignlarRWebApi.Data
{
    public class SharedDb
    {
        public ConcurrentDictionary<string, UserConnection> connections { get; } = new();

    }
}
