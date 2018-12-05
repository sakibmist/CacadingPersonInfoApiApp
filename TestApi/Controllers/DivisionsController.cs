using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestApi.Models;

namespace TestApi.Controllers {
    [Route ("Api/divisions")]
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
            } catch (System.Exception) {
                return BadRequest (); //400
            }
        }

        [HttpGet ("{id}", Name = "GetData")]
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
                return CreatedAtRoute ("GetData", new { id = division.Id }, division); //201
            } catch (System.Exception) {
                return BadRequest ();
            }
        }

        [HttpDelete ("{id}")]
        public IActionResult DeleteById (int id) {
            try {
                var data = _dataContext.Divisions.FirstOrDefault (x => x.Id == id);
                if (data == null) return null;
                _dataContext.Divisions.Remove (data);
                _dataContext.SaveChanges ();
                return Ok (); //200
            } catch (System.Exception) {
                return BadRequest (); // 400
            }
        }

        [HttpPut ("{id}")]
        public IActionResult UpdateData (int id, Division division) {
            try {
                if (id != division.Id) return BadRequest ("Invalid Data"); // validation status 400
                var data = _dataContext.Divisions.FirstOrDefault (x => x.Id == id);
                if (data == null) return NotFound (); // 404
                data.Name = division.Name;
                _dataContext.Divisions.Update (data);
                _dataContext.SaveChanges ();
                return NoContent (); // 204
            } catch (System.Exception) {
                return BadRequest ("Error occured"); //400
            }
        }
    }
}