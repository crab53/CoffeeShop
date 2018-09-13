using CF.Business.Common;
using CF.Business.Core;
using CF.Data.Context;
using CF.Data.Entities;
using CF.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Inventory
{
    public class CFBusProduct
    {
        private static CFBusProduct instance;

        private CFBusProduct() { }

        public static CFBusProduct Instance
        {
            get
            {
                if (instance == null) instance = new CFBusProduct();
                return instance;
            }
        }

        public GetListProductResponse GetListProduct(GetListProductRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            GetListProductResponse response = new GetListProductResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    var query = _db.Products.Where(o => o.StoreID == input.StoreID && !o.IsDelete);

                    if (input.IsShowActive)
                        query = query.Where(o => o.IsActive);
                    if (Enum.IsDefined(typeof(Constants.EProductType), input.ProductType))
                        query = query.Where(o => o.ProductType == input.ProductType);

                    response.ListProduct = query.OrderBy(o => o.Name).Skip(input.PageIndex * input.PageSize).Take(input.PageSize)
                        .Join(_db.Categories, p => p.CategoryID, c => c.ID, (p, c) => new { p, c })
                        .Select(o => new ProductDTO()
                        {
                            ID = o.p.ID,
                            CategoryID = o.c.ID,
                            CategoryName = o.c.Name,
                            Name = o.p.Name,
                            Description = o.p.Description,
                            IsActive = o.p.IsActive,
                            ProductType = o.p.ProductType,
                            Price = o.p.Price,
                            ImageUrl = o.p.ImageUrl,
                        }).ToList();
                    response.Success = true;
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public GetProductInfoResponse GetProductInfo(GetProductInfoRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            GetProductInfoResponse response = new GetProductInfoResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    response.Product = _db.Products.Where(o => o.ID == input.ID && o.StoreID == input.StoreID && !o.IsDelete)
                        .Join(_db.Categories, p => p.CategoryID, c => c.ID, (p, c) => new { p, c })
                        .Select(o => new ProductDTO()
                        {
                            ID = o.p.ID,
                            CategoryID = o.c.ID,
                            CategoryName = o.c.Name,
                            Name = o.p.Name,
                            Description = o.p.Description,
                            IsActive = o.p.IsActive,
                            ProductType = o.p.ProductType,
                            Price = o.p.Price,
                            ImageUrl = o.p.ImageUrl,
                        }).FirstOrDefault();

                    if (response.Product != null)
                        response.Success = true;
                    else
                        response.Message = "Không tìm thấy sản phẩm này.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public CreateProductResponse CreateProduct(CreateProductRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            CreateProductResponse response = new CreateProductResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    if (input.Product != null)
                    {
                        var cate = _db.Categories.Where(o => o.StoreID == input.StoreID && !o.IsDelete && o.ID == input.Product.CategoryID).FirstOrDefault();
                        if (cate != null)
                        {
                            if (!string.IsNullOrEmpty(input.Product.Name))
                            {
                                string nameStr = CommonFunction.RemoveSign4VietnameseString(input.Product.Name);
                                var product = _db.Products.Where(o => o.NameStr == nameStr && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                if (product == null)
                                {
                                    product = new Product()
                                    {
                                        ID = Guid.NewGuid().ToString(),
                                        StoreID = input.StoreID,
                                        CategoryID = cate.ID,
                                        ImageUrl = input.Product.ImageUrl,
                                        Name = input.Product.Name,
                                        NameStr = nameStr,
                                        Price = input.Product.Price,
                                        Description = input.Product.Description,
                                        ProductType = input.Product.ProductType,
                                        IsActive = input.Product.IsActive,
                                        NumberOfOrder = 0,
                                        IsDelete = false,
                                    };
                                    _db.Products.Add(product);

                                    if (_db.SaveChanges() > 0)
                                        response.Success = true;
                                    else
                                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thêm mới sản phẩm.";
                                }
                                else
                                    response.Message = "Tên sản phẩm này đã tồn tại. Vui lòng chọn tên khác.";
                            }
                            else
                                response.Message = "Vui lòng nhập tên sản phẩm.";
                        }
                        else
                            response.Message = "Vui lòng chọn danh mục sản phẩm.";
                    }
                    else
                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thêm mới sản phẩm.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public UpdateProductResponse UpdateProduct(UpdateProductRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            UpdateProductResponse response = new UpdateProductResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    if (input.Product != null)
                    {
                        var cate = _db.Categories.Where(o => o.StoreID == input.StoreID && !o.IsDelete && o.ID == input.Product.CategoryID).FirstOrDefault();
                        if (cate != null)
                        {
                            if (!string.IsNullOrEmpty(input.Product.Name))
                            {
                                string nameStr = CommonFunction.RemoveSign4VietnameseString(input.Product.Name);
                                var product = _db.Products.Where(o => o.NameStr == nameStr && o.ID != input.Product.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                if (product == null)
                                {
                                    product = _db.Products.Where(o => o.ID == input.Product.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                                    if (product != null)
                                    {
                                        product.CategoryID = cate.ID;
                                        product.ImageUrl = input.Product.ImageUrl;
                                        product.Name = input.Product.Name;
                                        product.NameStr = nameStr;
                                        product.Price = input.Product.Price;
                                        product.Description = input.Product.Description;
                                        product.IsActive = input.Product.IsActive;

                                        if (_db.SaveChanges() > 0)
                                            response.Success = true;
                                        else
                                            response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thay đổi thông tin sản phẩm.";
                                    }
                                    else
                                        response.Message = "Không tìm thấy sản phẩm được chọn.";
                                }
                                else
                                    response.Message = "Tên sản phẩm này đã tồn tại. Vui lòng chọn tên khác.";
                            }
                            else
                                response.Message = "Vui lòng nhập tên sản phẩm.";
                        }
                        else
                            response.Message = "Vui lòng kiểm tra lại danh mục sản phẩm.";
                    }
                    else
                        response.Message = "Đã có lỗi xảy ra. Tạm thời không thể thay đổi thông tin sản phẩm.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }

        public DeleteProductResponse DeleteProduct(DeleteProductRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            Log.Logger.Info(methodName, input);
            DeleteProductResponse response = new DeleteProductResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    var product = _db.Products.Where(o => o.ID == input.ID && o.StoreID == input.StoreID && !o.IsDelete).FirstOrDefault();
                    if (product != null)
                    {
                        product.IsDelete = true;

                        if (_db.SaveChanges() > 0)
                            response.Success = true;
                        else
                            response.Message = "Đã có lỗi xảy ra. Tạm thời không thể xoá sản phẩm.";
                    }
                    else
                        response.Message = "Không tìm thấy sản phẩm được chọn.";
                }
            }
            catch (Exception ex) { Log.Logger.Error("Error" + methodName, ex); }
            Log.Logger.Info("Response" + methodName, response);
            return response;
        }
    }
}
