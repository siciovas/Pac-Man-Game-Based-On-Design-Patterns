using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.Hubs;

namespace SignalRServer.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IHubContext<ServerHub> _hubContext;
        public ValuesController(IHubContext<ServerHub> hubcontext)
        {
            _hubContext = hubcontext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
            _hubContext.Clients.All.SendAsync("Posted", value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }
    }
}
