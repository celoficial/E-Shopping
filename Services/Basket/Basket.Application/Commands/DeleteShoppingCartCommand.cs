using MediatR;

namespace Basket.Application.Commands
{
    public class DeleteShoppingCartCommand : IRequest<bool>
    {
        public string UserName { get; set; } = string.Empty;
    }
}
