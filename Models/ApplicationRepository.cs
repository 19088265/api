using System.Linq;
using Microsoft.EntityFrameworkCore;
using Architecture.Models;

namespace Architecture.Models
{
    public class ApplicationRepository: IApplicationRepository
    {
        private readonly AppDbContext _appDbContext;

        public ApplicationRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //

        //View applications
        public async Task<Application[]> GetAllApplicationAsync()
        {
            IQueryable<Application> query = _appDbContext.Application.Include(e => e.ApplicationType).Include(e => e.ApplicationStatus);
            return await query.ToArrayAsync();
        }

        //Get one record
        public async Task<Application> GetOneApplicationAsync(Guid applicationId)
        {
            IQueryable<Application> query = _appDbContext.Application.Where(c => c.ApplicationId == applicationId).Include(e => e.ApplicationType).Include(e => e.ApplicationStatus);
            return await query.FirstOrDefaultAsync();
        }

        //Add Application
        public async Task<Application> AddApplication(Application newApplication)
        {
            _appDbContext.Application.Add(newApplication);
            await _appDbContext.SaveChangesAsync();
            return newApplication;
        }

        //Update application
        public async Task<Application> EditApplication(Application editedApplication)
        {
            _appDbContext.Entry(editedApplication).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return editedApplication;
        }

        //Delete application 
        public bool DeleteApplication(Guid ID)
        {
            var application = _appDbContext.Application.FirstOrDefault(p => p.ApplicationId == ID);
            if (application != null)
            {
                _appDbContext.Application.Remove(application);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        //////////////////////////////////////////////////////////////////////////////
        //View Application types
        public async Task<ApplicationType[]> GetApplicationTypeAsync()
        {
            IQueryable<ApplicationType> query = _appDbContext.ApplicationType;
            return await query.ToArrayAsync();
        }

        //Get one record
        public async Task<ApplicationType> GetOneApplicationTypeAsync(Guid applicationTypeId)
        {
            IQueryable<ApplicationType> query = _appDbContext.ApplicationType.Where(c => c.ApplicationTypeId == applicationTypeId);
            return await query.FirstOrDefaultAsync();
        }

        //Add Application type
        public async Task<ApplicationType> AddApplicationType(ApplicationType newApplicationType)
        {
            _appDbContext.ApplicationType.Add(newApplicationType);
            await _appDbContext.SaveChangesAsync();
            return newApplicationType;
        }

        //Update application type
        public async Task<ApplicationType> EditApplicationType(ApplicationType editedApplicationType)
        {
            _appDbContext.Entry(editedApplicationType).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return editedApplicationType;
        }

        //Delete application type
        public bool DeleteApplicationType(Guid ID)
        {
            var applicationType = _appDbContext.ApplicationType.FirstOrDefault(p => p.ApplicationTypeId == ID);

            if (applicationType != null)
            {
                // Check if the application type is referenced in the employee table
                var isReferencedInApplication = _appDbContext.Application.Any(e => e.ApplicationTypeId == ID);

                if (isReferencedInApplication)
                {
                    // If referenced, do not delete and return false
                    return false;
                }
                else
                {
                    _appDbContext.ApplicationType.Remove(applicationType);
                    _appDbContext.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }



        //View Application Statuses
        public async Task<ApplicationStatus[]> GetApplicationStatusAsync()
        {
            IQueryable<ApplicationStatus> query = _appDbContext.ApplicationStatus;
            return await query.ToArrayAsync();
        }

        //Get one record
        public async Task<ApplicationStatus> GetOneApplicationStatusAsync(Guid applicationStatusId)
        {
            IQueryable<ApplicationStatus> query = _appDbContext.ApplicationStatus.Where(c => c.ApplicationStatusId == applicationStatusId);
            return await query.FirstOrDefaultAsync();
        }

        //Add Application status
        public async Task<ApplicationStatus> AddApplicationStatus(ApplicationStatus newApplicationStatus)
        {
            _appDbContext.ApplicationStatus.Add(newApplicationStatus);
            await _appDbContext.SaveChangesAsync();
            return newApplicationStatus;
        }

        //Update Application Status
        public async Task<ApplicationStatus> EditApplicationStatus(ApplicationStatus editedApplicationStatus)
        {
            _appDbContext.Entry(editedApplicationStatus).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return editedApplicationStatus;
        }

        //Delete application status
        public bool DeleteApplicationStatus(Guid ID)
        {
            var applicationStatus = _appDbContext.ApplicationStatus.FirstOrDefault(p => p.ApplicationStatusId == ID);

            if (applicationStatus != null)
            {
                // Update related records with ApplicationTypeId set to NULL
                var relatedStatus = _appDbContext.Application.Where(r => r.ApplicationStatusId == ID);
                foreach (var related in relatedStatus)
                {
                    related.ApplicationStatusId = null;
                }

                _appDbContext.ApplicationStatus.Remove(applicationStatus);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
