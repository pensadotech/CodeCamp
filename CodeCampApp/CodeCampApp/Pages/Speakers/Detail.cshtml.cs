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
        public SpeakerModel SpeakerModel { get; set; }

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
            Domain.Entities.Speaker domainSpkr = _campRepository.GetSpeakerById(speakerId);

            // Copy data from Domain DTo to App Model
            SpeakerModel = _mapper.Map<SpeakerModel>(domainSpkr);

            if (SpeakerModel != null)
            {
                //string imageDataURL = "https://via.placeholder.com/200";
                string imageDataURL = @"..\..\images\dummySpeakerImg.jpg";

                if (domainSpkr.ProfileImageData != null)
                {
                    string imageBase64Data = Convert.ToBase64String(domainSpkr.ProfileImageData);
                    imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                }

                SpeakerModel.ProfileImageFilename = imageDataURL;
                SpeakerModel.ProfileImageFormFile = null;

            }
            else
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }
    }
}