using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage_ProductManager.Core.Interfaces;
using RazorPage_ProductManager.Core.Models;

namespace RazorPage_ProductManager.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _service;
        public DeleteModel(IProductService service)
        {
            _service = service;
        }
        [BindProperty]
        public Product Product { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null) return NotFound();
            var product = await _service.GetProductByIdAsync(id.Value);
            if (product == null) return NotFound();
            Product = product;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                await _service.DeleteProductAsync(id.Value);
                return RedirectToPage("/Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                Product = await _service.GetProductByIdAsync(id.Value);
                return Page();
            }
            
        }
    }
}
