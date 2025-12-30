using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage_ProductManager.Core.Interfaces;
using RazorPage_ProductManager.Core.Models;
using System.Threading.Tasks;

namespace RazorPage_ProductManager.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _service;

        public IndexModel(IProductService service)
        {
            _service = service;
        }

        public IList<Product> Products { get; set; }
        public async Task OnGet()
        {
            Products = await _service.GetAllProductsAsync();
        }
    }
}
