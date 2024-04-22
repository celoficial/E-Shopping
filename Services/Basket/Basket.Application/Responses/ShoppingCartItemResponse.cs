﻿
namespace Basket.Application.Responses
{
    public class ShoppingCartItemResponse
    {
        public int Quantity { get; set; }
        public string ImageFile { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
    }
}