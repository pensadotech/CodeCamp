using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using AutoMapper;
using Domain = CodeCamp.Domain;
using CodeCampApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace CodeCampApp.Pages.Locations
{
    public class EditModel : PageModel
    {
        // Private members .......................
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<ListModel> _logger;
        private readonly Domain.Repositories.ICampRepository _campRepository;
        private IWebHostEnvironment _environment;

        // Properties .....................................
        // Bind property will help populate the Location property upon a POST operation
        // BindProperty works for POST operation by default
        [BindProperty]
        public LocationModel LocationModel { get; set; }

        // Construrctors ........................
        public EditModel(IConfiguration config, ILogger<ListModel> logger,
            IMapper mapper, Domain.Repositories.ICampRepository campRepository, IWebHostEnvironment environment)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _campRepository = campRepository;
            _environment = environment;
        }

        // Methods ..............................
        public IActionResult OnGet(int? locationId)
        {
            string imageBase64Data = null;
            string imageDataURL = "https://via.placeholder.com/200";

            // Depending if this program is called from EDIT or ADD
            // The parameter locationId can have a value or not.
            // If it does, search for the Location and display it, 
            // otherwise, create a new one and display empty fields
            if (locationId.HasValue)
            {
                // Retreive the deail for the selected model 
                Domain.Entities.Location domainLocation = _campRepository.GetLocationById(locationId.Value);

                // Map data from Domain Entity to App Model
                // Note: Auto mapping does not include image filename and data
                LocationModel = _mapper.Map<LocationModel>(domainLocation);

                if (domainLocation.ProfileImageData != null)
                {
                    if (domainLocation.ProfileImageData != null)
                    {
                        imageBase64Data = Convert.ToBase64String(domainLocation.ProfileImageData);
                        imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                    }
                }
            }
            else
            {
                LocationModel = new LocationModel();
            }

            // Map manually the image name 
            LocationModel.ProfileImageFilename = imageDataURL;
            LocationModel.ProfileImageFormFile = null;

            if (LocationModel == null)
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
            // Validation enforces Model attributes indicated restrictions
            if (!ModelState.IsValid)
            {
                // If input is not valid, render again and return
                // to allow user to correct problems
                return Page();
            }

            // Domain entity to add or update
            Domain.Entities.Location domainLocationToAddOrUpd = null;

            // Determine if will be an ADD or Update
            if (LocationModel.Id > 0)
            {
                // UPDATE operation
                // Convert Model into Domain entity
                // Note: Auto mapping does not include image filename and data
                domainLocationToAddOrUpd = _mapper.Map<Domain.Entities.Location>(LocationModel);
            }
            else
            {
                // ADD operation
                // Convert Model into Domain entity
                // Note: Auto mapping does not include image filename and data
                domainLocationToAddOrUpd = _mapper.Map<Domain.Entities.Location>(LocationModel);
            }

            // Map manually the image filename and data to teh new or updated record
            if (LocationModel.ProfileImageFormFile != null)
            {
                // sync new filenane selected to the string representation in the model
                LocationModel.ProfileImageFilename = LocationModel.ProfileImageFormFile.FileName;
                // Update the image filename in the Domain entity
                domainLocationToAddOrUpd.ProfileImageFilename = LocationModel.ProfileImageFormFile.FileName;

                // Convert image into a memory string and into the Entity byte array
                using (MemoryStream ms = new MemoryStream())
                {
                    await LocationModel.ProfileImageFormFile.CopyToAsync(ms);
                    domainLocationToAddOrUpd.ProfileImageData = ms.ToArray();
                }
            }

            // Run Add or update operation at repository 
            if (LocationModel.Id > 0)
            {
                // Update Domain Entity
                _campRepository.Update<Domain.Entities.Location>(domainLocationToAddOrUpd);
            }
            else
            {
                // Add the location to teh list
                // Note: Auto mapping does not include image filename and data
                _campRepository.Add<Domain.Entities.Location>(domainLocationToAddOrUpd);

                // Copy data from Domain DTO to App Model, to reflect the new ID
                // Note: Auto mapping does not include image filename and data
                LocationModel = _mapper.Map<LocationModel>(domainLocationToAddOrUpd);
            }

            // Commit changes in repository
            _campRepository.CommitChanges();

            // Send to the paget that will be redirected, a message through TempData
            // to inform the user about the operation that took place. 
            TempData["ActionMessage"] = "Location saved!";

            // IMPORTANT: For ADD, EDIT, or DELETE operations allways redirect to a different page
            // Force re-direction to detail page, do not stay in curent page
            return RedirectToPage("./Detail", new { locationId = this.LocationModel.Id });
        }



    }
}