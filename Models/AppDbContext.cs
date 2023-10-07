using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //Inventory
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryType> InventoryType { get; set; }
        //public DbSet<Program> Program { get; set; }

        //Employee
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeType> EmployeeType { get; set; }

        //Application
        public DbSet<Application> Application { get; set; }
        public DbSet<ApplicationType> ApplicationType { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatus { get; set; }

        //Student
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentType> StudentType { get; set; }

        public DbSet<Beneficiary> Beneficiary { get; set; }
        public DbSet<Cafeteria> Cafeteria { get; set; }
        public DbSet<Sponsor> Sponsor { get; set; }
        public DbSet<SponsorType> SponsorType { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<AttendanceType> AttendanceType { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<CafeteriaType> CafeteriaType { get; set; }
        public DbSet<Donation> Donation { get; set; }
        public DbSet<DonationType> DonationType { get; set; }
        public DbSet<BookGenre> BookGenre { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookStatus> BookStatus { get; set; }
        public DbSet<CheckIn> CheckIn { get; set; }
        public DbSet<CheckOut> CheckOut { get; set; }
        public DbSet<Program> Program { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<ShopItem> ShopItem { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Suburb> Suburb { get; set; }
        public DbSet<ToDo> Tasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Employee
            modelBuilder.Entity<Employee>().HasKey(x => x.EmployeeId);

            modelBuilder.Entity<Employee>()
               .HasOne(e => e.EmployeeType).WithMany().HasForeignKey(e => e.EmployeeTypeId);


            //Task
            modelBuilder.Entity<ToDo>().HasKey(x => x.TaskId);

            // Employee type
            modelBuilder.Entity<EmployeeType>().HasKey(x => x.EmployeeTypeId);

            //Programs
            modelBuilder.Entity<Program>().HasKey(x => x.ProgramId);

            //Inventory
            modelBuilder.Entity<Inventory>().HasKey(x => x.InventoryId);

            // Inventory type
            modelBuilder.Entity<InventoryType>().HasKey(x => x.InventoryTypeId);

            //Application
            modelBuilder.Entity<Application>().HasKey(x => x.ApplicationId);

            //Application Type
            modelBuilder.Entity<ApplicationType>().HasKey(x => x.ApplicationTypeId);

            //ApplicationStatus
            modelBuilder.Entity<ApplicationStatus>().HasKey(x => x.ApplicationStatusId);
            
            //Student
            modelBuilder.Entity<Student>().HasKey(x => x.StudentId);

            //Student Type
            modelBuilder.Entity<StudentType>().HasKey(x => x.StudentTypeId);


            //Beneficiary
            modelBuilder.Entity<Beneficiary>().HasKey(x => x.BeneficiaryId);
            //Cafeteria
            modelBuilder.Entity<Cafeteria>().HasKey(x => x.CafeteriaId);
            //Sponsor
            modelBuilder.Entity<Sponsor>().HasKey(x => x.SponsorId);
            //Attendance
            modelBuilder.Entity<Attendance>().HasKey(x => x.AttendanceId);
            //Province
            modelBuilder.Entity<Province>().HasKey(x => x.ProvinceId);
            //City
            modelBuilder.Entity<City>().HasKey(x => x.CityId);
            //Applicatiion
            modelBuilder.Entity<Application>().HasKey(x => x.ApplicationId);
            //ApplicationType
            modelBuilder.Entity<ApplicationType>().HasKey(x => x.ApplicationTypeId);
            //CafeteriaType
            modelBuilder.Entity<CafeteriaType>().HasKey(x => x.CafeteriaTypeId);
            //Donation
            modelBuilder.Entity<Donation>().HasKey(c => c.DonationId);
            //DonationType
            modelBuilder.Entity<DonationType>().HasKey(c => c.DonationTypeId);
            //BookGenre
            modelBuilder.Entity<BookGenre>().HasKey(b => b.BookGenreId);
            //Book
            modelBuilder.Entity<Book>().HasKey(b => b.BookId);
            //BookStatus
            modelBuilder.Entity<BookStatus>().HasKey(b => b.BookStatusId);
            //CheckIn
            modelBuilder.Entity<CheckIn>().HasKey(b => b.CheckInId);
            //CheckOut
            modelBuilder.Entity<CheckOut>().HasKey(c => c.CheckOutId);
            //Program
            modelBuilder.Entity<Program>().HasKey(c => c.ProgramId);
            //Schedule
            modelBuilder.Entity<Schedule>().HasKey(c => c.ScheduleId);
            //ShopItem
            modelBuilder.Entity<ShopItem>().HasKey(s => s.ShopItemId);
            //Item
            modelBuilder.Entity<Items>().HasKey(s => s.ItemId);
            //Invoice
            modelBuilder.Entity<Invoice>().HasKey(c => c.InvoiceId);
            //Payment
            modelBuilder.Entity<Payment>().HasKey(p => p.PaymentId);
            //PaymentType
            modelBuilder.Entity<PaymentType>().HasKey(p => p.PaymentTypeId);
            //Suburb
            modelBuilder.Entity<Suburb>().HasKey(s => s.SuburbId);


            modelBuilder.Entity<Province>()
            .HasIndex(p => p.ProvinceName)
            .IsUnique();

            modelBuilder.Entity<SponsorType>()
                .HasIndex(s => s.SponsorTypeDescription)
                .IsUnique();

            modelBuilder.Entity<DonationType>()
                .HasIndex(d => d.DonationTypeDescription)
                .IsUnique();

            modelBuilder.Entity<City>()
                .HasIndex(c => c.CityName)
                .IsUnique();

            modelBuilder.Entity<CafeteriaType>()
                .HasIndex(ct => ct.CafeteriaTypeDescription)
                .IsUnique();

            modelBuilder.Entity<AttendanceType>()
                .HasIndex(a => a.AttendanceTypeDescription)
                .IsUnique();

            modelBuilder.Entity<Beneficiary>()
                .HasIndex(b => b.BeneficiaryIdNumber)
                .IsUnique();

            modelBuilder.Entity<Attendance>()
                .HasIndex(a => a.CafeteriaId)
                .IsUnique();

            modelBuilder.Entity<BookGenre>()
                .HasIndex(b => b.GenreDescription)
                .IsUnique();

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.Isbn)
                .IsUnique();


            modelBuilder.Entity<BookStatus>()
                .HasIndex(b => b.BookDescription)
                .IsUnique();

            modelBuilder.Entity<Suburb>()
                .HasIndex(s => s.SuburbName)
                .IsUnique();




            modelBuilder.Entity<Sponsor>();
            //.HasOne(s => s.SponsorType)
            //.WithMany(st => st.Sponsors)
            //.HasForeignKey(s => s.SponsorTypeId);

            modelBuilder.Entity<City>();
                //.HasOne(s => s.Province)
                //.WithMany(st => st.City)
                //.HasForeignKey(s => s.ProvinceId);

            modelBuilder.Entity<Attendance>();
                //.HasOne(s => s.AttendanceType)
                //.WithMany(st => st.Attendances)
                //.HasForeignKey(s => s.AttendanceTypeId);

            modelBuilder.Entity<Cafeteria>();
                //.HasOne(s => s.CafeteriaType)
                //.WithMany(st => st.Cafeterias)
                //.HasForeignKey(s => s.CafeteriaTypeId);


            //modelBuilder.Entity<Attendance>()
            // .HasOne(s => s.AttendanceType)
            //.WithMany(st => st.Attendances)
            //.HasForeignKey(s =>s.AttendanceTypeId);

            //modelBuilder.Entity<Attendance>()
            //  .HasOne(s => s.Beneficiary)
            //.WithMany(st => st.Attendance)
            //.HasForeignKey(s => s.BeneficiaryId);

            //modelBuilder.Entity<Attendance>()
            //  .HasOne(s => s.Cafeteria)
            //.WithMany(st => st.Attendance)
            //.HasForeignKey(s =>s.CafeteriaId);
            //AttendanceType
            modelBuilder.Entity<AttendanceType>().HasKey(x => x.AttendanceTypeId);

            //SponsorType
            modelBuilder.Entity<SponsorType>().HasKey(x => x.SponsorTypeId);

        }
    }
}