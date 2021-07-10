using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService) 
        {
            _productService = productService;
            _categoryService = categoryService;
            
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        [HttpGet()]
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.CategoryId = new SelectList( await _categoryService.GetCategories(), "Id", "Name");
            return View();
        }

    }
}