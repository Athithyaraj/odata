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
    public class TitlesController : ODataController
    {
        private readonly EmpContext _db;
        private readonly ILogger<TitlesController> _logger;
        public TitlesController(EmpContext dbContext, ILogger<TitlesController> logger)
        {
            _logger = logger;
            _db = dbContext;
        }

        //Get all Title        
        [EnableQuery]
        public IQueryable<Title> Get()
        {
            return _db.Titles;
        }

        //Get by Id
        [EnableQuery]
        public SingleResult<Title> Get([FromODataUri] int key)
        {
            var result = _db.Titles.Where(c => c.EmpNo == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Title student)
        {
            _db.Titles.Add(student);
            await _db.SaveChangesAsync();
            return Created(student);
        }

        //Patch
        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Title> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.Titles.FindAsync(key);
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
            Title existingNote = await _db.Titles.FindAsync(key);
            if (existingNote == null)
            {
                return NotFound();
            }

            _db.Titles.Remove(existingNote);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool NoteExists(int key)
        {
            return _db.Titles.Any(p => p.EmpNo == key);
        }

    }
}