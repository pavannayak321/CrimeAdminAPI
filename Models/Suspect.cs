using System.ComponentModel.DataAnnotations;

namespace CrimeAdminAPI.Models
{
    public class Suspect
    {
        [Key]
        public int SuspectID { get; set; }
        public string SuspectName { get; set; }
        public string SuspectAddress { get; set; }
        public string SuspectImageUrl { get; set; }

    }
}
