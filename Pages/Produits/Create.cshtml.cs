using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestionPanier.Data;
using GestionPanier.Models;

namespace GestionPanier.Pages.Produits
{
    public class CreateModel : PageModel
    {
        private readonly GestionPanier.Data.GestionPanierContext _context;

        public CreateModel(GestionPanier.Data.GestionPanierContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Produit Produit { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Produit.Add(Produit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
