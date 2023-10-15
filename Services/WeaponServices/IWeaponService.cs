using dotnetRPG.Dtos;
using dotnetRPG.Models;

namespace dotnetRPG.Services.WeaponServices
{
    public interface IWeaponService
    {
        Task<ServiceResponse<CharacterResponseDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}