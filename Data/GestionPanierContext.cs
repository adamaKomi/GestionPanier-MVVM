using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Pour utiliser Entity Framework Core (ORM)
using GestionPanier.Models; // Pour accéder aux modèles Produit, User, Commande

namespace GestionPanier.Data
{
    // Contexte de la base de données pour l'application GestionPanier
    // Il représente la "passerelle" entre la base de données et le code C#
    public class GestionPanierContext : DbContext
    {
        // Constructeur injectant les options du DbContext (chaîne de connexion, etc.)
        public GestionPanierContext(DbContextOptions<GestionPanierContext> options)
            : base(options)
        {
        }

        // Déclare un DbSet pour chaque table de la base de données

        // Table des produits
        public DbSet<GestionPanier.Models.Produit> Produit { get; set; } = default!;

    }
}
