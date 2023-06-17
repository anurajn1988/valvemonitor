using Microsoft.AspNetCore.Mvc;
using VillaValveController.Data;

namespace VillaValveController.Controller
{
    [ApiController]
    public class ValveController : ControllerBase
    {
        [HttpGet("/valve-status")]
        public async Task<IActionResult> ValveDetails([FromQuery] int id,
            [FromQuery] int v,
            [FromQuery] int m,
            [FromQuery] string t1,
            [FromQuery] string t2,
            [FromQuery] string t3,
            [FromQuery] string t4,
            [FromQuery] string t5,
            [FromQuery] string t6,
            [FromQuery] string t7,
            [FromQuery] string t8)
        {
            ValveDetail valve = new ValveDetail()
            {
                Id = id,
                Voltage = v,
                MotorStatus = m == 1 ? true : false,
                T1 = t1,
                T2 = t2,
                T3 = t3,
                T4 = t4,
                T5 = t5,
                T6 = t6,
                T7 = t7,
                T8 = t8,
            };
            if (!Store.Valves.ContainsKey(valve.Id))
            {
                Store.Valves.TryAdd(valve.Id, new System.Collections.Concurrent.ConcurrentQueue<ValveDetail>());
            }

            Store.Valves[valve.Id].Enqueue(valve);

            if (Store.Valves[valve.Id].Count > 10)
            {
                Store.Valves[valve.Id].TryDequeue(out ValveDetail detail);
            }
            return Ok("success");
        }
    }
}
