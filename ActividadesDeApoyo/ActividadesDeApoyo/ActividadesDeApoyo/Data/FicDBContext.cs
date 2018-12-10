using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Microsoft.EntityFrameworkCore;
using ActividadesDeApoyo.Models.Actividades;

namespace ActividadesDeApoyo.Data
{
    class FicDBContext : DbContext
    {
        private readonly string FicDBPath;

        public FicDBContext(string FicPaDBPath)
        {
            FicDBPath = FicPaDBPath;
            FicMetCrearDB();
        } //CONSTRUCTOR

        public async void FicMetCrearDB()
        {
            try
            {
                await Database.EnsureCreatedAsync();
            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("¡ALERTA!", e.Message.ToString(), "OK");
            }
        }

        protected async override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            try
            {
                OptionsBuilder.UseSqlite($"Filename={FicDBPath}");
                OptionsBuilder.EnableSensitiveDataLogging();
            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }
        }

        public DbSet<cat_actividades> cat_actividades { get; set; }
        

        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                //PK's
                modelBuilder.Entity<cat_actividades>().HasKey(pk => new { pk.IdActividad });

            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }

        }//AL CREAR EL MODELO
    }//CLASS
}
