using System.Security.Claims;
using AutoMapper;
using dotnetRPG.Data;
using dotnetRPG.Dtos;
using dotnetRPG.Dtos.Skill;
using dotnetRPG.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetRPG.Services.CharacterServices
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
        public async Task<ServiceResponse<List<CharacterResponseDto>>> CreateCharacter(CharacterRequestDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            
            var character = _mapper.Map<Character>(newCharacter);

            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Characters.Add(character);

            await _context.SaveChangesAsync();

            var charactersDto = await _context.Characters
                                    .Include(c => c.Weapon)
                                    .Include(c => c.Skills)
                                    .Where(c => c.User!.Id == GetUserId()).Select(c => _mapper.Map<CharacterResponseDto>(c)).ToListAsync();
            
            serviceResponse.Data = charactersDto;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> DeleteCharacter(int id)
        {
    
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            try
            {
                var character = await _context.Characters
                                        .Include(c => c.Weapon)
                                        .Include(c => c.Skills)
                                        .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
                if (character == null)
                {
                    throw new Exception("Character not found");
                }
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                var charactersDto = await _context.Characters.Where(c => c.User!.Id == GetUserId()).Select(c => _mapper.Map<CharacterResponseDto>(c)).ToListAsync();
                serviceResponse.Data = charactersDto;
                return serviceResponse;
                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            
            var dbCharacters = await _context.Characters.Include(c => c.Weapon)
                                                        .Include(c => c.Skills)
                                                        .Where(c => c.User!.Id == GetUserId())
                                                        .ToListAsync();
            var charactersDto =  dbCharacters.Select(c =>_mapper.Map<CharacterResponseDto>(c)).ToList();
            serviceResponse.Data = charactersDto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var character = await _context.Characters
                                            .Include(c => c.Weapon)
                                            .Include(c => c.Skills)
                                            .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
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

        public async Task<ServiceResponse<List<CharacterResponseDto>>> UpdateCharacter(int id, CharacterRequestDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            try
            {
                var character = await _context.Characters
                .Include(c => c.User)
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id);
                if(character == null || character.User!.Id != GetUserId()){
                    throw new System.Exception("Character not found");
                }

                character.Name = updatedCharacter.Name;
                character.Class = updatedCharacter.Class;
                var characterDto = _mapper.Map<CharacterResponseDto>(character);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Characters.Where(c => c.User!.Id == GetUserId()).Select(c => _mapper.Map<CharacterResponseDto>(c)).ToListAsync();
                return serviceResponse;
            }
            catch (System.Exception ex)
            {
                
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
            

        }

        public async Task<ServiceResponse<CharacterResponseDto>> AddCharacterSkill(AddCharacterSkillDto addCharacterSkill)
        {
            var response = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var character = await _context.Characters
                                        .Include(c => c.Weapon)
                                        .Include(c => c.Skills)
                                        .FirstOrDefaultAsync(c => 
                                        c.Id == addCharacterSkill.CharacterId &&
                                        c.User!.Id == GetUserId());
                
                if(character is null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                var skill = await _context.skills.FirstOrDefaultAsync(s => s.Id == addCharacterSkill.SkillId);
                if(skill is null)
                {
                    response.Success = false;
                    response.Message = "Skill not found";
                    return response;
                }
        
                character.Skills.Add(skill);
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