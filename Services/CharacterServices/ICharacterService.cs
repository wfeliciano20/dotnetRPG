using dotnetRPG.Dtos;
using dotnetRPG.Dtos.Skill;
using dotnetRPG.Models;

namespace dotnetRPG.Services.CharacterServices
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<CharacterResponseDto>>> GetAllCharacters();
        Task<ServiceResponse<CharacterResponseDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<CharacterResponseDto>>> CreateCharacter(CharacterRequestDto newCharacter);
        Task<ServiceResponse<List<CharacterResponseDto>>> UpdateCharacter(int id, CharacterRequestDto updatedCharacter);
        Task<ServiceResponse<List<CharacterResponseDto>>> DeleteCharacter(int id);

        Task<ServiceResponse<CharacterResponseDto>> AddCharacterSkill(AddCharacterSkillDto addCharacterSkill);
    }
}