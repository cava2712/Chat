namespace Chat.Shared;

public class FatturaDTO//mi serve un fatture DTO perchè quando creo una nuova fattura non ho tutti i dati del destinatario(es la password che è obbligatoria) ma conosco solo il nome(devo poi tirarlo fuori dal db) e mi serve solo il token del utente
{

    public string TokenMittente { get; set; }// quando scrivo una nuova fattura essa deve essere scritta attraverso il token segreto dell'utente, così che solo lui possa scrivere una nuova fattura.
    //se avessi messo come riferimento al mittente per una nuova fattura l'username allora chiunque con postman avrebbe potuto inviare una nuova fattura a mio nome, ma non può perchè non conosce il token
    public string UsernameDestinatario { get; set; }

    public string? Descrizione { get; set; }

    public float PrezzoNoIva { get; set; }

    public int Iva { get; set; }

    public float PrezzoFinale => PrezzoNoIva + PrezzoNoIva/100* Iva;
}