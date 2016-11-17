using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Movies
        public ActionResult Index()
        {
            //var movies = getMovies();
            var movies = _context.Movies.Include(c => c.MovieGenre).ToList();
            return View(movies);
        }

        public ActionResult Details(int? id){
            if (id == null)
                return HttpNotFound();

            //var movie = getMovies().SingleOrDefault(c => c.Id == id);
            var movie = _context.Movies.Include(c => c.MovieGenre).SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        public ActionResult New()
        {
            var moviesGenre = _context.MovieGenres.ToList();
            var viewmodel = new MovieViewModel 
            {
                MovieGenre= moviesGenre
            };
            return View("MoviesForm", viewmodel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var viewmodel = new MovieViewModel
            {
                Movie = movie,
                MovieGenre = _context.MovieGenres.ToList()
            };
            return View("MoviesForm", viewmodel);
        }

        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
                _context.Movies.Add(movie);
            else{
                var movieInDb = _context.Movies.Single(m=> m.Id == movie.Id);
                movieInDb.Name=movie.Name;
                movieInDb.ReleaseDate=movie.ReleaseDate;
                movieInDb.MovieGenreId=movie.MovieGenreId;
                movieInDb.NumberInStock=movie.NumberInStock;
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e);
            }
           
            return RedirectToAction("Index", "Movies");
        }

        List<Movie> getMovies()
        {
            return new List<Movie>{
                new Movie(){Id=1, Name="Shrek"},
                new Movie(){Id=2, Name="Wall-e"}
            };

            
        }
    }
}