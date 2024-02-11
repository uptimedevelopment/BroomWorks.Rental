using Microsoft.AspNetCore.Mvc;

namespace BroomWorks.Rental.Web.Controllers;

public class HomeController : Controller
{
    public ViewResult Index()
    {
        return View();
    }
}
