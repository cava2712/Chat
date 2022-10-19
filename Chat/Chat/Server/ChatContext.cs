namespace Chat.Server;

using Chat.Shared;
using Microsoft.EntityFrameworkCore;


//context della mia app in cui aggiungo gli elementi che hanno delle tabelle e faccio la connessione al db
public class ChatContext: DbContext
{

    public ChatContext(DbContextOptions<ChatContext> options)
        : base(options){ }
    public DbSet<Messaggio> Messaggi { get; set; } = null!;
    public DbSet<Utente> Utenti { get; set; } = null!;
    
    public DbSet<Indirizzo> Indirizzi { get; set; } = null!;
    
    public DbSet<Fattura> Fatture { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=ChatDB;Integrated Security=True;trustServerCertificate=true;");//collegamento al db
    }
}
