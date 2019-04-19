using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAdvert.Models;

namespace WebAdvert.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly UserManager<CognitoUser> _userManager;
        private readonly CognitoUserPool _pool;
        public AccountController(SignInManager<CognitoUser> SignInManager, UserManager<CognitoUser> UserManager,
        CognitoUserPool userPool)
        {
            _signInManager = SignInManager;
            _userManager = UserManager;
            _pool = userPool;
        }
        public async Task<IActionResult> SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _pool.GetUser(model.Email);
                if (user.Status != null)
                {
                    ModelState.AddModelError("User exists", "User already exists");
                    return View(model);
                }
                user.Attributes.Add(CognitoAttribute.Name.ToString(), model.Email);
                var createdUser = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);

                if(createdUser.Succeeded){
                    return RedirectToAction("Confirm");
                }
            }
            return View();
        }

public async Task<IActionResult> Confirm(){
    return View();
}
          [HttpPost]
        public async Task<IActionResult> Confirm(ConfirmModel model)
        {
            
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
                if (user == null)
                {
                    ModelState.AddModelError("NotFound","A user with given email id was not found");
                    return View(model);
                }
                var result = await _userManager.ConfirmEmailAsync(user, model.Code).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                }
            }
            return View();
        }
    }

}