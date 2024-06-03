using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs.Option;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class OptionController : ControllerBase
    {
        private readonly AppDbContext _context; 
        private readonly IMapper _mapper;

        public OptionController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var options = await _context.Options
                .Select(x=> _mapper.Map(x, new OptionGetDto()))
                .AsNoTracking()
                .ToListAsync();

            return Ok(options);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] OptionPutDto dto, int id)
        {
            var option = await _context.Options.FirstOrDefaultAsync(x => x.Id == id);
            if (option is null) return NotFound();

            _mapper.Map(dto, option);
            _context.SaveChanges();

            return Ok(option.Id);
        }
    }
}
