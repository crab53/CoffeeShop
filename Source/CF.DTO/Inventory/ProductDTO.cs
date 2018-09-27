using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CF.DTO.Inventory
{
    public class ProductDTO
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int ProductType { get; set; }
        public string ImageUrl { get; set; }
        public string ImageData { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm ")]
        [MaxLength(250, ErrorMessage = "Tên thể loại tối đa 250 kí tự")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public ProductDTO()
        {
            IsActive = true;
        }
    }

    #region Request

    public class GetListProductRequest : RequestBase
    {
        public bool IsShowActive { get; set; }
        public int ProductType { get; set; }
    }

    public class GetProductInfoRequest : RequestBase { }

    public class CreateOrUpdateProductRequest : RequestBase
    {
        public ProductDTO Product { get; set; }
    }
    
    public class DeleteProductRequest : RequestBase { }

    #endregion Request

    #region Response

    public class GetListProductResponse : ResponseBase
    {
        public List<ProductDTO> ListProduct { get; set; }

        public GetListProductResponse()
        {
            ListProduct = new List<ProductDTO>();
        }
    }

    public class GetProductInfoResponse : ResponseBase
    {
        public ProductDTO Product { get; set; }
    }

    public class CreateOrUpdateProductResponse : ResponseBase { }
    
    public class DeleteProductResponse : ResponseBase { }

    #endregion Response
}