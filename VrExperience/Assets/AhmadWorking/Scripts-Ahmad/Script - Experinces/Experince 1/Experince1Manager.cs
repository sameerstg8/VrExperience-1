using BNG;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ahmad.TVFE.Managers.Experince1
{
    public class Experince1Manager : MonoBehaviour
    {
        #region Singelton
        private static Experince1Manager _instance;

        public static Experince1Manager experince1Manager { get { return _instance; } }
        #endregion

        [Header("____________Player__________")]
        public GameObject m_player;
        public SmoothLocomotion m_playerSmoothLocomotion;
        [Header("Player After Shifting to mainScene")]
        public Transform startPos;
        public Vector3 playerScaleAftertelePort;
        public float playerspeedAfterTeleport;
        [Header("_________CustomFader_______")]
        public GameObject PlayerFaceCustomFader;
        public GameObject CustomSceneFader;
        [Header("____________OpeningScene___________")]
        public GameObject KatherinceWheelPortion;
        public GameObject SoilOpening;
        private Animator SoilOpeningAnimator;
        public GameObject ExitGate, SoilOpeningTrigger,Affirmations_Panel,WritinPen;
        public int Aff_Num;
        public TextMeshProUGUI AffirmationTextFeild, Next_Exit_Button_Text;
        //public GameObject Meshgrass1,Meshgrass2,Meshgrass3;
        public List<Animator> Grass1, Grass2, Grass3;
        public Transform rootMarker;
        public List<GameObject> Ants;
        public List<int> AntsTime;
        public GameObject GoBackArrows;

        private void Awake()
        {
            if (_instance)
                Destroy(gameObject);
            _instance = this;
            SoilOpeningAnimator = SoilOpening.GetComponent<Animator>();

            StartCoroutine(antsTurnon());
            ExitGate.SetActive(false);

            //Grass1 = Meshgrass1.gameObject.GetComponent<Animator>();
            //Grass2 = Meshgrass2.gameObject.GetComponent<Animator>();
            //Grass3 = Meshgrass3.gameObject.GetComponent<Animator>();
        }
        private void Start()
        {
           // NextButton.SetActive(false);
        }
        //public void PlaySoilOpeningAnimation()
        //{
        //    print("dsfjhhgsdjh");

        //    SoilOpeningAnimator.speed = 0.5f;

        //}

      
        public void StartPlayerAfterSecond(float seconds)
        {
            StartCoroutine(StartPlayerAfterSeconds(seconds));
        }

        public IEnumerator StartPlayerAfterSeconds(float seconds)
        {

            yield return new WaitForSeconds(seconds);
            m_playerSmoothLocomotion.GetComponent<BNG.SmoothLocomotion>().AllowInput = true;
            Destroy(SoilOpeningTrigger.GetComponent<BoxCollider>());

        }

        public void ExitGateOnFuntion(float seconds)
        {
            StartCoroutine(OnExitGate(seconds));

        }

        public IEnumerator OnExitGate(float seconds)
        {

            yield return new WaitForSeconds(seconds);
            ExitGate.SetActive(true);

        }

        //public void TruFuntion()
        //{
        //    SoilOpeningAnimator.SetBool("PaperGoingBack", true);

        //}
        public void Next_Aff()
        {
            switch (Aff_Num)
            {
                case 4:
                  //  PlaySoilOpeningAnimation();
                  //  NextButton.SetActive(false);
                    SoilOpeningAnimator.SetBool("PaperGoingPermanently", true);
                    SoilOpeningAnimator.SetBool("PaperGoingBack3", false);
                    SoilOpeningAnimator.SetBool("PaperGoingBack2", false);
                    SoilOpeningAnimator.SetBool("PaperGoingBack1", false);
                    GoBackArrows.SetActive(true);
                    Affirmations_Panel.SetActive(false);
                    Destroy(WritinPen);
                    if (rootMarker != null)
                    {
                        Destroy(rootMarker.gameObject);

                    }
                    break;
                case 3:
                    AffirmationTextFeild.text = "Write an affirmation to acknowledge your thankfulness for how you are blessed.";
                    Next_Exit_Button_Text.text = "EXIT >>";
                    SoilOpeningAnimator.SetBool("PaperGoingBack3", true);
                    SoilOpeningAnimator.SetBool("PaperGoingBack2", false);
                    SoilOpeningAnimator.SetBool("PaperGoingBack1", false);
                    SoilOpeningAnimator.SetBool("PaperGoingPermanently", false);
                    Aff_Num++;
                    if (rootMarker != null)
                    {
                        Destroy(rootMarker.gameObject);

                    }

                    break;
                case 2:
                    AffirmationTextFeild.text = "Write on the paper everything you appreciate about yourself and how you love yourself unconditionally.";
                    SoilOpeningAnimator.SetBool("PaperGoingBack2", true);
                    SoilOpeningAnimator.SetBool("PaperGoingBack1", false);
                    SoilOpeningAnimator.SetBool("PaperGoingBack3", false);
                    SoilOpeningAnimator.SetBool("PaperGoingPermanently", false);
                    Aff_Num++;
                    if (rootMarker != null)
                    {
                        Destroy(rootMarker.gameObject);

                    }
                    break;
                case 1:
                    AffirmationTextFeild.text = "Write on the paper things you can give mother earth.";
                    SoilOpeningAnimator.SetBool("PaperGoingBack1", true);
                    SoilOpeningAnimator.SetBool("PaperGoingBack2", false);
                    SoilOpeningAnimator.SetBool("PaperGoingBack3", false);
                    SoilOpeningAnimator.SetBool("PaperGoingPermanently", false);
                    Aff_Num++;
                    if (rootMarker != null)
                    {
                        Destroy(rootMarker.gameObject);

                    }
                    break;
               
            }
        }
        public void ChangeGreenColor()
        {
            foreach (Animator item in Grass1)
            {

                item.SetBool("GreenGrass", true);
            };
            foreach (Animator item in Grass2)
            {

                item.SetBool("GreenGrass", true);
            };
            foreach (Animator item in Grass3)
            {

                item.SetBool("GreenGrass", true);
            };
        }


        public IEnumerator antsTurnon()
        {
            for (int i = 0; i < Ants.Count; i++)
            {
                Ants[i].SetActive(false);
            }
            for (int i = 0; i < Ants.Count; i++)
            {
                yield return new WaitForSeconds(AntsTime[i]);
                Ants[i].SetActive(true);
                yield return new WaitForSeconds(AntsTime[i]);
            }
        }
        public void SwitchtoExperince1()
        {
            m_player.transform.position = startPos.position;
            m_player.transform.localScale = playerScaleAftertelePort;
            PlayerFaceCustomFader.SetActive(true);
            Destroy(KatherinceWheelPortion);
            m_playerSmoothLocomotion.MovementSpeed = playerspeedAfterTeleport;

        }
    }
}

