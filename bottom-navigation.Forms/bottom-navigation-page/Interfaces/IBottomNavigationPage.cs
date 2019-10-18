using System;
using System.Collections.Generic;

namespace bottomnavigation.Forms
{
    public interface IBottomNavigationPage
    {
        Dictionary<string, object> Args { get; set; }
        void OnCreate();
        void OnDestroy();
    }
}
