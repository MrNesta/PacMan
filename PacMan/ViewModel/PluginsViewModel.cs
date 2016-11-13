using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PacMan.Core;

namespace PacMan.ViewModel
{
    public class PluginsViewModel: DialogViewModelBase
    {
        [ImportMany(typeof(IPluginEnemyBehaviorAlgorithm))]
        private IEnumerable<IPluginEnemyBehaviorAlgorithm> _plugins { get; set; }

        private CompositionContainer _container;

        private List<Type> _enemyAlgorithms;

        public List<Type> EnemyAlgorithms
        {
            get
            {               
                DoImport();
                _enemyAlgorithms = new List<Type>();
                _enemyAlgorithms.Add(typeof(EnemiesBehaviorAlgorithm));
                _enemyAlgorithms.AddRange(_plugins.Select((alg) => alg.GetType()));
                return _enemyAlgorithms;
            }
        }

        private Type _selectedAlgorithm;

        public Type SelectedAlgorithm
        {
            get { return _selectedAlgorithm; }
            set
            {
                _selectedAlgorithm = value;
                RaisePropertyChanged("SelectedAlgorithm");
            }
        }

        private RelayCommand _choosePluginCommand;

        public ICommand ChoosePluginCommand
        {
            get
            {
                if (_choosePluginCommand == null)
                    _choosePluginCommand = new RelayCommand(
                        ExecuteChoosePluginCommand);
                return _choosePluginCommand;
            }
        }

        public void ExecuteChoosePluginCommand()
        {
            if (_selectedAlgorithm == EnemyAlgorithms[0])
            {
                Type nullType = null;
                Messenger.Default.Send(nullType);
            }
            else
            {
                Messenger.Default.Send(_selectedAlgorithm);               
            }
           
            DialogResult = true;
            DialogResult = null;
            _container.Dispose();
            _selectedAlgorithm = null;
            _enemyAlgorithms.Clear();
        }

        private void DoImport()
        {
            var catalog = new AggregateCatalog();

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
                                    + "\\plugins"; ;
            catalog.Catalogs.Add(new DirectoryCatalog(path));

            _container = new CompositionContainer(catalog);

            _container.ComposeParts(this);
        }
    }
}
