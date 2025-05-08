using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace AM.UI.Web.Controllers
{
    public class FlightController : Controller
    {

        IFlightMethods Sp;
        IServicePlane Pl;

        public FlightController(IFlightMethods sp, IServicePlane pl)
        {
            Sp = sp;
            Pl = pl;
        }





        // GET: FlightController
        public ActionResult Index(DateTime? dateDepart)
        {
            if(dateDepart == null)
                return View(Sp.GetAll().ToList());
            else
                return View(Sp.GetMany(f => f.FlightDate.Date.Equals(dateDepart.Value.Date)).ToList());
        }

        // GET: FlightController/Details/5
        public ActionResult Details(int id)
        {
            var flight = Sp.GetById(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        // GET: FlightController/Create
        public ActionResult Create()
        {
            //afficher les capacity dans les relation entre les deuxtables (dans tables flight afficher liste des plane)
            ViewBag.Planeid=new SelectList(Pl.GetAll(), "PlaneId","Capacity");
            return View();
        }

        // POST: FlightController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Flight flight, IFormFile logoFile)
        {
            try
            {
                if (logoFile != null && logoFile.Length > 0)
                {
                    var fileName = Path.GetFileName(logoFile.FileName);
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
                    var relativePath = "/uploads/" + fileName;

                   
                    Directory.CreateDirectory(Path.GetDirectoryName(uploadPath));

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        logoFile.CopyTo(stream);
                    }

                    flight.AirlineLogo = relativePath;
                }

                Sp.Add(flight);
                Sp.Commit();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightController/Edit/5
        public ActionResult Edit(int id)
        {
            var flight = Sp.GetById(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewBag.Planeid = new SelectList(Pl.GetAll(), "PlaneId", "Capacity", flight.planefk);
            return View(flight);
        }

        // POST: FlightController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Flight flight, IFormFile logoFile)
        {
            try
            {
                if (id != flight.FlightId)
                {
                    return NotFound();
                }

                if (logoFile != null && logoFile.Length > 0)
                {
                    var fileName = Path.GetFileName(logoFile.FileName);
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
                    var relativePath = "/uploads/" + fileName;

                    Directory.CreateDirectory(Path.GetDirectoryName(uploadPath));

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        logoFile.CopyTo(stream);
                    }

                    flight.AirlineLogo = relativePath;
                }

                Sp.Update(flight);
                Sp.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Planeid = new SelectList(Pl.GetAll(), "PlaneId", "Capacity", flight.planefk);
                return View(flight);
            }
        }

        // GET: FlightController/Delete/5
        public ActionResult Delete(int id)
        {
            var flight = Sp.GetById(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        // POST: FlightController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Flight flight)
        {
            try
            {
                var flightToDelete = Sp.GetById(id);
                if (flightToDelete == null)
                {
                    return NotFound();
                }

                Sp.Delete(flightToDelete);
                Sp.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(flight);
            }
        }

        public ActionResult Sort()
        {
            return View("Index", Sp.GetAll().OrderByDescending(f => f.FlightDate).ToList());
        }
    }
}
