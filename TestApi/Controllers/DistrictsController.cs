using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestApi.Models;

namespace TestApi.Controllers {
    [Route ("Api/districts")]
    [ApiController]
    public class DistrictsController : ControllerBase {
        private readonly DataContext _dataContext;
        public DistrictsController (DataContext dataContext) {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAllData () {
            try {
                var items = _dataContext.Districts.ToList ();
                return Ok (items); //200
            } catch (System.Exception) {
                return BadRequest (); //400
            }
        }

        [HttpGet ("{id}", Name = "GetData")]
        public IActionResult GetDataById (int id) {
            try {
                var data = _dataContext.Districts.FirstOrDefault (x => x.Id == id);
                return Ok (data); //200
            } catch (System.Exception) {

                return BadRequest (); //400
            }
        }

        [HttpPost]
        public IActionResult AddData (District district) {
            try {
                if (district == null) return NotFound (); //404
                _dataContext.Add (district);
                _dataContext.SaveChanges ();
                return CreatedAtRoute ("GetData", new { id = district.Id }, district); //201
            } catch (System.Exception) {
                return BadRequest ();
            }
        }

        [HttpDelete ("{id}")]
        public IActionResult DeleteById (int id) {
            try {
                var data = _dataContext.Districts.FirstOrDefault (x => x.Id == id);
                if (data == null) return null;
                _dataContext.Districts.Remove (data);
                _dataContext.SaveChanges ();
                return Ok (); //200
            } catch (System.Exception) {
                return BadRequest (); // 400
            }
        }

        [HttpPut ("{id}")]
        public IActionResult UpdateData (int id, District district) {
            try {
                if (id != district.Id) return BadRequest ("Invalid Data"); // validation status 400
                var data = _dataContext.Districts.FirstOrDefault (x => x.Id == id);
                if (data == null) return NotFound (); // 404
                data.Name = district.Name;
                _dataContext.Districts.Update (data);
                _dataContext.SaveChanges ();
                return NoContent (); // 204
            } catch (System.Exception) {
                return BadRequest ("Error occured"); //400
            }
        }
    }
}