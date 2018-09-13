using CF.Business.Common;
using CF.Data.Context;
using CF.Data.Entities;
using CF.DTO.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Map
{
    public class CFBusTable
    {
        private static CFBusTable instance;

        private CFBusTable() { }

        public static CFBusTable Instance
        {
            get
            {
                if (instance == null) instance = new CFBusTable();
                return instance;
            }
        }

        public GetListTableResponse GetListTable(GetListTableRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            GetListTableResponse response = new GetListTableResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    var query = _db.Tables.Where(o => o.StoreID == input.StoreID && !o.IsDelete);

                    if (input.IsShowActive)
                        query = query.Where(o => o.IsActive);

                    response.ListTable = query.OrderBy(o => o.Name).Skip(input.PageIndex * input.PageSize).Take(input.PageSize)
                        .Join(_db.Zones, t => t.ZoneID, z => z.ID, (t, z) => new { t, z })
                        .Select(o => new TableDTO()
                        {
                            ID = o.t.ID,
                            ZoneID = o.z.ID,
                            ZoneName = o.z.Name,
                            Name = o.t.Name,
                            Cover = o.t.Cover,
                            Description = o.t.Description,
                            ViewMode = o.t.ViewMode,
                            XPoint = o.t.XPoint,
                            YPoint = o.t.YPoint,
                            IsActive = o.t.IsActive,
                        }).ToList();
                    response.Success = true;
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public GetTableInfoResponse GetTableInfo(GetTableInfoRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            GetTableInfoResponse response = new GetTableInfoResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    response.Table = _db.Tables.Where(o => o.ID == input.ID && o.StoreID == input.StoreID && !o.IsDelete)
                        .Join(_db.Zones, t => t.ZoneID, z => z.ID, (t, z) => new { t, z })
                        .Select(o => new TableDTO()
                        {
                            ID = o.t.ID,
                            ZoneID = o.z.ID,
                            ZoneName = o.z.Name,
                            Name = o.t.Name,
                            Cover = o.t.Cover,
                            Description = o.t.Description,
                            ViewMode = o.t.ViewMode,
                            XPoint = o.t.XPoint,
                            YPoint = o.t.YPoint,
                            IsActive = o.t.IsActive,
                        }).FirstOrDefault();

                    if (response.Table != null)
                        response.Success = true;
                    else
                        response.Message = "Không tìm thấy bàn này.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public CreateTableResponse CreateTable(CreateTableRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            CreateTableResponse response = new CreateTableResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    if (input.Table != null)
                    {
                        var zone = _db.Zones.Where(o => o.StoreID == input.StoreID && !o.IsDelete && o.ID == input.Table.ZoneID).FirstOrDefault();
                        if (zone != null)
                        {
                            if (!string.IsNullOrEmpty(input.Table.Name))
                            {
                                string nameStr = CommonFunction.RemoveSign4VietnameseString(input.Table.Name);
                                var table = _db.Tables.Where(o => o.NameStr == nameStr && o.ZoneID == zone.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                if (table == null)
                                {
                                    table = new Table()
                                    {
                                        ID = Guid.NewGuid().ToString(),
                                        StoreID = input.StoreID,
                                        ZoneID = zone.ID,
                                        Name = input.Table.Name,
                                        NameStr = nameStr,
                                        Cover = input.Table.Cover,
                                        ViewMode = input.Table.ViewMode,
                                        XPoint = input.Table.XPoint,
                                        YPoint = input.Table.YPoint,
                                        Description = input.Table.Description,
                                        IsActive = input.Table.IsActive,
                                        IsDelete = false,
                                    };
                                    _db.Tables.Add(table);

                                    if (_db.SaveChanges() > 0)
                                        response.Success = true;
                                    else
                                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thêm mới bàn.";
                                }
                                else
                                    response.Message = "Tên bàn này đã tồn tại. Vui lòng chọn tên khác.";
                            }
                            else
                                response.Message = "Vui lòng nhập tên bàn.";
                        }
                        else
                            response.Message = "Vui lòng chọn khu vực bàn.";
                    }
                    else
                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thêm mới bàn.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public UpdateTableResponse UpdateTable(UpdateTableRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            UpdateTableResponse response = new UpdateTableResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    if (input.Table != null)
                    {
                        var zone = _db.Zones.Where(o => o.StoreID == input.StoreID && !o.IsDelete && o.ID == input.Table.ZoneID).FirstOrDefault();
                        if (zone != null)
                        {
                            if (!string.IsNullOrEmpty(input.Table.Name))
                            {
                                string nameStr = CommonFunction.RemoveSign4VietnameseString(input.Table.Name);
                                var table = _db.Tables.Where(o => o.NameStr == nameStr && o.ZoneID == zone.ID && o.ID != input.Table.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                if (table == null)
                                {
                                    table = _db.Tables.Where(o => o.ID == input.Table.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                    if (table != null)
                                    {
                                        table.ZoneID = zone.ID;
                                        table.Name = input.Table.Name;
                                        table.NameStr = nameStr;
                                        table.Description = input.Table.Description;
                                        table.Cover = input.Table.Cover;
                                        table.ViewMode = input.Table.ViewMode;
                                        table.XPoint = input.Table.XPoint;
                                        table.YPoint = input.Table.YPoint;
                                        table.IsActive = input.Table.IsActive;

                                        if (_db.SaveChanges() > 0)
                                            response.Success = true;
                                        else
                                            response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thay đổi thông tin bàn.";
                                    }
                                    else
                                        response.Message = "Không tìm thấy bàn được chọn.";
                                }
                                else
                                    response.Message = "Tên bàn này đã tồn tại. Vui lòng chọn tên khác.";
                            }
                            else
                                response.Message = "Vui lòng nhập tên bàn.";
                        }
                        else
                            response.Message = "Vui lòng kiểm tra lại khu vực bàn.";
                    }
                    else
                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thay đổi thông tin bàn.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public DeleteTableResponse DeleteTable(DeleteTableRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            DeleteTableResponse response = new DeleteTableResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    var table = _db.Tables.Where(o => o.ID == input.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                    if (table != null)
                    {
                        table.IsDelete = true;

                        if (_db.SaveChanges() > 0)
                            response.Success = true;
                        else
                            response.Message = "Đã có lỗi xảy ra. Tạm thời không thể xoá bàn.";
                    }
                    else
                        response.Message = "Không tìm thấy bàn được chọn.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }
    }
}
