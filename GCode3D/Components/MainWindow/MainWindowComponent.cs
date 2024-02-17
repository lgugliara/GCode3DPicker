﻿using System.Windows.Controls;
using CommunityToolkit.Mvvm.Input;

namespace GCode3D.Components
{
    public class MainWindowComponent : Use<MainWindowComponent>
    {
        public SidebarComponent? SidebarComponent { get; set; } = 
            new Use<SidebarComponent>(new());

        public UserControl? CurrentPage { get; set; }

        public RunningComponent? RunningComponent { get; set; } =
            new Use<RunningComponent>(new());

        public MainWindowComponent()
        {
            SidebarComponent = new()
            {
                OnSelect = new RelayCommand<UserControl>((UserControl? page) =>
                {
                    CurrentPage = page;
                })
            };
            CurrentPage = SidebarComponent?.Pages?.FirstOrDefault();
        }
    }
}