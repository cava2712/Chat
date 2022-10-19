namespace Chat.Shared;

public class Fattura
{
    public int Id { get; set; }
    public Utente Mittente { get; set; }

    public Utente? Destinatario { get; set; }//destinatario l'abbiamo messo a null perchè in caso venisse eliminato il destinatario bisogna metterlo a null
    //questo perchè sqlserver non ti permette di avere 2 foreing key delate cascade
    public string? Descrizione { get; set; }
    
    public DateTime Data { get; set; }

    public float PrezzoNoIva { get; set; }

    public int Iva { get; set; }

    public float PrezzoFinale { get; set; }
}