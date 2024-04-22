using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers
{
    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public UpdateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var resultSuccess = await _discountRepository.UpdateDiscount(_mapper.Map<Coupon>(request.Coupon));
            if (!resultSuccess)
            {
                throw new RpcException(new Status(StatusCode.Aborted, $"Discount with ProductName = {request.Coupon.ProductName} failed to update."));
            }
            return request.Coupon;

        }
    }
}
