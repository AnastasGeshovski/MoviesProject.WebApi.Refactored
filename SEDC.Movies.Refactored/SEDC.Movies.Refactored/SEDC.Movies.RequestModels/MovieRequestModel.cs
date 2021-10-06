using SEDC.Movies.RequestModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Movies.RequestModels
{
    public class MovieRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public Genre? Genre { get; set; }
        public int UserId { get; set; }
    }
}
