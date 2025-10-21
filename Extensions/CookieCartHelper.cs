using System.Text.Json; // Pour la sérialisation/désérialisation JSON
using Microsoft.AspNetCore.Http; // Pour gérer les cookies et le HttpContext
using GestionPanier.Pages; // Pour utiliser CartItemViewModel

namespace GestionPanier.Extensions
{
    // Classe statique pour gérer le panier stocké dans les cookies
    public static class CookieCartHelper
    {
        /// Enregistre le panier dans un cookie côté client.
        /// <param name="context">Le contexte HTTP pour accéder aux cookies</param>
        /// <param name="cart">La liste des items du panier à sauvegarder</param>
        public static void SetCartCookie(HttpContext context, List<CartItemViewModel> cart)
        {
            // Définir les options du cookie : expire dans 7 jours
            var options = new CookieOptions { Expires = DateTimeOffset.Now.AddDays(7) };

            // Sérialise la liste d'items en JSON
            var json = JsonSerializer.Serialize(cart);

            // Enregistre le cookie avec le nom "Cart" et les données JSON
            context.Response.Cookies.Append("Cart", json, options);
        }

        /// <summary>
        /// Récupère le panier depuis le cookie côté client.
        /// </summary>
        /// <param name="context">Le contexte HTTP pour accéder aux cookies</param>
        /// <returns>Liste des items du panier</returns>
        public static List<CartItemViewModel> GetCartCookie(HttpContext context)
        {
            // Lit le cookie "Cart"
            var json = context.Request.Cookies["Cart"];

            // Si le cookie est vide ou inexistant, retourne une liste vide
            // Sinon, désérialise le JSON en liste d'items du panier
            return string.IsNullOrEmpty(json)
                ? new List<CartItemViewModel>()
                : JsonSerializer.Deserialize<List<CartItemViewModel>>(json) ?? new List<CartItemViewModel>();
        }
    }
}
