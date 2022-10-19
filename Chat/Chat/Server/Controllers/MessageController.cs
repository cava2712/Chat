using Microsoft.AspNetCore.Mvc;
using Chat.Server;
using Chat.Server.Controllers;
using Chat.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//api dei messaggi,gestisce tutte le chiamate rest dei messaggi

namespace Chat.Server.Controllers;
[ApiController]
[Route("api/messaggi")]
public class MessagesController : ControllerWithContext
{
    public MessagesController(ChatContext context) : base(context)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Messaggio>>> GetMessaggi(string token)
    {
        var messaggi = await Context.Messaggi
            .Include(_ => _.Mittente)
            .Include(_ => _.Destinatario)
            .Where(_ => _.Destinatario == null || (_.Destinatario.Token == token || _.Mittente.Token == token))
            .ToListAsync();
        messaggi.ForEach(_ =>
        {
            _.Mittente.Token = "";
            if (_.Destinatario != null) _.Destinatario.Token = "";
        }); // quando ritorno i messaggi,li ritorno mettendo il token nullo perchè senno trasferirei in chiaro il token e sarebbe visibile a qualsiasi utente

        return messaggi;
    }
    
    [HttpPut]
    public async Task<ActionResult> PutMessaggiLetti(NewMessaggioDTO msgDto)//modifichiamo i messaggi mettendo letto=true per indicare che sono stati letti
    {
        var messaggi = await Context.Messaggi
            .Include(_ => _.Mittente)
            .Include(_ => _.Destinatario)
            .Where(_ => _.Destinatario != null && _.Destinatario.Token==msgDto.token && _.Mittente.Username == msgDto.Destinatario)
            .ToListAsync();
        messaggi.ForEach(_ =>
        {
            _.Letto = true;
        }); // quando ritorno i messaggi,li ritorno mettendo la prop letto a true così sa che sono stati letti e non ha messaggi da leggere
        await Context.SaveChangesAsync();
        return new OkResult();
    }

    [HttpPost]
    public async Task<ActionResult> PostMessaggio(NewMessaggioDTO msgDto)
    {
        var user = await Context.Utenti
            .SingleOrDefaultAsync(_ => _.Token == msgDto.token);
        if (user == null)
        {
            //TODO: se danno problema i messaggi e' questo
            return new UnauthorizedResult();
        }
        Utente? dest;
        if (msgDto.Destinatario != null)
        {
            dest = await Context.Utenti
                .SingleOrDefaultAsync(_ => _.Username == msgDto.Destinatario);
            if (dest == null)
            {
                return new NotFoundResult();
            }
        }
        else
        {
            dest = null;
        }
        Messaggio msg = new Messaggio();
        msg.Mittente = user;
        msg.Testo = msgDto.messaggio;
        msg.Data = DateTime.Now;
        msg.Destinatario = dest;
        msg.Letto = false;
        Context.Messaggi.Add(msg);
        await Context.SaveChangesAsync();
        return new OkResult();
    }
}