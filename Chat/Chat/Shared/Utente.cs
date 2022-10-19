using System.ComponentModel.DataAnnotations;

namespace Chat.Shared;

public class Utente
{
    [Key]
    [MinLength(1)]
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string Token { get; set; }

    public Indirizzo? Indirizzo { get; set; }
}