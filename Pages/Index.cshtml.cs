using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GestionPanier.Models; // Pour utiliser la classe Produit
using GestionPanier.Data;   // Pour accéder au contexte de la base de données
using Microsoft.EntityFrameworkCore; // Pour les opérations async sur la base
using GestionPanier.Extensions; // Pour accéder à CookieCartHelper

namespace GestionPanier.Pages
{
    // PageModel associé à la page Index (page d'accueil)
    public class IndexModel : PageModel
    {
        // Logger pour enregistrer des informations dans les logs
        private readonly ILogger<IndexModel> _logger;

        // Contexte EF Core pour accéder à la base de données
        private readonly GestionPanierContext _context;

        // Liste des produits à afficher sur la page
        public List<Produit> Produits { get; set; } = new List<Produit>();

        // Constructeur injectant le logger et le contexte de la base
        public IndexModel(ILogger<IndexModel> logger, GestionPanierContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Handler GET pour charger la page d'accueil.
        /// </summary>
        public async Task OnGetAsync()
        {
            // Récupère tous les produits depuis la base de données
            Produits = await _context.Produit.ToListAsync();

            // Récupère le panier actuel depuis le cookie
            var cart = CookieCartHelper.GetCartCookie(HttpContext);

            // Stocke le nombre total d'articles dans ViewData pour l'affichage dans la page
            ViewData["CartCount"] = cart.Sum(i => i.Quantity);
        }
    }
}
