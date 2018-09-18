using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CF.Api.FnB.Areas.Admin.Models;
using CF.Business.Business.Inventory;
using CF.DTO.Inventory;
using AutoMapper;
namespace CF.Api.FnB.Areas.Admin.Controllers
{
    public class CFCategoryController : Controller
    {
        // GET: Admin/Product
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

            };
            var response = CFBusCategory.Instance.GetListCategory(request);

            /* response */
            if (response.Success == true)
            {
                if (response.ListCategory.Count == 0) /* test data */
                {
                    model.Add(new CategoryDTO { ID = "Pro01", Name = "pro 01", Description = "Description", ImageUrl = "" });
                    model.Add(new CategoryDTO { ID = "Pro02", Name = "pro 01", Description = "Description", ImageUrl = "" });
                    model.Add(new CategoryDTO { ID = "Pro03", Name = "pro 01", Description = "Description", ImageUrl = "" });
                    model.Add(new CategoryDTO { ID = "Pro04", Name = "pro 01", Description = "Description", ImageUrl = "" });
                    model.Add(new CategoryDTO { ID = "Pro05", Name = "pro 01", Description = "Description", ImageUrl = "" });
                    model.Add(new CategoryDTO { ID = "Pro06", Name = "pro 01", Description = "Description", ImageUrl = "" });
                    model.Add(new CategoryDTO { ID = "Pro07", Name = "pro 01", Description = "Description", ImageUrl = "" });
                    model.Add(new CategoryDTO { ID = "Pro08", Name = "pro 01", Description = "Description", ImageUrl = "" });
                    model.Add(new CategoryDTO { ID = "Pro09", Name = "pro 01", Description = "Description", ImageUrl = "" });
                    model.Add(new CategoryDTO { ID = "Pro10", Name = "pro 01", Description = "Description", ImageUrl = "" });
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

        public ActionResult LoadDetail(string ID)
        {
            var model = new CategoryDTO();

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

        public ActionResult Create(CategoryDTO Model)
        {
            try
            {
                /* request */
                var request = new CreateCategoryRequest
                {
                    Category = Model,
                };

                /* call bus */
                var response = CFBusCategory.Instance.CreateCategory(request);

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