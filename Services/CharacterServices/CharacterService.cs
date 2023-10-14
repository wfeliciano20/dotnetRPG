using AutoMapper;
using dotnetRPG.Data;
using dotnetRPG.Dtos;
using dotnetRPG.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetRPG.Services.CharacterServices
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ServiceResponse<List<CharacterResponseDto>>> CreateCharacter(CharacterRequestDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            // use automapper to map from CharacterRequestDto to Character
            var character = _mapper.Map<Character>(newCharacter);

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            var charactersDto = await _context.Characters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToListAsync();
            serviceResponse.Data = charactersDto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> DeleteCharacter(int id)
        {
    
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (character == null)
                {
                    throw new Exception("Character not found");
                }
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                var characterDto = _mapper.Map<CharacterResponseDto>(character);
                serviceResponse.Data = characterDto;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> GetAllCharacters(int userId)
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            var dbCharacters = await _context.Characters.Where(c => c.User!.Id == userId).ToListAsync();
            var charactersDto =  dbCharacters.Select(c =>_mapper.Map<CharacterResponseDto>(c)).ToList();
            serviceResponse.Data = charactersDto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if(character == null){
                    throw new Exception("Character not found");

                }
                var characterDto = _mapper.Map<CharacterResponseDto>(character);
                serviceResponse.Data = characterDto;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<CharacterResponseDto>> UpdateCharacter(int id, CharacterRequestDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if(character == null){
                    throw new System.Exception("Character not found");

                }

                character.Name = updatedCharacter.Name;
                character.Class = updatedCharacter.Class;
                var characterDto = _mapper.Map<CharacterResponseDto>(character);
                await _context.SaveChangesAsync();
                serviceResponse.Data = characterDto;
                return serviceResponse;
            }
            catch (System.Exception ex)
            {
                
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
            

        }
    }
}