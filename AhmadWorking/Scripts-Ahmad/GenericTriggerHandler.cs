using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using static Ahmad.TVFE.Managers.Experince1.Experince1Manager;
using static Ahmad.TVFE.Managers.OpeningScene.OpeningSceneManager;
namespace Ahmad.TVFE.Triggers
{
    public class GenericTriggerHandler : MonoBehaviour
    {
        [Header("_______________OnTriggerEnter_______________")]
        public UnityEvent onTriggerEnter;
        [Header("_______________OnTriggerStay_______________")]
        public UnityEvent onTriggerStay;
        [Header("_______________OnTriggerExit_______________")]
        public UnityEvent onTriggerExit;
        public string SceneName;

        #region TriggerFunctions
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onTriggerEnter.Invoke();
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onTriggerStay.Invoke();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onTriggerExit.Invoke();
            }
        }
        #endregion


        #region Haider Written Code
        public void LoadScene(float seconds)
        {
            StartCoroutine(LoadSceneAfterSecond(seconds));
        }
        public void StopnPlayer()
        {
            experince1Manager.m_player.GetComponentInChildren<BNG.SmoothLocomotion>().AllowInput = false;
        }  public void StartPlayer()
        {
            experince1Manager.m_player.GetComponentInChildren<BNG.SmoothLocomotion>().AllowInput = true;
        }
        public IEnumerator LoadSceneAfterSecond(float seconds)
        {
            if (SceneManager.GetActiveScene().name== "NewOpeningScene")
            {
                openingSceneManager.CustomSceneFader.SetActive(true);
            }
         if (SceneManager.GetActiveScene().name == "Experience1")
            {
                experince1Manager.CustomSceneFader.SetActive(true);
            }
         
          
            yield return new WaitForSeconds(seconds);
            BNG.SceneLoader.sceneLoader.LoadScene(SceneName);
        }
       
        #endregion

        #region Ahmad WrittenCode
        public void StartExperince1st()
        {
            experince1Manager.SwitchtoExperince1();

        } 
       

        #endregion
    }
}