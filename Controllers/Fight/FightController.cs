using dotnet_rpg.Dtos.Fight;
using dotnetRPG.Dtos.Fight;
using dotnetRPG.Models;
using dotnetRPG.Services.FightServices;
using Microsoft.AspNetCore.Mvc;

namespace dotnetRPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService _fightService;
        public FightController(IFightService fightService)
        {
            _fightService = fightService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<AttackResponseDto>>> WeaponAttack(WeaponAttackRequestDto request)
        {
            return Ok(await _fightService.WeaponAttack(request));
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<AttackResponseDto>>> SkillAttack(SkillAttackRequestDto request)
        {
            return Ok(await _fightService.SkillAttack(request));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FightResultDto>>> Fight(FightRequestDto request)
        {
            return Ok(await _fightService.Fight(request));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<HighscoreDto>>>> GetHighscore()
        {
            return Ok(await _fightService.GetHighscore());
        }

    }
}