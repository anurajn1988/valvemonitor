using System.Collections.Concurrent;
using VillaValveController.Data;

namespace VillaValveController
{
    public static class Store
    {
        public static ConcurrentDictionary<int, ConcurrentQueue<ValveDetail>> Valves { get; set; } = new();
    }
}
