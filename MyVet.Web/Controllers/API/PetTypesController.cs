using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyVet.Web.Data;
using MyVet.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyVet.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PetTypesController : ControllerBase
    {
        private readonly DataContext _datacontext;

        public PetTypesController(DataContext datacontext)
        {
            _datacontext = datacontext;
        }

        [HttpGet]
        public IEnumerable<PetType> GetPetTypes()
        {
            return _datacontext.PetTypes.OrderBy(pt => pt.Name);
        }
    }
}