﻿using Data.Models;
using HealthcareApp.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Numerics;

namespace HealthcareApp.Data
{
    public class HealthcareAppDbContext : IdentityDbContext
    {
        public HealthcareAppDbContext(DbContextOptions<HealthcareAppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Medication> Medications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-V8J7KQR\\SQLEXPRESS;Database=HealthcareDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            modelBuilder.Entity<Attendance>().ToTable("Attendances");
            modelBuilder.Entity<Medication>().ToTable("Medications");

            //Attendance
            modelBuilder.Entity<Attendance>()
                .HasMany<Medication>(a => a.Medications);

            //Appointment
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient);

            // Doctor
            modelBuilder.Entity<Doctor>()
                .HasMany<Patient>(d => d.Patients)
                .WithOne(p => p.PersonalDoctor)
                .HasForeignKey(p => p.PersonalDoctorId);

            modelBuilder.Entity<Doctor>()
                .HasMany<Appointment>(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Patient
            modelBuilder.Entity<Patient>()
                .HasMany<Appointment>(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Patient>()
                .HasMany<Attendance>(p => p.Attendances);

            modelBuilder.Entity<Patient>()
                .HasOne<Doctor>(p => p.PersonalDoctor)
                .WithMany(d => d.Patients)
                .HasForeignKey(p => p.PersonalDoctorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}