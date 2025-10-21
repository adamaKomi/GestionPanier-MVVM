using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionPanier.Data;
using GestionPanier.Models;

namespace GestionPanier.Pages.Produits
{
    public class DetailsModel : PageModel
    {
        private readonly GestionPanier.Data.GestionPanierContext _context;

        public DetailsModel(GestionPanier.Data.GestionPanierContext context)
        {
            _context = context;
        }

        public Produit Produit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produit.FirstOrDefaultAsync(m => m.Id == id);

            if (produit is not null)
            {
                Produit = produit;

                return Page();
            }

            return NotFound();
        }
    }
}
