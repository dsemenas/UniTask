using Microsoft.AspNetCore.Mvc;

namespace UniTask_backend.Controllers;
[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Labas, pasauli!";
    }
}