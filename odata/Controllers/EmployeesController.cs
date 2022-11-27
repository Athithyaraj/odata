using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using odata.Models;

namespace odata.Controllers
{
    public class EmployeesController : ODataController
    {
        private readonly EmpContext _db;
        private readonly ILogger<EmployeesController> _logger;
        public EmployeesController(EmpContext dbContext, ILogger<EmployeesController> logger)
        {
            _logger = logger;
            _db = dbContext;
        }

        //Get all Employee        
        [EnableQuery]
        public IQueryable<Employee> Get()
        {
            return _db.Employees;
        }

        //Get by Id
        [EnableQuery]
        public SingleResult<Employee> Get([FromODataUri] int key)
        {
            var result = _db.Employees.Where(c => c.EmpNo == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Employee student)
        {
            _db.Employees.Add(student);
            await _db.SaveChangesAsync();
            return Created(student);
        }

        //Patch
        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Employee> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.Employees.FindAsync(key);
            if (existingNote == null)
            {
                return NotFound();
            }

            note.Patch(existingNote);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(existingNote);
        }
        //Delete
        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            Employee existingNote = await _db.Employees.FindAsync(key);
            if (existingNote == null)
            {
                return NotFound();
            }

            _db.Employees.Remove(existingNote);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool NoteExists(int key)
        {
            return _db.Employees.Any(p => p.EmpNo == key);
        }

    }
}