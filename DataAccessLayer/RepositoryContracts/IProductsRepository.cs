using eCommerce.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace eCommerce.DataAccessLayer.RepositoryContracts;

/// <summary>
/// Represents a repository for managing 'Products' table
/// </summary>
public interface IProductsRepository
{
    /// <summary>
    ///  Retrevies the All Products
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Product>> GetProductsAsync();

    /// <summary>
    ///  Retrevies the All Products based on the condition
    /// </summary>
    /// <param name="conditionExpression"></param>
    /// <returns></returns>
    Task<IEnumerable<Product?>> GetProductsByConditionAsync(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="conditionExpression"></param>
    /// <returns></returns>
    Task<Product?> GetProductByConditionAsync(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task<Product?> AddProductAsync(Product product);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task<Product?> UpdateProductAsync(Product product);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<bool> DeleteProductAsync(Guid productID);

}
