using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CF.Business.Business.Inventory;
using CF.Business.Core;
using CF.DTO.Inventory;
namespace CF.Api.FnB.Areas.Admin.Controllers
{
    public class CFCategoryController : Controller
    {
        // GET: Admin/CFCategory
        public ActionResult Index()
        {
            /* show view */
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = new List<CategoryDTO>();

            /* request get data */
            GetListCategoryRequest request = new GetListCategoryRequest()
            {
                StoreID = "123StoreID",
                PageIndex = 0,
                PageSize = 9999,
            };
            var response = CFBusCategory.Instance.GetListCategory(request);

            /* response */
            if (response.Success == true)
            {
                model = response.ListCategory;
            }

            return PartialView("_ListItem", model);
        }

        public ActionResult Delete(string ID)
        {
            /* request bus */
            var request = new DeleteCategoryRequest()
            {
                ID = ID,
                StoreID = "123StoreID"
            };
            var response = CFBusCategory.Instance.DeleteCategory(request);

            /* response */
            if (response.Success)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        
        public ActionResult LoadDetail(string ID)
        {
            var model = new CategoryDTO();
            
            /* request bus */
            var request = new GetCategoryInfoRequest()
            {
                ID = ID,
                StoreID = "123StoreID"
            };
            var response = CFBusCategory.Instance.GetCategoryInfo(request);
            if (response.Success)
            {
                model = response.Category;
            }
            return PartialView("_Form", model);
        }

        public ActionResult CreateOrUpdate(CategoryDTO Model)
        {
            try
            {
                /* request */
                Model.PictureUpload = System.Web.HttpContext.Current.Request.Files["UploadedImage"];
                var request = new CreateOrUpdateCategoryRequest
                {
                    Category = Model,
                    StoreID = "123StoreID",
                };
                
                /* call bus */
                var response = CFBusCategory.Instance.CreateOrUpdateCategory(request);

                /* response */
                if (response.Success)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
            }
            catch(Exception ex) { };
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}