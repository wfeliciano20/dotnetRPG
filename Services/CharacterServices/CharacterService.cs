using AutoMapper;
using dotnetRPG.Dtos;
using dotnetRPG.Models;

namespace dotnetRPG.Services.CharacterServices
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character{ Id=1,Name = "Sam"}
        };
        public Task<ServiceResponse<List<CharacterResponseDto>>> CreateCharacter(CharacterRequestDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            // use automapper to map from CharacterRequestDto to Character
            var character = _mapper.Map<Character>(newCharacter);

            characters.Add(character);
            var charactersDto = characters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            serviceResponse.Data = charactersDto;
            return Task.FromResult(serviceResponse);
        }

        public Task<ServiceResponse<CharacterResponseDto>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var character = characters.FirstOrDefault(c => c.Id == id);
                characters.Remove(character);
                var charactersDto = _mapper.Map<CharacterResponseDto>(character);
                serviceResponse.Data = charactersDto;
                return Task.FromResult(serviceResponse);
            }
            catch (System.Exception)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
                return Task.FromResult(serviceResponse);
            }
            
            
            
        }

        public Task<ServiceResponse<List<CharacterResponseDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            var charactersDto =  characters.Select(c =>_mapper.Map<CharacterResponseDto>(c)).ToList();
            serviceResponse.Data = charactersDto;
            return Task.FromResult(serviceResponse);
        }

        public Task<ServiceResponse<CharacterResponseDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var character = characters.FirstOrDefault(c => c.Id == id);
                var characterDto = _mapper.Map<CharacterResponseDto>(character);
                serviceResponse.Data = characterDto;
                return Task.FromResult(serviceResponse);
            }
            catch (System.Exception)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
                return Task.FromResult(serviceResponse);
            }
        }

        public Task<ServiceResponse<CharacterResponseDto>> UpdateCharacter(int id, CharacterRequestDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var character = characters.FirstOrDefault(c => c.Id == id);

                character.Name = updatedCharacter.Name;
                character.Class = updatedCharacter.Class;
                var characterDto = _mapper.Map<CharacterResponseDto>(character);
                serviceResponse.Data = characterDto;
                return Task.FromResult(serviceResponse);
            }
            catch (System.Exception)
            {
                
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
                return Task.FromResult(serviceResponse);
            }
            

        }
    }
}