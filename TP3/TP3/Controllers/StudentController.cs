using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP3.Models;
using TP3.Models.Repositories;

namespace TP3.Controllers
{
    public class StudentController : Controller
    {
        readonly IStudentRepository studentrepository;
        readonly ISchoolRepository schoolrepository;

        public StudentController(IStudentRepository studentrepository, ISchoolRepository schoolrepository)
        {
            this.studentrepository = studentrepository;
            this.schoolrepository = schoolrepository;
        }
        // GET: StudentController

        public ActionResult Index()
        {
            ViewBag.SchoolID = new SelectList(schoolrepository.GetAll(), "SchoolID", "SchoolName");
            return View(studentrepository.GetAll());
        }

        public ActionResult Search(string name, int? schoolid)
        {
            var result = studentrepository.GetAll();
            if (!string.IsNullOrEmpty(name))
                result = studentrepository.FindByName(name);
            else
            if (schoolid != null)
                result = studentrepository.GetStudentsBySchoolID(schoolid);
            ViewBag.SchoolID = new SelectList(schoolrepository.GetAll(), "SchoolID", "SchoolName");
            return View("Index", result);
        }


        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View(studentrepository.GetById(id));
        }

        // GET: StudentController/Create

        public ActionResult Create()
        {
           // ViewBag.SchoolID = new SelectList(schoolrepository.GetAll(), "SchoolID", "SchoolName");
            ViewBag.SchoolID = new SelectList(schoolrepository.GetAll(), "SchoolID", "SchoolName");
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Student s)
        {
            try
            {
                ViewBag.SchoolID = new SelectList(schoolrepository.GetAll(), "SchoolID", "SchoolName", s.SchoolID);
                studentrepository.Add(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.SchoolID = new SelectList(schoolrepository.GetAll(), "SchoolID", "SchoolName");
            return View(studentrepository.GetById(id));
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student s)
        {
            try
            {
                ViewBag.SchoolID = new SelectList(schoolrepository.GetAll(), "SchoolID", "SchoolName", s.SchoolID);
                studentrepository.Edit(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(studentrepository.GetById(id));
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Student s)
        {
            try
            {
                studentrepository.Delete(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
