using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<UserDTO>> Get() 
        {
            try 
            {
                List<UserDTO> users = new List<UserDTO> 
                {
                  new UserDTO {Id=1,Name="Saqib Amin",Email="sa@gmail.com"},
                  new UserDTO {Id=2,Name="Waqar Amin",Email="wa@gmail.com" }
                };
                return users;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return new List<UserDTO>();
            }
        }
    }




    public class UserDTO 
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
    }
}
