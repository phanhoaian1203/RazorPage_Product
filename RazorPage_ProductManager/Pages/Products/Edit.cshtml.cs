using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage_ProductManager.Core.Interfaces;
using RazorPage_ProductManager.Core.Models;

namespace RazorPage_ProductManager.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductService _service;
        public EditModel(IProductService service)
        {
            _service = service;
        }
        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            var product = await _service.GetProductByIdAsync(id.Value);
            if (product == null) return NotFound();
            Product = product;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _service.UpdateProductAsync(Product);
            return RedirectToPage("/Index");
        }
    }
}
