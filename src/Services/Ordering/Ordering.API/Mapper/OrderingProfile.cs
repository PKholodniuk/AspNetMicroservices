using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Command.CheckoutOrder;

namespace Ordering.API.Mapper
{
	public class OrderingProfile : Profile
	{
		public OrderingProfile()
		{
			CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
		}
	}
}
