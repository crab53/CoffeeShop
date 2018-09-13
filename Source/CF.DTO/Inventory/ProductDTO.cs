using System.Collections.Generic;

namespace CF.DTO.Inventory
{
    public class ProductDTO
    {
        public string ID { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int ProductType { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    #region Request

    public class GetListProductRequest : RequestBase
    {
        public bool IsShowActive { get; set; }
        public int ProductType { get; set; }
    }

    public class GetProductInfoRequest : RequestBase { }

    public class CreateProductRequest : RequestBase
    {
        public ProductDTO Product { get; set; }
    }

    public class UpdateProductRequest : RequestBase
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

    public class CreateProductResponse : ResponseBase { }

    public class UpdateProductResponse : ResponseBase { }

    public class DeleteProductResponse : ResponseBase { }

    #endregion Response
}