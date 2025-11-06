# Orchestrazione della soluzione
** Creare la soluzione e il progetto WebAssembly con il wizard [App autonoma Balzor WebAssembly (C#)]
- Configurare opzioni:
	Nome: FidelityCard
	Percorso 
	[Avanti]
- Configurare opzioni:
	Farmework .NET 8 (LTS)
	https
	Applicazione Web Progressive
	[Avanti]

** Aggiungere il progetto WebApi con il wizard [Api Web ASP.NET Core]
- Configurare opzioni:
	Nome: FidelityCard.Srv
	Percorso
	[Avanti]
- Configurare opzioni:
	Farmework .NET 8 (LTS)
	https
	Abilita supporto OpenAPI
	Usa i controller
	[Avanti]
	
** Aggiungere il progetto della libreria condivisa con il wizard [Libreria di classi (C#)]
- Configurare opzioni:
	Nome: FidelityCard.Lib
	Percorso
	[Avanti]
- Configurare opzioni:
	Farmework .NET 8 (LTS)
	[Avanti]	


**************************************************************************************
** NEL PROGETTO FidelitCard.Lib
# Agiungere il modello di esempio nella cartella Models
```C#
public class Example
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
```


**************************************************************************************
** NEL PROGETTO FidelityCard.Srv

# Aggiungere i pacchetti NuGet per Entity Framework Core per SQL Server
- Microsoft.EntityFrameworkCore (ultima versione 8.x)
- Microsoft.EntityFrameworkCore.SqlServer (ultima versione 8.x)
- Microsoft.EntityFrameworkCore.Tools (ultima versione 8.x)

Aggioranre
- Swashbuckle.AspNetCore all'ultima versione stabile


# Creare DbContext
- Aggiungere cartella [Data] nella root del progetto server
- Aggiungere classe FidelityCardDbContext[.cs] nella cartella [Data]

```C#
public class FidelityCardDbContext(DbContextOptions<FidelityCardDbContext> options) : DbContext(options)
{
    public DbSet<Example> Examples { get; set; }
}
```

# Personalizzazione di appsettings.json
- Aggiungere

```Json
  "ConnectionStrings": {
    "DefaultConnection": "ID=sa;PWD=???;Data Source=server\\instance;Initial Catalog=FidelityCard;MultipleActiveResultSets=True;TrustServerCertificate=True;Application Name=FidelityCard"
  },
```

# Registrazione di EF Core in Program.cs

```C#
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

# Aggiungere supporto CORS (Cross-Origin Resouce Sharing)
- Subito sotto var builder = ...

```C#// Supporto CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://ulrwebassembly:port") // URL del client Blazor WebAssembly
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

- Subito sotto var app = ...
```C#
app.UseCors();
```


# Creazione di un controller di esempio

Usiamo lo [Scaffolding] di Visual Studio per generare un controller di esempio
- Cliccare con il tasto destro sulla cartella [Controllers]
- Cliccare su [Aggiungi/Controller]
- Selezionare [Controller MVC con visualizzazioni, che usa Entity Frameowrk]
- In classe modello selezionare [Example]
- DbContext class selezionare [FidelityCardDbContext]
- Disattivare [Genera visualizzazioni]
- Eventualmente modificare il nome del controller, di solito NomemodelloController
[Aggiungi]

** Perfezionare il controller aggiungendo 

- Cambiare la derivazione del controller da [Controller] a [ControllerBase], più adatto ad Api RestFULL. ApiController valida automaticamente i modelli in base a DatAnnotation e restituisce 400 Bad Request quando il modello non viene validato.
È la decorazione [ApiController] che introduce questo meccanismo di validazione automatica. Eliminandolo è possbile tornare alla validazione tramite ModelState.

- In testa alla classe
    [ApiController]
    [Route("api/[controller]")]

- Su ogni action 
	[HttpGet]
	[HttpGet("[action]")]
	[HttpPost]
	[HttpPost("[action]")]
	
** N.B. Lo Scaffolding genera action che restituiscono View ma nel nostro progetto non abbiamo View perché le nostre sono Blazor e si trovano nel progetto principale [FidelityCard]
	
# Migrazioni

In [Console di Gestione Pacchetti] selezionare il progetto [.Srv] come [Progetto predefinito] 
(Il progetto in cui si trova DbContext)

Eseguire:
- Add-Migration InitialCreate -OutputDir Data\Migrations
- update-database



# Personalizzazione del file [.http]
Il fiel .http non fa parte del codice sorgente ma è utile per l'esecuzione di test delle action della Web API senza aver bisogno di tool esterni. Per utilizzarlo è sufficiente personalizzare il codice e usare i link "Invia richiesta" e "Esegui debug" che compaiono su ogni richiesta.




**************************************************************************************
** NEL PROGETTO FidelityCard
# Creare un componente che esegue una richiesta al controller e ne mostra il risultato

```razor
@page "/"
@using FidelityCard.Lib.Models

@inject HttpClient HC
@inject NavigationManager NM

<PageTitle>Home</PageTitle>

<h1>Esempio</h1>

@foreach (var item in Examples)
{
    <p>Elemento numero [@item.ExampleId] @item.ExampleName - @item.ExampleDescription</p>
}
@if (!string.IsNullOrEmpty(ErrorMessage))
{
    <p style="color: #c00;">@ErrorMessage</p>
}

@code 
{
    private IEnumerable<Example> Examples { get; set; } = [];
    private string ErrorMessage { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var uri = "https://localhost:7008/api/examples";
            var result = await HC.GetFromJsonAsync<IEnumerable<Example>>(uri);
            if (result is not null)
            {
                Examples = result;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }   

        await base.OnInitializedAsync();
    }
}
```