﻿//using AppControle.Shared.DTO;
//using AppControle.Shared.DTO.EntitiesDTO;
//using AppControle.Shared.Entities;
//using Microsoft.JSInterop;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AppControle.DeskMob.Helpers
//{
//    public static class ConvertToDTO
//    {
//        public static ProductDTO ProductToDTO(Product product)
//        {
//            return new ProductDTO
//            {
//                Description = product.Description,
//                Id = product.Id,
//                Name = product.Name,
//                Price = product.Price,
//                Stock = product.Stock,
//                ProductCategoryIds = product.lProductCategories!.Select(x => x.CategoryId).ToList(),
//                ProductImages = product.lProductImages!.Select(x => x.Image).ToList()
//            };
//        }
//    }
//}