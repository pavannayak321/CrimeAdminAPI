using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;


namespace CrimeAdminAPI.Models
{
    public class Crime
    {
        public int Id { get; set; }
        public string CrimeType { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
       
    }
}
