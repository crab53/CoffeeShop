using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CF.Business.Business.Permission;
using CF.Business.Common;
using CF.Business.Core;
using CF.DTO.Permission;

namespace CF.Api.FnB.Areas.Admin.Controllers
{
    public class CFEmployeeController : Controller
    {
        public CFEmployeeController()
        {
            ViewBag.Role = CFBusEmployee.Instance.GetListRoleSelectItem("123StoreID");
        }

        // GET: Admin/CFEmployee
        public ActionResult Index()
        {
            /* show view */
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = new List<EmployeeDTO>();

            /* request get data */
            var request = new GetListEmployeeRequest()
            {
                StoreID = "123StoreID",
            };
            var response = CFBusEmployee.Instance.GetListEmployee(request);

            /* response */
            if (response.Success == true)
            {
                model = response.ListEmployee;
            }

            return PartialView("_ListItem", model);
        }

        public ActionResult Delete(string ID)
        {
            /* request bus */
            var request = new DeleteEmployeeRequest()
            {
                ID = ID,
                StoreID = "123StoreID"
            };
            var response = CFBusEmployee.Instance.DeleteEmployee(request);

            /* response */
            if (response.Success)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult LoadDetail(string ID)
        {
            var model = new EmployeeDTO();

            /* request bus */
            var request = new GetEmployeeInfoRequest()
            {
                ID = ID,
                StoreID = "123StoreID"
            };
            var response = CFBusEmployee.Instance.GetEmployeeInfo(request);
            if (response.Success)
            {
                model = response.Employee;
            }
            return PartialView("_Form", model);
        }

        public ActionResult CreateOrUpdate(EmployeeDTO model)
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
                
                var request = new CreateOrUpdateEmployeeRequest
                {
                    Employee = model,
                    StoreID = "123StoreID",
                };
                /* call bus */
                var response = CFBusEmployee.Instance.CreateOrUpdateEmployee(request);

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