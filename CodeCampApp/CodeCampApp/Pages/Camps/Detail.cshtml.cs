using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain = CodeCamp.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodeCampApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CodeCampApp
{
    public class DetailModel : PageModel
    {
        // Private members..........................
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<DetailModel> _logger;
        private readonly Domain.Repositories.ICampRepository _campRepository;

        // Propertes ....................................
        public CampModel Camp { get; set; }
        
        // This message can be seter by another page upon an action
        [TempData]
        public string ActionMessage { get; set; }

        // Constructors ...................................
        public DetailModel(IConfiguration config, ILogger<DetailModel> logger,
            IMapper mapper, Domain.Repositories.ICampRepository campRepository)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _campRepository = campRepository;
        }

        // Methods ........................................

        /// <summary>
        /// OnGet - will return an IActionResult, redenring the Detail web page
        /// in which the user will be able read the recred details
        /// </summary>
        /// <param name="moniker"></param>
        /// <returns></returns>
        public IActionResult OnGet(string moniker)
        {
            // Rereive teh Camp from the data source, in this case 
            // will be a Domain DTO
            var domainCamp = _campRepository.GetCampById(moniker);

            // Copy data from Domain DTO to App Model
            Camp = _mapper.Map<CampModel>(domainCamp);

            if (Camp == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

    }
}