using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour
{
    //HingeJointコンポーネントを入れる
    private HingeJoint myHingeJoint;

    //初期の傾き
    private float defaultAngle = 20;
    //弾いた時の傾き
    private float flickAngle = -20;

    //タップのカウント
    private int lefttouchcount = 0;
    private int righttouchcount = 0;


    float[] touchposx0 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        //HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);
    }

    // Update is called once per frame
    void Update()
    {
        //タッチ操作かキーボード操作か判定
        if (Input.touchCount > 0)
        {
            ScreenTouch();
        }
        else
        {
            KeyControl();
        }
    }

    //フリッパーの傾きを設定
    public void SetAngle (float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }

    //キーボード操作
    public void KeyControl()
    {
        //左フリッパーを動かす
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && tag == "LeftFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //右フリッパーを動かす
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && tag == "RightFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //左右両方のフリッパーを動かす
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            SetAngle(this.flickAngle);
        }

        //左フリッパーを戻す
        if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) && tag == "LeftFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        //右フリッパーを戻す
        if ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) && tag == "RightFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        //左右両方のフリッパーを戻す
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            SetAngle(this.defaultAngle);
        }

    }

    //タッチ操作
    public void ScreenTouch()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            //タップされたら左右それぞれのタップ回数をカウント
            if (Input.touches[i].phase == TouchPhase.Began)
            {
                if (0 < Input.touches[i].position.x && Input.touches[i].position.x <= Screen.width / 2.0f)
                {
                    lefttouchcount++;
                    touchposx0[i] = Input.touches[i].position.x;
                }
                if (Input.touches[i].position.x > Screen.width / 2.0f)
                {
                    righttouchcount++;
                    touchposx0[i] = Input.touches[i].position.x;
                }
            }

            //画面から指が離れたら左右それぞれのカウントを減らす
            if (Input.touches[i].phase == TouchPhase.Ended)
            {
                if (0 < Input.touches[i].position.x && Input.touches[i].position.x <= Screen.width / 2.0f)
                {
                    lefttouchcount--;
                    touchposx0[i] = 0;
                }
                if (Input.touches[i].position.x > Screen.width / 2.0f)
                {
                    righttouchcount--;
                    touchposx0[i] = 0;
                }
            }

            //画面の左右逆側に移動した場合もカウントを減らす
            if (Input.touches[i].phase == TouchPhase.Moved)
            {
                if (0 < Input.touches[i].position.x && Input.touches[i].position.x <= Screen.width / 2.0f)
                {
                    if (touchposx0[i] > Screen.width / 2.0f)
                    {
                        righttouchcount--;
                        touchposx0[i] = Input.touches[i].position.x;
                    }
                }
                if (Input.touches[i].position.x > Screen.width / 2.0f)
                {
                    if (0 < touchposx0[i] && touchposx0[i] <= Screen.width / 2.0f)
                    {
                        lefttouchcount--;
                        touchposx0[i] = Input.touches[i].position.x;
                    }
                }
            }
        }

        //カウントが1以上ならフリッパー動かす
        if (lefttouchcount > 0 && tag == "LeftFripperTag")
        {
            SetAngle(this.flickAngle);
            if (lefttouchcount > Input.touchCount)
            {
                lefttouchcount = Input.touchCount;
            }
        }
        if (righttouchcount > 0 && tag == "RightFripperTag")
        {
            SetAngle(this.flickAngle);
            if (righttouchcount > Input.touchCount)
            {
                righttouchcount = Input.touchCount;
            }
        }

        //カウントが0以下ならフリッパー戻す
        if (lefttouchcount <= 0 && tag == "LeftFripperTag")
        {
            SetAngle(this.defaultAngle);
            lefttouchcount = 0;
        }
        if (righttouchcount <= 0 && tag == "RightFripperTag")
        {
            SetAngle(this.defaultAngle);
            righttouchcount = 0;
        }
    }

    /*
    public void ScreenTouch0()
    {
        for(int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touches[i].phase == TouchPhase.Began)
            {
                if (Input.touches[i].position.x <= Screen.width / 2.0f && tag == "LeftFripperTag")
                {
                    SetAngle(this.flickAngle);
                }
                if (Input.touches[i].position.x > Screen.width / 2.0f && tag == "RightFripperTag")
                {
                    SetAngle(this.flickAngle);
                }
            }

            if (Input.touches[i].phase == TouchPhase.Ended)
            {
                if (Input.touches[i].position.x <= Screen.width / 2.0f && tag == "LeftFripperTag")
                {
                    SetAngle(this.defaultAngle);
                }
                if (Input.touches[i].position.x > Screen.width / 2.0f && tag == "RightFripperTag")
                {
                    SetAngle(this.defaultAngle);
                }
            }

        }
       
    }
    */
}
