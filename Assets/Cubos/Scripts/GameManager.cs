
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace JellyCube
{
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public string m_NextLevelName;

        public Animator m_Animator;

        private Dictionary<string,float> m_AnimationClipsLength =  new Dictionary<string,float>();

        private CheckPoint[] m_Checkpoints;

        private bool m_CheckGame = false;

        private CameraController m_CameraController;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            m_Checkpoints = GameObject.FindObjectsOfType(typeof(CheckPoint)) as CheckPoint[];

            foreach (AnimationClip clip in m_Animator.runtimeAnimatorController.animationClips)
            {
                m_AnimationClipsLength.Add(clip.name, clip.length);
            }

            m_CameraController = CameraController.Instance;
        }

        public void CheckLevelCompletion()
        {
            if (m_CheckGame)
            {
                int activeCheckpointsNumber = 0;

                foreach (CheckPoint cp in m_Checkpoints)
                {
                    if (cp.m_Success)
                    {
                        activeCheckpointsNumber++;
                    }
                }

                if (activeCheckpointsNumber == m_Checkpoints.Length)
                {
                    Debug.Log("Level complete!");

                    InputManager.Instance.LockControls();

                    StartCoroutine(LevelComplete());
                }

                m_CheckGame = false;

            }

          
        }

        public void CheckGame()
        {
            m_CheckGame = true;
        }

        public void ReloadLevel()
        {
            StartCoroutine(ReloadLevelRoutine());
        }

        private IEnumerator ReloadLevelRoutine()
        {
            InputManager.Instance.LockControls();
            
            m_Animator.Play("LevelFadeIn");

            yield return new WaitForSeconds(m_Animator.GetCurrentAnimatorStateInfo(0).length + 1);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        IEnumerator LevelComplete()
        {
            m_Animator.Play("LevelComplete");

            if (m_CameraController != null)
            {
                m_CameraController.SetLevelCompleteCamera();
            }

            yield return new WaitForSeconds(m_AnimationClipsLength["LevelComplete"]);

            if (m_NextLevelName != "")
            {
                SceneManager.LoadScene(m_NextLevelName);
            }
            else
            {
                Debug.Log("Assign the var 'NextLevelName' of the component 'GameManager.cs' to load a next level.");
            }
        }
    }
}