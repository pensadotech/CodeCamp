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

namespace CodeCampApp.Pages.Speakers
{
    public class EditModel : PageModel
    {
        // Private members .......................
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<ListModel> _logger;
        private readonly Domain.Repositories.ICampRepository _campRepository;

        // Properties .....................................
        // Bind property will help populate the Location property upon a POST operation
        // BindProperty works for POST operation by default
        [BindProperty]
        public SpeakerModel Speaker { get; set; }

        // Construrctors ........................
        public EditModel(IConfiguration config, ILogger<ListModel> logger,
            IMapper mapper, Domain.Repositories.ICampRepository campRepository)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _campRepository = campRepository;
        }

        // Methods ..............................
        public IActionResult OnGet(int? speakerId)
        {
            // Depending if this program is called from EDIT or ADD
            // The parameter speakerId can have a value or not.
            // If it does, search for the Speaker and display it, 
            // otherwise, create a new one and display empty fields
            if (speakerId.HasValue)
            {
                // Retreive the deail for the selected model 
                Domain.Entities.Speaker spker = _campRepository.GetSpeakerById(speakerId.Value);

                // Copy data from Domain DTo to App Model
                Speaker = _mapper.Map<SpeakerModel>(spker);
            }
            else
            {
                Speaker = new SpeakerModel();
            }

            if (Speaker == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            // ModelState can help visualize a particular field from the POST operation
            // for example Model["Location"]. However, it also helps to validate 
            // if in general the input fields are valid
            if (!ModelState.IsValid)
            {
                // If input is not valid, render again the Cuisine type and return
                // to allow user to correct problems
                return Page();
            }

            // Determine if will be an ADD or Update
            if (Speaker.Id > 0)
            {
                // UPDATE operation

                // Convert Model into Domain entity
                Domain.Entities.Speaker spkrToUpd = _mapper.Map<Domain.Entities.Speaker>(Speaker);

                // Update 
                _campRepository.Update<Domain.Entities.Speaker>(spkrToUpd);
            }
            else
            {
                // ADD operation

                // Convert Model into Domain entity
                Domain.Entities.Speaker spkrToAdd = _mapper.Map<Domain.Entities.Speaker>(Speaker);

                // Add the location to teh list
                _campRepository.Add<Domain.Entities.Speaker>(spkrToAdd);

                // Copy data from Domain DTO to App Model, to reflect the new ID
                Speaker = _mapper.Map<SpeakerModel>(spkrToAdd);
            }

            // Commit changes 
            _campRepository.CommitChanges();

            // Send to the paget that will be redirected, a message through TempData
            // to inform the user about the operation that took place. 
            TempData["ActionMessage"] = "Speaker saved!";

            // IMPORTANT: For ADD, EDIT, or DELETE operations allways redirect to a different page
            // Force re-direction to detail page, do not stay in curent page
            return RedirectToPage("./Detail", new { speakerId = this.Speaker.Id });

        }
    }
}