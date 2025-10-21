using System.ComponentModel.DataAnnotations;

namespace GestionPanier.Models
{
    public class Produit
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Le nom du produit est requis")]
        public string Nom { get; set; } = string.Empty;

        [Range(1, 1000000, ErrorMessage ="Le prix doit etre positif")]
        public decimal Prix { get; set; }

        [Range(0, int.MaxValue, ErrorMessage ="Le stock doit être positif")]
        public int Stock { get; set; }
    }
}
