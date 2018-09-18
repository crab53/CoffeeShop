using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CF.DTO.Inventory
{
    public class CategoryDTO
    {
        public string ID { get; set; }
        public string ImageUrl { get; set; }
        public string ImageData { get; set; }
        public HttpPostedFile PictureUpload { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public CategoryDTO()
        {
            IsActive = true;
        }
    }

    #region Request

    public class GetListCategoryRequest : RequestBase
    {
        public bool IsShowActive { get; set; }
    }

    public class GetCategoryInfoRequest : RequestBase { }

    public class CreateCategoryRequest : RequestBase
    {
        public CategoryDTO Category { get; set; }
    }

    public class UpdateCategoryRequest : RequestBase
    {
        public CategoryDTO Category { get; set; }
    }

    public class DeleteCategoryRequest : RequestBase { }

    #endregion Request

    #region Response

    public class GetListCategoryResponse : ResponseBase
    {
        public List<CategoryDTO> ListCategory { get; set; }

        public GetListCategoryResponse()
        {
            ListCategory = new List<CategoryDTO>();
        }
    }

    public class GetCategoryInfoResponse : ResponseBase
    {
        public CategoryDTO Category { get; set; }
    }

    public class CreateCategoryResponse : ResponseBase { }

    public class UpdateCategoryResponse : ResponseBase { }

    public class DeleteCategoryResponse : ResponseBase { }

    #endregion Response
}
