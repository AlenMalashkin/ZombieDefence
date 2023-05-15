using UnityEngine;
using Zenject;

public class InfrastructureInstaller : MonoInstaller
{
    [SerializeField] private Sound sound;
    
    public override void InstallBindings()
    {
        Bank bank = new Bank();
        CurrentAfterWaveState afterWaveState = new CurrentAfterWaveState();

        Container
            .Bind<Bank>()
            .FromInstance(bank)
            .AsSingle();

        Container
            .Bind<CurrentAfterWaveState>()
            .FromInstance(afterWaveState)
            .AsSingle();
        
        var soundPlayer = Container.InstantiatePrefabForComponent<Sound>(sound);
        DontDestroyOnLoad(soundPlayer);
        
        Container
            .Bind<Sound>()
            .FromInstance(soundPlayer)
            .AsSingle();
    }
}