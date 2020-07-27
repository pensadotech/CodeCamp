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
        public SpeakerModel SpeakerModel { get; set; }

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
            Domain.Entities.Speaker domainSpker = _campRepository.GetSpeakerById(speakerId);

            // Copy data from Domain DTo to App Model
            // Note: Auto mapping does not include image filename and data
            SpeakerModel = _mapper.Map<SpeakerModel>(domainSpker);

            if (SpeakerModel != null)
            {
                //string imageDataURL = "https://via.placeholder.com/200";
                string imageDataURL = @"..\..\images\dummySpeakerImg.jpg";

                if (domainSpker.ProfileImageData != null)
                {
                    string imageBase64Data = Convert.ToBase64String(domainSpker.ProfileImageData);
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

        // In Razor pages, we have two default handlers, OnGet and OnPost
        // The POTS operation will be used to take a delete action
        public IActionResult OnPost(int speakerId)
        {
            // Retreive the deail for the selected model 
            Domain.Entities.Speaker domainSpker = _campRepository.GetSpeakerById(speakerId);

            // Retreive the deail for the selected model 
            domainSpker = _campRepository.Delete<Domain.Entities.Speaker>(domainSpker);

            // Commit the operation
            this._campRepository.CommitChanges();

            // Copy data from Domain DTo to App Model
            SpeakerModel = _mapper.Map<SpeakerModel>(domainSpker);

            // Send to the paget that will be redirected, a message through TempData
            // to inform the user about the operation that took place. 
            TempData["ActionMessage"] = $"{SpeakerModel.LastName} deleted";

            // IMPORTANT: For ADD, EDIT, or DELETE operations allwyas redirect to a different page
            // Force re-direction to detail page, do not stay in curent page
            return RedirectToPage("./List");
        }
    }
}