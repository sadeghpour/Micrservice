using MassTransit;
using ProcessCenter.Entity;
using System.Threading.Tasks;
using static ProcessCenter.Infrastructure.Contracts;

namespace ProcessCenter.Consumers
{
    public class OrderDeletedConsumer : IConsumer<OrderDeleted>
    {
        private readonly IRepository<Order> repository;
        public OrderDeletedConsumer(IRepository<Order> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<OrderDeleted> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.Id);
            if (item == null)
            {
                return;
            }
            await repository.RemoveAsync(message.Id);
        }
    }
}
