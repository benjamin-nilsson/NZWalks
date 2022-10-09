using System.Collections;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        [HttpGet()]
        public IActionResult GetAllRegions()
        {
            IEnumerable<Region> regions = _regionRepository.GetAll();

            // return DTO regions
            var regionsDTO = new List<Model.DTO.Region>();
            regions.ToList().ForEach(region =>
            {
                var regionDTO = new Model.DTO.Region()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    Area = region.Area,
                    Lat = region.Lat,
                    Long = region.Long,
                    Population = region.Population
                };

                regionsDTO.Add(regionDTO);
            });

            return Ok(regionsDTO);
        }

    }
}
