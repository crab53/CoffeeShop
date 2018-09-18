using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CF.Business.Business.Inventory;
using CF.DTO.Inventory;

namespace CF.Api.FnB.Areas.Admin.Controllers
{
    public class CFProductController : Controller
    {
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

            };
            var response = CFBusProduct.Instance.GetListProduct(request);

            /* response */
            if (response.Success == true)
            {
                if (response.ListProduct.Count == 0) /* test data */
                {
                    model.Add(new ProductDTO { ID = "Pro01", Name = "pro 01", Price = 1, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro02", Name = "pro 01", Price = 2, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro03", Name = "pro 01", Price = 3, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro04", Name = "pro 01", Price = 4, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro05", Name = "pro 01", Price = 5, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro06", Name = "pro 01", Price = 6, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro07", Name = "pro 01", Price = 7, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro08", Name = "pro 01", Price = 8, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro09", Name = "pro 01", Price = 9, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro10", Name = "pro 01", Price = 10, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro11", Name = "pro 01", Price = 11, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro12", Name = "pro 01", Price = 12, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro13", Name = "pro 01", Price = 13, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro14", Name = "pro 01", Price = 14, ImageUrl = "" });
                    model.Add(new ProductDTO { ID = "Pro15", Name = "pro 01", Price = 15, ImageUrl = "" });
                }
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

        public ActionResult LoadProductDetail(string ID)
        {
            var model = new ProductDTO();

            var msg = "";
            var result = true;
            //var result = _fac.HidePin(ID, "Admin", ref msg);
            if (result)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;

                //return new HttpStatusCodeResult(HttpStatusCode.OK);

                return PartialView("_Form", model);
            }
            return PartialView("_Form", model);
        }

        public ActionResult Create(ProductDTO Model)
        {
            try
            {
                /* request */
                var request = new CreateProductRequest
                {
                    Product = Model,
                };

                /* call bus */
                var response = CFBusProduct.Instance.CreateProduct(request);

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