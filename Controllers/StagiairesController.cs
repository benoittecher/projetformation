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
using Microsoft.AspNet.Identity;

namespace WebApplicationFormation.Controllers
{
    public class StagiairesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stagiaires
        public async Task<ActionResult> Index()
        {
            return View(await db.Stagiaires.ToListAsync());
        }

        // GET: Stagiaires/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stagiaire stagiaire = await db.Stagiaires.FindAsync(id);
            if (stagiaire == null)
            {
                return HttpNotFound();
            }
            return View(stagiaire);
        }

        // GET: Stagiaires/Create
        public ActionResult Create()
        {
            StagiaireVM stagiaireVm = new StagiaireVM();
            stagiaireVm.Mail = User.Identity.GetUserName();
            List<Session> sessions = db.Sessions.ToList();
            ViewBag.IdSession = new SelectList(sessions, "Id", "Nom");
            if (string.IsNullOrEmpty(stagiaireVm.Mail))
            {
                return RedirectToAction("Register2", "Account", new { idCas = 1 });
            }
            return View(stagiaireVm);
        }

        // POST: Stagiaires/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Nom,Prenom,Mail,Adresse,Téléphone,IdSession")] StagiaireVM stagiaireVm)
        {
            if (ModelState.IsValid)
            {
                Stagiaire stagiaireAModifier = await db.Stagiaires.FirstOrDefaultAsync(x => x.Mail.Equals(stagiaireVm.Mail));
                 //MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<StagiaireVM, Stagiaire>());
       
                // 2 : créer un Mapper
                 //Mapper mapper = new Mapper(config);
               
                // 3 : mappage
   
                // Stagiaire stagiaire = mapper.Map<Stagiaire>(stagiaireVm);
                //stagiaireAModifier = mapper.Map<Stagiaire>(stagiaireVm);

                stagiaireAModifier.Nom = stagiaireVm.Nom;
                stagiaireAModifier.Prenom = stagiaireVm.Prenom;
                stagiaireAModifier.Téléphone = stagiaireVm.Téléphone;
                stagiaireAModifier.Adresse = stagiaireVm.Adresse;
                Session session = db.Sessions.SingleOrDefault(x => x.Id == stagiaireVm.IdSession);
                stagiaireAModifier.SessionSouhaitee = session;

                stagiaireAModifier.Statut = "Inscription en cours";
                //db.Stagiaires.Add(stagiaire);
                await db.SaveChangesAsync();
                if(true)
                return RedirectToAction("Index", "Home");
            }

            return View(stagiaireVm);
        }

        // GET: Stagiaires/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stagiaire stagiaire = await db.Stagiaires.FindAsync(id);
            if (stagiaire == null)
            {
                return HttpNotFound();
            }
            return View(stagiaire);
        }

        // POST: Stagiaires/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nom,Prenom,Mail,Adresse,Téléphone,DateInscription,Infos,Statut")] Stagiaire stagiaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stagiaire).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stagiaire);
        }

        // GET: Stagiaires/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stagiaire stagiaire = await db.Stagiaires.FindAsync(id);
            if (stagiaire == null)
            {
                return HttpNotFound();
            }
            return View(stagiaire);
        }

        // POST: Stagiaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Stagiaire stagiaire = await db.Stagiaires.FindAsync(id);
            db.Stagiaires.Remove(stagiaire);
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

    }
}
