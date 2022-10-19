using System.Net;
using Chat.Server;
using Chat.Server.Controllers;
using Chat.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//api degli utenti gestisce tutte le chiamate rest degli utenti

namespace BlazorApp1.Server.Controllers
{
    [ApiController]
    [Route("api/utenti")]
    public class UsersController : ControllerWithContext
    {
        public UsersController(ChatContext context) : base(context)
        {
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utente>>> GetUtenti()
        {
            if (Context.Utenti == null)
            {
                return NotFound();
            }
            var Utenti = await Context.Utenti.Include(_ => _.Indirizzo).ToListAsync();
            return Utenti;
        }
        
        [HttpPost]
        public async Task<ActionResult> RegisterUser(AuthDTO u)
        {
            if(u.Username=="")
                return new UnauthorizedResult();
            try
            {
                Utente user = new Utente();
                user.Password = u.Password;
                user.Username = u.Username;
                Guid token = Guid.NewGuid();
                user.Token = token.ToString();
                Context.Utenti.Add(user);
                await Context.SaveChangesAsync();
                return new ObjectResult(user);
            }
            catch (Exception e)
            {
                return new UnauthorizedResult();
            }
            
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(AuthDTO u)
        {
            var user = await Context.Utenti
                .Include(_ => _.Indirizzo)
                .SingleOrDefaultAsync(_ => _.Username == u.Username && _.Password == u.Password);
            if (user != null)
            {
                return new ObjectResult(user);
                // logga
            }
            return NotFound();
        }
        [HttpPut("aggiorna")]
        public async Task<ActionResult> AggiornaUser(Utente u)
        {
            var user = await Context.Utenti
                .SingleOrDefaultAsync(_ => _.Username == u.Username);
            user.Indirizzo = u.Indirizzo;
            user.Password = u.Password;
            Context.SaveChanges();
            user.Token = "";
            return new ObjectResult(user);
        }


    }
}