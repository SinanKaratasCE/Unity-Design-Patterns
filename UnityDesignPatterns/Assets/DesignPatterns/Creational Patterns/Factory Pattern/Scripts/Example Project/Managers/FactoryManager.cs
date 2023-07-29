using System;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    [Serializable]
    public enum FactoryType
    {
        SwordFactory,
        AxeFactory,
        BowFactory,
        HammerFactory,
        WandFactory,
        DaggerFactory
    }


    public class FactoryManager : MonoBehaviour
    {
        #region Properties

        public bool CanProduce
        {
            get => _canProduce;
            set => _canProduce = value;
        }

        public int ProducedWeaponsCount
        {
            get => _producedWeaponsCount;
            set => _producedWeaponsCount = value;
        }

        #endregion


        #region Private References

        [SerializeField] private Factory[] factories;
        [SerializeField] private Transform factoryTransform;
        [SerializeField] private TimeProgressBar timeProgressBar;


        private Dictionary<FactoryType, Factory> factoryDictionary = new Dictionary<FactoryType, Factory>();
        private Factory _workingFactory;
        private ContainerShip _containerShip;
        private bool _canProduce = true;
        private int _producedWeaponsCount;

        #endregion


        private void Awake()
        {
            GetAllFactoriesFromChildren();
        }

        private void Start()
        {
            _containerShip = SingletonContainer.Instance.ContainerShip;
        }

        private void GetAllFactoriesFromChildren()
        {
            factories = GetComponentsInChildren<Factory>();
            AddFactoriesToDictionary();
        }

        private void AddFactoriesToDictionary()
        {
            foreach (var factory in factories)
            {
                factoryDictionary.Add(factory.FactoryType, factory);
            }
        }

        public void SetWorkingFactoryAndProduce(FactoryDecision factoryDecision)
        {
            if (_canProduce == false || _producedWeaponsCount >= _containerShip.MaxCapacity) return;

            _canProduce = false;
            _workingFactory = factoryDictionary[factoryDecision.factoryType];
            StartProductionProcess();
        }

        private void StartProductionProcess()
        {
            timeProgressBar.AnimateBar(_workingFactory.ProductionTime);
        }


        public void ProduceWeapon()
        {
            if (_workingFactory != null)
            {
                _workingFactory.GetWeapon(factoryTransform.position);
                _producedWeaponsCount++;
                _canProduce = true;
            }
        }

        public void ResetFactories()
        {
            _workingFactory = null;
            _canProduce = true;
            _producedWeaponsCount = 0;
        }


        public string GetLog(FactoryType factoryType, IWeapon weapon)
        {
            return factoryDictionary[factoryType].GetLog(weapon);
        }
    }
}