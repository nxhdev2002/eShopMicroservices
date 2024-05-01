
using Catalog.API.Products.GetProduct;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result =  await sender.Send(new DeleteProductCommand(id));
                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteProducts")
            .Produces<GetProductRepsonse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Products")
            .WithDescription("Delete Products");
        }
    }
}
