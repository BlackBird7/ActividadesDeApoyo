using System;
using System.Collections.Generic;
using System.Text;

using Autofac;

using ActividadesDeApoyo.ViewModels.CatActividades;
using ActividadesDeApoyo.Services.CatActividades;
using ActividadesDeApoyo.Interfaces.CatActividades;
using ActividadesDeApoyo.Services.Navigation;
using ActividadesDeApoyo.Interfaces.Navigation;

using ActividadesDeApoyo.Services.API;
using ActividadesDeApoyo.ViewModels.API;
using ActividadesDeApoyo.Interfaces.API;

namespace ActividadesDeApoyo.ViewModels.Base
{
    public class FicViewModelLocator
    {
        private static IContainer FicIContainer;

        public FicViewModelLocator()
        {
            //FIC: ContainerBuilder es una clase de la libreria de Autofac para poder ejecutar la interfaz en las diferentes plataformas 
            var FicContainerBuilder = new ContainerBuilder();

            #region REGISTO DE VIEWMODELS

            //Actividades
            FicContainerBuilder.RegisterType<FicVmCatActividadesList>();
            FicContainerBuilder.RegisterType<FicVmCatActividadesCreate>();
            FicContainerBuilder.RegisterType<FicVmCatActividadesRead>();
            FicContainerBuilder.RegisterType<FicVmCatActividadesUpdate>();

            FicContainerBuilder.RegisterType<FicVmExportar>();
            FicContainerBuilder.RegisterType<FicVmImportar>();

            #endregion

            #region REGISTRO DE VIEWMODELS CON SERVICIOS

            //Actividades
            FicContainerBuilder.RegisterType<FicSrvAppActividades>().As<IFicSrvAppActividades>();
            FicContainerBuilder.RegisterType<FicSrvNavigationActividades>().As<IFicSrvNavigationActividades>().SingleInstance();


            FicContainerBuilder.RegisterType<FicSrvImportar>().As<IFicSrvImportar>();
            FicContainerBuilder.RegisterType<FicSrvExportar>().As<IFicSrvExportar>();
            #endregion

            //FIC: se asigna o se libera el contenedor
            //-------------------------------------------
            if (FicIContainer != null)
            {
                FicIContainer.Dispose();
            }
            FicIContainer = FicContainerBuilder.Build();
        }//CONSTRUCTOR

        #region CONTROL DE CatActividades
        public FicVmCatActividadesList FicVmCatActividadesList
        {
            get { return FicIContainer.Resolve<FicVmCatActividadesList>(); }
        }

        public FicVmCatActividadesCreate FicVmCatActividadesCreate
        {
            get { return FicIContainer.Resolve<FicVmCatActividadesCreate>(); }
        }

        public FicVmCatActividadesRead FicVmCatActividadesRead
        {
            get { return FicIContainer.Resolve<FicVmCatActividadesRead>(); }
        }

        public FicVmCatActividadesUpdate FicVmCatActividadesUpdate
        {
            get { return FicIContainer.Resolve<FicVmCatActividadesUpdate>(); }
        }
        #endregion

        #region Control de Web Api
        public FicVmExportar FicVmExportar
        {
            get { return FicIContainer.Resolve<FicVmExportar>(); }
        }
        public FicVmImportar FicVmImportar
        {
            get { return FicIContainer.Resolve<FicVmImportar>(); }
        }
        #endregion


    }//CLASS
}//NAMESPACE
