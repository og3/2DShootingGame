using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // 弾丸を変数にセット
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        // ゲームスタートと同時にコルーチンをスタートさせる　引数はメソッド名
        StartCoroutine("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        // x軸の入力受付
        float dx = Input.GetAxis("Horizontal") * Time.deltaTime * 8f;
        // y軸の入力受付
        float dy = Input.GetAxis("Vertical") * Time.deltaTime * 8f;
        // 自キャラに入力値を反映する
        transform.position = new Vector3(
            // 動ける範囲を-8.1f〜8.1fに設定
            Mathf.Clamp(transform.position.x + dx, -8.1f, 8.1f),
            Mathf.Clamp(transform.position.y + dy, -4.5f, 4.5f),
            0f
        );
    }

    // 発射のメソッド
    // コルーチンとしてメソッドを作成する宣言のようなもの（IEnumerator）
    IEnumerator Shoot()
    {
        while (true)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            // 0.2秒待ってから値を返す
            yield return new WaitForSeconds(0.2f);
        }        
    }
}
