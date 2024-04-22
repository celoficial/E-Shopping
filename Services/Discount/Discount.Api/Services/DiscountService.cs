using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Api.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ISender _sender;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(ISender sender, ILogger<DiscountService> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountQuery()
            {
                ProductName = request.ProductName
            };
            var result = await _sender.Send(query);
            _logger.LogInformation("Discount is retrieved for ProductName: {ProductName}, Amount: {Amount}", result.ProductName, result.Amount);
            return result;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var command = new CreateDiscountCommand()
            {
                ProductName = request.Coupon.ProductName,
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description
            };

            var result = await _sender.Send(command);
            _logger.LogInformation("Discount is successfully created for ProductName: {ProductName}, Amount: {Amount}", result.ProductName, result.Amount);
            return result;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var command = new DeleteDiscountCommand()
            {
                ProductName = request.ProductName
            };

            var result = await _sender.Send(command);
            _logger.LogInformation("Discount is successfully deleted for ProductName: {ProductName}", request.ProductName);
            return new DeleteDiscountResponse
            {
                Success = result
            };
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var command = new UpdateDiscountCommand()
            {
                Coupon = request.Coupon
            };

            var result = await _sender.Send(command);
            _logger.LogInformation("Discount is successfully updated for ProductName: {ProductName}, Amount: {Amount}", result.ProductName, result.Amount);
            return result;
        }
    }
}
