using System.ComponentModel.DataAnnotations;
namespace BulkyBookWeb.Models
{
    public class MessMenuPrices
    {
      
        [Key]
        public int Id { get; set;}
        public bool FixedPrices { get; set; }
        public bool SetIndividualPrices { get; set;}

        public IndividualPrices IndividualPrices;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
