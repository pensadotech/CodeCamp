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
    public class ListModel : PageModel
    {
        // Private members .........................
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<ListModel> _logger;
        private readonly Domain.Repositories.ICampRepository _campRepository;

        // Properties ...............................
        public IEnumerable<TalkModel> TalkModels { get; set; }

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

        // Constructors ......................
        public ListModel(IConfiguration config, ILogger<ListModel> logger,
            IMapper mapper, Domain.Repositories.ICampRepository campRepository)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _campRepository = campRepository;
        }

        // Methods ..........................
        public void OnGet()
        {
            // Log activity
            _logger.LogInformation("** Executing Speakers ListModel");
            // Local message
            LocalMessage = "This is the list of all available Speakers";
            // Configuration message
            ConfigMessage = _config["CfgMessage"];

            try
            {
                // Retrive all talks from repository, using the SearchTerm coming from the frontend
                IEnumerable<Domain.Entities.Talk> domainTalks = _campRepository.GetAllTalks(SearchTerm);

                // Copy data from Domain Entity to App Model
                // Note: Auto mapping does not include image filename and data
                TalkModels = _mapper.Map<TalkModel[]>(domainTalks);

                // Mapp manually the profile images (filename and data)
                foreach (var dTlk in domainTalks)
                {
                    // get specific model that matches the domain entity 
                    var mTlk = TalkModels.SingleOrDefault(tlk => tlk.Id == dTlk.Id);

                    if (mTlk != null)
                    {
                        // Talk profile Image 
                        string imageBase64Data = null;
                        //string imageDataURL = "https://via.placeholder.com/200";
                        string imageDataURL = @"..\images\dummyTalk.jpg";

                        if (dTlk.ProfileImageData != null)
                        {
                            imageBase64Data = Convert.ToBase64String(dTlk.ProfileImageData);
                            imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                        }

                        // Assign image from database into file that will display it
                        mTlk.ProfileImageFilename = imageDataURL;
                        mTlk.ProfileImageFormFile = null;

                        // Speaker profile image 
                        imageBase64Data = null;
                        //string imageDataURL = "https://via.placeholder.com/200";
                        imageDataURL = @"..\images\dummySpeakerImg.jpg";

                        if (dTlk.Speaker != null)
                        {
                            if (dTlk.Speaker != null && dTlk.Speaker.ProfileImageData != null)
                            {
                                imageBase64Data = Convert.ToBase64String(dTlk.Speaker.ProfileImageData);
                                imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                            }

                            // Assign image from database into file that will display it
                            mTlk.SpeakerProfileImageFilename = imageDataURL;
                        }
                        else // if no speaker is defined, indicate unasigned
                        {
                            mTlk.SpeakerFirstName = "Unasigned";
                            // Assign image from database into file that will display it
                            mTlk.SpeakerProfileImageFilename = imageDataURL;
                        }
                    }
                }

            }
            catch
            {
                _logger.LogError("Unable to retreive Talks");
                LocalMessage = "Unable to retreive Talks";
            }

        }
    }
}