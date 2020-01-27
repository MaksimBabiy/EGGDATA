namespace Server.DataBaseCore.Test.Repository
{
    using Microsoft.AspNetCore.Identity;

    public class IdentityResultFixure : IdentityResult
    {
        public bool Succeeded { get; set; }

        //public void SetProperty()
        //{
        //    IdentityResult.Succeeded = this.Succeeded;
        //}
    }

}
