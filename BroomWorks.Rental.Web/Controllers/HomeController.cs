using Microsoft.AspNetCore.Mvc;

namespace BroomWorks.Rental.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public ViewResult Index()
    {
        return View();
    }

    [HttpGet("log")]
    public void Log()
    {
        _logger.LogTrace("trace");
        _logger.LogInformation("information");
        _logger.LogWarning("warning");
        _logger.LogError("error");
    }
}
