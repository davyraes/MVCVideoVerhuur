using System;
using MVCVideoVerhuur.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MVCVideoVerhuur.Tests.Models
{
    [TestClass]
    public class FilmTest
    {
        [TestMethod]
        public void KanTitelIngeven()
        {
            Film film = new Film();
            film.Titel = "tester";
            Assert.AreEqual("tester", film.Titel);
        }
        [TestMethod]
        public void KanFilmVerhurenAlsVooraadGroterAls0Is()
        {
            Film film = new Film();
            film.InVoorraad = 10;
            film.VerhuurFilm();
            Assert.AreEqual(9, film.InVoorraad);
        }
        [TestMethod,ExpectedException(typeof(Exception))]
        public void KangeenFilmVerhurenAlsVoorraadGelijkIsAan0()
        {
            Film film = new Film();
            film.InVoorraad = 0;
            film.VerhuurFilm();
        }
        [TestMethod, ExpectedException(typeof(Exception))]
        public void KangeenFilmVerhurenAlsVoorraadKleinerIsDan0()
        {
            Film film = new Film();
            film.InVoorraad = -5;
            film.VerhuurFilm();
        }
    }
}
