﻿using CF.Business.Common;
using CF.Business.Core;
using CF.Data.Context;
using CF.Data.Entities;
using CF.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace CF.Business.Business.Inventory
{
    public class CFBusCategory
    {
        private static CFBusCategory instance;

        private CFBusCategory()
        {
        }

        public static CFBusCategory Instance
        {
            get
            {
                if (instance == null) instance = new CFBusCategory();
                return instance;
            }
        }

        public GetListCategoryResponse GetListCategory(GetListCategoryRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            GetListCategoryResponse response = new GetListCategoryResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    var query = _db.Categories.Where(o => o.StoreID == input.StoreID && !o.IsDelete);

                    if (input.IsShowActive)
                        query = query.Where(o => o.IsActive);

                    response.ListCategory = query.OrderBy(o => o.Name).Skip(input.PageIndex * input.PageSize).Take(input.PageSize)
                        .Select(o => new CategoryDTO()
                        {
                            ID = o.ID,
                            Name = o.Name,
                            Description = o.Description,
                            IsActive = o.IsActive,
                            ImageUrl = string.IsNullOrEmpty(o.ImageUrl) ? "" : Constants._PublicImages + o.ImageUrl,
                        }).ToList();
                    response.Success = true;
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public GetCategoryInfoResponse GetCategoryInfo(GetCategoryInfoRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            GetCategoryInfoResponse response = new GetCategoryInfoResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    response.Category = _db.Categories.Where(o => o.ID == input.ID && o.StoreID == input.StoreID && !o.IsDelete)
                        .Select(o => new CategoryDTO()
                        {
                            ID = o.ID,
                            Name = o.Name,
                            Description = o.Description,
                            IsActive = o.IsActive,
                            ImageUrl = string.IsNullOrEmpty(o.ImageUrl) ? "" : Constants._PublicImages + o.ImageUrl,
                        }).FirstOrDefault();

                    if (response.Category != null)
                        response.Success = true;
                    else
                        response.Message = "Không tìm thấy danh mục này.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public CreateOrUpdateCategoryResponse CreateOrUpdateCategory(CreateOrUpdateCategoryRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            CreateOrUpdateCategoryResponse response = new CreateOrUpdateCategoryResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    if (input.Category != null)
                    {
                        if (!string.IsNullOrEmpty(input.Category.Name))
                        {
                            string nameStr = CommonFunction.RemoveSign4VietnameseString(input.Category.Name);
                            if (!string.IsNullOrEmpty(input.Category.ImageData))
                            {
                                var imageUrl = "";
                                if (CommonFunction.UploadImage(input.Category.ImageData, ref imageUrl))
                                    input.Category.ImageUrl = imageUrl;
                            }

                            if (string.IsNullOrEmpty(input.Category.ID))/* insert */
                            {
                                var category = _db.Categories.Where(o => o.NameStr == nameStr && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                if (category == null)
                                {
                                    category = new Category()
                                    {
                                        ID = Guid.NewGuid().ToString(),
                                        StoreID = input.StoreID,
                                        ImageUrl = input.Category.ImageUrl,
                                        Name = input.Category.Name,
                                        NameStr = nameStr,
                                        Description = input.Category.Description,
                                        IsActive = input.Category.IsActive,
                                        IsDelete = false,
                                    };
                                    _db.Categories.Add(category);

                                    if (_db.SaveChanges() > 0)
                                        response.Success = true;
                                    else
                                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thêm mới danh mục.";
                                }
                                else
                                    response.Message = "Tên danh mục này đã tồn tại. Vui lòng chọn tên khác.";
                            }
                            else /* update */
                            {
                                var category = _db.Categories.Where(o => o.NameStr == nameStr && o.ID != input.Category.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                if (category == null)
                                {
                                    category = _db.Categories.Where(o => o.ID == input.Category.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                    if (category != null)
                                    {
                                        category.ImageUrl = input.Category.ImageUrl;
                                        category.Name = input.Category.Name;
                                        category.NameStr = nameStr;
                                        category.Description = input.Category.Description;
                                        category.IsActive = input.Category.IsActive;

                                        if (_db.SaveChanges() >= 0)
                                            response.Success = true;
                                        else
                                            response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thay đổi thông tin danh mục.";
                                    }
                                    else
                                        response.Message = "Không tìm thấy danh mục được chọn.";
                                }
                                else
                                    response.Message = "Tên danh mục này đã tồn tại. Vui lòng chọn tên khác.";
                            }
                        }
                        else
                            response.Message = "Vui lòng nhập tên danh mục.";
                    }
                    else
                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thêm mới danh mục.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public DeleteCategoryResponse DeleteCategory(DeleteCategoryRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            DeleteCategoryResponse response = new DeleteCategoryResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    var category = _db.Categories.Where(o => o.ID == input.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                    if (category != null)
                    {
                        category.IsDelete = true;

                        if (_db.SaveChanges() > 0)
                            response.Success = true;
                        else
                            response.Message = "Đã có lỗi xảy ra. Tạm thời không thể xoá danh mục.";
                    }
                    else
                        response.Message = "Không tìm thấy danh mục được chọn.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public List<SelectListItem> GetListCategorySelectItem(string storeId)
        {
            var data = new List<SelectListItem>();
            try
            {
                using (var _db = new CfDb())
                {
                    data = _db.Categories.Where(o => o.StoreID == storeId && !o.IsDelete && o.IsActive)
                           .Select(x => new SelectListItem
                           {
                               Value = x.ID,
                               Text = x.Name,
                           }).ToList();
                }
            }
            catch (Exception ex) { };
            return data;
        }
    }
}