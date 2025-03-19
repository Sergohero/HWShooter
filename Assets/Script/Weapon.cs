using System;
using UnityEngine;

namespace HWShoter
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _barrel;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _count;
        [SerializeField] private float _force;
        [SerializeField] private float _delay;

        private Transform _bulletRoot;
        private Bullet[] _bullets;

        private bool _canShoot;
        private float _lastShootTime;

        private void Start()
        {
            _bulletRoot = new GameObject("BulletRoot").transform;
            Recharge();
        }

        private void Update()
        {
            _canShoot = _delay <= _lastShootTime;
            if (_canShoot)
            {
                return;
            }
            _lastShootTime += Time.deltaTime;
        }

        public void Recharge()
        {
            _bullets = new Bullet[_count];
            for (int i = 0; i < _count; i++)
            {
                Bullet bullet = Instantiate(_bulletPrefab, _bulletRoot);
                bullet.Sleep();
                _bullets[i] = bullet;
            }
        }

        public void Fire()
        {
            if (_canShoot == false)
            {
                return;
            }
            
            if (TryGetBullet(out Bullet bullet))
            {
               bullet.Run(_barrel.forward * _force, _barrel.position);
               _lastShootTime = 0.0f;
            }
        }

        private bool TryGetBullet(out Bullet result)
        {
            int candidate = -1;

            if (_bullets == null)
            {
                result = default;
                return false;
            }

            for (var i = 0; i < _bullets.Length; i++)
            {
                Bullet bullet = _bullets[i];
                if (_bullets[i] == null)
                {
                    continue;
                }

                if (bullet.IsActive)
                {
                    continue;
                }
                
                candidate = i;
                break;

            }
            
            if (candidate == -1)
            {
                result = default;
                return false;
            }
            
            result = _bullets[candidate];
            return true;
        }
    }
}