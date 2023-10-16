using AutoMapper;
using dotnet_rpg.Dtos.Fight;
using dotnetRPG.Dtos;
using dotnetRPG.Dtos.Skill;
using dotnetRPG.Models;

namespace dotnetRPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, CharacterResponseDto>();
            CreateMap<CharacterRequestDto, Character>();
            CreateMap<Weapon,GetWeaponDto>();
            CreateMap<Skill,GetSkillDto>();
            CreateMap<Character, HighscoreDto>();
        }
    }
}