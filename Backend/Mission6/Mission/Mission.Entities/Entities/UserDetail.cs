using Mission.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission.Entity.Entities
{
    [Table("UserDetail")] // Specify the table name
    public class UserDetail : BaseEntity // Assuming BaseEntity defines common properties
    {
        [Key]
        public int Id { get; set; }

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

        public string Availability { get; set; }

        public string LinkedInUrl { get; set; }

        public string MySkills { get; set; }

        public string UserImage { get; set; }

        public bool Status { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;
    }
}
