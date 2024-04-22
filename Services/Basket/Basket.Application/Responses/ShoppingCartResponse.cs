namespace Basket.Application.Responses
{
    public class ShoppingCartResponse
    {
        public string UserName { get; set; } = string.Empty;
        public List<ShoppingCartItemResponse> Items { get; set; } = null!;

        public decimal TotalPrice { get; set; }

        public ShoppingCartResponse()
        {

        }

        public ShoppingCartResponse(string userName)
        {
            UserName = userName;
        }
    }
}
