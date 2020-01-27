namespace AdminPanelInfrastructure.Interfaces
{
    using System.Threading.Tasks;
    using AdminPanelInfrastructure.Models.AccountViewModels;
    using Microsoft.AspNetCore.Identity;

    public interface ILogLogic
    {
        Task<SignInResult> Login(LoginViewModel model);

        Task Logout();
    }
}
