using System;
using System.Collections.Generic;
using appbase.Forms.ViewModel;

namespace bottomnavigation.Forms.Models
{
    public class SectionModel
    {
        public NavigationItemModel NavigationItemModel { get; set; }
        public Type PagesType { get; set; }
        public Dictionary<string, object> Args = new Dictionary<string, object>();
    }
}
