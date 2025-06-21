using AutoMapper;
using eCommerce.BussinessLogicLayer.ServiceContracts;
using eCommerce.BussinessLogicLayer.DTO;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace eCommerce.BussinessLogicLayer.Services;

public class ProductServices : IProductsService
{
    private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productsRepository;

    public ProductServices(IValidator<ProductAddRequest> productAddRequestValidator,
                               IValidator<ProductUpdateRequest> productUpdateRequestValidator,
                               IMapper mapper,
                               IProductsRepository productsRepository)
    {
        _productAddRequestValidator = productAddRequestValidator;
        _productUpdateRequestValidator = productUpdateRequestValidator;
        _mapper = mapper;
        _productsRepository = productsRepository;
    }


    public async Task<ProductResponse?> AddProductAsync(ProductAddRequest product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        ValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(product);

        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(_ => _.ErrorMessage));
            throw new ArgumentException($"Product validation failed: {errors}", nameof(product));
        }

        Product? productEntity = _mapper.Map<Product>(product);
        if (productEntity == null)
        {
            return null;
        }
        Product? addedProduct = await _productsRepository.AddProductAsync(productEntity);

        if (addedProduct == null)
        {
            return null;
        }

        ProductResponse productResponse = _mapper.Map<ProductResponse>(addedProduct);
        return productResponse;
    }

    public async Task<bool> DeleteProductAsync(Guid productID)
    {
        Product? product = await _productsRepository.GetProductByConditionAsync(_ => _.ProductID == productID);
        if (product == null)
        {
            return false;
        }
        bool isDeleted = await _productsRepository.DeleteProductAsync(productID);
        return isDeleted;
    }

    public async Task<ProductResponse?> GetProductByConditionAsync(Expression<Func<Product, bool>> conditionExpression)
    {
        Product? product = await _productsRepository.GetProductByConditionAsync(conditionExpression);
        if (product == null)
        {
            return null;
        }
        ProductResponse productResponse = _mapper.Map<ProductResponse>(product);
        return productResponse;
    }

    public async Task<List<ProductResponse>> GetProductsAsync()
    {
        IEnumerable<Product?> product = await _productsRepository.GetProductsAsync();

        IEnumerable<ProductResponse> productResponse = _mapper.Map<IEnumerable<ProductResponse>>(product);
        if (productResponse == null)
        {
            return new List<ProductResponse>();
        }
        return productResponse.ToList();
    }

    public async Task<List<ProductResponse>> GetProductsByConditionAsync(Expression<Func<Product, bool>> conditionExpression)
    {
        IEnumerable<Product?> product = await _productsRepository.GetProductsByConditionAsync(conditionExpression);

        IEnumerable<ProductResponse> productResponse = _mapper.Map<IEnumerable<ProductResponse>>(product);
        if (productResponse == null)
        {
            return new List<ProductResponse>();
        }
        return productResponse.ToList();
    }

    public async Task<ProductResponse?> UpdateProductAsync(ProductUpdateRequest product)
    {
        Product? info = await _productsRepository.GetProductByConditionAsync(_ => _.ProductID == product.ProductID);
        if (info == null)
        {
            throw new ArgumentException($"Product with ID {product.ProductID} does not exist.", nameof(product.ProductID));
        }

        ValidationResult validationResult = await _productUpdateRequestValidator.ValidateAsync(product);
        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(_ => _.ErrorMessage));
            throw new ArgumentException($"Product validation failed: {errors}", nameof(product));
        }
        Product? productEntity = _mapper.Map<Product>(product);

        Product? updatedProduct = await _productsRepository.UpdateProductAsync(productEntity);

        ProductResponse productResponse = _mapper.Map<ProductResponse>(updatedProduct);
        return productResponse;
    }
}
