using System.Data.Entity;
using PacMan.Model;

namespace PacMan.EF
{
    public class RecordsContext: DbContext
    {
        public RecordsContext(string conectionString)
            : base(conectionString)
        {

        }

        public DbSet<Player> Players { get; set; }
    }
}
