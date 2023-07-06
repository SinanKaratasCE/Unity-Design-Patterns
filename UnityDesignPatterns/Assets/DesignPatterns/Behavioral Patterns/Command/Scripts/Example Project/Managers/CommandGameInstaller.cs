using UnityEngine;
using DesignPatterns.Command;
using UnityEngine.Serialization;
using Zenject;


public class CommandGameInstaller : MonoInstaller
{
    [SerializeField] private PlayerMover playerMover;
    [SerializeField] private CommandGameManager commandGameManager;


    public override void InstallBindings()
    {
        Container.BindInstance(playerMover);
        Container.BindInstance(commandGameManager);
    }
}