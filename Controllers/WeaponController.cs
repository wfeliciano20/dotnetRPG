using dotnetRPG.Dtos;
using dotnetRPG.Models;
using dotnetRPG.Services.WeaponServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetRPG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> AddWeapon(AddWeaponDto addWeaponDto){
            return Ok( await _weaponService.AddWeapon(addWeaponDto));
        }
    }
}