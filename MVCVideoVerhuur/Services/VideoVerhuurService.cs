using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVCVideoVerhuur.Models;

namespace MVCVideoVerhuur.Services
{
    public static class VideoVerhuurService
    {
        public static List<Klant> GetKlanten()
        {
            using (var db = new VideoVerhuurEntities())
            {
                return db.Klanten.ToList();
            }
        }
        public static Klant FindKlant(string naam,int Postcode)
        {
            using (var db = new VideoVerhuurEntities())
            {
                var klant = db.Klanten.FirstOrDefault(k => k.Naam == naam && k.PostCode == Postcode);
                return klant;
            }
        }
        public static List<Genre> GetGenres()
        {
            
            using (var db = new VideoVerhuurEntities())
            {
                    return db.Genres.ToList();
            }
        }
        public static Genre GetGenre(int id)
        {
            using (var db = new VideoVerhuurEntities())
            {
                return db.Genres.FirstOrDefault(g => g.GenreNr == id);
            }
        }
        public static List<Film> GetFilmsPerGenre(int genreId)
        {
            using (var db = new VideoVerhuurEntities())
            {
                var films = (from film in db.Films
                             where film.GenreNr == genreId
                             select film).ToList();
                return films;
            }
        }
        public static Film GetFilm(int id)
        {
            using (var db = new VideoVerhuurEntities())
            {
                return db.Films.FirstOrDefault(f => f.BandNr == id);       
            }
        }
        public static List<Verhuur> HuurFilms(List<Verhuur>verhuringen)
        {
            List<Verhuur> Mislukt = new List<Verhuur>();
            using (var db = new VideoVerhuurEntities())
            {
                foreach(var verhuur in verhuringen)
                {
                    var film=db.Films.FirstOrDefault(f => f.BandNr == verhuur.BandNr);
                    if(film.InVoorraad>0)
                    {
                        db.Verhuur.Add(verhuur);
                        film.VerhuurFilm();
                    }
                    else
                    {
                        Mislukt.Add(verhuur);
                    }

                }
                db.SaveChanges();
            }
            return Mislukt;
        }
    }
}