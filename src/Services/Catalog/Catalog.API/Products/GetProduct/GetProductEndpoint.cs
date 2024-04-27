namespace Catalog.API.Products.GetProduct
{
    //public record GetProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record GetProductRepsonse(IEnumerable<Product> Products);

    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());

                var response = result.Adapt<GetProductRepsonse>();
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductRepsonse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
        }
    }
}
