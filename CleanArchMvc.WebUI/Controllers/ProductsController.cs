using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using CleanArchMvc.Application.DTOs;
using System;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchMvc.WebUI.Controllers
{
    [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductDTO productDto)
        {
            Console.WriteLine(productDto);
            if(!ModelState.IsValid)
                return View(productDto);

            await _productService.Add(productDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int? id)
        {
            if(!id.HasValue)
                return NotFound();
            var productDto = await _productService.GetById(id);
            
            if(productDto is null)
                return NotFound();
            
            ViewBag.CategoryId = 
            new SelectList( await _categoryService.GetCategories(), "Id", "Name");
            
            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(ProductDTO productDTO)
        {
            if(!ModelState.IsValid)
                return View(productDTO);

            await _productService.Update(productDTO);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if(!id.HasValue)
                return NotFound();
            
            var productDTO = await _productService.GetProductCategory(id);
            ViewBag.ImageExist = string.IsNullOrEmpty(productDTO.Image);

            return View(productDTO);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]        
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if(!id.HasValue)
                return NotFound();
            
            var productDTO = await _productService.GetById(id);

            if(productDTO is null)
                return NotFound();
            
            return View(productDTO);

        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed (int? id)
        {
              if(!id.HasValue)
                return NotFound();
            
            var productDTO = await _productService.GetById(id);

            if(productDTO is null)
                return NotFound();
            
            await _productService.Remove(id);

            return RedirectToAction(nameof(Index));

        }

    }
}