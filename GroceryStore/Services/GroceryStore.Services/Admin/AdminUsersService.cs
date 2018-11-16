﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GroceryStore.Common.BindingModels.Admin.Users;
using GroceryStore.Common.ViewModels.Admin.Users;
using GroceryStore.Data;
using GroceryStore.Data.Models;
using GroceryStore.Services.Admin.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GroceryStore.Services.Admin
{
    public class AdminUsersService : BaseEFService,IAdminUsersService
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public AdminUsersService(GroceryStoreDbContext dbContext, 
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) 
            : base(dbContext, mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        
        public async Task<IEnumerable<UserIndexViewModel>> GetUsers(ClaimsPrincipal sessionUser)
        {
            var currentUser = await this.userManager.GetUserAsync(sessionUser);
            var users = this.DbContext.Users
                .Where(u => u.Id != currentUser.Id)
                .ToList();

            return this.Mapper.Map<IEnumerable<UserIndexViewModel>>(users);
        }

        public async Task<UserDetailsViewModel> GetUserDatails(string id)
        {
            var user = await this.DbContext.Users.FindAsync(id);
            this.CheckIfUserExist(user);

            var model= this.Mapper.Map<UserDetailsViewModel>(user);

            return model;
        }

        public async Task BanUser(string id)
        {
            var user = await this.DbContext.Users.FindAsync(id);
            this.CheckIfUserExist(user);
            user.LockoutEnd = DateTime.UtcNow.AddYears(100);

            this.DbContext.SaveChanges();
        }

        public async Task<IdentityResult> ChangeUserPassword(string id, ChangePasswordBindingModel model)
        {
            var user = await this.DbContext.Users.FindAsync(id);
            this.CheckIfUserExist(user);
            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.NewPassword);

            return await userManager.UpdateAsync(user);
        }

        public async Task DeleteUser(string id)
        {
            var user = await this.DbContext.Users.FindAsync(id);
            this.CheckIfUserExist(user);

            await this.userManager.DeleteAsync(user);

            this.DbContext.SaveChanges();
        }

        private void CheckIfUserExist(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException($"User with {user.Id} id not found!");
            }
        }
    }
}
