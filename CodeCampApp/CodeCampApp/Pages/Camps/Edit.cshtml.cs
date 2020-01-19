using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain = CodeCamp.Domain;
using CodeCampApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CodeCampApp
{
    public class EditModel : PageModel
    {
        // Private memebrs ..................................
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<EditModel> _logger;
        private readonly Domain.Repositories.ICampRepository _campRepository;


        // Properties .....................................
        // Bind property will help populate the Camp property upon a POST operation
        [BindProperty]
        public CampModel Camp { get; set; }

        // Constructors .....................................
        public EditModel(IConfiguration config, ILogger<EditModel> logger,
            IMapper mapper, Domain.Repositories.ICampRepository campRepository)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _campRepository = campRepository;
        }

        // Methods .........................................

        /// <summary>
        /// OnGet - will return an IActionResult, redenring teh Edit web page
        /// in which teh user will be able to edit an existing record or 
        /// enter teh information for a new one.
        /// </summary>
        /// <param name="moniker"></param>
        /// <returns></returns>
        public IActionResult OnGet(string moniker)
        {
            // Depending if this program is called from EDIT or ADD
            // The parameter CampId can have a value or not.
            // If it does, search for the Camp and display it, 
            // otherwise, create a new one and display empty fields
            if (string.IsNullOrEmpty(moniker))
            {
                // Rereive teh Camp from the data source, in this case 
                // will be a Domain DTO
                var domainCamp = _campRepository.GetCampById(moniker);

                // Copy data from Domain DTO to App Model
                Camp = _mapper.Map<CampModel>(domainCamp);
            }
            else
            {
                Camp = new CampModel();
            }

            if (Camp == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }




    }
}