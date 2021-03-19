using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BallController : MonoBehaviour
{
    //得点
    private int score = 0;

    //ボールが見える可能性のあるz軸の最大値
    private float visiblePosZ = -6.5f;

    //ゲームオーバーを表示するテキスト
    private GameObject gameoverText;
    //得点を表示するテキスト
    private GameObject scoreText;

    // Start is called before the first frame update
    void Start()
    {
        //シーン中のGameOverTextオブジェクトを取得
        this.gameoverText = GameObject.Find("GameOverText");
        //シーン中のPointTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        //ボールが画面外に出た場合
        if (this.transform.position.z < this.visiblePosZ)
        {
            //GameOverTextにゲームオーバーを表示
            this.gameoverText.GetComponent<Text>().text = "Game Over";
        }

        //ScoreTextに得点を表示
        this.scoreText.GetComponent<Text>().text = "Score : " + score;
    }

    //衝突時に呼ばれる関数
    void OnCollisionEnter(Collision other)
    {
        //ターゲットに応じて得点加算
        if (other.gameObject.tag == "SmallStarTag")
        {
            this.score += 5;
        }
        else if (other.gameObject.tag == "LargeStarTag")
        {
            this.score += 20;
        }
        else if (other.gameObject.tag == "SmallCloudTag")
        {
            this.score += 10;
        }
        else if (other.gameObject.tag == "LargeCloudTag")
        {
            this.score += 50;
        }
    }

}
