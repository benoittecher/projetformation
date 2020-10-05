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
            return View();
        }

        // POST: Stagiaires/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nom,Prenom,Mail,Adresse,Téléphone,DateInscription,Infos,Statut")] Stagiaire stagiaire)
        {
            if (ModelState.IsValid)
            {
                db.Stagiaires.Add(stagiaire);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(stagiaire);
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
