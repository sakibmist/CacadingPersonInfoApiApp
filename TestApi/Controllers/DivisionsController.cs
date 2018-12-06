using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestApi.Models;

namespace TestApi.Controllers {
    [Route ("api/divisions")]
    [ApiController]
     
    public class DivisionsController : ControllerBase {
       
       private readonly DataContext _dataContext;
        public DivisionsController (DataContext dataContext) {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAllData () {
            try {
                var items = _dataContext.Divisions.ToList ();
                return Ok (items); //200
            } 
            catch (System.Exception)
             {
                return BadRequest (); //400
            }
        }

        
        [HttpGet ("{id}", Name = "GetDivision")]
        public IActionResult GetDataById (int id) {
            try {
                var data = _dataContext.Divisions.FirstOrDefault (x => x.Id == id);
                return Ok (data); //200
            } catch (System.Exception) {

                return BadRequest (); //400
            }
        } 


        
         [HttpPost]
        public IActionResult AddData (Division division) {
            try {
                if (division == null) return NotFound (); //404
                _dataContext.Add (division);
                _dataContext.SaveChanges ();
                return CreatedAtRoute ("GetDivision", new { id = division.Id }, division); //201
            } catch (System.Exception) {
                return BadRequest ();
            }
        }

    }
}