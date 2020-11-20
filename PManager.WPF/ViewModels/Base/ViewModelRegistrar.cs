using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PManager.WPF.ViewModels.Windows;

namespace PManager.WPF.ViewModels.Base
{
    internal static class ViewModelRegistrar
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
        ;
    }
}
