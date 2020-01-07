using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCamp.Domain.Entities;
using CodeCamp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CodeCampApp.Pages
{
    public class IndexModel : PageModel
    {
        // Private members..........................
        private readonly ILogger<IndexModel> _logger;


        // Constructors ............................
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
