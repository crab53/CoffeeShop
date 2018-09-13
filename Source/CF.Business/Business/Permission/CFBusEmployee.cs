using CF.Business.Common;
using CF.Business.Core;
using CF.Data.Context;
using CF.Data.Entities;
using CF.DTO.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Permission
{
    public class CFBusEmployee
    {
        private static CFBusEmployee instance;

        private CFBusEmployee() { }

        public static CFBusEmployee Instance
        {
            get
            {
                if (instance == null) instance = new CFBusEmployee();
                return instance;
            }
        }

        public GetListEmployeeResponse GetListEmployee(GetListEmployeeRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            GetListEmployeeResponse response = new GetListEmployeeResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    var query = _db.Employees.Where(o => o.StoreID == input.StoreID && !o.IsDelete);

                    if (input.IsShowActive)
                        query = query.Where(o => o.IsActive);

                    response.ListEmployee = query.OrderBy(o => o.Name).Skip(input.PageIndex * input.PageSize).Take(input.PageSize)
                        .GroupJoin(_db.Roles, e => e.RoleID, r => r.ID, (e, r) => new { e, r = r.FirstOrDefault() })
                        .Select(o => new EmployeeDTO()
                        {
                            ID = o.e.ID,
                            RoleID = o.r != null ? o.r.ID : null,
                            RoleName = o.e.IsSA ? "SuperAdmin" : (o.r != null ? o.r.Name : ""),
                            ImageUrl = o.e.ImageUrl,
                            Name = o.e.Name,
                            Email = o.e.Email,
                            Phone = o.e.Phone,
                            Address = o.e.Address,
                            Birthday = o.e.Birthday,
                            HiredDate = o.e.HiredDate,
                            IsActive = o.e.IsActive,
                        }).ToList();
                    response.Success = true;
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public GetEmployeeInfoResponse GetEmployeeInfo(GetEmployeeInfoRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            GetEmployeeInfoResponse response = new GetEmployeeInfoResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    response.Employee = _db.Employees.Where(o => o.ID == input.ID && o.StoreID == input.StoreID && !o.IsDelete)
                        .GroupJoin(_db.Roles, e => e.RoleID, r => r.ID, (e, r) => new { e, r = r.FirstOrDefault() })
                        .Select(o => new EmployeeDTO()
                        {
                            ID = o.e.ID,
                            RoleID = o.r != null ? o.r.ID : null,
                            RoleName = o.e.IsSA ? "SuperAdmin" : (o.r != null ? o.r.Name : ""),
                            ImageUrl = o.e.ImageUrl,
                            Name = o.e.Name,
                            Email = o.e.Email,
                            Phone = o.e.Phone,
                            Address = o.e.Address,
                            Birthday = o.e.Birthday,
                            HiredDate = o.e.HiredDate,
                            IsActive = o.e.IsActive,
                        }).FirstOrDefault();

                    if (response.Employee != null)
                        response.Success = true;
                    else
                        response.Message = "Không tìm thấy nhân viên này.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public CreateEmployeeResponse CreateEmployee(CreateEmployeeRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            CreateEmployeeResponse response = new CreateEmployeeResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    if (input.Employee != null)
                    {
                        var role = _db.Roles.Where(o => o.StoreID == input.StoreID && !o.IsDelete && o.ID == input.Employee.RoleID).FirstOrDefault();
                        if (role != null)
                        {
                            if (!string.IsNullOrEmpty(input.Employee.Name) && !string.IsNullOrEmpty(input.Employee.Email))
                            {
                                string email = input.Employee.Email.ToLower().Trim();
                                var employee = _db.Employees.Where(o => o.Email == email && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                if (employee == null)
                                {
                                    string pass = CommonFunction.GenerateKey(false);

                                    employee = new Employee()
                                    {
                                        ID = Guid.NewGuid().ToString(),
                                        StoreID = input.StoreID,
                                        RoleID = role.ID,
                                        IsSA = false,
                                        ImageUrl = input.Employee.ImageUrl,
                                        Name = input.Employee.Name,
                                        Email = email,
                                        Password = CommonFunction.GetSHA512(pass),
                                        Phone = input.Employee.Phone,
                                        Address = input.Employee.Address,
                                        Birthday = input.Employee.Birthday ?? Constants.MinDate,
                                        HiredDate = input.Employee.HiredDate ?? Constants.MinDate,
                                        IsActive = input.Employee.IsActive,
                                        IsDelete = false,
                                    };
                                    _db.Employees.Add(employee);

                                    if (_db.SaveChanges() > 0)
                                    {
                                        response.Success = true;
                                    }
                                    else
                                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thêm mới nhân viên.";
                                }
                                else
                                    response.Message = "Thư điện tử này đã tồn tại. Vui lòng chọn thư điện tử khác.";
                            }
                            else
                                response.Message = "Vui lòng kiểm tra lại tên hoặc thư điện tử của nhân viên.";
                        }
                        else
                            response.Message = "Vui lòng chọn phần quyền cho nhân viên.";
                    }
                    else
                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thêm mới nhân viên.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public UpdateEmployeeResponse UpdateEmployee(UpdateEmployeeRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            UpdateEmployeeResponse response = new UpdateEmployeeResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    if (input.Employee != null)
                    {
                        var employee = _db.Employees.Where(o => o.ID == input.Employee.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                        if (employee != null)
                        {
                            if (!employee.IsSA)
                            {
                                var role = _db.Roles.Where(o => o.ID == input.Employee.RoleID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                if (role != null)
                                    employee.RoleID = role.ID;
                                else
                                {
                                    response.Message = "Vui lòng chọn phần quyền cho nhân viên.";
                                    Log.Logger.Info("Response" + methodName, response);
                                    return response;
                                }
                            }

                            if (!string.IsNullOrEmpty(input.Employee.Name) && !string.IsNullOrEmpty(input.Employee.Email))
                            {
                                employee.ImageUrl = input.Employee.ImageUrl;
                                employee.Name = input.Employee.Name;
                                employee.Email = input.Employee.Email.ToLower().Trim();
                                employee.Phone = input.Employee.Phone;
                                employee.Address = input.Employee.Address;
                                employee.Birthday = input.Employee.Birthday ?? Constants.MinDate;
                                employee.HiredDate = input.Employee.HiredDate ?? Constants.MinDate;
                                employee.IsActive = input.Employee.IsActive;

                                if (_db.SaveChanges() > 0)
                                    response.Success = true;
                                else
                                    response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thay đổi thông tin nhân viên.";
                            }
                            else
                                response.Message = "Vui lòng kiểm tra lại tên hoặc thư điện tử của nhân viên.";
                        }
                        else
                            response.Message = "Không tìm thấy nhân viên được chọn.";
                    }
                    else
                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thay đổi thông tin nhân viên.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public DeleteEmployeeResponse DeleteEmployee(DeleteEmployeeRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            DeleteEmployeeResponse response = new DeleteEmployeeResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    var employee = _db.Employees.Where(o => o.ID == input.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                    if (employee != null)
                    {
                        if (!employee.IsSA)
                        {
                            employee.IsDelete = true;

                            if (_db.SaveChanges() > 0)
                                response.Success = true;
                            else
                                response.Message = "Đã có lỗi xảy ra. Tạm thời không thể xoá nhân viên.";
                        }
                        else
                            response.Message = "Không thể xoá SuperAdmin.";
                    }
                    else
                        response.Message = "Không tìm thấy nhân viên được chọn.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }
    }
}
