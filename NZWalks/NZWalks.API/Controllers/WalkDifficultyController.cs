using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;

        private readonly IMapper _mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            _walkDifficultyRepository = walkDifficultyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetWalkDifficultiesAsync()
        {
            var walkDifficulties = await _walkDifficultyRepository.GetWalkDifficultiesAsync();

            var walkDifficultiesDTO = _mapper.Map<List<Model.DTO.WalkDifficulty>>(walkDifficulties);

            return Ok(walkDifficultiesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyByIdAsync")]
        public async Task<IActionResult> GetWalkDifficultyByIdAsync([FromRoute] Guid id)
        {
            var walkDifficulty = await _walkDifficultyRepository.GetWalkDifficultyByIdAsync(id);

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = new Model.DTO.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            Model.Domain.WalkDifficulty walkDifficulty = new Model.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code,
            };

            walkDifficulty = await _walkDifficultyRepository.AddWalkDifficultyAsync(walkDifficulty);

            Model.DTO.WalkDifficulty walkDifficultyDTO = new Model.DTO.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };

            String actionName = nameof(GetWalkDifficultyByIdAsync);
            String controllerName = "WalkDifficulty";

            return CreatedAtAction(actionName, controllerName, new {id = walkDifficultyDTO.Id}, walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync([FromRoute] Guid id)
        {
            Model.Domain.WalkDifficulty walkDifficulty = await _walkDifficultyRepository.DeleteWalkDifficultyAsync(id);

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            Model.DTO.WalkDifficulty walkDifficultyDTO = new Model.DTO.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };

            return Ok(walkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            Model.Domain.WalkDifficulty walkDifficulty = new Model.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code
            };

            walkDifficulty = await _walkDifficultyRepository.UpdateWalkDifficultyAsync(id, walkDifficulty);

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            Model.DTO.WalkDifficulty walkDifficultyDTO = new Model.DTO.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };

            return Ok(walkDifficultyDTO);
        }


    }
}
