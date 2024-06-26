﻿
using CuaHangCongNghe.Models;
using CuaHangCongNghe.Models.Shop;
using System.Collections.Generic;

namespace Shop.Models
{
    public static class MappingExtensions
    {

        public static List<OrderViewModel> ToOrdersViewModel(this List<Order> orders)
        {
            var items = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                var item = order.ToOrderViewModel();
                items.Add(item);
            }
            return items;
        }
        public static OrderViewModel ToOrderViewModel(this Order order)
        {
            return new OrderViewModel
            {
                Id = order.OrderId,
                OrderDate = order.OrderDate,
                Status = order.Status,
                UserId = order.UserId,
                ItemViewModels = order.Orderitems.ToList().ToOrderItemsViewModel()
            };
        }

        public static List<OrderItemViewModel> ToOrderItemsViewModel(this List<Orderitem> orderitems)
        {
            var items = new List<OrderItemViewModel>();
            foreach (var orderItem in orderitems)
            {
                var item = orderItem.ToOrderItemViewModel();
                items.Add(item);
            }
            return items;
        }

        public static OrderItemViewModel ToOrderItemViewModel(this Orderitem orderitem)
        {
            return new OrderItemViewModel
            {
                Id = orderitem.OrderItemsId,
                quantity = orderitem.Quantity,
                Product = orderitem.Product.ToProductViewModel()
            };
        }

        public static ProductViewModel ToProductViewModel(this Product product)
        {
            return new ProductViewModel()
            {
               Id  = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Stockquantity = product.Stockquantity
            };
        }

        public static Product ToProduct(this ProductViewModel productViewModel)
        {
            var path = productViewModel.ImageUrl;
            if (productViewModel.File != null)
            {
                path = "images/" + productViewModel.File.FileName;
            }
            return new Product()
            {
                Id = productViewModel.Id,
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                ImageUrl = path
            };
        }

        public static UserViewModel toUserViewModel(this User user)
        {
            return new UserViewModel
            {
                Id = user.UserId,
                PhoneUser = user.PhoneUser,
                AddressUser = user.AddressUser,
                NameUser = user.NameUser,
                EmailUser = user.EmailUser,

                RegistrationDate = user.RegistrationDate,
                Orders = user.Orders.ToList().ToOrdersViewModel()       
               
            };
        }

        public static User ToUserInfo(this UserViewModel userViewModel)
        {
            return new User
            {           
                PhoneUser = userViewModel.PhoneUser,
                AddressUser = userViewModel.AddressUser,
                NameUser = userViewModel.NameUser,
                EmailUser = userViewModel.EmailUser,
                        
                RegistrationDate = userViewModel.RegistrationDate,
             
              

            };
        }
    }
}
