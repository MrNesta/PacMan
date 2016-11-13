using PacMan.Infrastructure;
using PacMan.Interfaces;
using PacMan.Model;
using PacMan.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PacMan.ViewModel
{
    public class RecordsTableViewModel : DialogViewModelBase
    {
        private IRepository<Player> _database;
        private List<Player> _players;

        public List<Player> Players
        {
            get
            {               
                try
                {
                    _database = new PlayerRepository("RecordsConnection");
                    using (_database)
                    {
                        _players = _database.GetAll().ToList();
                    }                   
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error accessing database");
                    LogService.SaveToLog(exc.Message);
                    DialogResult = false;
                }

                return _players;
            }
        }
    }
}
