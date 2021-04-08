using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlkemyChallenge;
using AlkemyChallenge.Models;
using AlkemyChallenge.Models.ViewModels;

namespace Alkemy.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index(string message = "")
        {
            List<ListTeacher> List;
            using (UniversityEntities DB = new UniversityEntities())
            {

                List = (from t in DB.Teachers
                        select new ListTeacher
                        {
                            Id = t.Id,
                            First_Name = t.First_Name,
                            Last_Name = t.Last_Name,
                            DNI = t.DNI,
                            File_Teacher = t.File_Teacher,
                            Active=t.Active
                        }).ToList();
            }
            ViewBag.Message = message;
            return View(List);
        }
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(DataTeacher NewT)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (UniversityEntities DB = new UniversityEntities())
                    {
                        var NTeacher = new Teachers();
                        NTeacher.First_Name = NewT.First_Name;
                        NTeacher.Last_Name = NewT.Last_Name;
                        NTeacher.DNI = NewT.DNI;
                        NTeacher.File_Teacher = NewT.File_Teacher;

                        DB.Teachers.Add(NTeacher);
                        DB.SaveChanges();
                    }
                    return Redirect("/Teacher");
                }
                return View(NewT);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public ActionResult Edit(int Id)
        {
            DataTeacher ETeachers = new DataTeacher();
            using (UniversityEntities DB = new UniversityEntities())
            {
                var ETeacher = DB.Teachers.Find(Id);
                ETeachers.First_Name = ETeacher.First_Name;
                ETeachers.Last_Name = ETeacher.Last_Name;
                ETeachers.DNI = ETeacher.DNI;
                ETeachers.File_Teacher = ETeacher.File_Teacher;
                ETeachers.Id = ETeacher.Id;
            }
            return View(ETeachers);
        }
        [HttpPost]
        public ActionResult Edit(DataTeacher ETeachers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (UniversityEntities DB = new UniversityEntities())
                    {
                        var ETeacher = DB.Teachers.Find(ETeachers.Id);
                        ETeacher.Id = ETeachers.Id;
                        ETeacher.First_Name = ETeachers.First_Name;
                        ETeacher.Last_Name = ETeachers.Last_Name;
                        ETeacher.DNI = ETeachers.DNI;
                        ETeacher.File_Teacher = ETeachers.File_Teacher;

                        DB.Entry(ETeacher).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                    }
                    return Redirect("/Teacher");
                }
                return View(ETeachers);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            using (UniversityEntities DB = new UniversityEntities())
            {
                var DTeacher = DB.Teachers.Find(Id);
                var IDuplicated = DB.Matters.ToList();
                foreach (var dato in IDuplicated)
                {
                    if (dato.Id_Teacher == Id)
                    {
                        return RedirectToAction("Index", new { message = "No se pudo eliminar ya que el Profesor tiene materias a su cargo" });
                    }
                }
                DB.Teachers.Remove(DTeacher);
                DB.SaveChanges();
            }
            return Redirect("/Teacher");
        }
        public ActionResult ViewTeacher(int id)
        {
            List<ListTeacher> List;
            using (UniversityEntities DB = new UniversityEntities())
            {
                List = (from t in DB.Teachers
                        select new ListTeacher
                        {
                            Id = t.Id,
                            First_Name = t.First_Name,
                            Last_Name = t.Last_Name,
                            DNI = t.DNI,
                            File_Teacher = t.File_Teacher,
                            Active = t.Active
                        }).ToList();

                foreach (var dato in List)
                {
                    if (dato.Id == id)
                    {
                        List<ListTeacher> TeacherId = new List<ListTeacher>();
                        TeacherId.Add(new ListTeacher() { Id = id, First_Name = dato.First_Name, Last_Name = dato.Last_Name, DNI = dato.DNI, File_Teacher = dato.File_Teacher });
                        return View(TeacherId);
                    }
                }
            }
            return View(List);
        }
           
        public ActionResult Active(int Id)
        {
            using (UniversityEntities DB = new UniversityEntities())
            {

                var DTeacher = DB.Teachers.Find(Id);
                if (!DTeacher.Active)
                {
                    DTeacher.Active = true;
                }
                else
                {
                    DTeacher.Active = false;
                }
                DB.Entry(DTeacher).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
            }
            return Redirect("/Teacher");
        }
    }
}