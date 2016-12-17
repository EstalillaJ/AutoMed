using System;

namespace AutoMed.Models.DataModels
{
    public class AutoMedUserDataModel
    {   
        public AutoMedUserDataModel(AutoMedUser principal)
        {
            this.Name = principal.Name;
            this.Id = principal.Id;
            this.Location = principal.Location;
            this.LocationId = principal.Location.Id;
            this.Role = principal.Role.ToString();
        }

        public AutoMedUserDataModel()
        {

        }
        public string Name { get; set; }
        public int Id { get; set;}
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }

        public AutoMedUser ToPrincipal()
        {
            return new AutoMedUser()
            {
                Name = Name,
                Location = Location,
                Role = (AutoMedUser.Roles)Enum.Parse(typeof(AutoMedUser.Roles), Role),
                Id = Id
            };
        }
    }
}