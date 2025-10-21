using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GestionPanier.Models; // Pour utiliser la classe Produit
using GestionPanier.Data;   // Pour acc�der au contexte de la base de donn�es
using Microsoft.EntityFrameworkCore; // Pour les op�rations async sur la base
using GestionPanier.Extensions; // Pour acc�der � CookieCartHelper

namespace GestionPanier.Pages
{
    // PageModel associ� � la page Index (page d'accueil)
    public class IndexModel : PageModel
    {
        // Logger pour enregistrer des informations dans les logs
        private readonly ILogger<IndexModel> _logger;

        // Contexte EF Core pour acc�der � la base de donn�es
        private readonly GestionPanierContext _context;

        // Liste des produits � afficher sur la page
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
            // R�cup�re tous les produits depuis la base de donn�es
            Produits = await _context.Produit.ToListAsync();

            // R�cup�re le panier actuel depuis le cookie
            var cart = CookieCartHelper.GetCartCookie(HttpContext);

            // Stocke le nombre total d'articles dans ViewData pour l'affichage dans la page
            ViewData["CartCount"] = cart.Sum(i => i.Quantity);
        }
    }
}
