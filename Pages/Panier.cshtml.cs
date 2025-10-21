using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GestionPanier.Models;
using GestionPanier.Data;
using GestionPanier.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace GestionPanier.Pages
{
    //[Authorize] // seul un utilisateur connect� acc�de au panier
    public class CartModel : PageModel
    {
        // Contexte EF Core pour acc�der � la base de donn�es
        private readonly GestionPanierContext _context;

        // Logger pour enregistrer des informations dans les logs
        private readonly ILogger<CartModel> _logger;

        // Constructeur injectant le contexte et le logger
        public CartModel(GestionPanierContext context, ILogger<CartModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Propri�t� li�e au formulaire Razor pour les items du panier
        [BindProperty]
        public List<CartItemViewModel> CartItems { get; set; } = new();

        // Propri�t� pour calculer le prix total du panier
        public decimal TotalPrice { get; set; }

        // Handler pour GET : charge les donn�es du panier
        public void OnGet()
        {
            // R�cup�re le panier depuis le cookie
            CartItems = CookieCartHelper.GetCartCookie(HttpContext);

            // Calcule le total
            TotalPrice = CartItems.Sum(i => i.UnitPrice * i.Quantity);

            // Met � jour le nombre d'articles dans ViewData
            ViewData["CartCount"] = CartItems.Sum(i => i.Quantity);

            // Log des informations pour le debug
            _logger.LogInformation("OnGet: Loaded cart with {Count} items", CartItems.Count);
            foreach (var item in CartItems)
            {
                _logger.LogInformation("OnGet item: ProductId={Id}, Quantity={Qty}", item.ProductId, item.Quantity);
            }
        }

        // Handler POST pour mettre � jour les quantit�s
        public IActionResult OnPostUpdateQuantities()
        {
            _logger.LogInformation("Starting update quantities. CartItems count: {Count}", CartItems?.Count ?? 0);
            _logger.LogInformation("CartItems from form: {@CartItems}", CartItems);

            // R�cup�re le panier existant depuis le cookie
            var existingCart = CookieCartHelper.GetCartCookie(HttpContext);
            _logger.LogInformation("Existing cart: {@Cart}", existingCart);

            // Met � jour les quantit�s des produits
            foreach (var item in CartItems)
            {
                _logger.LogInformation("Updating item: ProductId={Id}, Quantity={Qty}", item.ProductId, item.Quantity);
                var match = existingCart.FirstOrDefault(c => c.ProductId == item.ProductId);
                if (match != null)
                {
                    match.Quantity = item.Quantity;
                }
            }

            // S�rialise et enregistre le panier mis � jour dans le cookie
            var json = System.Text.Json.JsonSerializer.Serialize(existingCart);
            _logger.LogInformation("Setting cookie with JSON: {Json}", json);
            CookieCartHelper.SetCartCookie(HttpContext, existingCart);
            _logger.LogInformation("Updated cart: {@Cart}", existingCart);

            // Redirige vers la page du panier
            return RedirectToPage();
        }

        // Handler POST pour supprimer un produit sp�cifique
        public IActionResult OnPostRemove(int productId)
        {
            var cart = CookieCartHelper.GetCartCookie(HttpContext);
            var itemToRemove = cart.FirstOrDefault(c => c.ProductId == productId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }
            CookieCartHelper.SetCartCookie(HttpContext, cart);
            return RedirectToPage();
        }

        // Handler POST pour vider tout le panier
        public IActionResult OnPostClearCart()
        {
            // Remplace le panier par une liste vide
            CookieCartHelper.SetCartCookie(HttpContext, new List<CartItemViewModel>());
            return RedirectToPage();
        }

        // Handler POST pour ajouter un produit au panier
        public async Task<IActionResult> OnPostAddAsync(int productId)
        {
            // Cherche le produit dans la base
            var product = await _context.Produit.FindAsync(productId);
            if (product == null)
                return NotFound();

            // R�cup�re le panier depuis le cookie
            var cart = CookieCartHelper.GetCartCookie(HttpContext);

            // V�rifie si le produit est d�j� dans le panier
            var existingItem = cart.FirstOrDefault(c => c.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++; // Augmente la quantit�
            }
            else
            {
                // Sinon, ajoute un nouvel item
                cart.Add(new CartItemViewModel
                {
                    ProductId = product.Id,
                    ProductName = product.Nom,
                    UnitPrice = product.Prix,
                    Quantity = 1
                });
            }

            // Met � jour le cookie
            CookieCartHelper.SetCartCookie(HttpContext, cart);

            // Redirige vers la page d'accueil
            return RedirectToPage("/Index");
        }
    }

    // ViewModel repr�sentant un article dans le panier
    public class CartItemViewModel
    {
        public int ProductId { get; set; } // ID du produit
        public string ProductName { get; set; } = string.Empty; // Nom du produit
        public decimal UnitPrice { get; set; } // Prix unitaire
        public int Quantity { get; set; } // Quantit� dans le panier
    }
}
