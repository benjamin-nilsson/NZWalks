using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;
using Region = NZWalks.API.Model.Domain.Region;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            IEnumerable<Region> regions = await _regionRepository.GetAllAsync();

            // return DTO regions
            // var regionsDTO = new List<Model.DTO.Region>();
            // regions.ToList().ForEach(region =>
            // {
            //     var regionDTO = new Model.DTO.Region()
            //     {
            //         Id = region.Id,
            //         Code = region.Code,
            //         Name = region.Name,
            //         Area = region.Area,
            //         Lat = region.Lat,
            //         Long = region.Long,
            //         Population = region.Population
            //     };
            //
            //     regionsDTO.Add(regionDTO);
            // });
            //

            List<Model.DTO.Region> regionsDTO = _mapper.Map<List<Model.DTO.Region>>(regions);

            return Ok(regionsDTO);

        }

        [HttpGet()]
        [Route("{id:guid}")]
        [ActionName("GetRegionByIdAsync")]
        public async Task<IActionResult> GetRegionByIdAsync(Guid id)
        {
            Region maybeRegion = await _regionRepository.GetRegionByIdAsync(id);
            if (maybeRegion == null)
            {
                return NotFound();
            }

            Model.DTO.Region regionDTO = _mapper.Map<Model.DTO.Region>(maybeRegion);

            return Ok(regionDTO);
        }

        [HttpPost()]
        public async Task<IActionResult> AddRegionAsync(Model.DTO.AddRegionRequest addRegionRequest)
        {
            // Request(DTO) to Domain model
            var region = new Model.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Area = addRegionRequest.Area,
                Population = addRegionRequest.Population,
                Name = addRegionRequest.Name
            };

            // Pass details to Repository
            region = await _regionRepository.AddRegionAsync(region);

            // Convert back to DTO
            var regionDTO = new Model.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            var actionName = nameof(GetRegionByIdAsync);
            string controllerName = "Regions";

            return CreatedAtAction(actionName, controllerName, new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {



            // Get region from database
            var region = await _regionRepository.DeleteRegionAsync(id);

            // If exception NotFound
            if (region == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var regionDTO = new Model.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            // Return Ok response
            return Ok(regionDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            // Convert DTO to domain model
            var region = new Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };

            // Update region using rep
            region = await _regionRepository.UpdateRegionAsync(id, region);
            // Handle if null
            if (region == null)
            {
                return NotFound();
            }

            // Covert back to DTO
            var regionDTO = new Model.DTO.Region()
            {
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            // Return Ok resp
            return Ok(regionDTO);
        }
    }
}
