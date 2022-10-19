using System.Net;
using System.Net.Http.Json;
using Blazored.Toast;
using Chat.Shared;


//classe che ricorda l'utente loggato in quel momento,la injecti nelle altre classi per farci riferimento
namespace Chat.Client;

public class UserManager
{
    private readonly HttpClient _httpClient;

    private readonly Blazored.LocalStorage.ILocalStorageService _localStorage;//per tenere traccia delle sessioni
    
    public event Action OnUserChanged; //evento che chiamo quando loggo con un utente e quindi anche quando registro un nuovo utente che iscrivo nel navmenu per aggiurnare la ui

    public event Action OnErrorLogin; //evento che chiamo quando avviene un errore durante il login
    
    public event Action OnErrorRegister; //evento che chiamo quando avviene un errore durante la reg

    
    public UserManager(HttpClient httpclient,Blazored.LocalStorage.ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _httpClient = httpclient;
    }
    public Utente? Utente { get; set; } //utente corrente salvato in tutte le pagine

    public async void HandleLogin(string username, string password)
    {
        var u = new AuthDTO
        {
            Username = username,
            Password = password
        };
        var response = await _httpClient.PostAsJsonAsync("api/utenti/login", u); //controlla se l'utente esiste
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            OnErrorLogin();
            return;
        }
        Utente = await response.Content.ReadFromJsonAsync<Utente>(); //se esiste setta lo user che restituisce la chiamata
        OnUserChanged();
    } 

    public async void HandleRegister(string username, string password)
    {
        var u = new AuthDTO
        {
            Username = username,
            Password = password
        };
        var a = await _httpClient.PostAsJsonAsync("api/utenti", u);
        if (a.StatusCode != HttpStatusCode.OK)
        {
            OnErrorRegister();
            return;
        }

        Utente = await a.Content.ReadFromJsonAsync<Utente>();
        OnUserChanged();
    }

    public void NotifyUserChanged()
    {
        OnUserChanged();
    }


    public void HandleLogout()
    {
        Utente = null;
        _localStorage.SetItemAsync("username", "");
        _localStorage.SetItemAsync("username", "");
        OnUserChanged();
    }
}