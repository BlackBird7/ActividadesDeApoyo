using System;

namespace ActividadesDeApoyo.Interfaces.Navigation
{
    public interface IFicSrvNavigationActividades
    {
        void FicMetNavigateTo<TDestinationViewModel>(object navigationContext = null, bool show = true);
        void FicMetNavigateTo<TDestinationViewModel>(object navigationContext = null);
        void FicMetNavigateTo(Type destinationType, object navigationContext = null);
        void FicMetNavigateBack();
    }
}
