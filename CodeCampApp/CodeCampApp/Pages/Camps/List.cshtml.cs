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
        private readonly Domain.Repositories.ICampRepository _campRepository;
         private readonly IMapper _mapper;
        private readonly ILogger<IndexModel> _logger;

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
        public ListModel(IConfiguration config, ILogger<IndexModel> logger, 
            IMapper mapper, Domain.Repositories.ICampRepository campRepository)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _campRepository = campRepository;
        }

        // Methods .................................
        public void OnGet()
        {
            // Log activity
            this._logger.LogInformation("** Executing Camps ListModel");
            // Local message
            this.LocalMessage = "This is the list of all available camps";
            // Configuration message
            this.ConfigMessage = _config["CfgMessage"];

            try
            {
                bool includeTalks = true;
                // Retrive all camps from repository
                IEnumerable<Domain.Entities.Camp> domainCamps = _campRepository.GetAllCampsByName(SearchTerm, includeTalks);

                // Copy data from Domain DTo to App Model
                Camps = _mapper.Map<CampModel[]>(domainCamps);
            }
            catch
            {
                this._logger.LogError("Unable to retreive camps");
            }
        }
    }
}