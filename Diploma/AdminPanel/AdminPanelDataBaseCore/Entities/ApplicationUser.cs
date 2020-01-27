namespace AdminPanelDataBaseCore.Entities
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets a list of all the refresh tokens issued for this users.
        /// </summary>
        public virtual List<Token> Tokens { get; set; }
    }
}