using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;
using Walk = NZWalks.API.Model.Domain.Walk;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WalkController : Controller
    {
        private IWalkRepository _walkRepository;

        private IMapper _mapper;

        public WalkController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> getAllWalks()
        {
            IEnumerable<Walk> walks = await _walkRepository.GetAllWalksAsync();

            List<Model.DTO.Walk> walksDTO = _mapper.Map<List<Model.DTO.Walk>>(walks);

            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkByIdAsync")]
        public async Task<IActionResult> GetWalkByIdAsync([FromRoute] Guid id)
        {
            // Get the walk domain model
            Walk walk = await _walkRepository.GetWalkByIdAsync(id);

            // Null check
            if (walk == null)
            {
                return NotFound();
            }

            // Transform the model to DTO model
            Model.DTO.Walk walkDTO = _mapper.Map<Model.DTO.Walk>(walk);

            // Return Ok resp
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(AddWalkRequest addWalkRequest)
        {
            // Transfer into request into a domain model
            Walk walk = new Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
                RegionID = addWalkRequest.RegionID,
            };

            // Add Walk to database
            walk = await _walkRepository.AddWalkAsync(walk);

            // Transfer into a DTO model
            Model.DTO.Walk walkDTO = new Model.DTO.Walk()
            {
                Id = walk.Id,
                WalkDifficultyId = walk.WalkDifficultyId,
                RegionID = walk.RegionID,
                Name = walk.Name,
                Length = walk.Length
            };


            // Return CreateAtAction
            String actionName = nameof(GetWalkByIdAsync);
            String controllerName = "Walk";

            return CreatedAtAction(actionName, controllerName, new { id = walkDTO.Id }, walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync([FromRoute] Guid id)
        {
            // Get the domain model
            Walk walk = await _walkRepository.DeleteWalkAsync(id);

            // Handle null
            if (walk == null)
            {
                return NotFound();
            }

            // Transfer domain model into DTO model
            Model.DTO.Walk walkDTO = new Model.DTO.Walk()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionID = walk.RegionID,
                WalkDifficultyId = walk.WalkDifficultyId,
            };

            // Return Ok resp
            return Ok(walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            // RequestDTO into Domain Model
            Walk walk = new Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionID = updateWalkRequest.RegionID,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
            };

            // Perform the update
            walk =  await _walkRepository.UpdateWalkAsync(id, walk);
            // Check for null
            if (walk == null)
            {
                return NotFound();
            }

            // Transfer into DTO model
            Model.DTO.Walk walkDTO = new Model.DTO.Walk()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionID = walk.RegionID,
                WalkDifficultyId = walk.WalkDifficultyId,
            };

            // Return Ok resp
            return Ok(walkDTO);
        }
    }
}
