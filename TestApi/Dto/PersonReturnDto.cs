using System;

namespace TestApi.Dto
{
    public class PersonReturnDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public DateTime Dob { get; set; } 
        public string MobileNo { get; set; } 
        public string DivisionName { get; set; } 
        public string DistrictName { get; set; } 
        public string UpazillaName { get; set; }   
        public string VillageName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}