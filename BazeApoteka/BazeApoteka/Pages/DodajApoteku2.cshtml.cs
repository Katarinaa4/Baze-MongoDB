using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BazeApoteka.Pages
{
    public class DodajApoteku2Model : PageModel
    {
        [BindProperty]
        public String Prosledjeno { get; set; }
       
        public IActionResult OnGet([FromRoute]String id)
        
        {
             Prosledjeno = id;
             return Page();
            
        }
        public IActionResult OnPost([FromRoute]String id)

        {
            Prosledjeno = id;
            return Page();

        }

        public IActionResult OnPostDodajFarmaceuta(String idF)
        {
            Prosledjeno = idF;
            String tt = Prosledjeno;
            return RedirectToPage("./DodajFarmaceuta",new { id=tt});
        }
    }
}