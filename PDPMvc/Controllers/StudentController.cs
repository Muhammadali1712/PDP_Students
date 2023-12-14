using Microsoft.AspNetCore.Mvc;
using PDP_Students.Application;
using PDP_Students.Application.Servise;
using PDP_Students.Domain.Entities;
using PDP_Students.Infrasturucture.DataAcces;

namespace PDPMvc.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentServise _studentServise;
        private readonly IStudentCRUDServise _studentCRUDServise;
        private readonly PDP_StudentDbContext _studentDbContext;

        public StudentController(IStudentServise studentServise, IStudentCRUDServise studentCRUDServise, PDP_StudentDbContext studentDbContext)
        {
            _studentServise = studentServise;
            _studentCRUDServise = studentCRUDServise;
            _studentDbContext = studentDbContext;
        }

        // GET: StudentController
        [HttpGet]
        public async Task<ActionResult> Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var students = _studentDbContext.Students.AsQueryable().OrderBy(x=>x.Id);
            var pagedList = await PaginatedList<Student>.CreateAsync(students, pageNumber, pageSize);
            return View(pagedList);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                    _studentDbContext.Students.Add(student);
                    _studentDbContext.SaveChanges();

                    TempData["succesMassege"] = "Student created sucsesfully!!";
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Studentni Ma'lumotlarini to'ldiring";
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            Student? student = _studentDbContext.Students.SingleOrDefault(x => x.Id == id);
            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            try
            {
                _studentDbContext.Students.Update(student);
                _studentDbContext.SaveChanges();
                TempData["succesMassege"] = "Student Edit sucsesfully!!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["errorMessage"] = "Student not Edit! ERROR!!!";
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            Student? student = _studentDbContext.Students.SingleOrDefault(x => x.Id == id);
            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        public ActionResult Delete(Student student)
        {
            try
            {
                _studentDbContext.Students.Remove(student);
                _studentDbContext.SaveChanges();
                TempData["succesMassege"] = "Student Delete sucsesfully!!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["errorMessage"] = "Student not Delete! ERROR!!!";
                return View();
            }
        }
    }
}
