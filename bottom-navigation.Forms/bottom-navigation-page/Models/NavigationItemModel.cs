using System;
namespace bottomnavigation.Forms.Models
{
    /// <summary>
    /// Author: Abd Elgawwad, Amro
    /// </summary>
    public class NavigationItemModel
    {
        private string _title;
        public  string Title
        {
            get => _title;
            set => _title = value;
        }


        private string _iconSource;
        public string IconSource
        {
            get => _iconSource;
            set => _iconSource = value;
        }


    }
}
