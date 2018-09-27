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
    public class CFProductController : Controller
    {
        public CFProductController()
        {
            ViewBag.Category = CFBusCategory.Instance.GetListCategorySelectItem("123StoreID");
        }
        // GET: Admin/CFProduct
        public ActionResult Index()
        {
            /* show view */
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = new List<ProductDTO>();

            /* request get data */
            GetListProductRequest request = new GetListProductRequest()
            {
                StoreID = "123StoreID"
            };
            var response = CFBusProduct.Instance.GetListProduct(request);

            /* response */
            if (response.Success == true)
            {
                model = response.ListProduct;
            }
            return PartialView("_ListItem", model);
        }

        public ActionResult Delete(string ID)
        {
            var msg = "";
            var result = true;
            //var result = _fac.HidePin(ID, "Admin", ref msg);
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult LoadDetail(string ID)
        {
            var model = new ProductDTO();

            /* request bus */
            var request = new GetProductInfoRequest()
            {
                ID = ID,
                StoreID = "123StoreID"
            };
            var response = CFBusProduct.Instance.GetProductInfo(request);
            if (response.Success)
            {
                model = response.Product;
            }
            return PartialView("_Form", model);
        }

        public ActionResult CreateOrUpdate(ProductDTO model)
        {
            try
            {
                /* validate model */
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Form", model);
                }

                /* request */
                var pictureUpload = System.Web.HttpContext.Current.Request.Files["UploadedImage"];
                if (pictureUpload != null)
                {
                    //model.ImageData = CommonFunction.ToBase64String(pictureUpload);
                    model.ImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(pictureUpload.FileName);
                }
                else if (!string.IsNullOrEmpty(model.ImageUrl))
                {
                    model.ImageUrl = Path.GetFileName(model.ImageUrl);
                }

                var request = new CreateOrUpdateProductRequest
                {
                    Product = model,
                    StoreID = "123StoreID",
                };

                /* call bus */
                var response = CFBusProduct.Instance.CreateOrUpdateProduct(request);

                /* response */
                if (response.Success)
                {
                    if (pictureUpload != null) /* save image */
                    {
                        string path = System.Web.Hosting.HostingEnvironment.MapPath(Constants._PostImages);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        pictureUpload.SaveAs(path + model.ImageUrl);
                    }
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                else
                {
                    ModelState.AddModelError("Name", response.Message);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Form", model);
                }
            }
            catch (Exception ex) { };
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
        }

    }
}