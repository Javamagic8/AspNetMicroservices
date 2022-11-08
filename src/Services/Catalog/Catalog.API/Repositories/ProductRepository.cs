using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext ?? throw new ArgumentException(nameof(catalogContext));
        }
        public async Task CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string Id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, Id);

            DeleteResult deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string Id)
        {
            return await _catalogContext.Products.Find(x => x.Id.Equals(Id)).FirstAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string CategoryName)
        {
            return await _catalogContext.Products.Find(x => x.Category.Equals(CategoryName)).ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetProductByName(string Name)
        {
            return await _catalogContext.Products.Find(x => x.Name.Equals(Name)).ToListAsync();
        }

        public  async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext.Products.Find(x => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await _catalogContext.Products.ReplaceOneAsync(filter: x => x.Id.Equals(product.Id), replacement: product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
