using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class HeartUIManager : MonoBehaviour
    {
        private PlayerHealthManager _target;
        public int numberOfHearts = 3;

        public Image[] hearts;

        private void Start()
        {
            _target = GameObject.Find("Player").GetComponent<PlayerHealthManager>();
        }

        private void Update()
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].color = i < _target.lives ? new Color(1,1,1,1) : new Color(1, 1, 1, 0.1f);
            }
        }
    }
}