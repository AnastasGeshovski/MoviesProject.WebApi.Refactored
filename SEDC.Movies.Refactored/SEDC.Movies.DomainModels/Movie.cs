using SEDC.Movies.RequestModels.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEDC.Movies.DomainModels
{
    public class Movie
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public Genre? Genre { get; set; }
        

        //navigation properties
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
