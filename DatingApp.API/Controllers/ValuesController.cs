using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetValues() 
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateValue(Value value)
        {
            await _context.Values.AddAsync(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetValue), new { id = value.Id }, value);
        }
    }
}
