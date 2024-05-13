using System.ComponentModel.DataAnnotations.Schema;

namespace CrimeAdminAPI.Models
{
    public class Investigator
    {
        public int InvestigatorId { get; set; }
        public string InvestigatorName { get; set; }
        public string InvestigatorDescription { get; set; }
        public string InvestigatorUsername { get; set; }
        public string InvestigatorPassword { get; set; }
        public string InvestigatorConfirmPassword { get; set; }
        public string InvestigatorLocation { get; set; }
        public int AdminId { get; set; }  // Only store the AdminId       


    }

}
