using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using AM.Infrastructure;
using AM.InfraStructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AM.UI.Web.Controllers
{
    public class PlaneController : Controller 
    {
        private readonly IServicePlane _planeService;

        //Injection par constructor
        public PlaneController(IServicePlane planeService)
        {
            _planeService = planeService;
        }

        // GET: PlaneController
        public ActionResult Index()
        {
            return View(_planeService.GetAll().ToList());
        }

        // GET: PlaneController/Details/5
        public ActionResult Details(int id)
        {
            var plane = _planeService.GetById(id);
            if (plane == null)
            {
                return NotFound();
            }
            return View(plane);
        }

        // GET: PlaneController/Create
        public ActionResult Create()
        {
            ViewBag.PlaneTypes = new SelectList(Enum.GetValues(typeof(PlaneType)));
            return View();
        }

        // POST: PlaneController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Plane plane)
        {
            try
            {
                _planeService.Add(plane);
                _planeService.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlaneController/Edit/5
        public ActionResult Edit(int id)
        {
            var plane = _planeService.GetById(id);
            if (plane == null)
            {
                return NotFound();
            }
            ViewBag.PlaneTypes = new SelectList(Enum.GetValues(typeof(PlaneType)), plane.PlaneType);
            return View(plane);
        }

        // POST: PlaneController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Plane plane)
        {
            try
            {
                if (id != plane.PlaneId)
                {
                    return NotFound();
                }

                _planeService.Update(plane);
                _planeService.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.PlaneTypes = new SelectList(Enum.GetValues(typeof(PlaneType)), plane.PlaneType);
                return View(plane);
            }
        }

        // GET: PlaneController/Delete/5
        public ActionResult Delete(int id)
        {
            var plane = _planeService.GetById(id);
            if (plane == null)
            {
                return NotFound();
            }
            return View(plane);
        }

        // POST: PlaneController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Plane plane)
        {
            try
            {
                var planeToDelete = _planeService.GetById(id);
                if (planeToDelete == null)
                {
                    return NotFound();
                }

                _planeService.Delete(planeToDelete);
                _planeService.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(plane);
            }
        }
    }
}
