﻿using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.RepositoryContracts;
using eCommrce.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories;

public class ProductRepository : IProductsRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> AddProductAsync(Product product)
    {
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(Guid productID)
    {
        Product? product = await _dbContext.Products.FirstOrDefaultAsync(_ => _.ProductID == productID);

        if (product == null)
        {
            return false;
        }
        _dbContext.Remove(product);
        int res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    public async Task<Product?> GetProductByConditionAsync(Expression<Func<Product, bool>> conditionExpression)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(conditionExpression);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product?>> GetProductsByConditionAsync(Expression<Func<Product, bool>> conditionExpression)
    {
        return await _dbContext.Products.Where(conditionExpression).ToListAsync();

    }

    public async Task<Product?> UpdateProductAsync(Product product)
    {
        Product? existingProduct = await _dbContext.Products.FirstOrDefaultAsync(_ => _.ProductID == product.ProductID);

        if (existingProduct == null)
        {
            return null;
        }
        existingProduct.ProductName = product.ProductName;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.QuantityInStock = product.QuantityInStock;
        existingProduct.Category = product.Category;

        await _dbContext.SaveChangesAsync();
        return existingProduct;
    }
}
