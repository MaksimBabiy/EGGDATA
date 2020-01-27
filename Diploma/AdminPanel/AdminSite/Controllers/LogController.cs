namespace AdminSite.Controllers
{
    using System.Threading.Tasks;
    using AdminPanelDataBaseCore.Interfaces;
    using AdminPanelInfrastructure.Interfaces;
    using AdminPanelInfrastructure.Models.AccountViewModels;
    using Microsoft.AspNetCore.Mvc;

    // [Route("api/Login")]
    public class LogController : Controller
    {
        private readonly ILogLogic logic;

        private readonly IAdminRepositoryDb repo;

        public LogController(ILogLogic logic, IAdminRepositoryDb repo)
        {
            this.logic = logic;
            this.repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this.repo.PasswordSignInAsync(model.Email, model.Password, model.RememberMe);
                if (result.Succeeded)
                {
                    return new OkObjectResult("User logged in.");
                }

                if (result.IsLockedOut)
                {
                    return new BadRequestObjectResult("Locked account.");
                }
                else
                {
                    return new BadRequestObjectResult(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return new BadRequestObjectResult("Failed");
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await this.logic.Logout();
            return new OkObjectResult("Admin logged out");
        }
    }
}
