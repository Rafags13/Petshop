using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PetController: ControllerBase
    {
        private readonly IPetService _petService;
        public PetController(IPetService petService) 
        {
            _petService = petService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {   
                var pets = _petService.GetAll();

                if (pets.Count == 0)
                {
                    return Ok("Nenhum pet foi encontrado");
                }

                return Ok(pets);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var pet = _petService.GetById(id);

                if (pet == null)
                {
                    return NotFound("Não foi possível encontrar o pet buscado. Tente novamente mais tarde.");
                }

                return Ok(pet);

            } catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Pet pet) 
        {
            try
            {
                var success = await _petService.Add(pet);

                if (!success)
                {
                    return BadRequest("Não foi possível adicionar este pet. Tente novamente mais tarde.");
                }

                return Ok("Pet adicionado com sucesso");

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{idPet}")]
        public async Task<IActionResult> Update([FromBody] Pet pet, [FromRoute] int? idPet)
        {
            try 
            {
                if (idPet == 0 || idPet == null)
                {
                    return NotFound("O valor informado para a busca do pet não existe.");
                }

                var success = await _petService.Update(pet, idPet ?? 0);

                if (!success)
                {
                    return BadRequest("Não foi possível modificar o pet requisitado. Tente novamente mais tarde.");
                }

                return Ok("Pet alterado com sucesso!");

            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{idPet}")]
        public async Task<IActionResult> Remove([FromRoute] int? idPet)
        {
            try
            {
                if (idPet == 0 || idPet == null)
                {
                    return NotFound("O valor informado para a busca do pet não existe.");
                }

                var sucess = await _petService.DeleteById(idPet ?? 0);

                if (!sucess)
                {
                    return BadRequest("Não possível excluir o pet informado. Tente novamente mais tarde.");
                }

                return Ok("Pet excluido com sucesso");

            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }
    }
}
