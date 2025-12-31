using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage_ProductManager.Core.Interfaces;
using RazorPage_ProductManager.Core.Models;

namespace RazorPage_ProductManager.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductService _service;
        private readonly IWebHostEnvironment _environment;
        public EditModel(IProductService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }
        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public IFormFile? UploadFile { get; set; }

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
            try
            {
                // 3. LOGIC XỬ LÝ ẢNH
                if (UploadFile != null)
                {
                    // --- A. Xóa ảnh cũ (Optional - Làm cho sạch server) ---
                    // Nếu sản phẩm đã có ảnh cũ, ta xóa nó đi trước khi lưu ảnh mới
                    if (!string.IsNullOrEmpty(Product.Avatar))
                    {
                        var oldFilePath = Path.Combine(_environment.WebRootPath, "images", Product.Avatar);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // --- B. Lưu ảnh mới (Giống hệt trang Create) ---
                    var fileName = $"{Path.GetFileNameWithoutExtension(UploadFile.FileName)}_{Guid.NewGuid()}{Path.GetExtension(UploadFile.FileName)}";
                    var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await UploadFile.CopyToAsync(stream);
                    }

                    // Cập nhật tên ảnh mới vào Model
                    Product.Avatar = fileName;
                }

                // Lưu ý: Nếu UploadFile == null
                // Thì Product.Avatar vẫn giữ nguyên giá trị cũ (nhờ cái <input type="hidden"> bên View)
                // Nên ta không cần viết lệnh else.

                // 4. Gọi Service cập nhật
                await _service.UpdateProductAsync(Product);

                return RedirectToPage("../Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
