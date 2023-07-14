using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace DesignPatterns.SingletonObjectPool
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        private GameObject _player;
        private OPGameManager _opGameManager;
        private float _health = 100f;
        private float _damage = 100f;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private CharacterStatsSO _characterStats;

        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public float Health
        {
            get => _health;
            set
            {
                if (_health > 0)
                {
                    _health = value;
                }
            }
        }

        private void Start()
        {
            Timer.Instance.TimerWait(0.1f, () =>
            {
                _navMeshAgent = GetComponent<NavMeshAgent>();
                _opGameManager = SingletonContainer.Instance.OpGameManager;
                _player = _opGameManager.Player;
            });

            Health = _characterStats.Health;
            Health = _characterStats.Damage;
        }


        private void Update()
        {
            ChasePlayer();
        }

        private void ChasePlayer()
        {
            if (_navMeshAgent == null || _player == null)
            {
                return;
            }

            _navMeshAgent.SetDestination(_player.transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.TakeDamage(Damage);
                TakeDamage(player.CollisionDamage);
            }
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            CheckHealth();
        }

        private void CheckHealth()
        {
            if (Health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            gameObject.SetActive(false);
            _opGameManager.AddScore();
        }
    }
}