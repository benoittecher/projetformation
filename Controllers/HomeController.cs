using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationFormation.Models;

namespace WebApplicationFormation.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private SessionsController sc = new SessionsController();
        public ActionResult Index()
        {
            List<Session> _sessions = db.Sessions.ToList();
           
            List<Session> sessionsDisponibles = _sessions.FindAll(x => (x.DateDebut >= DateTime.Now) && (sc.NbPlacesRestantes(x.Id) > 0));
            IOrderedEnumerable<Session> _sessionsDisponiblesOrdonnee = sessionsDisponibles.OrderBy(x => x.DateDebut);
            IEnumerable<Session> troisProchainesSessions = _sessionsDisponiblesOrdonnee.Take(3);
            Session[] prochainesSessionsArray = troisProchainesSessions.ToArray();
            //_sessions.FindAll(x => (x.DateDebut >= DateTime.Now) && (x.NbInscrits < 20 ));
            //List<Session> troisProchainesSessions = _sessions.FindAll(x => (_sessions.IndexOf(x) <= 2));
            //List<Session> troisProchainesSessions = _sessions.FindAll(x => (_sessions.IndexOf(x) <= 2));
            ViewBag.prochainesSessionsArray = prochainesSessionsArray;
            ViewBag.SessionsController = sc;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult MentionsLegales()
        {
            ViewBag.Message = "Page mentions légales";

            return View();
        }
    }
}