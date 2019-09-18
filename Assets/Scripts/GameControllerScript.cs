using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    // 敵のゲームオブジェクトを作成　見た目はインスペクタにて設定する
    public GameObject enemy;

    // コルーチンの処理を書く
    IEnumerator SpawnEnemy()
    {
        // 無限ループ
        while (true) {
            // enemyのインスタンスを作成
            Instantiate(
            enemy,
            new Vector3(Random.Range(-8f, 8f), transform.position.y, 0f),
            transform.rotation
            );
            // 0.2秒間隔でInstantiateが値を返すようにする
            yield return new WaitForSeconds(0.2f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // コルーチンの実行
        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
