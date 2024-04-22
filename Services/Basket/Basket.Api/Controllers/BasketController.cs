using Basket.Application.Commands;
using Basket.Application.GrpcServices;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    public class BasketController : ApiController
    {
        private readonly ISender _sender;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(ISender sender, DiscountGrpcService discountGrpcService)
        {
            _sender = sender;
            _discountGrpcService = discountGrpcService;
        }

        [HttpGet]
        [Route("[action]/{username}", Name = "GetBasketByUserName")]
        [ProducesResponseType(type: typeof(ShoppingCartResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasketByUserName(string username)
        {
            var query = new GetBasketByUserNameQuery
            {
                UserName = username
            };
            var result = await _sender.Send(query);
            return Ok(result);
        }

        [HttpPost("CreateOrUpdateBasket")]
        [ProducesResponseType(type: typeof(ShoppingCartResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartResponse>> CreateOrUpdateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
        {
            foreach (var item in createShoppingCartCommand.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            var command = new CreateShoppingCartCommand 
            {
                Username = createShoppingCartCommand.Username,
                Items = createShoppingCartCommand.Items
            };

            var result = await _sender.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        [Route("[action]/{username}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType(type: typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartResponse>> DeleteBasket(string username)
        {
            var command = new DeleteShoppingCartCommand 
            { 
                UserName = username 
            };
            var result = await _sender.Send(command);
            return Ok(result);
        }
    }
}
