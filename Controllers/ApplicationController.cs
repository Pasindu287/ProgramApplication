using Microsoft.AspNetCore.Mvc;
using ProgramApplication.DTOs;
using ProgramApplication.Models;
using ProgramApplication.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramApplicationController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public ProgramApplicationController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProgramApplication([FromBody] ApplicationDTO applicationDTO)
        {
            var programApplication = new Application
            {
                Id = Guid.NewGuid().ToString(),
                ProgramId = applicationDTO.ProgramId,
                Questions = applicationDTO.Questions.Select(q => new Question
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = q.Type,
                    Text = q.Text,
                    Options = q.Options
                }).ToList()
            };

            await _cosmosDbService.AddItemAsync(programApplication);
            return CreatedAtAction(nameof(GetProgramApplication), new { id = programApplication.Id }, programApplication);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgramApplication(string id, [FromBody] ApplicationDTO applicationDTO)
        {
            var programApplication = new Application
            {
                Id = id,
                ProgramId = applicationDTO.ProgramId,
                Questions = applicationDTO.Questions.Select(q => new Question
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = q.Type,
                    Text = q.Text,
                    Options = q.Options
                }).ToList()
            };

            await _cosmosDbService.UpdateItemAsync(id, programApplication);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgramApplication(string id)
        {
            var programApplication = await _cosmosDbService.GetItemAsync(id);
            if (programApplication == null)
            {
                return NotFound();
            }
            return Ok(programApplication);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramApplication(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
            return NoContent();
        }



        [HttpPost("submit")]
        public async Task<IActionResult> SubmitApplication([FromBody] ApplicationResponseDTO responseDTO)
        {
            var applicationResponse = new ApplicationResponse
            {
                Id = Guid.NewGuid().ToString(),
                ProgramId = responseDTO.ProgramId,
                Answers = responseDTO.Answers
            };

            await _cosmosDbService.AddItemsAsync(applicationResponse);
            return CreatedAtAction(nameof(GetApplicationResponse), new { id = applicationResponse.Id }, applicationResponse);
        }

        [HttpGet("response/{id}")]
        public async Task<IActionResult> GetApplicationResponse(string id)
        {
            var applicationResponse = await _cosmosDbService.GetItemAsync(id);
            if (applicationResponse == null)
            {
                return NotFound();
            }
            return Ok(applicationResponse);
        }
    }
}
