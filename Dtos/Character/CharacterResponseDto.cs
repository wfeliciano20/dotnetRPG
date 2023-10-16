using dotnetRPG.Dtos.Skill;
using dotnetRPG.Models;

namespace dotnetRPG.Dtos
{
    public class CharacterResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "Frodo";

        public int HitPoints { get; set; } = 100;

        public int Strength { get; set; } = 10;

        public int Defense { get; set; } = 10;

        public int Intelligence { get; set; } = 10;

        public GetWeaponDto? Weapon { get; set; }

        public RpgClass Class { get; set; } = RpgClass.Knight;

        public List<GetSkillDto>? Skills {get; set; }

        public int Fights { get; set; }

        public int Victories { get; set; }

        public int Defeats { get; set; }
    }
}