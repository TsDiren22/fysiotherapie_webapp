using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using AvansFysioApp.Models;
    using AvansFysioAppDomain.Domain;
    using AvansFysioAppDomainServices.DomainServices;
    using AvansFysioAppInfrastructure.Seed;
    using Microsoft.AspNetCore.Identity;

namespace AvansFysioApp.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private IRepo repository;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,IRepo repository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.repository = repository;

            IdentityData.EnsurePopulated(userManager).Wait();
        }
        
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModel {ReturnUrl = returnUrl});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user =
                    await userManager.FindByNameAsync(loginModel.Username);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Home/Index");
                    }
                }

            }
            ModelState.AddModelError("", "Invalid username or password!");
            return View(loginModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var account = new IdentityUser
                {
                    UserName = registerModel.Username,
                    Email = registerModel.Email
                };
                
                Patient patient = repository.GetPatientByEmail(account.Email);
                IdentityResult result = null;
                if (patient != null)
                {
                    Debug.WriteLine(account.Email + " " + registerModel.Email);
                    result = await userManager.CreateAsync(account, registerModel.Password);
                }

                Debug.WriteLine(account.Email + " " + registerModel.Email);

                if (patient != null)
                {
                    await userManager.AddClaimAsync(account, new Claim("Patient", "true"));
                }


                if (result != null && result.Succeeded)
                {
                    var signIn = await signInManager.PasswordSignInAsync(account, registerModel.Password, false, false);
                    if (signIn.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                if (result == null)
                {
                    ModelState.AddModelError("", "This email does not exist in our system. Please contact your physiotherapist.");
                }
                else
                {
                    ModelState.AddModelError("", "Username and/or Email are already in use.");
                }
            }

            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}