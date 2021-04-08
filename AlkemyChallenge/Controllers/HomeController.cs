using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AlkemyChallenge.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string DniI, string File)
        {
            int Dni =0;
            bool DniC = int.TryParse(DniI, out Dni);
            if (DniC)
            {
                UniversityEntities DB = new UniversityEntities();
                var UStudent = DB.Students.FirstOrDefault(e => e.DNI == Dni && e.File_Student == File);
                var UTeacher = DB.Teachers.FirstOrDefault(t => t.DNI == Dni && t.File_Teacher == File);
                
                if (UStudent == null && UTeacher != null)
                {
                    if (UTeacher.Active == false)
                    {
                        return RedirectToAction("Index", new { message = "Lo sentimos, el Profesor no se encuentra Activo" });
                    }
                    FormsAuthentication.SetAuthCookie("Administrador", true);
                    return Redirect("/Teacher/Index");
                }
                else if (UStudent != null && UTeacher == null)
                {
                    FormsAuthentication.SetAuthCookie(UStudent.File_Student, true);
                    return Redirect("/Student/Index");
                }
            }
            return RedirectToAction("Index", new { message = "Lo sentimos, no se encontro ningun Usuario con los datos ingresados" });
        }
        
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

    }
}