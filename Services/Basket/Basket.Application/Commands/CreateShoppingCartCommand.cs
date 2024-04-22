using Basket.Application.Responses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands
{
    public class CreateShoppingCartCommand : IRequest<ShoppingCartResponse>
    {
        public string Username { get; set; } = string.Empty;
        public List<ShoppingCartItem> Items { get; set; } = null!;
    }
}
