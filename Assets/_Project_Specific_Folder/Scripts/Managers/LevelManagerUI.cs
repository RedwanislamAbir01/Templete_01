using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace KaijuRun
{
    public class LevelManagerUI : MonoBehaviour
    {
        public GameObject[] LevelBtn;


        public void VuiFor(int m_LevelNo, string m_UiName)
        {
            LevelBtn[m_LevelNo % 6].transform.GetChild(4).gameObject.SetActive(true);
            LevelBtn[m_LevelNo % 6].transform.GetChild(5).gameObject.SetActive(true);
            LevelBtn[m_LevelNo % 6].transform.GetChild(5).GetComponent<Image>().sprite =
                Resources.Load<Sprite>("UI/" + m_UiName);
            // LevelBtn[m_LevelNo % 5].transform.GetChild(4).gameObject.transform.parent.DOScale(new Vector3(1.448387f, 1.448387f, 1.448387f), 0);
        }

        private void OnEnable()
        {
            int i = PlayerPrefs.GetInt("current_scene_text", 0);


            int j = 0;
            while (j < 6)
            {
                if (j < i % 6)
                {
                    LevelBtn[j].transform.GetChild(2).gameObject.SetActive(true);
                    LevelBtn[j].transform.GetChild(2).gameObject.transform.parent
                        .DOScale(new Vector3(1.448387f, 1.448387f, 1.448387f), 0);
                }

                LevelBtn[j].transform.GetChild(3).gameObject.GetComponent<Text>().text =
                    (Mathf.Floor(i / 6) * 6 + j + 1).ToString();
                j++;
            }

            LevelBtn[i % 6].transform.GetChild(1).gameObject.SetActive(true);
            LevelBtn[i % 6].transform.GetChild(4).gameObject.GetComponent<Image>().sprite =
             Resources.Load<Sprite>("UI/Select");
            LevelBtn[i % 6].transform.GetChild(1).gameObject.transform.parent.DOScale(new Vector3(1.8f, 1.8f, 1.8f), .5f).SetLoops(-1, LoopType.Yoyo);
            LevelBtn[i % 6].transform.GetChild(4).gameObject.transform.parent.DOScale(new Vector3(1.8f, 1.8f, 1.8f), .5f).SetLoops(-1, LoopType.Yoyo);
            if(i > 2)
            {
                LevelBtn[2].transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(false);
                LevelBtn[2].transform.GetChild(5).gameObject.GetComponent<Image>().enabled = false;
            }
            if (i > 4)
            {
                LevelBtn[4].transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(false);
                LevelBtn[4].transform.GetChild(5).gameObject.GetComponent<Image>().enabled = false;
            }
            if (i > 7)
            {
                LevelBtn[1].transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(false);
                LevelBtn[1].transform.GetChild(5).gameObject.GetComponent<Image>().enabled = false;
            }
            if (i >= 0 && i <6)
            {
                VuiFor(2, "3");

                VuiFor(4, "5");
            }

            if (i >= 6 && i <= 12)
            {
                VuiFor(7, "8");
            }
            if (i >=13)
            {
                VuiFor(13, "13");
            }
            //if (i >= 5 && i < 10)
            //{
            //    VuiFor(9, "10");
            //}

            //if (i >= 10 && i < 14)
            //{
            //    VuiFor(13, "14");
            //}
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}