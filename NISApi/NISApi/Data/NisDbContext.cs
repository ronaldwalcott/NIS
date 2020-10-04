using Microsoft.EntityFrameworkCore;
using NISApi.Data.Entity;
using NISApi.Data.Entity.Employer;
using NISApi.Data.Entity.InsuredPerson;
using NISApi.Data.Entity.SystemTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data
{
    public class NisDbContext : DbContext
    {
        public NisDbContext(DbContextOptions<NisDbContext> options)
                : base(options)
        {
        }

        public DbSet<TableCountry> TableCountries { get; set; }
        public DbSet<TableCollection> TableCollections { get; set; }
        public DbSet<TableDistrict> TableDistricts { get; set; }
        public DbSet<TableDocumentType> TableDocumentTypes { get; set; }
        public DbSet<TableEmploymentType> TableEmploymentTypes { get; set; }
        public DbSet<TableIndustry> TableIndustries { get; set; }
        public DbSet<TableMaritalStatus> TableMaritalStatuses { get; set; }
        public DbSet<TableNationality> TableNationalities { get; set; }
        public DbSet<TableOccupation> TableOccupations { get; set; }
        public DbSet<TableParish> TableParishes { get; set; }
        public DbSet<TablePostalCode> TablePostalCodes { get; set; }
        public DbSet<TablePostOffice> TablePostOffices { get; set; }
        public DbSet<TableStreet> TableStreets { get; set; }

        public DbSet<ERBasic> ERBasics { get; set; }
        public DbSet<ERBasic2> ERBasic2s { get; set; }
        public DbSet<ERContact> ERContacts { get; set; }
        public DbSet<ERDirector> ERDirectors { get; set; }
        public DbSet<ERMaster> ERMasters { get; set; }

        public DbSet<IPAddress> IPAddresses { get; set; }
        public DbSet<IPAddressApproved> IPAddressApproveds { get; set; }
        public DbSet<IPBasic> IPBasics { get; set; }
        public DbSet<IPContact> IPContacts { get; set; }
        public DbSet<IPEmploymentTypeApproval> IPEmploymentTypeApprovals { get; set; }
        public DbSet<IPMaster> IPMasters { get; set; }
        public DbSet<IPOccupationApproved> IPOccupationApproveds { get; set; }
        public DbSet<IPRegEmployer> IPRegEmployers { get; set; }
        public DbSet<IPRegistrationDocument> IPRegistrationDocuments { get; set; }
        public DbSet<IPSpouse> IPSpouses { get; set; }


        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TableCountry>().ToTable("TableCountry");
            modelBuilder.Entity<TableCollection>().ToTable("TableCollection");
            modelBuilder.Entity<TableDistrict>().ToTable("TableDistrict");
            modelBuilder.Entity<TableDocumentType>().ToTable("TableDocumentType");
            modelBuilder.Entity<TableEmploymentType>().ToTable("TableEmploymentType");
            modelBuilder.Entity<TableIndustry>().ToTable("TableIndustry");
            modelBuilder.Entity<TableMaritalStatus>().ToTable("TableMaritalStatus");
            modelBuilder.Entity<TableNationality>().ToTable("TableNationality");
            modelBuilder.Entity<TableOccupation>().ToTable("TableOccupation");
            modelBuilder.Entity<TableParish>().ToTable("TableParish");
            modelBuilder.Entity<TablePostalCode>().ToTable("TablePostalCode");
            modelBuilder.Entity<TablePostOffice>().ToTable("TablePostOffice");
            modelBuilder.Entity<TableStreet>().ToTable("TableStreet");


            modelBuilder.Entity<ERBasic>().ToTable("ERBasic");
            modelBuilder.Entity<ERBasic2>().ToTable("ERBasic2");
            modelBuilder.Entity<ERContact>().ToTable("ERContact");
            modelBuilder.Entity<ERDirector>().ToTable("ERDirector");
            modelBuilder.Entity<ERMaster>().ToTable("ERMaster");

            modelBuilder.Entity<IPAddress>().ToTable("IPAddress");
            modelBuilder.Entity<IPAddressApproved>().ToTable("IPAddressApproved");
            modelBuilder.Entity<IPBasic>().ToTable("IPBasic");
            modelBuilder.Entity<IPContact>().ToTable("IPContact");
            modelBuilder.Entity<IPEmploymentTypeApproval>().ToTable("IPEmploymentTypeApproval");
            modelBuilder.Entity<IPMaster>().ToTable("IPMaster");
            modelBuilder.Entity<IPOccupationApproved>().ToTable("IPOccupationApproved");
            modelBuilder.Entity<IPRegEmployer>().ToTable("IPRegEmployer");
            modelBuilder.Entity<IPRegistrationDocument>().ToTable("IPRegistrationDocument");
            modelBuilder.Entity<IPSpouse>().ToTable("IPSpouse");
            modelBuilder.Entity<IPSSN>().ToTable("IPSSN");


            modelBuilder.Entity<TableCountry>()
                .HasAlternateKey(t => t.Code);
            modelBuilder.Entity<TableCountry>()
                .HasIndex(t => t.ShortDescription);

            modelBuilder.Entity<TableCollection>()
                .HasAlternateKey(t => t.Code);
            modelBuilder.Entity<TableCollection>()
                .HasIndex(t => t.ShortDescription);

            modelBuilder.Entity<TableDistrict>()
                .HasAlternateKey(t => t.Code);
            modelBuilder.Entity<TableDistrict>()
                .HasIndex(t => t.ShortDescription);

            modelBuilder.Entity<TableDocumentType>()
                .HasAlternateKey(t => t.Code);
            modelBuilder.Entity<TableDocumentType>()
                .HasIndex(t => t.ShortDescription);

            modelBuilder.Entity<TableEmploymentType>()
                .HasAlternateKey(t => t.Code);
            modelBuilder.Entity<TableEmploymentType>()
                .HasIndex(t => t.ShortDescription);

            modelBuilder.Entity<TableIndustry>()
                .HasAlternateKey(t => t.Code);
            modelBuilder.Entity<TableIndustry>()
                .HasIndex(t => t.ShortDescription);

            modelBuilder.Entity<TableMaritalStatus>()
                .HasAlternateKey(t => t.Code);
            modelBuilder.Entity<TableMaritalStatus>()
                .HasIndex(t => t.ShortDescription);

            modelBuilder.Entity<TableNationality>()
                .HasAlternateKey(t => t.Code);
            modelBuilder.Entity<TableNationality>()
                .HasIndex(t => t.ShortDescription);

            modelBuilder.Entity<TableOccupation>()
                .HasAlternateKey(t => t.Code);
            modelBuilder.Entity<TableOccupation>()
                .HasIndex(t => t.ShortDescription);

            modelBuilder.Entity<TableParish>()
                .HasAlternateKey(t => t.Code);
            modelBuilder.Entity<TableParish>()
                .HasIndex(t => t.ShortDescription);

            modelBuilder.Entity<TablePostalCode>()
                .HasAlternateKey(t => t.Code);
            modelBuilder.Entity<TablePostalCode>()
                .HasIndex(t => t.ShortDescription);

            modelBuilder.Entity<TablePostOffice>()
                .HasAlternateKey(t => t.Code);
            modelBuilder.Entity<TablePostOffice>()
                .HasIndex(t => t.ShortDescription);

            modelBuilder.Entity<TableStreet>()
                .HasAlternateKey(t => t.Code);
            modelBuilder.Entity<TableStreet>()
                .HasIndex(t => t.ShortDescription);

            modelBuilder.Entity<ERMaster>()
                .HasAlternateKey(t => t.EmployerName);
            modelBuilder.Entity<ERMaster>()
                .HasAlternateKey(t => t.TradeName);
            modelBuilder.Entity<ERMaster>()
                .HasAlternateKey(t => t.RegistrationNumber);

            modelBuilder.Entity<IPSSN>()
                .HasAlternateKey(t => t.SSN);


            //modelBuilder.Entity<IPMaster>()
            //    .HasIndex(t => t.SSN)
            //    .IsUnique();
            modelBuilder.Entity<IPMaster>()
                .HasIndex(t => new { t.Surname, t.FirstName, t.MiddleName });


            modelBuilder.Entity<TableCollection>().HasQueryFilter(t => t.IsDeleted == false || t.IsDeleted == null);
            modelBuilder.Entity<TableCountry>().HasQueryFilter(t => t.IsDeleted == false || t.IsDeleted == null);
            modelBuilder.Entity<TableDistrict>().HasQueryFilter(t => t.IsDeleted == false || t.IsDeleted == null);
            modelBuilder.Entity<TableDocumentType>().HasQueryFilter(t => t.IsDeleted == false || t.IsDeleted == null);
            modelBuilder.Entity<TableEmploymentType>().HasQueryFilter(t => t.IsDeleted == false || t.IsDeleted == null);
            modelBuilder.Entity<TableIndustry>().HasQueryFilter(t => t.IsDeleted == false || t.IsDeleted == null);
            modelBuilder.Entity<TableMaritalStatus>().HasQueryFilter(t => t.IsDeleted == false || t.IsDeleted == null);
            modelBuilder.Entity<TableNationality>().HasQueryFilter(t => t.IsDeleted == false || t.IsDeleted == null);
            modelBuilder.Entity<TableOccupation>().HasQueryFilter(t => t.IsDeleted == false || t.IsDeleted == null);
            modelBuilder.Entity<TableParish>().HasQueryFilter(t => t.IsDeleted == false || t.IsDeleted == null);
            modelBuilder.Entity<TablePostalCode>().HasQueryFilter(t => t.IsDeleted == false || t.IsDeleted == null);
            modelBuilder.Entity<TablePostOffice>().HasQueryFilter(t => t.IsDeleted == false || t.IsDeleted == null);
            modelBuilder.Entity<TableStreet>().HasQueryFilter(t => t.IsDeleted == false || t.IsDeleted == null);





            //modelBuilder.Entity<Test>()
            //    .HasIndex(t => t.Code)
            //    .IsUnique();

            //modelBuilder.Entity<Test>()
            //    .Property(t => t.ModifiedDate)
            //    .HasDefaultValueSql("Now()");

            //modelBuilder.Entity<Test>()
            //    .Property(t => t.Rowversion)
            //    .HasDefaultValueSql("Now()");


        }
    }
}
