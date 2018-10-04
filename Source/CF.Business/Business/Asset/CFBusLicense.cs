using CF.Business.Common;
using CF.Business.Core;
using CF.Data.Context;
using CF.Data.Entities;
using CF.DTO.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Asset
{
    public class CFBusLicense
    {
        private static CFBusLicense instance;

        private CFBusLicense() { }

        public static CFBusLicense Instance
        {
            get
            {
                if (instance == null) instance = new CFBusLicense();
                return instance;
            }
        }

        public RegisterNewStoreResponse GetCategoryInfo(RegisterNewStoreRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            RegisterNewStoreResponse response = new RegisterNewStoreResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    if (input.Owner != null && input.Store != null)
                    {
                        string email = input.Owner.Email.Trim().ToLower();
                        string addressStr = CommonFunction.RemoveSign4VietnameseString(input.Store.Address, true);

                        var employee = _db.Employees.Where(o => o.Email == email && !o.IsDelete).FirstOrDefault();
                        var store = _db.Stores.Where(o => o.AddressStr == addressStr && !o.IsDelete).FirstOrDefault();
                        if (employee == null && store == null)
                        {
                            store = new Store()
                            {
                                ID = Guid.NewGuid().ToString(),
                                ImageUrl = input.Store.ImageUrl,
                                Name = input.Store.Name,
                                Phone = input.Store.Phone,
                                Address = input.Store.Address,
                                AddressStr = addressStr,
                                Description = input.Store.Description,
                                ExpiredDate = Constants.MinDate,
                                IsDelete = false,
                            };
                            _db.Stores.Add(store);

                            employee = new Employee()
                            {
                                ID = Guid.NewGuid().ToString(),
                                StoreID = store.ID,
                                RoleID = null,
                                IsSA = true,
                                ImageUrl = input.Owner.ImageUrl,
                                Name = input.Owner.Name,
                                Email = email,
                                Password = CommonFunction.GetSHA512(input.Owner.Password),
                                Phone = input.Owner.Phone,
                                Address = input.Owner.Address,
                                Birthday = Constants.MinDate,
                                HiredDate = DateTime.Now,
                                IsActive = true,
                                IsDelete = false,
                            };
                            _db.Employees.Add(employee);

                            License license = new License()
                            {
                                ID = Guid.NewGuid().ToString(),
                                EmployeeID = employee.ID,
                                StoreID = store.ID,
                                Key = CommonFunction.GenerateKey(Constants.EKey.Code),
                                Period = 1,
                                PeriodType = (int)Constants.EPeriodType.Month,
                                IsUsed = false,
                                IsDelete = false,
                            };
                            _db.Licenses.Add(license);

                            if (_db.SaveChanges() > 0)
                            {
                                response.Success = true;
                            }
                            else
                                response.Message = "Có lỗi xãy ra. Vui lòng kiểm tra lại.";
                        }
                        else if (employee != null)
                            response.Message = "Thư điện tử này đã tồn tại. Vui lòng chọn thư điện tử khác.";
                        else
                            response.Message = "Cửa hàng ở địa chỉ này đã tồn tại.";
                    }
                    else
                        response.Message = "Có lỗi xãy ra. Vui lòng kiểm tra lại.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }
    }
}
