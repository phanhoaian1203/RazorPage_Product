using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPage_ProductManager.Core.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Mã sản phẩm")]
        [Required(ErrorMessage ="Mã sản phẩm không được để trống")]
        [StringLength(10, ErrorMessage ="Tối đa 10 kí tự")]
        public string Code { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(100, ErrorMessage = "Tối đa 100 kí tự")]
        public string Name { get; set; }

        [Display(Name = "Giá bán")]
        
        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [Range(1000, 100000000, ErrorMessage = "Giá bán phải từ 1,000 đến 100,000,000")]
        public decimal Price { get; set; }

        [Display(Name = "Số lượng tồn kho")]
        [Required(ErrorMessage = "Số lượng sản phẩm tồn kho không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số tồn kho phải lớn hơn hoặc bằng 0")]
        public int Quantity { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        public string? Avatar { get; set; }
    }
}
