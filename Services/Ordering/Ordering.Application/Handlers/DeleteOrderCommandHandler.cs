using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);
            if (order is null)
            {
                throw new OrderNotFoundException(nameof(Order), request.Id);
            }
            await _orderRepository.DeleteAsync(order);
            _logger.LogInformation("Order {OrderId} was successfully deleted", request.Id);
        }
    }
}
