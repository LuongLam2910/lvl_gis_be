using System.Collections.Generic;

namespace App.Qtht.Services.Models;

public class MenuModel
{
    public class ListMenuModel
    {
        public string Id { get; set; }
        public string IconClass { get; set; }
        public string Parent { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public string CssClass { get; set; }
        public string State { get; set; }
    }

    public class FuseNavigation
    {
        public List<FuseNavigationItem> compact { get; set; }
        public List<FuseNavigationItem> defaultFuse { get; set; }
        public List<FuseNavigationItem> futuristic { get; set; }

        public List<FuseNavigationItem> horizontal { get; set; }
    }

    public class FuseNavigationItem
    {
        public string Id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string subtitle { get; set; }
        public string icon { get; set; }

        public List<FuseNavigationItem> children { get; set; }
        //        id?: string;
        //title?: string;
        //subtitle?: string;
        //type:
        //    | 'aside'
        //    | 'basic'
        //    | 'collapsable'
        //    | 'divider'
        //    | 'group'
        //    | 'spacer';
        //hidden?: (item: FuseNavigationItem) => boolean;
        //active?: boolean;
        //disabled?: boolean;
        //tooltip?: string;
        //link?: string;
        //externalLink?: boolean;
        //target?:
        //    | '_blank'
        //    | '_self'
        //    | '_parent'
        //    | '_top'
        //    | string;
        //exactMatch?: boolean;
        //isActiveMatchOptions?: IsActiveMatchOptions;
        //function?: (item: FuseNavigationItem) => void;
        //classes?: {
        //    title?: string;
        //    subtitle?: string;
        //    icon?: string;
        //    wrapper?: string;
        //};
        //    icon?: string;
        //badge?: {
        //    title?: string;
        //    classes?: string;
        //};
        //children?: FuseNavigationItem[];
        //meta?: any;
    }
}