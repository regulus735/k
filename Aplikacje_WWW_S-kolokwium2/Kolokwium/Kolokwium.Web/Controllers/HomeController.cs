using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoMapper;
using Microsoft.Extensions.Localization;
using Kolokwium.ViewModel.VM;

namespace Kolokwium.Web.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(ILogger logger, IMapper mapper, IStringLocalizer localizer) 
            : base(logger, mapper, localizer)
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpGet]
[Authorize (Roles = "Admin")]
public IActionResult AddOrEditSubject (int? id = null) {
var teachersVm = _teacherService.GetTeachers ();
ViewBag.TeachersSelectList = new SelectList (teachersVm.Select (t => new {
Text = $"{t.FirstName} {t.LastName}",
Value = t.Id
}), "Value", "Text");
if (id.HasValue) {
var subjectVm = _subjectService.GetSubject (x => x.Id == id);
ViewBag.ActionType = "Edit";
return View (Mapper.Map<AddOrUpdateSubjectVm> (subjectVm));
}
ViewBag.ActionType = "Add";
return View ();
}
public IActionResult Details (int id) {
var subjectVm = _subjectService.GetSubject (x => x.Id == id);
return View (subjectVm);
}
[HttpPost]
[ValidateAntiForgeryToken]
[Authorize (Roles = "Admin")]
public IActionResult AddOrEditSubject (AddOrUpdateSubjectVm addOrUpdateSubjectVm) {
if (ModelState.IsValid) {
_subjectService.AddOrUpdateSubject (addOrUpdateSubjectVm);
return RedirectToAction ("Index");
}
return View ();
}

<select asp-for="TeacherId" class="form-control"
asp-items="@ViewBag.TeachersSelectList"></select>

        
    }
}
