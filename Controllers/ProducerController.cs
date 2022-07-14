using Microsoft.AspNetCore.Mvc;
using Confluent.Kafka;
using System.Threading.Tasks;
using kafkamicro.Models;
using System;
using Newtonsoft.Json;

namespace kafkamicro.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProducerController:ControllerBase
    {
        private ProducerConfig _config;
        public ProducerController(ProducerConfig config)
        {
            this._config = config;
        }
        [HttpPost("send")]
        public async Task<ActionResult> Get(string topic, [FromBody]Employee employee)
        {
            string serializedEmployee = JsonConvert.SerializeObject(employee);
            using(var producer = new ProducerBuilder<Null,string>(_config).Build())
            {
                await producer.ProduceAsync(topic,new Message<Null, string>{ Value=serializedEmployee });//ProduceAsync sends message to Kafka topic asynchronously
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok(true);
            }
        }
        
    }
}