using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BroomWorks.Rental.Web.Controllers;

[Authorize]
public class ReservationController : Controller
{
    public ViewResult Index()
    {
        return View();
    }


}