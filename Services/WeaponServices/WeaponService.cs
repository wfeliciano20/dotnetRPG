using System.Security.Claims;
using AutoMapper;
using dotnetRPG.Data;
using dotnetRPG.Dtos;
using dotnetRPG.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetRPG.Services.WeaponServices
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        public WeaponService(DataContext context, IHttpContextAccessor httpContext, IMapper mapper)
        {
            _mapper = mapper;
            _httpContext = httpContext;
            _context = context;
        }

        private int GetUserId() => int.Parse(_httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);


        public async Task<ServiceResponse<CharacterResponseDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var response = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var character = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.User!.Id == GetUserId());

                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                var weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character
                };

                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<CharacterResponseDto>(character);
            }
            catch (Exception ex)
            {
                
                response.Success = false;
                response.Message = ex.Message;
                
            }

            return response;
        }
    }
}