using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.GrpcServices;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    public class BasketController : ApiController
    {
        private readonly ISender _sender;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(ISender sender,
            DiscountGrpcService discountGrpcService,
            IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _sender = sender;
            _discountGrpcService = discountGrpcService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckOut basketCheckout)
        {
            var query = new GetBasketByUserNameQuery
            {
                UserName = basketCheckout.UserName ?? string.Empty
            };
            var basket = await _sender.Send(query);

            if (basket is null)
            {
                return BadRequest();
            }

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish(eventMessage);

            var deleteCommand = new DeleteShoppingCartCommand
            {
                UserName = basketCheckout.UserName ?? string.Empty
            };
            await _sender.Send(deleteCommand);

            return Accepted();
        }
    }
}
