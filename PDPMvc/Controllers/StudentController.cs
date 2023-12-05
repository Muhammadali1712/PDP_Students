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
			int pageSize = 2;
			int pageNumber = (page ?? 1);
			var students = _studentDbContext.Students.AsQueryable();
			var pagedList =await PaginatedList<Student>.CreateAsync(students, pageNumber, pageSize);
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
				TempData["Errormassage"] = "Catchga tushdi";
				return View();
			}
		}

		// GET: StudentController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: StudentController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
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
			return View();
		}

		// POST: StudentController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
