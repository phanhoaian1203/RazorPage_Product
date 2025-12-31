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
        private readonly IWebHostEnvironment _environment;
        public CreateModel(IProductService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }
        public IActionResult OnGet() => Page();
        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public IFormFile? UploadFile { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            try
            {
                if (UploadFile != null)
                {
                    var fileName = $"{Path.GetFileNameWithoutExtension(UploadFile.FileName)}_{Guid.NewGuid()}{Path.GetExtension(UploadFile.FileName)}";
                    var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                    var dirPath = Path.Combine(_environment.WebRootPath, "images");
                    if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

                    // Lưu file vật lý xuống ổ cứng server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await UploadFile.CopyToAsync(stream);
                    }

                    // Gán tên file vào Model để lưu xuống Database
                    Product.Avatar = fileName;
                }
                await _service.AddProductAsync(Product);
                return RedirectToPage("/Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            
        }
    }
}
