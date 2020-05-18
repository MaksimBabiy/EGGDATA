namespace AdminSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using AspNet.Security.OAuth.Validation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Server.DataBaseCore.Entities;
    using Server.Infrastructure.Interfaces;
    using Server.Infrastructure.Models;


    [Authorize]
    [Route("api/[controller]")]
    public class AdminDoctorsController : Controller
    {

        private readonly IDoctorLogic doctorLogic;

        public AdminDoctorsController(IDoctorLogic doctorLogic)
        {
            
            this.doctorLogic = doctorLogic;
        }

        // POST: api/AdminDoctors/AddPatient
        // [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        // [Authorize(Roles = "SystemAdministrator")]
        [AllowAnonymous]
        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctorAsync([FromBody] DoctorViewModel doctorViewModel)
        {
            if (Equals(doctorViewModel, null))
            {
                return this.BadRequest("Форму повністю не заповнено");
            }

            try
            {
                doctorViewModel = await this.doctorLogic.AddDoctorAsync(doctorViewModel);
                if (Equals(doctorViewModel.DoctorId, 0))
                {
                    return this.Json("Доктор з цим e-mail вже існує!");
                }

                return this.Json("Доктора включено до системи!");
            }
            catch (Exception ex)
            {
                return this.Json(new string[] { "Exception", ex.Message });
            }
        }

        // PATCH: api/AdminDoctors/UpdateDoctor
        // [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        // [Authorize(Roles = "SystemAdministrator")]
        [AllowAnonymous]
        [HttpPatch("UpdateDoctor")]
        public async Task<IActionResult> UpdateDoctorAsync([FromBody] DoctorViewModel model)
        {
            try
            {
                await this.doctorLogic.UpdateDoctorAsync(model);
            }
            catch (Exception ex)
            {
                return this.Json(new string[] { "Exception", ex.Message });
            }

            return this.Json("Дані доктора успішно відредаговано!");
        }

        // Delete: api/AdminDoctors/DeleteDoctor/{doctorId}
        // [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        // [Authorize(Roles = "SystemAdministrator")]
        [AllowAnonymous]
        [HttpDelete("DeleteDoctor/{doctorId}")]
        public async Task<IActionResult> DeletePatientAsync(int doctorId)
        {
            try
            {
                await this.doctorLogic.DeleteDoctorAsync(doctorId);
                return this.Json("Доктора видалено з системи.");
            }
            catch (Exception ex)
            {
                return this.Json(new string[] { "Exception", ex.Message });
            }
        }

        // [Authorize(Roles = "SystemAdministrator")]
        [AllowAnonymous]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            return this.Json(await this.doctorLogic.GetDoctorsListAsync());
        }
    }
}
