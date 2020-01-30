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
    public class DetailModel : PageModel
    {
        // Private members..........................
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<ListModel> _logger;
        private readonly Domain.Repositories.ICampRepository _campRepository;

        // Properties .....................................
        public SpeakerModel Speaker { get; set; }



        // Constructors .........................
        public DetailModel(IConfiguration config, ILogger<ListModel> logger,
            IMapper mapper, Domain.Repositories.ICampRepository campRepository)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _campRepository = campRepository;
        }

        // Methods .............................
        public IActionResult OnGet(int speakerId)
        {
            // Retreive the deail for the selected model 
            Domain.Entities.Speaker spkr = _campRepository.GetSpeakerById(speakerId);

            // Copy data from Domain DTo to App Model
            Speaker = _mapper.Map<SpeakerModel>(spkr);

            if (Speaker == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }
    }
}