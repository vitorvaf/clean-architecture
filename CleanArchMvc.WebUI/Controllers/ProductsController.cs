using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CleanArchMvc.Application.Interfaces;


namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
            
        }


        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }
        
    }
}