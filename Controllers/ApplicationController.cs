using Architecture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;

namespace Architecture.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {

        private readonly IApplicationRepository _applicationRepository;
        private readonly AppDbContext _appDbContext;

        public ApplicationController(IApplicationRepository applicationRepository, AppDbContext appDbContext)
        {
            _applicationRepository = applicationRepository;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("UploadConsentForm")]
        public async Task<IActionResult> UploadFile(IFormFile file, Guid applicationId)
        {
            if (file != null && file.Length > 0)
            {
                if (file.ContentType != "application/pdf")
                {
                    // Handle invalid file type (not a PDF)
                    ModelState.AddModelError("file", "The uploaded file must be a PDF.");
                    return StatusCode(500, "Internal Server Error. Please contact support.");
                }

                // Read the file into a byte array
                byte[] fileData;
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    fileData = stream.ToArray();
                }

                // Convert the byte array to a base64 encoded string
                string base64File = Convert.ToBase64String(fileData);
                //Console.WriteLine(base64File);

                // Find the application by applicationId
                var application = _appDbContext.Application.FirstOrDefault(a => a.ApplicationId == applicationId);

                if (application != null)
                {
                    application.ConsentForm = base64File; // Assign the PDF data to the ConsentForm property
                    _appDbContext.Update(application);
                    await _appDbContext.SaveChangesAsync();
                }

                // Redirect to a success page or return JSON response
                return Ok("Success" + base64File);
            }

            // Handle invalid file uploads

            return StatusCode(500, "Internal Server Error. Please contact support.");
        }

        [HttpGet]
        [Route("DownloadConsentForm/{applicationId}")]
        public async Task<IActionResult> DownloadConsentForm(Guid applicationId)
        {
            // Find the application by applicationId
            var application = _appDbContext.Application.FirstOrDefault(a => a.ApplicationId == applicationId);

            if (application != null && !string.IsNullOrEmpty(application.ConsentForm))
            {
                try
                {
                    // Convert the base64 encoded string back to a byte array
                    byte[] fileData = Convert.FromBase64String(application.ConsentForm);

                    // Set the content type to "application/pdf" or the appropriate file type
                    const string contentType = "application/pdf";

                    // Return the file as a stream
                    return File(fileData, contentType, "ConsentForm.pdf");
                }
                catch (Exception ex)
                {
                    // Handle any exceptions, e.g., invalid base64 data
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }

            // Handle the case where the application or file data is missing
            return NotFound();
        }

        //View applications
        [HttpGet]
        [Route("GetAllApplications")]
        public async Task<IActionResult> GetAllApplication()
        {
            try
            {
                var results = await _applicationRepository.GetAllApplicationAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //Get one record
        [HttpGet]
        [Route("GetApplication/{applicationId}")]
        public async Task<IActionResult> GetOneApplicationAsync(Guid applicationId)
        {
            try
            {
                var result = await _applicationRepository.GetOneApplicationAsync(applicationId);

                if (result == null) return NotFound("Application does not exist. You need to create it first");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        // Add application to the database
        [HttpPost]
        [Route("AddApplication")]
        public async Task<IActionResult> Post(Application application)
        {

            // Get the existing application type and status by ID
            var existingApplicationType = await _applicationRepository.GetOneApplicationTypeAsync((Guid)application.ApplicationTypeId);
            var existingApplicationStatus = await _applicationRepository.GetOneApplicationStatusAsync((Guid)application.ApplicationStatusId);

            if (existingApplicationType == null || existingApplicationStatus == null)
            {
                return BadRequest("Invalid application type/status ID"); // Return a 400 Bad Request if the appplication type/status ID  doesn't exist
            }

            // Set the employee's type to the existing one
            application.ApplicationType = existingApplicationType;
            application.ApplicationStatus = existingApplicationStatus;

            var result = await _applicationRepository.AddApplication(application);
            if (result == null || result.ApplicationId == Guid.Empty)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }

            // Update the application's status to "In progress" in the Application table
            var app = await _applicationRepository.GetOneApplicationAsync(application.ApplicationId);
            if (app != null)
            {
                app.ApplicationStatusId = new Guid("A651F95C-C4E3-4798-8F0D-7FC0AB29699A"); // Choose status
                await _applicationRepository.EditApplication(app);
            }

            return Ok("Application added successfully");
        }

        // Edit Application Type
        [HttpPut]
        [Route("EditApplication")]
        public async Task<IActionResult> Put(Guid id, Models.Application editedApplication)
        {
            editedApplication.ApplicationId = id;
            await _applicationRepository.EditApplication(editedApplication);
            return Ok("Application edited successfully");
        }

        // Delete application
        [HttpDelete]
        [Route("DeleteApplication/{id}")]
        public JsonResult DeleteApplication(Guid id)
        {
            _applicationRepository.DeleteApplication(id);
            return new JsonResult("Application deleted successfully");
        }

        /////////////////////////////////////////////////////////////////////
        
        //Get all application types
        [HttpGet]
        [Route("GetAllApplicationType")]
        public async Task<IActionResult> GetAllApplicationType()
        {
            try
            {
                var results = await _applicationRepository.GetApplicationTypeAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //Get one record
        [HttpGet]
        [Route("GetApplicationType/{applicationTypeId}")]
        public async Task<IActionResult> GetOneApplicationTypeAsync(Guid applicationTypeId)
        {
            try
            {
                var result = await _applicationRepository.GetOneApplicationTypeAsync(applicationTypeId);

                if (result == null) return NotFound("Application type does not exist. You need to create it first");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        // Add an application type to the database
        [HttpPost]
        [Route("AddApplicationType")]
        public async Task<IActionResult> Post(ApplicationType applicationType)
        {
            var result = await _applicationRepository.AddApplicationType(applicationType);
            if (result.ApplicationTypeId == Guid.Empty)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
            return Ok("Application type added successfully");
        }

        // Edit Application Type
        [HttpPut]
        [Route("EditApplicationType")]
        public async Task<IActionResult> Put(Guid id, Models.ApplicationType editedApplicationType)
        {
            editedApplicationType.ApplicationTypeId = id;
            await _applicationRepository.EditApplicationType(editedApplicationType);
            return Ok("Application type edited successfully");
        }

        // Delete application type
        [HttpDelete]
        [Route("DeleteApplicationType/{ID}")]
        public IActionResult DeleteApplicationType(Guid ID)
        {
            try
            {
                bool isDeleted = _applicationRepository.DeleteApplicationType(ID);

                if (isDeleted)
                {
                    return Ok(new { message = "Application type deleted successfully" });
                }
                else
                {
                    return NotFound(new { message = "Application type not found or referenced by application, deletion failed" });
                }
            }
            catch (Exception)
            {
                // Log the exception for debugging purposes.
                // You can also customize the error message as needed.
                return StatusCode(500, new { message = "An error occurred while deleting the application type" });
            }
        }

        //View all Application Statuses
        [HttpGet]
        [Route("GetAllApplicationStatus")]
        public async Task<IActionResult> GetAllApplicationStatus()
        {
            try
            {
                var results = await _applicationRepository.GetApplicationStatusAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //Get one record
        [HttpGet]
        [Route("GetApplicationStatus/{applicationStatusId}")]
        public async Task<IActionResult> GetOneApplicationStatusAsync(Guid applicationStatusId)
        {
            try
            {
                var result = await _applicationRepository.GetOneApplicationStatusAsync(applicationStatusId);

                if (result == null) return NotFound("Application status does not exist. You need to create it first");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        // Add an application status to the database
        [HttpPost]
        [Route("AddApplicationStatus")]
        public async Task<IActionResult> Post(ApplicationStatus applicationStatus)
        {
            var result = await _applicationRepository.AddApplicationStatus(applicationStatus);
            if (result.ApplicationStatusId == Guid.Empty)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
            return Ok("Application status added successfully");
        }

        // Edit Application Status
        [HttpPut]
        [Route("EditApplicationStatus")]
        public async Task<IActionResult> Put(Guid id, Models.ApplicationStatus editedApplicationStatus)
        {
            editedApplicationStatus.ApplicationStatusId = id;
            await _applicationRepository.EditApplicationStatus(editedApplicationStatus);
            return Ok("Application status edited successfully");
        }

        // Delete Application Status
        [HttpDelete]
        [Route("DeleteApplicationStatus/{id}")]
        public JsonResult DeleteApplicationStatus(Guid ID)
        {
            _applicationRepository.DeleteApplicationStatus(ID);
            return new JsonResult("Application status deleted successfully");
        }
    }
}
