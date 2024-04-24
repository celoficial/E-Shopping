using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public CreateShoppingCartCommandHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _basketRepository.UpdateBasket(new ShoppingCart
            {
                Items = request.Items,
                UserName = request.Username
            });
            var shoppingCartResponse = _mapper.Map<ShoppingCartResponse>(shoppingCart);
            if (shoppingCart is not null)
            {
                shoppingCartResponse.TotalPrice = shoppingCart.TotalPrice;
            }

            return shoppingCartResponse;
        }
    }
}
