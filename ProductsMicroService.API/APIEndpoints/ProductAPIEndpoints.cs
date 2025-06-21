using eCommerce.BussinessLogicLayer.DTO;
using eCommerce.BussinessLogicLayer.ServiceContracts;
using FluentValidation;
using FluentValidation.Results;

namespace eCommerce.ProductsMicroService.API.APIEndpoints;

public static class ProductAPIEndpoints
{

    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        //GET /api/products
        app.MapGet("/api/products", async (IProductsService productsService) =>
        {
            List<ProductResponse> products = await productsService.GetProductsAsync();
            return Results.Ok(products);
        });

        // GET /api/products/search/product-id/0000-00000-0000-000000
        app.MapGet("/api/products/search/product-id/{ProductID:guid}", async (Guid ProductID, IProductsService productsService) =>
        {
            List<ProductResponse>? product = await productsService.GetProductsByConditionAsync(_ => _.ProductID == ProductID);
            return product is not null ? Results.Ok(product) : Results.NotFound();
        });

        // GET /api/products/search/xxxxxxx
        
        app.MapGet("/api/products/search/{SearchString}", async (string SearchString, IProductsService productsService) =>
        {
            List<ProductResponse>? productsByName = await productsService.GetProductsByConditionAsync(_ => _.ProductName != null && _.ProductName.Contains(SearchString, StringComparison.OrdinalIgnoreCase));

            List<ProductResponse>? productsByCategory = await productsService.GetProductsByConditionAsync(_ => _.Category != null && _.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase));

            List<ProductResponse>? product = productsByName.Concat(productsByCategory).Distinct().ToList();
            return product is not null ? Results.Ok(product) : Results.NotFound();
        });
        

        // POST /api/products
        app.MapPost("/api/products", async (ProductAddRequest product, IProductsService productsService, IValidator<ProductAddRequest> productAddRequestValidator) =>
        {
            ValidationResult validationResult = await productAddRequestValidator.ValidateAsync(product);

            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(_ => _.PropertyName).ToDictionary(grp => grp.Key, grp => grp.Select(e => e.ErrorMessage).ToArray());

                // Return a validation errors
                return Results.ValidationProblem(errors);
            }

            ProductResponse? addedProduct = await productsService.AddProductAsync(product);
            return addedProduct is not null ? Results.Created($"/api/products/{addedProduct.ProductID}", addedProduct) : Results.Problem("Error in adding product");
        });

        //PUT /api/products/{id:guid}
        app.MapPut("/api/products", async (ProductUpdateRequest product, IProductsService productsService, IValidator<ProductUpdateRequest> productUpdateRequestValidator) =>
        {

            ValidationResult validationResult = await productUpdateRequestValidator.ValidateAsync(product);

            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(_ => _.PropertyName).ToDictionary(grp => grp.Key, grp => grp.Select(e => e.ErrorMessage).ToArray());

                // Return a validation errors
                return Results.ValidationProblem(errors);
            }

            ProductResponse? updatedProduct = await productsService.UpdateProductAsync(product);
            return updatedProduct is not null ? Results.Ok(updatedProduct) : Results.Problem("Error in adding product");
        });



        // lete /api/products/{id:guid}
        app.MapDelete("/api/products/{ProductID:guid}", async (Guid ProductID, IProductsService productsService) =>
        {
            bool isDeleted = await productsService.DeleteProductAsync(ProductID);
            return isDeleted ? Results.NoContent() : Results.NotFound();
        });

        return app;
    }
}
