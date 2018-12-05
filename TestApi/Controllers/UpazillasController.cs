using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestApi.Models;

namespace TestApi.Controllers {
    [Route ("api/upazillas")]
    [ApiController]
    public class UpazillasController : ControllerBase {
        private readonly DataContext _dataContext;
        public UpazillasController (DataContext dataContext) {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAllData () {
            try {
                var items = _dataContext.Upazillas.ToList ();
                return Ok (items); //200
            } catch (System.Exception) {
                return BadRequest (); //400
            }
        }

        [HttpGet ("{id}", Name = "GetData")]
        public IActionResult GetDataById (int id) {
            try {
                var data = _dataContext.Upazillas.FirstOrDefault (x => x.Id == id);
                return Ok (data); //200
            } catch (System.Exception) {

                return BadRequest (); //400
            }
        }

        [HttpPost]
        public IActionResult AddData (Upazilla upazilla) {
            try {
                if (upazilla == null) return NotFound (); //404
                _dataContext.Add (upazilla);
                _dataContext.SaveChanges ();
                return CreatedAtRoute ("GetData", new { id = upazilla.Id }, upazilla); //201
            } catch (System.Exception) {
                return BadRequest ();
            }
        }

        [HttpDelete ("{id}")]
        public IActionResult DeleteById (int id) {
            try {
                var data = _dataContext.Upazillas.FirstOrDefault (x => x.Id == id);
                if (data == null) return null;
                _dataContext.Upazillas.Remove (data);
                _dataContext.SaveChanges ();
                return Ok (); //200
            } catch (System.Exception) {
                return BadRequest (); // 400
            }
        }

        [HttpPut ("{id}")]
        public IActionResult UpdateData (int id, Upazilla upazilla) {
            try {
                if (id != upazilla.Id) return BadRequest ("Invalid Data"); // validation status 400
                var data = _dataContext.Upazillas.FirstOrDefault (x => x.Id == id);
                if (data == null) return NotFound (); // 404
                data.Name = upazilla.Name;
                _dataContext.Upazillas.Update (data);
                _dataContext.SaveChanges ();
                return NoContent (); // 204
            } catch (System.Exception) {
                return BadRequest ("Error occured"); //400
            }
        }
    }
}