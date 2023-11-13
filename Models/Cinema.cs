using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "The Cinema Logo")]
        public string Logo { get; set; }
        [Display(Name = "The Cinema Name")]
        public string Name { get; set; }
        [Display(Name = "The Cinema Description")]
        public string Description { get; set; }


        //Relationships
        public List<Movie> Movies { get; set; }

    }
}
