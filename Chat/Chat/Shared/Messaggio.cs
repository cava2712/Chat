namespace Chat.Shared;

public class Messaggio
{
    public string Testo { get; set; }
    
    public DateTime Data{ get; set; }
    
    public int Id { get; set; }
    
    public Utente Mittente { get; set; }
    
    public Utente? Destinatario { get; set; }

    public bool Letto { get; set; }
}