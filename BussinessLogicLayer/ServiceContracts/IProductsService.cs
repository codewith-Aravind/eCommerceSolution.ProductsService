using eCommerce.BussinessLogicLayer.DTO;
using eCommerce.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace eCommerce.BussinessLogicLayer.ServiceContracts;

public interface IProductsService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<List<ProductResponse>> GetProductsAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="conditionExpression"></param>
    /// <returns></returns>
    Task<List<ProductResponse>> GetProductsByConditionAsync(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="conditionExpression"></param>
    /// <returns></returns>
    Task<ProductResponse?> GetProductByConditionAsync(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task<ProductResponse?> AddProductAsync(ProductAddRequest product);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task<ProductResponse?> UpdateProductAsync(ProductUpdateRequest product);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productID"></param>
    /// <returns></returns>
    Task<bool> DeleteProductAsync(Guid productID);


}
