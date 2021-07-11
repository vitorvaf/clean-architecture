using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;            
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var cateories = await _categoryService.GetCategories();
            return View(cateories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            if(ModelState.IsValid)
            {
                await _categoryService.Add(category);
                return RedirectToAction(nameof(Index)); 
            }
            return View(category);            
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if(!id.HasValue)
                return NotFound();
            
            var categoryDto = await _categoryService.GetById(id);

            if(categoryDto is null)
                return NotFound();
            
            return View(categoryDto);            
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
        {
            if(!ModelState.IsValid)
                return View(categoryDTO);
            
            try
            {
                await _categoryService.Update(categoryDTO); 
                return RedirectToAction(nameof(Index));   
            }
            catch (System.Exception)
            {                
                throw;
            }
            
        }

        [Authorize(Roles ="Admin")]
        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if(!id.HasValue)
                return NotFound();
            
            var categoryDTO = await _categoryService.GetById(id);

            if(categoryDTO is null)
                return NotFound();
            
            return View(categoryDTO);
        }


        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if(!id.HasValue)
                return NotFound();
            try
            {
                await _categoryService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception)
            {                
                throw;
            }            
        }        

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(!id.HasValue)
                return NotFound();
            
            var categoryDTO = await _categoryService.GetById(id);

            if(categoryDTO is null)
                return NotFound();
            
            return View(categoryDTO);
        }
    }
}