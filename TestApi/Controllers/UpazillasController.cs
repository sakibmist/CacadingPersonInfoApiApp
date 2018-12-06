using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestApi.Dto;
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

        [HttpGet ("{id}", Name = "GetUpazilla")]
        public IActionResult GetDataById (int id) {
            try {
                var data = _dataContext.Upazillas.FirstOrDefault (x => x.Id == id);
                return Ok (data); //200
            } catch (System.Exception) {

                return BadRequest (); //400
            }
        }


//takes district data by divisionId
         [HttpGet ("id/{districtId}")]
        public IActionResult GetDataByDistrictId (int districtId) {
            try {
                var upazillas = _dataContext.Upazillas
                .Where(x=>x.DistrictId == districtId)
                .Select(x=>new DistrictDto{
                    Name = x.Name,
                    Id =x.Id
                }).ToList();
                return Ok (upazillas); //200 
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
                return CreatedAtRoute ("GetUpazilla", new { id = upazilla.Id }, upazilla); //201
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