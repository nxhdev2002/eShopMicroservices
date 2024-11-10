
namespace Ordering.Application.Orders.Queries.GetOrders;
internal class GetOrdersHandler(IApplicationDbContext dbContext) 
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginatedRequest.PageIndex;
        var pageSize = query.PaginatedRequest.PageSize;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders
            .Include(x => x.OrderItems)
            .AsNoTracking()
            .OrderBy(x => x.OrderName.Value)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetOrdersResult(
            new PaginatedResult<OrderDto>(
                pageIndex, pageSize, totalCount, orders.ToOrderDtoList()
            ));
    }
}
