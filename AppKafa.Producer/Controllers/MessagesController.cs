using AppKafka.Core.Domain;
using AppKafka.Core.ServiceContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppKafa.Producer.Controllers
{
    [Route("api/message")]
    public class MessagesController : ControllerBase
    {
        private IKafkaMessenger<string> kafkaMessenger;

        public MessagesController(IKafkaMessenger<string> kafkaMessenger)
        {
            this.kafkaMessenger = kafkaMessenger;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(IEnumerable<string>))]
        public async Task<IActionResult> PostItemAsync(string message)
        {
            var kafkaMessage = new KafkaMessage { Id = Guid.NewGuid() , MessageValue = message };
            this.kafkaMessenger.MessageProducer(kafkaMessage);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
