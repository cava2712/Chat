using System.Net.Http.Json;
using Chat.Shared;
namespace Chat.Client;
//classe per gestire le fatture che continua a fare il polling e quando trova una nuova fattura trigghera l'evento
//la injecti nelle altre classi quando ti servono la lista dei messaggi
public class FattureManager
{
    private readonly UserManager _userManager;
    private readonly HttpClient _httpClient;
    public event Action? OnFattureRecieved;
    public List<Fattura> Fatture { get; set; } = new List<Fattura>();
    
    public FattureManager(UserManager userManager, HttpClient httpClient)
    {
        _userManager = userManager;
        _httpClient = httpClient;
        FatturePolling();
    }

    protected async void ReadFatture()
    {
        var oldCount = Fatture.Count;
        Fatture = (await _httpClient.GetFromJsonAsync<List<Fattura>>("api/fatture?token="+_userManager.Utente!.Token))!;
        if (oldCount != Fatture.Count)
        {
            OnFattureRecieved?.Invoke();
        }
    }
    
    public async Task FatturePolling()
    {
        while (_userManager.Utente!=null)
        {
            ReadFatture();
            await Task.Delay(5000);
        }
    }

    
}