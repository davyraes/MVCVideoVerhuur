using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVideoVerhuur.Models;
using MVCVideoVerhuur.Services;

namespace MVCVideoVerhuur.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string Naam = Request["naam"];
            string Postcode = Request["postcode"];
            int pc;
            if (Naam == null ||!(int.TryParse(Postcode,out pc)))
            {
                Session.Clear();
            }
            else
            {
                var klant= VideoVerhuurService.FindKlant(Naam,pc);
                if (klant != null)
                {
                    Session["klant"] = klant;
                }
                else
                {
                    ViewBag.fout = true;
                }
            }
            return View(); 
        }
        public ActionResult Verhuren(int? id)
        {
            if (Session["klant"]==null)
            {
                return RedirectToAction("Index");
            }
            if (id != null)
            {
                ViewBag.genre = (VideoVerhuurService.GetGenre((int)id)).Naam;
                ViewBag.genreid = id;
            }
            return View();
        }
        public PartialViewResult GenreLijst()
        {
            return PartialView(VideoVerhuurService.GetGenres());
        }
        public PartialViewResult FilmsPerGenre(int GenreId)
        {
            return PartialView(VideoVerhuurService.GetFilmsPerGenre(GenreId));
        }
        public ActionResult Huren(int? id)
        {
            if (Session["klant"] == null)
            {
                return RedirectToAction("Index");
            }
            List<Film> films = new List<Film>();
            if (Session["films"] != null)
            {
                films = (List<Film>)Session["films"];
            }                           
            if (id != null)
            {
                var film = VideoVerhuurService.GetFilm((int)id);
                if (!(films.Contains(film)))
                {
                    films.Add(film);
                }
            }
            Session["films"] = films;
            return View(films);
        }
        public ActionResult Verwijder(int id)
        {
            if (Session["klant"] == null)
            {
                return RedirectToAction("Index");
            }
            var film = VideoVerhuurService.GetFilm(id);
            ViewBag.filmid= film.BandNr;
            ViewBag.filmTitel= film.Titel;
            return View();
        }
        public ActionResult Verwijderen(int id)
        {
            if (Session["klant"] == null)
            {
                return RedirectToAction("Index");
            }
            List<Film> films = new List<Film>();
            if (Session["films"] != null)
            {
                films = (List<Film>)Session["films"];
                var film = films.FirstOrDefault(f => f.BandNr == id);
                films.Remove(film);
            }
            Session["films"] = films;
            return RedirectToAction("Huren");
        }
        public ActionResult Afrekenen()
        {
            if (Session["klant"] == null)
            {
                return RedirectToAction("Index");
            }
            List<Film> films = new List<Film>();
            Klant klant = (Klant)Session["klant"];
            if (Session["films"]!=null)
            {
                films = (List<Film>)Session["films"];
            }
            List<Verhuur> verhuringen = new List<Verhuur>();
            decimal Totaal = 0;
            foreach(var film in films)
            {
                Verhuur verhuur = new Verhuur();
                verhuur.BandNr = film.BandNr;
                verhuur.KlantNr = klant.KlantNr;
                verhuur.VerhuurDatum = DateTime.Now;
                Totaal +=film.Prijs;
                verhuringen.Add(verhuur);
            }
            List<Verhuur> mislukt = VideoVerhuurService.HuurFilms(verhuringen);
            if(mislukt.Count!=0)
            {
                foreach(var verhuur in mislukt)
                {
                    verhuringen.Remove(verhuur);
                    Totaal -= verhuur.Film.Prijs;
                }
            }
            AfrekenenViewModel vm = new AfrekenenViewModel();
            vm.Verhuringen = verhuringen;
            vm.Totaal = Totaal;
            vm.Klant = klant;
            Session.Clear(); 
            return View(vm);
        }
    }
}