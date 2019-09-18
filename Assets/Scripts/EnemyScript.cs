using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // 位相の定義
    public float phase;
    // Start is called before the first frame update
    void Start()
    {
        // 敵が出現した時にphaseの値を決定する
        phase = Random.Range(0f, Mathf.PI * 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(
            // 三角関数にて敵の動きを設定
            Mathf.Cos(Time.frameCount * 0.05f + phase) * 0.05f,
            - 2f * Time.deltaTime,
            0f
            );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // これと衝突したオブジェクトを削除する（弾丸）
            //Destroy(collision.gameObject);
            // このオブジェクト自身を削除する（敵機）
            Destroy(gameObject);
        }
    }
}
