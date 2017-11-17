using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreTodoApp.Models;

namespace AspNetCoreTodoApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        private readonly TodoContext _context;

        public TodosController(TodoContext context)
        {
            _context = context;

            if (_context.Todos.Any()) return;

            _context.Todos.Add(new Todo {Name = "Todo1"});
            _context.SaveChanges();
        }

        // GET: api/todos
        [HttpGet]
        public IEnumerable<Todo> GetTodos()
        {
            return _context.Todos;
        }

        // GET: api/todos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var todo = await _context.Todos.SingleOrDefaultAsync(m => m.Id == id);

            if (todo == null) return NotFound();

            return Ok(todo);
        }
        
        // POST: api/todos
        [HttpPost]
        public async Task<IActionResult> PostTodo([FromBody] Todo todo)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new {id = todo.Id}, todo);
        }

        // PUT: api/todos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo([FromRoute] long id, [FromBody] Todo todo)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != todo.Id) return BadRequest();

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id)) return NotFound();

                throw;
            }

            return Ok(todo);
        }

        // DELETE: api/todos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var todo = await _context.Todos.SingleOrDefaultAsync(m => m.Id == id);

            if (todo == null) return NotFound();

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(long id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}