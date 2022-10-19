namespace Chat.Shared;
//classe per nascondere alcuni parametri dei client
public class NewMessaggioDTO
{
    public string token { get; set; }
    public string messaggio { get; set; }

    public string? Destinatario{ get; set; }
}