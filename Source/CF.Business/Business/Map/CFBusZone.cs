using CF.Business.Common;
using CF.Data.Context;
using CF.Data.Entities;
using CF.DTO.Map;
using System;
using System.Linq;
using System.Reflection;

namespace CF.Business.Business.Map
{
    public class CFBusZone
    {
        private static CFBusZone instance;

        private CFBusZone()
        {
        }

        public static CFBusZone Instance
        {
            get
            {
                if (instance == null) instance = new CFBusZone();
                return instance;
            }
        }

        public GetListZoneResponse GetListZone(GetListZoneRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            GetListZoneResponse response = new GetListZoneResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    var query = _db.Zones.Where(o => o.StoreID == input.StoreID && !o.IsDelete);

                    if (input.IsShowActive)
                        query = query.Where(o => o.IsActive);

                    response.ListZone = query.OrderBy(o => o.Name).Skip(input.PageIndex * input.PageSize).Take(input.PageSize)
                        .Select(o => new ZoneDTO()
                        {
                            ID = o.ID,
                            Name = o.Name,
                            Description = o.Description,
                            Height = o.Height,
                            Width = o.Width,
                            IsActive = o.IsActive,
                        }).ToList();
                    response.Success = true;
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public GetZoneInfoResponse GetZoneInfo(GetZoneInfoRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            GetZoneInfoResponse response = new GetZoneInfoResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    response.Zone = _db.Zones.Where(o => o.ID == input.ID && o.StoreID == input.StoreID && !o.IsDelete)
                        .Select(o => new ZoneDTO()
                        {
                            ID = o.ID,
                            Name = o.Name,
                            Description = o.Description,
                            Height = o.Height,
                            Width = o.Width,
                            IsActive = o.IsActive,
                        }).FirstOrDefault();

                    if (response.Zone != null)
                        response.Success = true;
                    else
                        response.Message = "Không tìm thấy khu vực này.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public CreateOrUpdateZoneResponse CreateOrUpdateZone(CreateOrUpdateZoneRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            CreateOrUpdateZoneResponse response = new CreateOrUpdateZoneResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    if (input.Zone != null)
                    {
                        if (!string.IsNullOrEmpty(input.Zone.Name))
                        {
                            string nameStr = CommonFunction.RemoveSign4VietnameseString(input.Zone.Name);
                            if (string.IsNullOrEmpty(input.Zone.ID))
                            {
                                var zone = _db.Zones.Where(o => o.NameStr == nameStr && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                if (zone == null)
                                {
                                    zone = new Zone()
                                    {
                                        ID = Guid.NewGuid().ToString(),
                                        StoreID = input.StoreID,
                                        Name = input.Zone.Name,
                                        NameStr = nameStr,
                                        Description = input.Zone.Description,
                                        Height = input.Zone.Height,
                                        Width = input.Zone.Width,
                                        IsActive = input.Zone.IsActive,
                                        IsDelete = false,
                                    };
                                    _db.Zones.Add(zone);

                                    if (_db.SaveChanges() > 0)
                                        response.Success = true;
                                    else
                                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thêm mới khu vực.";
                                }
                                else
                                    response.Message = "Tên khu vực này đã tồn tại. Vui lòng chọn tên khác.";
                            }
                            else
                            {
                                var zone = _db.Zones.Where(o => o.NameStr == nameStr && o.ID != input.Zone.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                if (zone == null)
                                {
                                    zone = _db.Zones.Where(o => o.ID == input.Zone.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                    if (zone != null)
                                    {
                                        var maxPosion = _db.Tables.Where(o => o.StoreID == input.StoreID && o.ZoneID == input.Zone.ID && !o.IsDelete)
                                            .Select(o => new { x = o.XPoint - 1, y = o.YPoint - 1 }).GroupBy(o => o)
                                            .Select(o => new { maxX = o.Max(u => u.x), maxY = o.Max(u => u.y) }).FirstOrDefault();
                                        if (input.Zone.Width >= maxPosion.maxX && input.Zone.Height >= maxPosion.maxY)
                                        {
                                            zone.Name = input.Zone.Name;
                                            zone.NameStr = nameStr;
                                            zone.Description = input.Zone.Description;
                                            zone.Height = input.Zone.Height;
                                            zone.Width = input.Zone.Width;
                                            zone.IsActive = input.Zone.IsActive;

                                            if (_db.SaveChanges() > 0)
                                                response.Success = true;
                                            else
                                                response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thay đổi thông tin khu vực.";
                                        }
                                        else
                                            response.Message = "Bạn không thể thay đởi kích thước khu vực. Vui lòng xem lại vị trí cái bàn trong khu vực.";
                                    }
                                    else
                                        response.Message = "Không tìm thấy khu vực được chọn.";
                                }
                                else
                                    response.Message = "Tên khu vực này đã tồn tại. Vui lòng chọn tên khác.";
                            }
                        }
                        else
                            response.Message = "Vui lòng nhập tên khu vực.";
                    }
                    else
                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thêm mới khu vực.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }
        
        public DeleteZoneResponse DeleteZone(DeleteZoneRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            DeleteZoneResponse response = new DeleteZoneResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    var zone = _db.Zones.Where(o => o.ID == input.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                    if (zone != null)
                    {
                        zone.IsDelete = true;

                        if (_db.SaveChanges() > 0)
                            response.Success = true;
                        else
                            response.Message = "Đã có lỗi xảy ra. Tạm thời không thể xoá khu vực.";
                    }
                    else
                        response.Message = "Không tìm thấy khu vực được chọn.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }
    }
}