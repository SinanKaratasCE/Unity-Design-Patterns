using UnityEngine;
using DesignPatterns.Command;
using Zenject;


public class CommandGameInstaller : MonoInstaller
{
    [SerializeField] private PlayerMover playerMover;
    [SerializeField] private GameManager gameManager;


    public override void InstallBindings()
    {
        Container.BindInstance(playerMover);
        Container.BindInstance(gameManager);
    }
}