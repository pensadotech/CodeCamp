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
    public class DeleteModel : PageModel
    {
        // Private members..........................
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<ListModel> _logger;
        private readonly Domain.Repositories.ICampRepository _campRepository;

        // Properties .....................................
        public SpeakerModel Speaker { get; set; }

        // Constructors ....................................
        public DeleteModel(IConfiguration config, ILogger<ListModel> logger,
            IMapper mapper, Domain.Repositories.ICampRepository campRepository)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _campRepository = campRepository;
        }

        // Methods ......................
        public IActionResult OnGet(int speakerId)
        {
            // Retreive the deail for the selected model 
            Domain.Entities.Speaker spker = _campRepository.GetSpeakerById(speakerId);

            // Copy data from Domain DTO to App Model
            Speaker = _mapper.Map<SpeakerModel>(spker);

            if (Speaker == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        // In Razor pages, we have two default handlers, OnGet and OnPost
        // The POTS operation will be used to take a delete action
        public IActionResult OnPost(int speakerId)
        {
            // Retreive the deail for the selected model 
            Domain.Entities.Speaker spker = _campRepository.GetSpeakerById(speakerId);

            // Retreive the deail for the selected model 
            spker = _campRepository.Delete<Domain.Entities.Speaker>(spker);

            // Commit the operation
            this._campRepository.CommitChanges();
            
            // Copy data from Domain DTo to App Model
            Speaker = _mapper.Map<SpeakerModel>(spker);

            // Send to the paget that will be redirected, a message through TempData
            // to inform the user about the operation that took place. 
            TempData["ActionMessage"] = $"{Speaker.LastName} deleted";

            // IMPORTANT: For ADD, EDIT, or DELETE operations allwyas redirect to a different page
            // Force re-direction to detail page, do not stay in curent page
            return RedirectToPage("./List");
        }
    }
}