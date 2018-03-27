using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using LibraryLite.UI.Web.MVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Net;
using System.Security.Claims;
using LibraryLite.UI.Web.MVC.Helpers;
using System.Threading;
using Microsoft.Owin.Security;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using LibraryLite.UI.Web.MVC.Presentation.ViewModels;


namespace LibraryLite.UI.Web.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }
        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();

            model = _userManager.Users.Select(u => new UserListViewModel
            {
                Id =u.Id,
                Name =u.UserName,
                Email =u.Email
            }).ToList();

            return View(model);
        }
        public ActionResult AddUser()
        {
            UserViewModel model = new UserViewModel();
            model.UserClaims = UserClaim.UserClaims.Select(c => new SelectListItem
            {
                Text = c,
                Value = c
            }).ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                var  userClaims = model.SelectedClaims.ToList();
                
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    foreach (var claimValue in userClaims)
                    {
                        var claim = new Claim(ClaimTypes.Role, claimValue);

                        await _userManager.AddClaimAsync(user.Id, claim);
                    }
                    await _userManager.UpdateAsync(user);
                    RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public async Task<ActionResult> EditUser(string id)
        {
            EditUserViewModel model = new EditUserViewModel();

            if (!string.IsNullOrWhiteSpace(id))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    model.Name=user.UserName;
                    model.Email = user.Email;

                    var claims = _context.UserClaim.ToList().Where(c => c.UserId == id);
  
                    model.UserClaims = UserClaim.UserClaims.Select(c => new SelectListItem
                    {
                        Text = c,
                        Value = c,
                        Selected = claims.Any(x => x.ClaimValue == c)
                    }).ToList();
                }
                else
                {
                    model.UserClaims = UserClaim.UserClaims.Select(c => new SelectListItem
                    {
                        Text = c,
                        Value = c
                    }).ToList();
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> EditUser(string id, EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if(user!=null)
                {
                    user.UserName= model.Name;
                    user.Email = model.Email;
                    var claims = _context.UserClaim.ToList().Where(c => c.UserId == id);

                    var newClaims = model.SelectedClaims.Select(c => new SelectListItem
                    {
                        Value = c,
                        Text = c,                    
                    });
                    //Add new claims to user
                    var claimsToAdd = model.SelectedClaims.Where(c=>!claims.Any(u=> u.ClaimValue == c));
                    foreach(var c in claimsToAdd)
                    {
                        var claim = new Claim(ClaimTypes.Role, c);
                        await _userManager.AddClaimAsync(user.Id, claim);
                    }

                    //Remove claims from user 
                    var claimsToRemove = claims.Where(c => !model.SelectedClaims.Any(u => u == c.ClaimValue));
                    foreach(var item in claimsToRemove)
                    {
                        var k =item as IdentityUserClaim;
                        _context.UserClaim.Remove(k);
                    }
                    await _context.SaveChangesAsync();

                    IdentityResult result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Admin");
                    }
                }
            }
            //if we got this far something must have happened
            return View(model);
        }
    }
}