using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Botix.Bot.Telegram.API.Application;
using Botix.Bot.Telegram.API.Models;
using Botix.Common.API.Filters;
using Botix.Common.Logging.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Botix.Bot.Telegram.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiExceptionFilter]
    [ServiceFilter(typeof(LoggingExceptionFilter))]
    public class TextMessageController : ControllerBase
    {
        private readonly TextMessageAdapter _messageAdapter;
        private readonly ILogger<TextMessageController> _logger;

        public TextMessageController(TextMessageAdapter messageAdapter, ILogger<TextMessageController> logger)
        {
            _messageAdapter = messageAdapter ?? throw new ArgumentNullException(nameof(messageAdapter));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Message([Required] string username, [FromBody] TextMessageModel message, CancellationToken cancellationToken)
        {
            using (_logger.BeginCorrelationScope())
            {
                await _messageAdapter.SendTextMessage(username, message, cancellationToken);
                return Ok();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> InlineButtonMessage([Required] string username, [FromBody] TextWithButtonModel message, CancellationToken cancellationToken)
        {
            using (_logger.BeginCorrelationScope())
            {
                await _messageAdapter.SendInlineButtonMessage(username, message, cancellationToken);
                return Ok();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ReplyButtonMessage([Required] string username, [FromBody] TextWithButtonModel message, CancellationToken cancellationToken)
        {
            using (_logger.BeginCorrelationScope())
            {
                await _messageAdapter.SendReplyButtonMessage(username, message, cancellationToken);
                return Ok();
            }
        }
    }
}
