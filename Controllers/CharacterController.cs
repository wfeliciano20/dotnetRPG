using dotnetRPG.Dtos;
using dotnetRPG.Models;
using dotnetRPG.Services.CharacterServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetRPG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<CharacterResponseDto>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CharacterResponseDto>>>> AddCharacter(CharacterRequestDto newCharacter)
        {
            return Ok( await _characterService.CreateCharacter(newCharacter));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> UpdateCharacter(int id, CharacterRequestDto updatedCharacter)
        {
            return Ok(await _characterService.UpdateCharacter(id, updatedCharacter));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> DeleteCharacter(int id)
        {
            return Ok(await _characterService.DeleteCharacter(id));
        }
    }
}