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

namespace CodeCampApp.Pages.Camps
{
    public class ListModel : PageModel
    {
        // Private members..........................
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<ListModel> _logger;
        private readonly Domain.Repositories.ICampRepository _campRepository;

        // Properties ...............................
        public IEnumerable<CampModel> Camps { get; set; }

        // To handle messages
        public string LocalMessage { get; set; }
        public string ConfigMessage { get; set; }
        // This message can be seter by another page upon an action
        [TempData]
        public string ActionMessage { get; set; }

        // BindProperty works for POST operation by default, 
        // but with get switch can respond to a GET operation
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        // Constructors ............................
        public ListModel(IConfiguration config, ILogger<ListModel> logger, 
            IMapper mapper, Domain.Repositories.ICampRepository campRepository)
        {
           _config = config;
           _logger = logger;
           _mapper = mapper;
           _campRepository = campRepository;
        }

        // Methods .................................

        /// <summary>
        /// OnGet() - wil not use IActionResult as is not returning a page, but rather
        /// is setting the properties that are referenced in the web page
        /// </summary>
        public void OnGet()
        {
            // Log activity
            _logger.LogInformation("** Executing Camps ListModel");
            // Local message
            LocalMessage = "This is the list of all available camps";
            // Configuration message
            ConfigMessage = _config["CfgMessage"];

            try
            {
                // Retrive all camps from repository, using the SearchTerm coming from teh frontend
                IEnumerable<Domain.Entities.Camp> domainCamps = _campRepository.GetAllCampsByName(SearchTerm);

                // Copy data from Domain DTo to App Model
                Camps = _mapper.Map<CampModel[]>(domainCamps);
            }
            catch
            {
                _logger.LogError("Unable to retreive camps");
            }
        }
    }
}