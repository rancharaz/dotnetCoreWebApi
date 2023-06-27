/* import context data where the model is  */
using Data;
using Microsoft.AspNetCore.Mvc;
using Models; /* data containing the users */
using Microsoft.EntityFrameworkCore;
namespace csharp_crud.Controllers;


[ApiController]
[Route("api/[controller]")] /* already hoist the route for the api  */
public class UsersController : ControllerBase
/* dependency injection */
{
    /* inport userContext */
    private readonly UserContext _context;

    /* design constructor */
    public UsersController(UserContext context)
    {
        _context = context;
    }

    /* get user api/users */

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    /* GET SINGLE USER by id */
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id); /* find my id */
        /* if user not found */
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }




    /* Post api/users */
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
    /* adding user  */
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    
        return CreatedAtAction(nameof(GetUser), new {id = user.Id}, user);
    }

    /* put api/users/5 */

    [HttpPut("{id}")]

    public async Task<IActionResult> PutUser(int id, User user){
        /* if id is not found into user id for change */
        if(id != user.Id){
            return BadRequest();
        }
    /* return content */
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    /* return no content */
        return NoContent();
    }

    /* delete api/users/5 */
    public async Task<IActionResult> DeleteUser(int id)
    {
        /* find the user id before delete */
        var user = await _context.Users.FindAsync(id);
        /* if not found */
        if(user == null){
            return NotFound();
        }
    /* remove */
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /* testing api */

  [HttpGet("test")]
  public string Test()
  {
    return "Hello World!";
  }


}

