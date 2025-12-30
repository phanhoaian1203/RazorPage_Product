using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
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

        [BindProperty(SupportsGet =true)]
        public string SearchString { get; set; }
        public async Task OnGetAsync()
        {
            if (!SearchString.IsNullOrEmpty())
            {
                Products = await _service.GetByKeywordsAsync(SearchString);
            }
            else
            {
                Products = await _service.GetAllProductsAsync();
            }
                
        }
    }
}
