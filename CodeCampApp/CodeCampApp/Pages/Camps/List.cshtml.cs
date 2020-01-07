using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCamp.Domain.Entities;
using CodeCamp.Domain.Repositories;
using CodeCampApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CodeCampApp.Pages.Camps
{
    public class ListModel : PageModel
    {
        // Private members..........................
        private readonly ICampRepository _campRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IndexModel> _logger;

        // Properties ...............................
        public IEnumerable<CampModel> Camps { get; set; }
        public string LocalMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        // Constructors ............................
        public ListModel(ICampRepository campRepository, IMapper mapper, ILogger<IndexModel> logger)
        {
            _campRepository = campRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // Methods .................................
        public void OnGet(bool includeTalks = false)
        {
            // Log activity
            this._logger.LogInformation("Executing Camps ListModel");

            // Local message
            this.LocalMessage = "This is the list of all available camps";
            
            try
            {
                // Retrive all camps from repository
                IEnumerable<Camp> domainCamps = _campRepository.GetAllCampsByName(SearchTerm, includeTalks);

                if (domainCamps != null)
                {
                    // Convert camps into CampModel collection and set his page property
                    this.Camps = _mapper.Map<IEnumerable<Camp>, IEnumerable<CampModel>>(domainCamps);
                    this._logger.LogInformation("Total camps:" + domainCamps.ToList().Count.ToString());
                }
                else
                {
                    this._logger.LogInformation("There are no camps defined");
                }
            }
            catch
            {
                this._logger.LogError("Unable to retreive camps");
            }
        }
    }
}