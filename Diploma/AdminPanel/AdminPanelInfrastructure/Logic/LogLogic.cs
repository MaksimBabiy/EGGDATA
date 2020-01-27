namespace AdminPanelInfrastructure.Logic
{
    using System.Threading.Tasks;
    using AdminPanelDataBaseCore.Interfaces;
    using AdminPanelInfrastructure.Interfaces;
    using AdminPanelInfrastructure.Models.AccountViewModels;
    using Microsoft.AspNetCore.Identity;

    public class LogLogic : ILogLogic
    {
        private readonly IAdminRepositoryDb repo;

        public LogLogic(IAdminRepositoryDb repo)
        {
            this.repo = repo;
        }

        public Task<SignInResult> Login(LoginViewModel model)
        {
            throw new System.NotImplementedException();
        }

        //public async Task<SignInResult> Login(LoginViewModel model)
        //{
        //    return await this.repo.Login(model);
        //}

        public async Task Logout()
        {
            await this.repo.LogOut();
        }
    }
}
