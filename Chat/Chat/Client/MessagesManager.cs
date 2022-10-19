using System.Net;
using System.Net.Http.Json;
using Chat.Shared;
using Microsoft.JSInterop;

namespace Chat.Client;
//classe per gestire i messaggi che continua a fare il polling e quando trova un nuovo messaggio trigghera l'evento
//la injecti nelle altre classi quando ti servono la lista dei messaggi
public class MessagesManager
{
    private readonly UserManager _userManager;
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private int _ricordaConta;
    
    public event Action OnMessageRecieved = null!;
    public Dictionary<string,List<Messaggio>> Messaggi { get; set; } = new();//dizionario di messaggi che nella chiave contiene lo username del mittente e come valore la lista dei suoi messaggi
    
    public MessagesManager(UserManager userManager, HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _userManager = userManager;
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
        _userManager.OnUserChanged += HandleUserChanged;
    }

    private void HandleUserChanged()//faccio partire il message polling quando entro con un nuovo utente
    {
        if (_userManager.Utente != null)
        {
            MsgsPolling();
        }
    }

    private async Task MsgsPolling()
    {
        while (_userManager.Utente!=null)
        {
            ReadMessages();
            await Task.Delay(5000);
        }
    }
    
    public async void ReadMessages()
    {
        Messaggi.Clear();
        if (_userManager.Utente == null) throw new Exception("Cant read messages with UserManager.Utente == null");
        var messaggiLetti = (await _httpClient.GetFromJsonAsync<List<Messaggio>>("api/messaggi?token="+_userManager.Utente.Token))!;
        foreach (var msg in messaggiLetti)//separo dentro al dizionario i messaggi dei vari utenti
        {
            if (msg.Destinatario != null)//prendo i messaggi di chat privata
            {
                var bucket = msg.Mittente.Username == _userManager.Utente.Username ? msg.Destinatario.Username : msg.Mittente.Username;
                if (!Messaggi.ContainsKey(bucket))
                    Messaggi[bucket] = new List<Messaggio>();
                Messaggi[bucket].Add(msg);
            }
            else//prendo i messaggi di chat globale
            {
                if (!Messaggi.ContainsKey(""))
                    Messaggi[""] = new List<Messaggio>();
                Messaggi[""].Add(msg);
            }
        }//alla fine del foreach ho sia i messaggi di chat globale,sia i messaggi privati destinati o mandati da me
        if (_ricordaConta != messaggiLetti.Count)
        {
            OnMessageRecieved();
            //NotifyNewMessageArrive();
        }
        _ricordaConta = messaggiLetti.Count;
    }

    
    public async Task NotifyNewMessageArrive()
    {
        await _jsRuntime.InvokeVoidAsync("PlayAudioFile", "/sounds/duh.mp3");
    }

    public async void HandleNewMessageCreate(string testo,string? altroUtente)
    {
        if (_userManager.Utente == null) throw new Exception("Cant Create a new message with Usermanager.utente == null");
        var newMessage = new NewMessaggioDTO
        {
            messaggio = testo,
            token = _userManager.Utente.Token,
            Destinatario = altroUtente
        };
        var a = await _httpClient.PostAsJsonAsync("api/messaggi", newMessage);
        ReadMessages();
    }
    
    public async void HandleMessagesRead(string? altroUtente) //metodo che mi mette a letto i messaggi di quel utente quando entro nella chatPage
    {
        if (altroUtente == null) return;
        if (_userManager.Utente == null) throw new Exception("Cant read messages with Usermanager.utente == null");
        var msgDto = new NewMessaggioDTO
        {
            messaggio = "",
            token = _userManager.Utente.Token,
            Destinatario = altroUtente
        };
        var a = await _httpClient.PutAsJsonAsync("api/messaggi", msgDto);
        if (a.StatusCode != HttpStatusCode.OK) return;
        
        if(Messaggi.ContainsKey(altroUtente))
            Messaggi[altroUtente].ForEach(_ => _.Letto = true);
        OnMessageRecieved();
        //todo:crea un altro evento per aggiornare l'ui dei navlink
    }
}