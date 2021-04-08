using AlkemyChallenge;
using AlkemyChallenge.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Alkemy.Controllers
{
    [Authorize]
    public class MatterController : Controller
    {
        // GET: Matters
        public ActionResult Index(string message = "")
        {
            List<ListMatters> List = new List<ListMatters>();
            List<ListMatters> ListOrder = new List<ListMatters>();
            bool List0 = false;
            using (UniversityEntities DB = new UniversityEntities())
            {
                var Inscriptioncount = DB.Inscription.ToList();
                List = (from m in DB.Matters
                        select new ListMatters
                        {
                            Id = m.Id,
                            Name_Matter = m.Name_Matter,
                            Schedule = m.Schedule,
                            Teacher = m.Id_Teacher,
                            Quota = m.Quota
                        }).ToList();
            }
            foreach (var ListC in List)
            {
                if (ListC.Quota != 0)
                {
                    List0 = true;
                    ListOrder.Add(new ListMatters() { Id = ListC.Id, Name_Matter = ListC.Name_Matter, Schedule = ListC.Schedule, Teacher = ListC.Teacher, Quota = ListC.Quota });
                }
            }
            string Usuario = User.Identity.Name;
            if (Usuario == "Administrador")
            {
                ListOrder = List.OrderBy(x => x.Name_Matter).ToList();
            }
            else if (List0)
            {
                ListOrder = ListOrder.OrderBy(x => x.Name_Matter).ToList();
            }
            else
            {
                ListOrder = List.OrderBy(x => x.Name_Matter).ToList();
            }
            ViewBag.Message = message;
            return View(ListOrder);
        }


        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(DataMatters EMatters)
        {
            bool Teacherexist = false;
            try
            {
                if (ModelState.IsValid)
                {
                    using (UniversityEntities DB = new UniversityEntities())
                    {
                        var EMatter = new Matters();
                        var Teachers = DB.Teachers.ToList();
                        EMatter.Name_Matter = EMatters.Name_Matter;
                        EMatter.Schedule = EMatters.Schedule;
                        foreach (var TeacherFind in Teachers)
                        {
                            if (TeacherFind.Id == EMatters.Id_Teacher)
                            {
                                EMatter.Id_Teacher = EMatters.Id_Teacher;
                                Teacherexist = true;
                            }
                        }
                        if (!Teacherexist)
                        {
                            return RedirectToAction("Index", new { message = "El Id del profesor no existe" });
                        }
                        if (EMatters.Quota<=0)
                        {
                            return RedirectToAction("Index", new { message = "El cupo debe ser mayor a 0" });
                        }
                        EMatter.Quota = EMatters.Quota;

                        DB.Matters.Add(EMatter);
                        DB.SaveChanges();
                    }
                    return Redirect("/Matter");
                }
                return View(EMatters);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }



        public ActionResult Edit(int Id)
        {
            DataMatters EMatters = new DataMatters();
            using (UniversityEntities DB = new UniversityEntities())
            {
                var EMatter= DB.Matters.Find(Id);
                EMatters.Name_Matter = EMatter.Name_Matter;
                EMatters.Schedule = EMatter.Schedule;
                EMatters.Id_Teacher = EMatter.Id_Teacher;
                EMatters.Quota = EMatter.Quota;
                EMatters.Id = EMatter.Id;
            }
            return View(EMatters);
        }
        [HttpPost]
        public ActionResult Edit(DataMatters EMatters)
        {
            bool Teacherexist= false;
            try
            {
                if (ModelState.IsValid)
                {
                    using (UniversityEntities DB = new UniversityEntities())
                    {
                        var EMatter = DB.Matters.Find(EMatters.Id);
                        var Teachers = DB.Teachers.ToList();
                        EMatter.Name_Matter = EMatters.Name_Matter;
                        EMatter.Schedule = EMatters.Schedule;
                        foreach (var TeacherFind in Teachers)
                        {
                            if (TeacherFind.Id == EMatters.Id_Teacher)
                            {
                                EMatter.Id_Teacher = EMatters.Id_Teacher;
                                Teacherexist = true;
                            }
                        }
                        if (!Teacherexist)
                        {
                            return RedirectToAction("Index", new { message = "El Id del profesor no existe" });
                        }
                        
                        if (EMatters.Quota <= 0)
                        {
                            return RedirectToAction("Index", new { message = "El cupo debe ser mayor a 0" });
                        }
                        EMatter.Quota = EMatters.Quota;
                        EMatter.Id = EMatters.Id;

                        DB.Entry(EMatter).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                    }
                    return Redirect("/Matter");
                }
                return View(EMatters);
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

                var DMatter = DB.Matters.Find(Id);
                var DInscription = DB.Inscription.Find(Id);
                var Inscription = DB.Inscription.ToList();
                foreach (var dato in Inscription)
                {
                    if (dato.Id_Matter == Id)
                    {
                        DInscription = DB.Inscription.Find(dato.Id);
                        DB.Inscription.Remove(DInscription);
                    }
                }
                DB.Matters.Remove(DMatter);
                DB.SaveChanges();
            }
            return Redirect("/Matter");
        }
    }
}