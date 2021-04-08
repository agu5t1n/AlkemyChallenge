using AlkemyChallenge.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlkemyChallenge.Controllers
{
    public class InscriptionController : Controller
    {
        // GET: Inscription
        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

            [HttpGet]
        public ActionResult Inscription(int Id)
        {
            int Id_User = Type_User();
            try
            {
                if (ModelState.IsValid)
                {
                    using (UniversityEntities DB = new UniversityEntities())
                    {
                        var Inscription = new Inscription();
                        var MatterQuota= DB.Matters.ToList();
                        var IDuplicated = DB.Inscription.ToList();
                        foreach (var dato in IDuplicated)
                        {
                            
                            if (dato.Id_Student == Id_User && dato.Id_Matter == Id)
                            {
                                return RedirectToAction("Index", new { message = "Ya se encuentra inscripto en la Materia " });
                            }
                        }
                        foreach (var Quota in MatterQuota)
                        {
                            if (Quota.Id==Id)
                            {
                                if (Quota.Quota<=0)
                                {
                                    return RedirectToAction("Index", new { message = "No Quedan cupos" });
                                }
                                Quota.Quota = Quota.Quota - 1;
                                DB.Entry(Quota).State = System.Data.Entity.EntityState.Modified;
                            }
                        }
                        Inscription.Id_Matter = Id;
                        Inscription.Id_Student = Id_User;
                        DB.Inscription.Add(Inscription);
                        DB.SaveChanges();
                        return RedirectToAction("Index", new { message = "Se inscribio correctamente a la materia" });
                    }
                }
                return View();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public int Type_User()
        {
            int IDUser = 1;
            UniversityEntities DB = new UniversityEntities();
            var Student = DB.Students.ToList();
            if (User.Identity.IsAuthenticated)
            {
                string UID = User.Identity.Name;
                foreach (var dato in Student)
                {
                    if (dato.File_Student == UID)
                    {
                        IDUser = dato.Id;
                    }
                }
            }
            return IDUser;
        }
    }
}