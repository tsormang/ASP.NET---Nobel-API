using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using NobelProjectAPI.Models;

namespace NobelProjectAPI.Controllers
{
    public class PrizeController : Controller
    {
        private NobelDBContext db = new NobelDBContext();

        // GET: Prize
        public async Task<ActionResult> Index()
        {
            return View(await db.Prizes.ToListAsync());
        }


        // GET: Prize/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prize prize = await db.Prizes.FindAsync(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        // GET: Prize/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prize/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Year,Category,FirstName,LastName")] Prize prize)
        {
            if (ModelState.IsValid)
            {
                db.Prizes.Add(prize);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(prize);
        }

        // GET: Prize/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prize prize = await db.Prizes.FindAsync(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        // POST: Prize/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Year,Category,FirstName,LastName")] Prize prize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prize).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(prize);
        }

        // GET: Prize/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prize prize = await db.Prizes.FindAsync(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        // POST: Prize/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Prize prize = await db.Prizes.FindAsync(id);
            db.Prizes.Remove(prize);
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
        
        [HttpPost]
        public async Task<ActionResult> GetPrizes()
        {

            return Json(await db.Prizes.ToListAsync(), JsonRequestBehavior.AllowGet);

        }
        
        [HttpPost]
        public async Task<ActionResult> SavePrizes(List<Prize> Prizes)
        {
            if (db.Prizes.Any())
            {

                foreach (Prize prize in db.Prizes)
                {
                    db.Prizes.Remove(prize);
                }

            }

            foreach (var item in Prizes)
            {
                if (ModelState.IsValid)
                {
                    db.Prizes.Add(item);
                    await db.SaveChangesAsync();

                }
            }
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public async Task<ActionResult> ClearPrizes()
        {

            if (db.Prizes.Any())
            {

                foreach (Prize prize in db.Prizes)
                {
                    db.Prizes.Remove(prize);
                }
                await db.SaveChangesAsync();

            }
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public JsonResult CountPrizes()
        {
            var signatures = db.Prizes.Count();
            return Json(signatures, JsonRequestBehavior.AllowGet);
        }
    }
}
