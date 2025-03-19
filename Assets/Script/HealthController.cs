using System.Collections;
using UnityEngine;

namespace HWShoter
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private float _health = 3.0f;
        [SerializeField] private float _lifeTime = 3.0f;

        private bool _isAlive = true;
        public bool CanTakeDamage(float damage)
        {
            if (_isAlive == false)
            {
                return false;
            }
            
            _health -= damage;
            
            if (_health <= 0)
            {
                StartCoroutine(Die());
                _isAlive = false;
                return false;
            }
            
            return true;
        }

        private IEnumerator Die()
        {
            var component = GetComponent<Renderer>();

            component.material.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            component.material.color = Color.green;
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);

        }
    }
}