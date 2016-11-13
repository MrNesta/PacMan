using System.ComponentModel;

public interface IPluginEnemyBehaviorAlgorithm : INotifyPropertyChanged
{
    void Frightened(object objectToMove);
    void Chase(object objectToMove);
    void Scatter(object objectToMove);
}