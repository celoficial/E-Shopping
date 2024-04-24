using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Commands;

namespace Ordering.Api.EventBusConsumer
{
    public class BasketOrderingConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketOrderingConsumer> _logger;

        public BasketOrderingConsumer(ISender sender, IMapper mapper, ILogger<BasketOrderingConsumer> logger)
        {
            _sender = sender;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = await _sender.Send(command);
            _logger.LogInformation("Basket checkout event {Id} completed", context.Message.Id);
        }
    }
}
