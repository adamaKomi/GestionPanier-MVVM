using GestionPanier.Data; // Pour accéder au contexte de la base de données
using Microsoft.AspNetCore.Authentication.Cookies; // Pour l'authentification via cookies
using Microsoft.EntityFrameworkCore; // Pour la configuration de la base SQL
using Microsoft.Extensions.DependencyInjection; // Pour configurer les services

// Crée le builder pour configurer l'application
var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Configuration des services
// -------------------------

// Ajoute le support des Razor Pages (pages .cshtml)
builder.Services.AddRazorPages();

// Ajoute le contexte de la base de données (EF Core) avec SQL Server
builder.Services.AddDbContext<GestionPanierContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("GestionPanierContext")
        ?? throw new InvalidOperationException("Connection string 'GestionPanierContext' not found.")
    )
);


// Ajout du support des sessions (permet de stocker des données côté serveur)
builder.Services.AddSession();

// -------------------------
// Construction de l'application
// -------------------------
var app = builder.Build();

// -------------------------
// Pipeline de traitement des requêtes
// -------------------------

// Si on est en production (pas en développement), gérer les erreurs et HSTS
if (!app.Environment.IsDevelopment())
{
    // Page d'erreur personnalisée
    app.UseExceptionHandler("/Error");

    // HSTS : force HTTPS strict pour 30 jours
    app.UseHsts();
}

// Redirige automatiquement les requêtes HTTP vers HTTPS
app.UseHttpsRedirection();

// Active le routage pour Razor Pages et autres middlewares
app.UseRouting();

// Active les sessions pour que l'application puisse stocker des données côté serveur
app.UseSession();

// Active l'authentification et l'autorisation
app.UseAuthentication();
app.UseAuthorization();

// Mappe les fichiers statiques (CSS, JS, images)
app.MapStaticAssets();

// Mappe les Razor Pages pour qu'elles soient accessibles via URL
app.MapRazorPages()
   .WithStaticAssets();

// Démarre l'application
app.Run();
