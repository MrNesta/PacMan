using System.Collections.Generic;
using System.ComponentModel;
using PacMan.Model;

namespace PacMan.Interfaces
{
    public interface IGameField : INotifyPropertyChanged
    {
        Player CurrentPlayer { get; set; }

        byte[,] MatrixForWalls { get; }

        Enemy Inky { get; }

        Enemy Pinky { get; }

        Enemy Blinky { get; }

        Enemy Clyde { get; }

        Pac Pacman { get; }

        EnemyPlace EnemyPlace { get; }

        int Rows { get; }

        int Cols { get; }

        List<Dot> Dots { get; }

        int InTheEnemyPlace { get; set; }

        bool IsFrighteningMode { get; set; }

        void RemoveDots(Dot d);

        int GetRandomInt(int min, int max);

        void GenerateWalls();

        void GenerateMaze();

        void GenerateDots();

        void InitializeCharacters(IPluginEnemyBehaviorAlgorithm algorithm = null);

        void InitializeWay();

        void InitializeEnemyWay();

        bool CanMove(Point p);

        bool CanEnemyMove(Point pointCheck);

        void CloseEnemyPlace();

        void ChasingMode(params Enemy[] enemies);

        void ScatteringMode(params Enemy[] enemies);

        void FrighteningMode(params Enemy[] enemies);

        Level NextLevel();

        void MoveCharacters();

        void ResetCharacter();
    }
}
