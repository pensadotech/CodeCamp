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

namespace CodeCampApp.Pages.Talks
{
    public class DetailModel : PageModel
    {
        // Private members..........................
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<ListModel> _logger;
        private readonly Domain.Repositories.ICampRepository _campRepository;

        // Properties .....................................
        public TalkModel TalkModel { get; set; }


        // Constructors ...........................
        public DetailModel(IConfiguration config, ILogger<ListModel> logger,
            IMapper mapper, Domain.Repositories.ICampRepository campRepository)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _campRepository = campRepository;
        }


        // Methods ................................
        public IActionResult OnGet(int talkId)
        { 
            // Retreive the deail for the selected model 
            Domain.Entities.Talk domainTalk = _campRepository.GetTalkById(talkId);

            // Copy data from Domain DTo to App Model
            // Note: Auto mapping does not include image filename and data
            TalkModel = _mapper.Map<TalkModel>(domainTalk);

            if (TalkModel != null)
            {
                // Talk profile Image 
                string imageBase64Data = null;
                //string imageDataURL = "https://via.placeholder.com/200";
                string imageDataURL = @"..\..\images\dummyTalk.jpg";

                if (domainTalk.ProfileImageData != null)
                {
                    imageBase64Data = Convert.ToBase64String(domainTalk.ProfileImageData);
                    imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                }

                TalkModel.ProfileImageFilename = imageDataURL;
                TalkModel.ProfileImageFormFile = null;

                // Speaker profile image 
                imageBase64Data = null;
                //string imageDataURL = "https://via.placeholder.com/200";
                imageDataURL = @"..\..\images\dummySpeakerImg.jpg";

                if (domainTalk.Speaker != null)
                {
                    if (domainTalk.Speaker != null && domainTalk.Speaker.ProfileImageData != null)
                    {
                        imageBase64Data = Convert.ToBase64String(domainTalk.Speaker.ProfileImageData);
                        imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                    }

                    // Assign image from database into file that will display it
                    TalkModel.SpeakerProfileImageFilename = imageDataURL;
                }
                else // if no speaker is defined, indicate unasigned
                {
                    TalkModel.SpeakerFirstName = "Unasigned";
                    // Assign image from database into file that will display it
                    TalkModel.SpeakerProfileImageFilename = imageDataURL;
                }

            }
            else
            {
                return RedirectToPage("./NotFound");
            }


            return Page();
        }
    }
}
