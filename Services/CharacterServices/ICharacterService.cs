using dotnetRPG.Dtos;
using dotnetRPG.Models;

namespace dotnetRPG.Services.CharacterServices
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<CharacterResponseDto>>> GetAllCharacters(int userId);
        Task<ServiceResponse<CharacterResponseDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<CharacterResponseDto>>> CreateCharacter(CharacterRequestDto newCharacter);
        Task<ServiceResponse<CharacterResponseDto>> UpdateCharacter(int id, CharacterRequestDto updatedCharacter);
        Task<ServiceResponse<CharacterResponseDto>> DeleteCharacter(int id);
    }
}