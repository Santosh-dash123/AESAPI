using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AESAPI.Models
{
    public partial class AESDBContext : DbContext
    {
        public AESDBContext()
        {
        }

        public AESDBContext(DbContextOptions<AESDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblMCourse> TblMCourses { get; set; } = null!;
        public virtual DbSet<TblMExam> TblMExams { get; set; } = null!;
        public virtual DbSet<TblMExamSetup> TblMExamSetups { get; set; } = null!;
        public virtual DbSet<TblMExamSetupStudent> TblMExamSetupStudents { get; set; } = null!;
        public virtual DbSet<TblMOption> TblMOptions { get; set; } = null!;
        public virtual DbSet<TblMParent> TblMParents { get; set; } = null!;
        public virtual DbSet<TblMQuestionan> TblMQuestionans { get; set; } = null!;
        public virtual DbSet<TblMStudent> TblMStudents { get; set; } = null!;
        public virtual DbSet<TblMStudentAdemic> TblMStudentAdemics { get; set; } = null!;
        public virtual DbSet<TblMStudentCourse> TblMStudentCourses { get; set; } = null!;
        public virtual DbSet<TblMUser> TblMUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblMCourse>(entity =>
            {
                entity.HasKey(e => e.CoursId)
                    .HasName("PK__Tbl_M_Co__2836A06F19478C5E");

                entity.ToTable("Tbl_M_Courses");

                entity.Property(e => e.CoursId).HasColumnName("cours_id");

                entity.Property(e => e.CoursDuration)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cours_duration");

                entity.Property(e => e.CoursFee)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("cours_fee");

                entity.Property(e => e.CoursName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cours_name");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("created_ip");

                entity.Property(e => e.DFlag).HasColumnName("d_flag");

                entity.Property(e => e.ModifyBy).HasColumnName("modify_by");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_date");

                entity.Property(e => e.ModifyIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("modify_ip");

                entity.Property(e => e.TrnerDescr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("trner_descr");

                entity.Property(e => e.TrnerName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("trner_name");
            });

            modelBuilder.Entity<TblMExam>(entity =>
            {
                entity.HasKey(e => e.ExamId)
                    .HasName("PK__Tbl_M_Ex__9C8C7BE9743C3736");

                entity.ToTable("Tbl_M_Exam");

                entity.Property(e => e.ExamId).HasColumnName("exam_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("created_ip");

                entity.Property(e => e.DFlag).HasColumnName("d_flag");

                entity.Property(e => e.ExamDescr)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("exam_descr");

                entity.Property(e => e.ExamDuration).HasColumnName("exam_duration");

                entity.Property(e => e.ExamName)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("exam_name");

                entity.Property(e => e.ExamSubject)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("exam_subject");

                entity.Property(e => e.ModifyBy).HasColumnName("modify_by");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_date");

                entity.Property(e => e.ModifyIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("modify_ip");

                entity.Property(e => e.Passmark).HasColumnName("passmark");

                entity.Property(e => e.Tnoqans).HasColumnName("tnoqans");
            });

            modelBuilder.Entity<TblMExamSetup>(entity =>
            {
                entity.HasKey(e => e.ExamsetupId);

                entity.ToTable("Tbl_M_Exam_Setup");

                entity.Property(e => e.ExamsetupId).HasColumnName("examsetup_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("created_ip");

                entity.Property(e => e.DFlag).HasColumnName("d_flag");

                entity.Property(e => e.ExamDate)
                    .HasColumnType("datetime")
                    .HasColumnName("exam_date");

                entity.Property(e => e.ExamId).HasColumnName("exam_id");

                entity.Property(e => e.Fromtime)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fromtime");

                entity.Property(e => e.ModifyBy).HasColumnName("modify_by");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_date");

                entity.Property(e => e.ModifyIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("modify_ip");

                entity.Property(e => e.Totime)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("totime");
            });

            modelBuilder.Entity<TblMExamSetupStudent>(entity =>
            {
                entity.HasKey(e => e.ExamsetupstdId);

                entity.ToTable("Tbl_M_Exam_Setup_Student");

                entity.Property(e => e.ExamsetupstdId).HasColumnName("examsetupstd_id");

                entity.Property(e => e.AttndStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("attnd_status");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("created_ip");

                entity.Property(e => e.DFlag).HasColumnName("d_flag");

                entity.Property(e => e.ExamsetupId).HasColumnName("examsetup_id");

                entity.Property(e => e.ModifyBy).HasColumnName("modify_by");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_date");

                entity.Property(e => e.ModifyIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("modify_ip");

                entity.Property(e => e.StdId).HasColumnName("std_id");
            });

            modelBuilder.Entity<TblMOption>(entity =>
            {
                entity.HasKey(e => e.OptionId)
                    .HasName("PK__Tbl_M_Op__F4EACE1B76766E84");

                entity.ToTable("Tbl_M_Option");

                entity.Property(e => e.OptionId).HasColumnName("option_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("created_ip");

                entity.Property(e => e.DFlag).HasColumnName("d_flag");

                entity.Property(e => e.ExamId).HasColumnName("exam_id");

                entity.Property(e => e.ModifyBy).HasColumnName("modify_by");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_date");

                entity.Property(e => e.ModifyIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("modify_ip");

                entity.Property(e => e.OptionContent)
                    .IsUnicode(false)
                    .HasColumnName("option_content");

                entity.Property(e => e.OptionStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("option_status");

                entity.Property(e => e.QansId).HasColumnName("qans_id");
            });

            modelBuilder.Entity<TblMParent>(entity =>
            {
                entity.HasKey(e => e.PrntId)
                    .HasName("PK__Tbl_M_Pa__6F6CCED1BD63E705");

                entity.ToTable("Tbl_M_Parents");

                entity.Property(e => e.PrntId).HasColumnName("prnt_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("created_ip");

                entity.Property(e => e.DFlag).HasColumnName("d_flag");

                entity.Property(e => e.ModifyBy).HasColumnName("modify_by");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_date");

                entity.Property(e => e.ModifyIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("modify_ip");

                entity.Property(e => e.PrntAdrs)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prnt_adrs");

                entity.Property(e => e.PrntEmail)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("prnt_email");

                entity.Property(e => e.PrntIncom)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("prnt_incom");

                entity.Property(e => e.PrntMobno)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("prnt_mobno");

                entity.Property(e => e.PrntName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prnt_name");

                entity.Property(e => e.PrntOcuption)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prnt_ocuption");

                entity.Property(e => e.PrntType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prnt_type");

                entity.Property(e => e.StdId).HasColumnName("std_id");
            });

            modelBuilder.Entity<TblMQuestionan>(entity =>
            {
                entity.HasKey(e => e.QansId)
                    .HasName("PK__Tbl_M_Qu__F02310FCA68DE939");

                entity.ToTable("Tbl_M_Questionans");

                entity.Property(e => e.QansId).HasColumnName("qans_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("created_ip");

                entity.Property(e => e.DFlag).HasColumnName("d_flag");

                entity.Property(e => e.ExamId).HasColumnName("exam_id");

                entity.Property(e => e.ModifyBy).HasColumnName("modify_by");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_date");

                entity.Property(e => e.ModifyIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("modify_ip");

                entity.Property(e => e.QansContent)
                    .IsUnicode(false)
                    .HasColumnName("qans_content");
            });

            modelBuilder.Entity<TblMStudent>(entity =>
            {
                entity.HasKey(e => e.StdId)
                    .HasName("PK__Tbl_M_St__0B0245BA5FF50792");

                entity.ToTable("Tbl_M_Student");

                entity.Property(e => e.StdId).HasColumnName("std_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("created_ip");

                entity.Property(e => e.DFlag).HasColumnName("d_flag");

                entity.Property(e => e.ModifyBy).HasColumnName("modify_by");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_date");

                entity.Property(e => e.ModifyIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("modify_ip");

                entity.Property(e => e.StdAdharno)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("std_adharno");

                entity.Property(e => e.StdBldgrp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("std_bldgrp");

                entity.Property(e => e.StdCurntadrs)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("std_curntadrs");

                entity.Property(e => e.StdDob)
                    .HasColumnType("date")
                    .HasColumnName("std_dob");

                entity.Property(e => e.StdEmail)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("std_email");

                entity.Property(e => e.StdFrstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("std_frstname");

                entity.Property(e => e.StdGnder)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("std_gnder");

                entity.Property(e => e.StdLstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("std_lstname");

                entity.Property(e => e.StdMobNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("std_mobNo");

                entity.Property(e => e.StdNationality)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("std_nationality");

                entity.Property(e => e.StdPermntadrs)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("std_permntadrs");

                entity.Property(e => e.StdPhoto)
                    .IsUnicode(false)
                    .HasColumnName("std_photo");

                entity.Property(e => e.StdPwd)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("std_pwd");

                entity.Property(e => e.StdReligion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("std_religion");

                entity.Property(e => e.StdState)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("std_state");
            });

            modelBuilder.Entity<TblMStudentAdemic>(entity =>
            {
                entity.HasKey(e => e.SacdmcId)
                    .HasName("PK__Tbl_M_St__7A255F6AE64C2791");

                entity.ToTable("Tbl_M_Student_Ademic");

                entity.Property(e => e.SacdmcId).HasColumnName("sacdmc_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("created_ip");

                entity.Property(e => e.DFlag).HasColumnName("d_flag");

                entity.Property(e => e.ModifyBy).HasColumnName("modify_by");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_date");

                entity.Property(e => e.ModifyIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("modify_ip");

                entity.Property(e => e.SacdmcBord)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sacdmc_bord");

                entity.Property(e => e.SacdmcInstute)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sacdmc_instute");

                entity.Property(e => e.SacdmcPasyer)
                    .HasColumnType("date")
                    .HasColumnName("sacdmc_pasyer");

                entity.Property(e => e.SacdmcResult)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("sacdmc_result");

                entity.Property(e => e.SacdmcType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sacdmc_type");

                entity.Property(e => e.StdId).HasColumnName("std_id");
            });

            modelBuilder.Entity<TblMStudentCourse>(entity =>
            {
                entity.HasKey(e => e.StdcoursId)
                    .HasName("PK__Tbl_M_St__9E733FBA46B3EAC1");

                entity.ToTable("Tbl_M_Student_Courses");

                entity.Property(e => e.StdcoursId).HasColumnName("stdcours_id");

                entity.Property(e => e.CoursId).HasColumnName("cours_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("created_ip");

                entity.Property(e => e.DFlag).HasColumnName("d_flag");

                entity.Property(e => e.ModifyBy).HasColumnName("modify_by");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_date");

                entity.Property(e => e.ModifyIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("modify_ip");

                entity.Property(e => e.PaymentAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("payment_amount");

                entity.Property(e => e.PaymentStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("payment_status");

                entity.Property(e => e.StdId).HasColumnName("std_id");
            });

            modelBuilder.Entity<TblMUser>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK__Tbl_M_Us__B51D3DEAE7ACCD61");

                entity.ToTable("Tbl_M_User");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("created_ip");

                entity.Property(e => e.DFlag).HasColumnName("d_flag");

                entity.Property(e => e.ModifyBy).HasColumnName("modify_by");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_date");

                entity.Property(e => e.ModifyIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("modify_ip");

                entity.Property(e => e.UAddrss)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("u_addrss");

                entity.Property(e => e.UEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_email");

                entity.Property(e => e.UMobno)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("u_mobno");

                entity.Property(e => e.UName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_name");

                entity.Property(e => e.UPwd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_pwd");

                entity.Property(e => e.UType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
