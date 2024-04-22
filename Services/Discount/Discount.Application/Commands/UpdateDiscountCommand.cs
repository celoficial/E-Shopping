using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands
{
    public class UpdateDiscountCommand : IRequest<CouponModel>
    {
        public CouponModel Coupon { get; set; } = null!;
    }
}
