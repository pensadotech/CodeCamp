﻿using System;
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
    public class ListModel : PageModel
    {
        // Private members..........................
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<ListModel> _logger;
        private readonly Domain.Repositories.ICampRepository _campRepository;

        // Properties ...............................
        public IEnumerable<SpeakerModel> SpeakerModels { get; set; }

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
                // Retrive all speakers from repository, using the SearchTerm coming from the frontend
                IEnumerable<Domain.Entities.Speaker> domainSpeakers = _campRepository.GetAllSpeakers(SearchTerm);

                // Copy data from Domain Entity to App Model
                // Note: Auto mapping does not include image filename and data
                SpeakerModels = _mapper.Map<SpeakerModel[]>(domainSpeakers);

                // Mapp manually the profile image (filename and data)
                foreach (var dSpk in domainSpeakers)
                {   
                    // get specific model that matches the domain entity 
                    var mSpk = SpeakerModels.SingleOrDefault(l => l.Id == dSpk.Id);

                    // If found, assign image
                    if (mSpk != null)
                    {
                        string imageBase64Data = null;
                        //string imageDataURL = "https://via.placeholder.com/200";
                        string imageDataURL = @"..\images\dummySpeakerImg.jpg";

                        if (dSpk.ProfileImageData != null)
                        {
                            imageBase64Data = Convert.ToBase64String(dSpk.ProfileImageData);
                            imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                        }

                        // Assign image from database into file that will display it
                        mSpk.ProfileImageFilename = imageDataURL;
                        mSpk.ProfileImageFormFile = null;
                    }
                }
            }
            catch
            {
                _logger.LogError("Unable to retreive Speakers");
                LocalMessage = "Unable to retreive Speakers";
            }
        }
    }
}