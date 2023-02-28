using BNG;
using System.Collections;
using UnityEngine;




namespace Ahmad.TVFE.Managers.OpeningScene
{

    public class OpeningSceneManager : MonoBehaviour
    {

        #region Singelton
        private static OpeningSceneManager _instance;

        public static OpeningSceneManager openingSceneManager { get { return _instance; } }
        #endregion

        [Header("____________Player__________")]
        public GameObject m_player,PlayerHaider;
        public SmoothLocomotion m_playerSmoothLocomotion;
        [Header("Player After Shifting to mainScene")]
        public Transform startPos;
        public Vector3 playerScaleAftertelePort;
        
        [Header("_________CustomFader_______")]
        public GameObject PlayerFaceCustomFader;
        public GameObject CustomSceneFader;
        [Header("____________OpeningScene___________")]
        public GameObject ShiningPortion;

        private void Awake()
        {
            if (_instance)
                Destroy(gameObject);
            _instance = this;
            

        }
        private void Start()
        {
            //m_playerSmoothLocomotion = m_player.GetComponent<SmoothLocomotion>();
        }
        public void SwitchtoOpeningScene()
        {
            m_player.transform.position = startPos.transform.position;
           Destroy( PlayerHaider.GetComponent<Animator>());
            m_player.transform.localScale = playerScaleAftertelePort;
            PlayerFaceCustomFader.SetActive(true);
        
            Destroy(ShiningPortion);
        }
        public void StartPlayerAfterSecond(float seconds)
        {
            //openingSceneManager.m_player.GetComponentInChildren<BNG.SmoothLocomotion>().AllowInput = true;
            StartCoroutine(StartPlayerAfterSeconds(seconds));
        }

        public IEnumerator StartPlayerAfterSeconds( float seconds)
        {

            yield return new WaitForSeconds(seconds);
            openingSceneManager.m_playerSmoothLocomotion.GetComponent<BNG.SmoothLocomotion>().AllowInput = true;

        }
    }
}