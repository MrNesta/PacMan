using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PacMan.EF;
using PacMan.Interfaces;
using PacMan.Model;

namespace PacMan.Repositories
{
    public class PlayerRepository : IRepository<Player>
    {       
        private RecordsContext _context;
        private bool disposed = false;

        public PlayerRepository(string connection)
        {
            _context = new RecordsContext(connection);
        }

        public void Create(Player item)
        {
            _context.Players.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Player player = _context.Players.Find(id);
            if (player != null)
            {
                _context.Entry(player).State = EntityState.Deleted;
                _context.SaveChanges();                
            }
        }

        public IEnumerable<Player> Find(Func<Player, bool> predicate)
        {
            return _context.Players.Where(predicate);
        }

        public Player Get(int id)
        {
            return _context.Players.Find(id);
        }

        public IEnumerable<Player> GetAll()
        {
            return _context.Players;
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
