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
    public class IndexModel : PageModel
    {
        private readonly GestionPanier.Data.GestionPanierContext _context;

        public IndexModel(GestionPanier.Data.GestionPanierContext context)
        {
            _context = context;
        }

        public IList<Produit> Produit { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Produit = await _context.Produit.ToListAsync();
        }
    }
}
