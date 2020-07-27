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
        public SpeakerModel SpeakerModel { get; set; }

        // Constructors ........................
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
            //string imageDataURL = "https://via.placeholder.com/200";
            string imageDataURL = @"..\..\images\dummySpeakerImg.jpg";

            // Depending if this program is called from EDIT or ADD
            // The parameter speakerId can have a value or not.
            // If it does, search for the Speaker and display it, 
            // otherwise, create a new one and display empty fields
            if (speakerId.HasValue)
            {
                // Retreive the deail for the selected model 
                Domain.Entities.Speaker domianSpker = _campRepository.GetSpeakerById(speakerId.Value);

                // Copy data from Domain DTo to App Model
                SpeakerModel = _mapper.Map<SpeakerModel>(domianSpker);

                if (domianSpker.ProfileImageData != null)
                {
                    string imageBase64Data = Convert.ToBase64String(domianSpker.ProfileImageData);
                    imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                }
            }
            else
            {
                SpeakerModel = new SpeakerModel();
            }

            // Map manually the image name 
            SpeakerModel.ProfileImageFilename = imageDataURL;
            SpeakerModel.ProfileImageFormFile = null;

            if (SpeakerModel == null)
            {
                return RedirectToPage("./NotFound");
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
            Domain.Entities.Speaker domainSpkrToAddOrUpd = null;

            // Determine if will be an ADD or Update
            if (SpeakerModel.Id > 0)
            {
                // UPDATE operation
                // Convert Model into Domain entity
                // Note: Auto mapping does not include image filename and data
                domainSpkrToAddOrUpd = _mapper.Map<Domain.Entities.Speaker>(SpeakerModel);

                // If not image was selected. restore the current on file
                if (SpeakerModel.ProfileImageFormFile == null)
                {
                    var domainLocOld = _campRepository.GetLocationById(SpeakerModel.Id);
                    domainSpkrToAddOrUpd.ProfileImageFilename = domainLocOld.ProfileImageFilename;
                    domainSpkrToAddOrUpd.ProfileImageData = domainLocOld.ProfileImageData;
                }
            }
            else
            {
                // ADD operation
                // Convert Model into Domain entity
                // Note: Auto mapping does not include image filename and data
                domainSpkrToAddOrUpd = _mapper.Map<Domain.Entities.Speaker>(SpeakerModel);
                
            }

            // IMAGE: Map manually the image filename and data to the new or updated record
            if (SpeakerModel.ProfileImageFormFile != null)
            {
                // sync new filenane selected to the string representation in the model
                SpeakerModel.ProfileImageFilename = SpeakerModel.ProfileImageFormFile.FileName;
                // Update the image filename in the Domain entity
                domainSpkrToAddOrUpd.ProfileImageFilename = SpeakerModel.ProfileImageFormFile.FileName;

                // Convert image into a memory string and into the Entity byte array
                using (MemoryStream ms = new MemoryStream())
                {
                    await SpeakerModel.ProfileImageFormFile.CopyToAsync(ms);
                    domainSpkrToAddOrUpd.ProfileImageData = ms.ToArray();
                }
            }

            // Run Add or update operation at repository 
            if (SpeakerModel.Id > 0)
            {
                // Update Domain Entity
                _campRepository.Update<Domain.Entities.Speaker>(domainSpkrToAddOrUpd);
            }
            else
            {
                // Add the location to the list
                // Note: Auto mapping does not include image filename and data
                _campRepository.Add<Domain.Entities.Speaker>(domainSpkrToAddOrUpd);

                // Copy data from Domain DTO to App Model, to reflect the new ID
                // Note: Auto mapping does not include image filename and data
                SpeakerModel = _mapper.Map<SpeakerModel>(domainSpkrToAddOrUpd);
            }


            // Commit changes in repository
            _campRepository.CommitChanges();

            // Send to the page that will be redirected, a message through TempData
            // to inform the user about the operation that took place. 
            TempData["ActionMessage"] = "Speaker saved!";

            // IMPORTANT: For ADD, EDIT, or DELETE operations allways redirect to a different page
            // Force re-direction to detail page, do not stay in curent page
            return RedirectToPage("./Detail", new { speakerId = this.SpeakerModel.Id });

        }
    }
}