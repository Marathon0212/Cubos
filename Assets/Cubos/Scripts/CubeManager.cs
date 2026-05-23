
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JellyCube
{
    public class CubeManager : MonoBehaviour
    {
        public static CubeManager Instance;
        
        public bool m_JellyEffectEnabled = true;

        [HideInInspector]
        public int m_NumberOfMoves = 0;

        private List<CubeController> m_CubeControllers;

        private List<CubeController> m_CubeMoving;


        void Awake()
        {
            Instance = this;

            m_CubeControllers = new List<CubeController>();
            
            m_CubeMoving = new List<CubeController>();
        }

        public void Register(CubeController controller)
        {
            controller.m_Cube.GetComponent<RubberEffect>().enabled = m_JellyEffectEnabled;

            m_CubeControllers.Add(controller);
        }

        public void RegisterMove(CubeController controller)
        {
            InputManager.Instance.LockControls();

            m_CubeMoving.Add(controller);
        }

        public void UnregisterMove(CubeController controller)
        {
            m_CubeMoving.Remove(controller);

            if (m_CubeMoving.Count == 0)
            {
                InputManager.Instance.UnlockControls();

                GameManager.Instance.CheckLevelCompletion();
            }
        }

        public void Move(Vector3 direction)
        {
            if (m_CubeMoving.Count == 0)
            {
                int countMovingCubes = 0;

                foreach (CubeController controller in m_CubeControllers)
                {
                    if (controller.m_CanControl)
                    {
                        CubeController moved = controller.DoMove(direction);

                        if (moved != null)
                        {
                            countMovingCubes++;
                        }
                    }
                }

                if (countMovingCubes > 0)
                {
                    m_NumberOfMoves++;
                }
            }
        }

    }
}