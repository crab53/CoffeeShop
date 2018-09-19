using CF.Business.Common;
using CF.Data.Context;
using CF.Data.Entities;
using CF.DTO.Permission;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Permission
{
    public class CFBusRole
    {
        private static CFBusRole instance;

        private CFBusRole() { }

        public static CFBusRole Instance
        {
            get
            {
                if (instance == null) instance = new CFBusRole();
                return instance;
            }
        }

        public GetListRoleResponse GetListRole(GetListRoleRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            GetListRoleResponse response = new GetListRoleResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    var query = _db.Roles.Where(o => o.StoreID == input.StoreID && !o.IsDelete);

                    if (input.IsShowActive)
                        query = query.Where(o => o.IsActive);

                    var listRole = query.OrderBy(o => o.Name).ToList();
                    foreach (var role in listRole)
                    {
                        RoleDTO dto = new RoleDTO()
                        {
                            ID = role.ID,
                            Name = role.Name,
                            Description = role.Description,
                            IsActive = role.IsActive,
                            ListPermission = GetPermissions(role.Permissions),
                        };
                        response.ListRole.Add(dto);
                    }

                    response.Success = true;
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public GetRoleInfoResponse GetRoleInfo(GetRoleInfoRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            GetRoleInfoResponse response = new GetRoleInfoResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    var role = _db.Roles.Where(o => o.ID == input.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                    if (role != null)
                    {
                        response.Role = new RoleDTO()
                        {
                            ID = role.ID,
                            Name = role.Name,
                            Description = role.Description,
                            IsActive = role.IsActive,
                            ListPermission = GetPermissions(role.Permissions),
                        };

                        response.Success = true;
                    }
                    else
                        response.Message = "Không tìm thấy phân quyền này.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public CreateOrUpdateRoleResponse CreateOrUpdateRole(CreateOrUpdateRoleRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            CreateOrUpdateRoleResponse response = new CreateOrUpdateRoleResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    if (input.Role != null)
                    {
                        if (!string.IsNullOrEmpty(input.Role.Name))
                        {
                            string nameStr = CommonFunction.RemoveSign4VietnameseString(input.Role.Name).Replace(" ", "");
                            if (string.IsNullOrEmpty(input.Role.ID))
                            {
                                var role = _db.Roles.Where(o => o.NameStr == nameStr && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                if (role == null)
                                {
                                    role = new Role()
                                    {
                                        ID = Guid.NewGuid().ToString(),
                                        StoreID = input.StoreID,
                                        Name = input.Role.Name,
                                        NameStr = nameStr,
                                        Description = input.Role.Description,
                                        IsActive = input.Role.IsActive,
                                        Permissions = JsonConvert.SerializeObject(input.Role.ListPermission),
                                        IsDelete = false,
                                    };
                                    _db.Roles.Add(role);

                                    if (_db.SaveChanges() > 0)
                                        response.Success = true;
                                    else
                                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thêm mới phân quyền.";
                                }
                                else
                                    response.Message = "Tên phân quyền này đã tồn tại. Vui lòng chọn tên khác.";
                            }
                            else
                            {
                                var role = _db.Roles.Where(o => o.NameStr == nameStr && o.ID != input.Role.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                if (role == null)
                                {
                                    role = _db.Roles.Where(o => o.ID == input.Role.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                    if (role != null)
                                    {
                                        role.Name = input.Role.Name;
                                        role.NameStr = nameStr;
                                        role.Description = input.Role.Description;
                                        role.IsActive = input.Role.IsActive;
                                        role.Permissions = JsonConvert.SerializeObject(input.Role.ListPermission);

                                        if (_db.SaveChanges() > 0)
                                            response.Success = true;
                                        else
                                            response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thay đổi thông tin phân quyền.";
                                    }
                                    else
                                        response.Message = "Không tìm thấy phân quyền được chọn.";
                                }
                                else
                                    response.Message = "Tên phân quyền này đã tồn tại. Vui lòng chọn tên khác.";
                            }
                        }
                        else
                            response.Message = "Vui lòng nhập tên phân quyền.";
                    }
                    else
                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thêm mới phân quyền.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public DeleteRoleResponse DeleteRole(DeleteRoleRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            DeleteRoleResponse response = new DeleteRoleResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    var role = _db.Roles.Where(o => o.ID == input.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                    if (role != null)
                    {
                        role.IsDelete = true;

                        if (_db.SaveChanges() > 0)
                            response.Success = true;
                        else
                            response.Message = "Đã có lỗi xảy ra. Tạm thời không thể xoá phân quyền.";
                    }
                    else
                        response.Message = "Không tìm thấy phân quyền được chọn.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public List<PermissionDTO> GetPermissions(string json)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, json);
            List<PermissionDTO> permissions = new List<PermissionDTO>();
            try
            {
                using (var _db = new CfDb())
                {
                    if (!string.IsNullOrEmpty(json))
                    {
                        var listPermission = JsonConvert.DeserializeObject<List<PermissionDTO>>(json);
                        var listModule = _db.Modules.Where(o => o.IsActive).ToList();

                        permissions = listModule.GroupJoin(listPermission, m => m.Code, p => p.Code, (m, p) => new { m, p = p.FirstOrDefault() })
                            .Select(o => new PermissionDTO()
                            {
                                Code = o.m.Code,
                                Name = o.m.Name,
                                IsAction = o.p != null ? o.p.IsAction : false,
                                IsView = o.p != null ? o.p.IsView : false,
                            }).OrderBy(o => o.Code).ToList();
                    }
                    else
                    {
                        permissions = _db.Modules.Where(o => o.IsActive)
                            .Select(o => new PermissionDTO()
                            {
                                Code = o.Code,
                                Name = o.Name,
                                IsAction = false,
                                IsView = false,
                            }).ToList();
                    }
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, permissions);
            return permissions;
        }
    }
}
