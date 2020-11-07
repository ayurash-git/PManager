using System;
using System.Collections.Generic;
using System.Text;
using PManager.WPF.ViewModels.Base;

namespace PManager.WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок Окна

        private string _title = "Project Manager";
        /// <summary> Заголовок окна </summary>
        public string Title
        {
            get => _title; 
            set => Set(ref _title, value);
        }

        #endregion

    }
}
