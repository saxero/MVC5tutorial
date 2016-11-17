using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public String Name { get; set; }
        [Required]
        public int MovieGenreId { get; set; }
        public MovieGenre  MovieGenre { get; set; } // aquesta propietat no ha de ser Required per evitar problemes al guardar
        [Required]
        public DateTime ReleaseDate { get; set; }
        
        public DateTime? DateAdded { get; set; } // DateTime? important per a que no hi hagoi excepcions guardant -- si no hi ha valor ell intenta posar un valor fora de rang
        [Required]
        public int NumberInStock { get; set; }
    }
}