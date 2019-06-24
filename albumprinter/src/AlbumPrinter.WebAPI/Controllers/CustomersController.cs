using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlbumPrinter.WebAPI.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersContext _context;

        public CustomersController(CustomersContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Task<List<Customer>> Get()
        {
            return _context.Customers.ToListAsync();
        }

        [HttpGet("{id}")]
        public Task<Customer> Get(Guid id)
        {
            // TODO: Return 404 when not found.
            return _context.Customers
                .Include(c => c.Orders)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        [HttpPost("{id}/orders")]
        public async Task Post(Guid id, [FromBody] Order value)
        {
            // TODO: Return 404 when not found and 400 when value invalid.
            value.CustomerID = id;
            await _context.Orders.AddAsync(value);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task Post([FromBody] Customer value)
        {
            // TODO: Return 400 when value invalid.
            await _context.Customers.AddAsync(value);
            await _context.SaveChangesAsync();
        }
    }
}
