
using UnityEngine;
using System.Collections;

namespace JellyCube
{
    public class CheckPoint : MonoBehaviour
    {
        public string m_CubeTag = "Player"; 

        [HideInInspector]
        public bool m_Success = false;

        void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == m_CubeTag)
            {
                m_Success = true;
                GameManager.Instance.CheckGame();
            }
        }

        void OnTriggerExit(Collider collider)
        {
            m_Success = false;
        }
    }
}