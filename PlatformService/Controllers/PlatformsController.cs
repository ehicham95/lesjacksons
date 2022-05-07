using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using Models;
using PlatformService.SyncDataServices.Http;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandClient;

        // Use IMapper to auto mapp platform object to create or read
        public PlatformsController(IPlatformRepo repo, IMapper mapper, ICommandDataClient commandClient)
        {
            _repo = repo;
            _mapper = mapper;
            _commandClient = commandClient;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(_repo.GetAllPlatforms()));
        }
        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform = _repo.GetPlatformById(id);
            if (platform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> AddPlatform(PlatformCreateDto platformCreateDto)
        {
            var platform = _mapper.Map<Platform>(platformCreateDto);
            _repo.CreatePlatform(platform);
            _repo.SaveChanges();

            var platformRead = _mapper.Map<PlatformReadDto>(platform);

            try
            {
                await _commandClient.SendPlatformToCommand(platformRead);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not end synchronously: {ex.Message}");
            }

            // TODO: see createdatroute works
            return CreatedAtRoute(nameof(GetPlatformById), new { id = platformRead.Id }, platformRead);
        }
    }
}