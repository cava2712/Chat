using Microsoft.AspNetCore.Mvc;
using Chat.Shared;
using Microsoft.EntityFrameworkCore;
namespace Chat.Server.Controllers;
[ApiController]
[Route("api/fatture")]
public class FattureController : ControllerWithContext //api fatture,gestisce tutte le chiamate rest delle fatture
{
    public FattureController(ChatContext context) : base(context){}

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Fattura>>> GetFatture(string token)
    {
        var fatture = await Context.Fatture
            .Include(_ => _.Mittente)
            .Include(_ => _.Destinatario)
            .Where(_ => _.Mittente.Token == token ||(_.Destinatario != null && _.Destinatario!.Token == token))
            .ToListAsync();
        fatture.ForEach(_ =>
        {
            _.Mittente.Token = "";
            _.Destinatario!.Token = "";
        }); // quando ritorno le fatture,le ritorno mettendo il token nullo perchè senno trasferirei in chiaro il token e sarebbe visibile a qualsiasi utente

        return fatture;
    }

    [HttpPost]
    public async Task<ActionResult> PostFattura(FatturaDTO fatturaDto)
    {
        var mittente = await Context.Utenti
            .SingleOrDefaultAsync(_ => _.Token == fatturaDto.TokenMittente);
        if (mittente == null)
        {
            //TODO: se danno problema i messaggi e' questo
            return new UnauthorizedResult();
        }
        var destinatario = await Context.Utenti
            .SingleOrDefaultAsync(_ => _.Username == fatturaDto.UsernameDestinatario);
        if (destinatario == null)
        {
            //TODO: se danno problema i messaggi e' questo
            return new UnauthorizedResult();
        }

        Fattura fattura = new Fattura();
        fattura.Descrizione = fatturaDto.Descrizione;
        fattura.Destinatario = destinatario;
        fattura.Mittente = mittente;
        fattura.Data=DateTime.Now;
        fattura.Iva = fatturaDto.Iva;
        fattura.PrezzoNoIva = fatturaDto.PrezzoNoIva;
        fattura.PrezzoFinale = fatturaDto.PrezzoFinale;
        Context.Fatture.Add(fattura);
        await Context.SaveChangesAsync();
        return new OkResult();
    }
}