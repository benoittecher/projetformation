using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationFormation.Models;
using AutoMapper;
using System.Web.SessionState;

namespace WebApplicationFormation.Controllers
{
    [Authorize]
    public class SessionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sessions
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            return View(await db.Sessions.ToListAsync());
        }

        // GET: Sessions/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = await db.Sessions.FindAsync(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // GET: Sessions/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            List<Parcours> parcours = db.Parcours.ToList();
            ViewBag.IdParcours = new SelectList(parcours, "Id", "Designation");
            return View();
        }

        // POST: Sessions/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "Id,DateDebut,DateFin,Nom,NbPlacesTotal,IdParcours")] SessionVM sessionVm)
        {
            if (ModelState.IsValid)
            {
                MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<SessionVM, Session>());
                // 2 : créer un Mapper
                Mapper mapper = new Mapper(config);
                // 3 : mappage
                Session session = mapper.Map<Session>(sessionVm);
                Parcours parcours = db.Parcours.SingleOrDefault(x => x.Id == sessionVm.IdParcours);
                session.Parcours = parcours;
                db.Sessions.Add(session);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sessionVm);
        }

        // GET: Sessions/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = await db.Sessions.FindAsync(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            List<Parcours> parcours = db.Parcours.ToList();
            ViewBag.IdParcours = new SelectList(parcours, "Id", "Designation");
            return View(session);
        }

        // POST: Sessions/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DateDebut,DateFin,Nom,NbInscrits")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(session);
        }

        // GET: Sessions/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = await db.Sessions.FindAsync(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Session session = await db.Sessions.FindAsync(id);
            db.Sessions.Remove(session);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public int NbInscrits(int sessionId)
        {
            var inscrits = db.Stagiaires.Where(x => ((x.SessionSouhaitee.Id == sessionId) && (x.Statut.Equals("Inscription finalisée"))));
            return (inscrits != null) ? inscrits.Count() : 0;
           //return db.Stagiaires.Where(x => ((x.SessionSouhaitee.Equals(session)) && (x.Statut.Equals("Inscription finalisée")))).Count();
            
            
        }
        public int NbPlacesRestantes(int sessionId)
        {
            Session session = db.Sessions.FirstOrDefault(x => x.Id == sessionId);
            return session.NbPlacesTotal - NbInscrits(sessionId);
        }
        public async Task<ActionResult> PendingCandidaciesAsync(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = await db.Sessions.FindAsync(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            List<Stagiaire> _listePostulants = await db.Stagiaires.Where(x => (x.SessionSouhaitee.Id == id) && (x.Statut.Equals("Inscription en cours"))).ToListAsync();
            ViewBag.Session = session.Nom;

            return View(_listePostulants);
        }
    }
}
