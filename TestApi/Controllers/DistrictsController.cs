using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestApi.Dto;
using TestApi.Models;

namespace TestApi.Controllers {
    [Route ("api/districts")]
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


        [HttpGet ("{id}", Name = "GetDistrict")]
        public IActionResult GetDataById (int id) {
            try {
                var data = _dataContext.Districts.FirstOrDefault (x => x.Id == id);
                return Ok (data); //200
            } catch (System.Exception) {

                return BadRequest (); //400
            }
        } 

 //takes district data by divisionId
         [HttpGet ("id/{divisionId}")]
        public IActionResult GetDataByDivisionId (int divisionId) {
            try {
                var districts = _dataContext.Districts
                .Where(x=>x.DivisionId == divisionId)
                .Select(x=>new DistrictDto{
                    Name = x.Name,
                    Id =x.Id
                }).ToList();
                return Ok (districts); //200 
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
                return CreatedAtRoute ("GetDistrict", new { id = district.Id }, district); //201
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