using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using PacMan.Infrastructure;
using PacMan.Interfaces;
using PacMan.Model;
using PacMan.ViewModel;

namespace PacMan.Util
{
    public class LibraryModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerViewModel>().AsSelf().As<DialogViewModelBase>();
            builder.RegisterType<PlayerNameDialogService>().AsSelf().As<IDialogService>();          
            builder.Register(c => new PlayerNameDialogService(c.Resolve<PlayerViewModel>()));
            builder.RegisterType<RecordsTableViewModel>().AsSelf().As<DialogViewModelBase>();           
            builder.RegisterType<RecordsTableDialogService>().AsSelf().As<IDialogService>();
            builder.Register(c => new RecordsTableDialogService(c.Resolve<RecordsTableViewModel>()));
            builder.RegisterType<PluginsViewModel>().AsSelf().As<DialogViewModelBase>();
            builder.RegisterType<PluginsDialogService>().AsSelf().As<IDialogService>();
            builder.Register(c => new PluginsDialogService(c.Resolve<PluginsViewModel>()));
            builder.RegisterType<GameField>().As<IGameField>()
                .WithParameters(new List<Parameter> { new NamedParameter("rows", 20),
                    new NamedParameter("cols", 20) });                    
            builder.Register(c => new GameViewModel(
            c.Resolve<PlayerNameDialogService>(),
            c.Resolve<RecordsTableDialogService>(),
            c.Resolve<PluginsDialogService>(),
            c.Resolve<IGameField>()));
        }
    }
}
