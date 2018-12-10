using System;
using System.Collections.Generic;
using System.Text;

namespace ActividadesDeApoyo.Views.Navigation
{
    public class FicMasterPageMenuItem
    {
        public FicMasterPageMenuItem()
        {
            TargetType = typeof(FicMasterPageDetail);
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public Type TargetType { get; set; }

        public string Icon { get; set; } //

        public string FicPageName { get; set; } //
    }
}
