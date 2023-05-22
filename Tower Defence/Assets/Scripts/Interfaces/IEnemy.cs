namespace Enemies.Interfaces
{
    public interface IEnemy
    {
        int Health { get; }
        int Damage { get; }
        float Speed { get; }
        float SpeedAfter { get; }
        int StealedHealth { get; }
        int MaxHealth { get; }
    }
}