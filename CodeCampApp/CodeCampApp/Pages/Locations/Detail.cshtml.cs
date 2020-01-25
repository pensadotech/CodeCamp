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

namespace CodeCampApp.Pages.Locations
{
    public class DetailModel : PageModel
    {
        // Private members..........................
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<ListModel> _logger;
        private readonly Domain.Repositories.ICampRepository _campRepository;

        // Properties .....................................
        public LocationModel Location { get; set; }


        // Constructors ................................
        public DetailModel(IConfiguration config, ILogger<ListModel> logger,
            IMapper mapper, Domain.Repositories.ICampRepository campRepository)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _campRepository = campRepository;
        }

        // Methods .................................
        public IActionResult OnGet(int locationId)
        {
            // Retreive the deail for the selected model 
            Domain.Entities.Location location = _campRepository.GetLocationById(locationId);

            // Copy data from Domain DTo to App Model
            Location = _mapper.Map<LocationModel>(location);

            if (Location == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }
    }
}