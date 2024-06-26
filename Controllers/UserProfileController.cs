﻿using CuaHangCongNghe.Models;
using CuaHangCongNghe.Repository;
using CuaHangCongNghe.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Extensions;


namespace CuaHangCongNghe.Controllers
{
    [Authorize(Policy = "UserAccess")]
    public class UserProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly oderItemService  oderItemService;
        public UserProfileController(UserManager<ApplicationUser> userManager, oderItemService oderItemService)
        {
            this.userManager = userManager;
            this.oderItemService = oderItemService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.FindByIdAsync(userManager.GetUserId(User));
            var model = new UserViewModel { Id = user.Id, EmailUser = user.Email, NameUser = user.UserName, PhoneUser = user.PhoneNumber };
            return View(model);

          
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UserViewModel { Id = user.Id, EmailUser = user.Email, NameUser = user.UserName, PhoneUser = user.PhoneNumber };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.EmailUser;
                    user.UserName = model.NameUser;
                    user.PhoneNumber = model.PhoneUser;                   
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        result.AddErrorsTo(ModelState);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UserViewModel { Id = user.Id, EmailUser = user.Email, NameUser = user.UserName, PhoneUser = user.PhoneNumber };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var result = await userManager.ChangePasswordAsync(user, model.PasswordConfirm, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        result.AddErrorsTo(ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "vui lòng nhập thông tin");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> GetAllOrders()
        {
            var user = await userManager.FindByIdAsync(userManager.GetUserId(User));

            var Orders = new List<OrderViewModel>();
            if(user != null)
            {
                Orders = oderItemService.GetCurrentAllOrder(user.Id);
            }
           
            return View(Orders);
        }

        public async Task<ActionResult> GetItemOreder(int id)
        {
            return View(oderItemService.GetOrderItem(id));
        }
    }
}
