using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private static List<UserDTO> userList = new List<UserDTO>
                {
                  new UserDTO {Id=1,Name="Saqib Amin",Email="sa@gmail.com"},
                  new UserDTO {Id=2,Name="Waqar Amin",Email="wa@gmail.com" }
                };



        [HttpGet]
        public ActionResult<List<UserDTO>> Get() 
        {
            try 
            {                
                return userList;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return new List<UserDTO>();
            }
        }
        [HttpPost]
        public ActionResult Post(UserDTO user)
        {
            user.Id = new Random().Next(3, 1000);
            userList.Add(user);
            return Ok(user);
        }

    }


    


    public class UserDTO 
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
    }
}
