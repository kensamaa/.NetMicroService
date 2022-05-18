using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController:ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(
            IPlatformRepo repo,
            IMapper mapper,
            ICommandDataClient commandDataClient
            )
        {
            _repo = repo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }


        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> getting platforms...");
            var platformitem = _repo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformitem));
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> createPlatformAsync(PlatformCreatedto platformCreatedto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreatedto);

            _repo.CreatePlatform(platformModel);
            _repo.SaveChanges();
            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            try
            {
                await _commandDataClient.SendPlatformToComand(platformReadDto);
            }
            catch (Exception e)
            {
                Console.WriteLine("could not send synch " + e.Message);
            }

            return CreatedAtRoute(nameof(GetPlatformsByid), new { Id = platformReadDto.Id }, platformReadDto);
        }

        [HttpGet("{id}",Name = "GetPlatformsByid")]
        public ActionResult<PlatformReadDto> GetPlatformsByid(int id)
        {
            
            var platformitem = _repo.GetPlatformById(id);
            if (platformitem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformitem));
            }
            return NotFound();
            
        }
    }
}
