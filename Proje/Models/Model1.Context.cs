﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proje.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SinemaOtomasyonEntities : DbContext
    {
        public SinemaOtomasyonEntities()
            : base("name=SinemaOtomasyonEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bilet> Bilet { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Salon> Salon { get; set; }
        public virtual DbSet<Seans> Seans { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
