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
    public class CFRoleController : Controller
    {
        // GET: Admin/CFRole
        public ActionResult Index()
        {
            /* show view */
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = new List<RoleDTO>();

            /* request get data */
            var request = new GetListRoleRequest()
            {
                StoreID = "123StoreID",
            };
            var response = CFBusRole.Instance.GetListRole(request);

            /* response */
            if (response.Success == true)
            {
                model = response.ListRole;
            }

            return PartialView("_ListItem", model);
        }

        public ActionResult Delete(string ID)
        {
            /* request bus */
            var request = new DeleteRoleRequest()
            {
                ID = ID,
                StoreID = "123StoreID"
            };
            var response = CFBusRole.Instance.DeleteRole(request);

            /* response */
            if (response.Success)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult LoadDetail(string ID)
        {
            var model = new RoleDTO();

            /* request bus */
            var request = new GetRoleInfoRequest()
            {
                ID = ID,
                StoreID = "123StoreID"
            };
            var response = CFBusRole.Instance.GetRoleInfo(request);
            if (response.Success)
            {
                model = response.Role;
            }
            return PartialView("_Form", model);
        }

        public ActionResult CreateOrUpdate(RoleDTO model)
        {
            try
            {
                /* validate model */
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Form", model);
                }

                var request = new CreateOrUpdateRoleRequest
                {
                    Role = model,
                    StoreID = "123StoreID",
                };
                /* call bus */
                var response = CFBusRole.Instance.CreateOrUpdateRole(request);

                /* response */
                if (response.Success)
                {
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