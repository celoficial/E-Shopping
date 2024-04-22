using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    public class BasketController : ApiController
    {
        private readonly ISender _sender;

        public BasketController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Route("[action]/{username}", Name = "GetBasketByUserName")]
        [ProducesResponseType(type: typeof(ShoppingCartResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasketByUserName(string username)
        {
            var query = new GetBasketByUserNameQuery();
            query.UserName = username;
            var result = await _sender.Send(query);
            return Ok(result);
        }

        [HttpPost("CreateOrUpdateBasket")]
        [ProducesResponseType(type: typeof(ShoppingCartResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartResponse>> CreateOrUpdateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
        {
            var command = new CreateShoppingCartCommand();
            command.Username = createShoppingCartCommand.Username;
            command.Items = createShoppingCartCommand.Items;
            var result = await _sender.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{username}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType(type: typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartResponse>> DeleteBasket(string username)
        {
            var command = new DeleteShoppingCartCommand();
            command.UserName = username;
            var result = await _sender.Send(command);
            return Ok(result);
        }
    }
}
