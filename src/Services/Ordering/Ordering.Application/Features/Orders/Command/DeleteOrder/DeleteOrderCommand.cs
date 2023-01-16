using MediatR;

namespace Ordering.Application.Features.Orders.Command.DeleteOrder
{
    public class DeleteOrderCommand : IRequest 
    {
        public int Id { get; set; }
    }
}
