using CF.Business.Core;
using CF.Data.Context;
using CF.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Inventory
{
    public class CFBusProduct : CFBusBase
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
            DebugLogInfo(methodName, input);
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
            catch (Exception ex) { DebugLogError(methodName, ex); }
            DebugLogInfo(methodName, response);
            return response;
        }

        public GetProductInfoResponse GetListProduct(GetProductInfoRequest input)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            DebugLogInfo(methodName, input);
            GetProductInfoResponse response = new GetProductInfoResponse();
            try
            {
                using (var _db = new CfDb())
                {
                    response.ProductInfo = _db.Products.Where(o => o.ID == input.ID && o.StoreID == input.StoreID && !o.IsDelete)
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

                    if (response.ProductInfo != null)
                        response.Success = true;
                    else
                        response.Message = "Không tìm thấy sản phẩm này.";
                }
            }
            catch (Exception ex) { DebugLogError(methodName, ex); }
            DebugLogInfo(methodName, response);
            return response;
        }
    }
}
