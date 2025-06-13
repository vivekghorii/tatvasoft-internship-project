using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Entities.Models
{
    public class AddUserDetailsRequestModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmployeeId { get; set; }

        public string Manager { get; set; }

        public string Title { get; set; }

        public string Department { get; set; }

        public string MyProfile { get; set; }

        public string WhyIVolunteer { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }
        
        public string Avilability { get; set; }

        public string LinkdInUrl { get; set; }

        public string MySkills { get; set; }

        public string UserImage { get; set; }

        public bool Status { get; set; }
    }
}
