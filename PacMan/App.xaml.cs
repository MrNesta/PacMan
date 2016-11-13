using System;
using System.IO;
using System.Windows;
using Autofac;
using PacMan.Infrastructure;
using PacMan.Util;
using PacMan.View;
using PacMan.ViewModel;

namespace PacMan
{
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());

            var window = new GameWindow();
            var builder = new ContainerBuilder();
            builder.RegisterModule<LibraryModule>();
            var container = builder.Build();
            var model = container.Resolve<GameViewModel>();
            window.DataContext = model;

            try
            {
                window.Show();
            }
            catch (InvalidOperationException exc)
            {
                if (exc.TargetSite.Name == "VerifyCanShow")
                {
                    Shutdown();
                }
                else
                {
                    LogService.SaveToLog(exc.Message);
                }
            }
            catch (Exception exc)
            {
                LogService.SaveToLog(exc.Message);
                MessageBox.Show(exc.Message);
                Shutdown();
            }
        }
    }
}
