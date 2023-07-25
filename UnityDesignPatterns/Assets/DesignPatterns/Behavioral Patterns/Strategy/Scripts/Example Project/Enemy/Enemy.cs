using System;
using DesignPatterns.SingletonObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DesignPatterns.Strategy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private SkinnedMeshRenderer[] skinnedMeshRenderers;
        public IDoDamage weakness;
        private MaterialPropertyBlock propertyBlock;

        private GameObject _player;
        private float _health = 100f;
        private float _damage = 100f;
        public float moveSpeed = 2f;

        [SerializeField] private float maxZigzagAmplitude = 1f; // Adjust the maximum amplitude of the zigzag movement
        [SerializeField] private float zigzagFrequency = 2f; // Adjust the frequency of the zigzag movement
        [SerializeField] private float leftLimit = -5f; // Left boundary of the zigzag range
        [SerializeField] private float rightLimit = 5f; // Right boundary of the zigzag range

        private float timer = 0f;
        private int direction = 1;
        private float zigzagAmplitude;

        public GameManager gameManager;

        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public float Health
        {
            get => _health;
            set => _health = value;
        }
        
        public void InjectGameManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        private void OnEnable()
        {
            CharacterReset();
        }

        private void CharacterReset()
        {
            Health = 100f;

            if (propertyBlock == null)
            {
                propertyBlock = new MaterialPropertyBlock();
            }

            var rand = Random.Range(0, 3);
            switch (rand)
            {
                case 0:
                    weakness = new FireDamage();
                    propertyBlock.SetColor("_Color", Color.red);
                    break;
                case 1:
                    weakness = new IceDamage();
                    propertyBlock.SetColor("_Color", Color.blue);
                    break;
                case 2:
                    weakness = new PoisonDamage();
                    propertyBlock.SetColor("_Color", Color.green);
                    break;
            }

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                skinnedMeshRenderers[i].SetPropertyBlock(propertyBlock);
            }
        }


        private void Update()
        {
            Move();
        }

        private void Move()
        {
            // Move the character forward
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // Update the timer
            timer += Time.deltaTime;

            // Calculate the zigzag movement based on sine wave
            float zigzagValue = Mathf.Sin(timer * zigzagFrequency) * zigzagAmplitude;

            // Calculate the new position with the zigzag movement
            Vector3 newPosition = transform.position + (Vector3.right * zigzagValue * direction);

            // Clamp the newPosition between the left and right limits
            newPosition.x = Mathf.Clamp(newPosition.x, leftLimit, rightLimit);

            // Apply the new position to the character
            transform.position = newPosition;

            // Change the zigzag direction when needed
            if (timer >= Mathf.PI / zigzagFrequency)
            {
                timer = 0f;

                // Randomly generate a new zigzag amplitude for the next cycle
                zigzagAmplitude = Random.Range(0f, maxZigzagAmplitude);

                direction *= -1; // Reverse the direction
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
            gameManager.EnemyKilled();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(Damage);
                Die();
            }
        }
    }
}