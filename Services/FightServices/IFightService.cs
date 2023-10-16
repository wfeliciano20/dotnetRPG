using dotnet_rpg.Dtos.Fight;
using dotnetRPG.Dtos.Fight;
using dotnetRPG.Models;

namespace dotnetRPG.Services.FightServices
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResponseDto>> WeaponAttack(WeaponAttackRequestDto request);
        Task<ServiceResponse<AttackResponseDto>> SkillAttack(SkillAttackRequestDto request);
        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request);

        Task<ServiceResponse<List<HighscoreDto>>> GetHighscore();
    }
}