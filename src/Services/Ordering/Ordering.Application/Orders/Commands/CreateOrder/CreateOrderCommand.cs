﻿using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

    public record CreateOrderResult(Guid Id);

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Ordername is required");
            RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("Customer Id is required");
            RuleFor(x => x.Order.OrdersItem).NotEmpty().WithMessage("Orders Item shoud not be empty");
        }
    }
}
