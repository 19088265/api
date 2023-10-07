namespace Architecture.Models
{
    public interface IApplicationRepository
    {
        // Application
        Task<Application[]> GetAllApplicationAsync();
        Task<Application> GetOneApplicationAsync(Guid applicationId);
        Task<Application> AddApplication(Application newApplication);
        Task<Application> EditApplication(Application editedApplication);
        bool DeleteApplication(Guid ID);



        // Application  Type
        Task<ApplicationType[]> GetApplicationTypeAsync();
        Task<ApplicationType> GetOneApplicationTypeAsync(Guid applicationTypeId);
        Task<ApplicationType> AddApplicationType(ApplicationType newApplicationType);
        Task<ApplicationType> EditApplicationType(ApplicationType editedApplicationType);
        bool DeleteApplicationType(Guid ID);


        // Application  Status
        Task<ApplicationStatus[]> GetApplicationStatusAsync();
        Task<ApplicationStatus> GetOneApplicationStatusAsync(Guid applicationStatusId);
        Task<ApplicationStatus> AddApplicationStatus(ApplicationStatus newApplicationStatus);
        Task<ApplicationStatus> EditApplicationStatus(ApplicationStatus editedApplicationStatus);
        bool DeleteApplicationStatus(Guid ID);
    }
}
