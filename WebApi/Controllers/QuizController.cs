using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs.Option;
using WebApi.DTOs.Question;
using WebApi.DTOs.Quiz;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public QuizController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var quizDto = await _context.Quizzes.Include(x=>x.Questions)!.ThenInclude(x=>x.Options).Select(x=> _mapper.Map(x, new QuizGetDto()))
                .AsNoTracking()
                .ToListAsync();

            return Ok(quizDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var quiz = await _context.Quizzes
                .Include(x=>x.Questions)!
                .ThenInclude(x=>x.Options)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (quiz is null) return NotFound();

            var dto = new QuizGetDto();

            _mapper.Map(quiz, dto);

            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] QuizPostDto dto)
        {
            if(!ModelState.IsValid) return BadRequest();

            var quiz = new Quiz();
            
            _mapper.Map(dto, quiz);

            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();


            return Ok(quiz.Id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, QuizPostDto dto)
        {
            var quiz = await _context.Quizzes.FirstOrDefaultAsync(x=> x.Id == id);

            if (quiz is null) return NotFound();

            _mapper.Map(dto, quiz);

            await _context.SaveChangesAsync();

            return Ok(quiz.Id);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var quiz = await _context.Quizzes.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);

            if (quiz is null) return NotFound();

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
