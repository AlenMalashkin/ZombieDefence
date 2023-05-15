using Zenject;

public class EnemyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        EnemyStatSetuper enemyStatSetuper = new EnemyStatSetuper(1, 2, 3.5f, 1);
        
        Container.
            Bind<EnemyStatSetuper>().
            FromInstance(enemyStatSetuper).
            AsSingle();
    }
}