namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
        }
    }


    internal class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var result = await validator.ValidateAsync(request);
            var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
            if (errors.Any())
            {
                throw new ValidationException(errors.FirstOrDefault());
            }


            //create Product entity from command object
            var product = new Product
            {
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price,
            };

            // save to db
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            // return result
            return new CreateProductResult(product.Id);
        }
    }
}
