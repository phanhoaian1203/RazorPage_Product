using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage_ProductManager.Core.Interfaces;
using RazorPage_ProductManager.Core.Models;
using System.Threading.Tasks;

namespace RazorPage_ProductManager.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _service;
        public CreateModel(IProductService service)
        {
            _service = service;
        }
        public IActionResult OnGet() => Page();
        [BindProperty]
        public Product Product { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            try
            {
                await _service.GetAllProductsAsync();
                return RedirectToPage("./Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            
        }
    }
}
