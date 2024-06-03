using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs.Option;
using WebApi.DTOs.Question;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class QuestionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public QuestionController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var questions = await _context.Questions
                .Include(x=>x.Options)
                .Select(x => _mapper.Map(x, new QuestionGetDto()))
                .AsNoTracking()
                .ToListAsync();

            return Ok(questions);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] QuestionPutDto dto, int id)
        {
            var question = await _context.Questions.Include(x => x.Options).FirstOrDefaultAsync(x => x.Id == id);
            if (question is null) return NotFound();

            _mapper.Map(dto, question);
            _context.SaveChanges();

            return Ok(question.Id);
        }
    }
}
