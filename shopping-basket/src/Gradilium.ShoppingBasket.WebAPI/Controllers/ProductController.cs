using Akka.Actor;
using Gradilium.ShoppingBasket.Products;
using Gradilium.ShoppingBasket.WebAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Gradilium.ShoppingBasket.Products.ProductsManagerActor;

namespace Gradilium.ShoppingBasket.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IActorRef _productsManager;

        public ProductController(NamedActorRef<ProductsManagerActor> productsManager)
        {
            _productsManager = productsManager.Actor;
        }

        /// <summary>
        /// List all products in product catalog.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<List<Product>> Get()
        {
            return _productsManager.Ask<List<Product>>(new GetProducts());
        }

        /// <summary>
        /// Add a new product to product catalog.
        /// </summary>
        [HttpPost]
        public Task Post([FromBody] Product product)
        {
            return _productsManager.Ask(new AddProduct { Product = product });
        }

        /// <summary>
        /// Add a bunch of dummy data to product catalog. Useful for testing.
        /// </summary>
        [HttpPost("test")]
        public void Test()
        {
            // Add test data.
            Product[] products =
            {
                new Product("Benediction Amulet", "Vipe", 18.0m, 7),
                new Product("Resurrection Texts", "Sysist", 84.0m, 5),
                new Product("Black Magic Texts", "Hemizu", 24.0m, 7),
                new Product("Oracle Tiara", "Zacy", 29.0m, 4),
                new Product("Spellbound Chest", "Intrafy", 70.0m, 2)
            };

            foreach (var product in products)
                _productsManager.Tell(new AddProduct { Product = product });
        }
    }
}
