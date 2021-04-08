using AlkemyChallenge.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlkemyChallenge.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            int Id_User = Type_User();
            List<ListStudent> Student= new List<ListStudent>();
            using (UniversityEntities DB = new UniversityEntities())
            {
                if (User.Identity.IsAuthenticated)
                {
                    Student = (from m in DB.Students
                               select new ListStudent
                               {
                                   Id = m.Id,
                                   First_Name = m.First_Name,
                                   Last_Name = m.Last_Name,
                                   DNI = m.DNI,
                                   File_Student = m.File_Student
                               }).ToList();

                    string Usuario = User.Identity.Name;
                    if (Usuario != "Administrador")
                    {
                        foreach (var dato in Student)
                        {
                            if (dato.Id == Id_User)
                            {
                                List<ListStudent> StudentId = new List<ListStudent>();
                                StudentId.Add(new ListStudent() { Id = Id_User, First_Name = dato.First_Name, Last_Name = dato.Last_Name, DNI = dato.DNI, File_Student = dato.File_Student });
                                return View(StudentId);
                            }
                        }
                    }
                }
            }
            return View(Student);
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