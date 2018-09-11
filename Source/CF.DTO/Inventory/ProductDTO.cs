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
        public ProductDTO ProductInfo { get; set; }
    }

    #endregion Response
}