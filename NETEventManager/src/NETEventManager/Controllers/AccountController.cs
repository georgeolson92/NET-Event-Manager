using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EventManager.Models;
using System.Threading.Tasks;
using EventManager.ViewModels;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Controllers
{

    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var userEvents = _db.Events.Where(x => x.User.Id == currentUser.Id).ToList();
            var eventRSVPs = _db.RSVPs.Include(rsvps => rsvps.Event).Where(rsvps => rsvps.User.Id == userId).ToList();
            AccountViewModel viewAccount = new AccountViewModel();
            viewAccount.eventRSVPs = eventRSVPs;
            viewAccount.userEvents = userEvents;
            viewAccount.currentUser = currentUser;
            return View(viewAccount);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email };
            var avatar = Request.Form["avatarURL"];
            if (avatar != "")
            {
                user.AvatarURL = avatar;
            }
            else
            {
                user.AvatarURL = "http://www.adweek.com/socialtimes/files/2012/03/twitter-egg-icon.jpg";
            }
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (signInResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}