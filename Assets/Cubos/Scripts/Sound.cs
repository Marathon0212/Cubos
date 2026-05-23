
using UnityEngine;
using System.Collections;

namespace JellyCube
{
    public class Sound : MonoBehaviour
    {
        public static bool m_Initialized = false;

        public void Awake()
        {
            if (m_Initialized)
            {
                Destroy(this.gameObject);
            }

            m_Initialized = true;
        }

        public void Start()
        {
            DontDestroyOnLoad(gameObject);

            GetComponent<AudioSource>().loop = true;
        }
    }
}