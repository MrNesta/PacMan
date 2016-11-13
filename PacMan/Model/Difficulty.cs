using System.Collections.Generic;

namespace PacMan.Model
{
    public class Difficulty
    {
        public int Id { get; set; }
        public string DifficultyName { get; set; }
    }


    public class DifficultyList: List<Difficulty>
    {
        public DifficultyList()
        {
            Add(new Difficulty { Id = 0, DifficultyName = "Easy" });
            Add(new Difficulty { Id = 1, DifficultyName = "Normal" });
            Add(new Difficulty { Id = 2, DifficultyName = "Hard" });
        }
    }
}
