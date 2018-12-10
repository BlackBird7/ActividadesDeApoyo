using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;
using Xamarin.Forms;
using System.IO;
using ActividadesDeApoyo.UWP.SQLite;
using ActividadesDeApoyo.Interfaces.SQLite;

[assembly: Dependency(typeof(FicConfigSQLiteUWP))]
namespace ActividadesDeApoyo.UWP.SQLite
{
    class FicConfigSQLiteUWP : IFicConfigSQLite
    {
        public string FicGetDBPath()
        {
            return Path.Combine(
                ApplicationData.Current.LocalFolder.Path,
                appSettings.FicDataBaseName);
        }
    }//CLASS
}
