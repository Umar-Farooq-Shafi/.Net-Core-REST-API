using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.DTOs;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;

namespace Commander.Controllers
{
    // api/commands
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController: ControllerBase
    {
        private readonly ICommanderRepo _repo;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commands = _repo.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        // api/commands/2
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var command = _repo.GetCommandById(id);
            if (command == null) return NotFound();

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        // api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var command = _mapper.Map<Command>(commandCreateDto);
            _repo.CreateCommand(command);
            
            if (!_repo.SaveChanges())
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
            return CreatedAtRoute(
                nameof(GetCommandById), 
                new {Id = command.Id}, 
                _mapper.Map<CommandReadDto>(command)
            );
        }

        // api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var comm = _repo.GetCommandById(id);
            if (comm == null) return NotFound();

            _mapper.Map(commandUpdateDto,  comm);
            _repo.UpdateCommand(comm);

            if (!_repo.SaveChanges())
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        // api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> jsonPatch)
        {
            var comm = _repo.GetCommandById(id);
            if (comm == null) return NotFound();

            var commandToPatch = _mapper.Map<CommandUpdateDto>(comm);
            jsonPatch.ApplyTo(commandToPatch, ModelState);
            if(!TryValidateModel(commandToPatch)) return ValidationProblem();

            _mapper.Map(commandToPatch,  comm);
            _repo.UpdateCommand(comm);

            if (!_repo.SaveChanges())
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        // api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var comm = _repo.GetCommandById(id);
            if (comm == null) return NotFound();

            _repo.DeleteCommand(comm);
            if (!_repo.SaveChanges())
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
    }
}