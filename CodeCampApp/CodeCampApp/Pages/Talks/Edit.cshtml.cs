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
using System.IO;

namespace CodeCampApp.Pages.Talks
{
    public class EditModel : PageModel
    {
        // Private members .......................
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<ListModel> _logger;
        private readonly Domain.Repositories.ICampRepository _campRepository;

        // Properties ............................
        // Bind property will help populate the Location property upon a POST operation
        // BindProperty works for POST operation by default
        [BindProperty]
        public TalkModel TalkModel { get; set; }


        // Constructors .........................
        public EditModel(IConfiguration config, ILogger<ListModel> logger,
            IMapper mapper, Domain.Repositories.ICampRepository campRepository)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _campRepository = campRepository;
        }

        // Methods .........................
        public IActionResult OnGet(int? talkId)
        {
            // Talk profile Image 
            string imageBase64Data = null;
            //string imageDataURL = "https://via.placeholder.com/200";
            string imageDataURL = @"..\..\images\dummyTalk.jpg";

            Domain.Entities.Talk domainTalk = null;

            // Depending if this program is called from EDIT or ADD
            // The parameter talkId can have a value or not.
            // If it does, search for the Speaker and display it, 
            // otherwise, create a new one and display empty fields
            if (talkId.HasValue)
            {
                // Retreive the deail for the selected model 
                domainTalk = _campRepository.GetTalkById(talkId.Value);

                // Copy data from Domain DTo to App Model
                // Note: Auto mapping does not include image filename and data
                TalkModel = _mapper.Map<TalkModel>(domainTalk);

                if (domainTalk.ProfileImageData != null)
                {
                    imageBase64Data = Convert.ToBase64String(domainTalk.ProfileImageData);
                    imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                }
            }
            else
            {
                TalkModel = new TalkModel();
            }

            TalkModel.ProfileImageFilename = imageDataURL;
            TalkModel.ProfileImageFormFile = null;

            // Speaker profile image 
            imageBase64Data = null;
            //string imageDataURL = "https://via.placeholder.com/200";
            imageDataURL = @"..\..\images\dummySpeakerImg.jpg";

            // Speaker 
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

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            // ModelState can help visualize a particular field from the POST operation
            // for example Model["Location"]. However, it also helps to validate 
            // if in general the input fields are valid
            // NOTE: Validation enforces restrictions based on Model attributes 
            if (!ModelState.IsValid)
            {
                // If input is not valid, render again the Cuisine type and return
                // to allow user to correct problems
                return Page();
            }

            // Domain entity to add or update
            Domain.Entities.Talk domainTalkToAddOrUpd = null;

            // Determine if will be an ADD or Update
            if (TalkModel.Id > 0)
            {
                // UPDATE operation
                // Convert Model into Domain entity
                // Note: Auto mapping does not include image filename and data
                domainTalkToAddOrUpd = _mapper.Map<Domain.Entities.Talk>(TalkModel);

                // If not image was selected. restore the current on file
                if (TalkModel.ProfileImageFormFile == null)
                {
                    var domainTalkrOld = _campRepository.GetSpeakerById(TalkModel.Id);
                    domainTalkToAddOrUpd.ProfileImageFilename = domainTalkrOld.ProfileImageFilename;
                    domainTalkToAddOrUpd.ProfileImageData = domainTalkrOld.ProfileImageData;
                }
            }
            else
            {
                // TODO: How avoid repeated records?

                // ADD operation
                // Convert Model into Domain entity
                // Note: Auto mapping does not include image filename and data
                domainTalkToAddOrUpd = _mapper.Map<Domain.Entities.Talk>(TalkModel);
            }

            if (TalkModel.ProfileImageFormFile != null)
            {
                // sync new filenane selected to the string representation in the model
                TalkModel.ProfileImageFilename = TalkModel.ProfileImageFormFile.FileName;
                // Update the image filename in the Domain entity
                domainTalkToAddOrUpd.ProfileImageFilename = TalkModel.ProfileImageFormFile.FileName;

                // Convert image into a memory string and into the Entity byte array
                using (MemoryStream ms = new MemoryStream())
                {
                    await TalkModel.ProfileImageFormFile.CopyToAsync(ms);
                    domainTalkToAddOrUpd.ProfileImageData = ms.ToArray();
                }
            }

            // Run Add or update operation at repository 
            if (TalkModel.Id > 0)
            {
                // Update Domain Entity
                _campRepository.Update<Domain.Entities.Talk>(domainTalkToAddOrUpd);
            }
            else
            {
                // Add the location to the list
                // Note: Auto mapping does not include image filename and data
                _campRepository.Add<Domain.Entities.Talk>(domainTalkToAddOrUpd);

                // Copy data from Domain DTO to App Model, to reflect the new ID
                // Note: Auto mapping does not include image filename and data
                TalkModel = _mapper.Map<TalkModel>(domainTalkToAddOrUpd);
            }

            // Commit changes in repository
            _campRepository.CommitChanges();

            // Send to the page that will be redirected, a message through TempData
            // to inform the user about the operation that took place. 
            TempData["ActionMessage"] = "Talk saved!";


            // IMPORTANT: For ADD, EDIT, or DELETE operations allways redirect to a different page
            // Force re-direction to detail page, do not stay in curent page
            return RedirectToPage("./Detail", new { speakerId = this.TalkModel.Id });
        }


    }
}
