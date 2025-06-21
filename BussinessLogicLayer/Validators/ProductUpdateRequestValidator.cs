using eCommerce.BussinessLogicLayer.DTO;
using FluentValidation;

namespace eCommerce.BussinessLogicLayer.Validators;

public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateRequestValidator()
    {
        //ProductId 
        RuleFor(x => x.ProductID).NotEmpty().WithMessage("Product ID can't be blank");

        //ProductName
        RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product name can't be blank");

        //Category
        RuleFor(_ => _.Category).IsInEnum().WithMessage("please select any valid Category");

        //UnitPrice
        RuleFor(_ => _.UnitPrice).InclusiveBetween(0, double.MaxValue).WithMessage($"Unit Price should be 0 to {double.MaxValue}");

        //QuantityInStock
        RuleFor(_ => _.QuantityInStock).InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity In Stock  should be 0 to {int.MaxValue}");
    }
}
