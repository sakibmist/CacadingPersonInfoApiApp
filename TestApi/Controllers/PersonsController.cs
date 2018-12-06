using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestApi.Dto;
using TestApi.Models;

namespace TestApi.Controllers {
    [Route ("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase {
        private readonly DataContext _dataContext;
        public PersonsController (DataContext dataContext) {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAllData () {
            try {
                var items = _dataContext.Persons.Select(x=>new PersonReturnDto{
                    Id= x.Id,
                    Name = x.Name,
                    Dob = x.Dob,
                    MobileNo = x.MobileNo,
                    DivisionName = x.Division.Name,
                    DistrictName = x.District.Name,
                    UpazillaName = x.Upazilla.Name,
                    VillageName = x.VillageName,
                    CreatedAt = x.CreatedAt

                }).ToList ();
                return Ok (items); //200
            } catch (System.Exception) {
                return BadRequest (); //400
            }
        }

        [HttpGet ("{id}", Name = "GetData")]
        public IActionResult GetDataById (int id) {
            try {
                var data = _dataContext.Persons.FirstOrDefault (x => x.Id == id);
                return Ok (data); //200
            } catch (System.Exception) {

                return BadRequest (); //400
            }
        }

        [HttpPost]
        public IActionResult AddData (Person person) {
            try {
                if (person == null) return NotFound (); //404
                _dataContext.Add (person);
                _dataContext.SaveChanges ();
                return CreatedAtRoute ("GetData", new { id = person.Id }, person); //201
            } catch (System.Exception) {
                return BadRequest ();
            }
        }

        [HttpGet ("check/{mobileNo}")]
        public IActionResult CheckIsAccountNoExists (string mobileNo) {
            try {
                var isExist = _dataContext.Persons.Any (x => x.MobileNo.ToLower () == mobileNo.ToLower ());
                return Ok (new { IsExist = isExist }); //200
            } catch (System.Exception) {

                return BadRequest (); //400
            }
        }

        [HttpDelete ("{id}")]
        public IActionResult DeleteById (int id) {
            try {
                var data = _dataContext.Persons.FirstOrDefault (x => x.Id == id);
                if (data == null) return null;
                _dataContext.Persons.Remove (data);
                _dataContext.SaveChanges ();
                return Ok (); //200
            } catch (System.Exception) {
                return BadRequest (); // 400
            }
        }

        [HttpPut ("{id}")]
        public IActionResult UpdateData (int id, Person person) {
            try {
                if (id != person.Id) return BadRequest ("Invalid Data"); // validation status 400
                var data = _dataContext.Persons.FirstOrDefault (x => x.Id == id);
                if (data == null) return NotFound (); // 404
                data.Name = person.Name;
                data.Dob = person.Dob;
                data.MobileNo = person.MobileNo;
                data.DivisionId = person.DivisionId;
                data.DistrictId = person.DistrictId;
                data.UpazillaId = person.UpazillaId;
                data.VillageName = person.VillageName;
                _dataContext.Persons.Update (data);
                return NoContent (); // 204
            } catch (System.Exception) {
                return BadRequest ("Error occured"); //400
            }
        }
    }
}