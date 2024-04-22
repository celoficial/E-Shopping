using Basket.Application.Commands;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class DeleteShoppingCartCommandHandler : IRequestHandler<DeleteShoppingCartCommand, bool>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteShoppingCartCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<bool> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
        {
            return await _basketRepository.DeleteBasket(request.UserName);
        }
    }
}
