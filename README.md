# 2DShootingGame
unityで２Dゲームを作成する
## 参考
https://dotinstall.com/lessons/2dshooting_unity

## メモ
- キャラクターはsprite画像で作る
- 自キャラを動かすスクリプト
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            transform.position.x + dx,
            transform.position.y + dy,
            0f
        );
    }
}
```
- 移動できる範囲を制限する（Mathf.Clamp）
```
// 動ける範囲を-8.1f〜8.1fに設定
Mathf.Clamp(transform.position.x + dx, -8.1f, 8.1f)
```
- 時間の調整(* Time.deltaTime）
```
Time.deltaTimeを使う方法
Time.deltaTimeには、最後のフレームからの経過時間[ms]が格納されています。
つまり前回のUpdate()からの経過時間がとれます。

Update()が実行されるたびに経過時間を積み上げていき、指定した時間を超えたら望みの処理を実行するようにします。
```
参考：https://qiita.com/Nagitch/items/fb9157b1cb27f3d37696
- 弾丸が連射できるようにする
```
手順：
- 弾丸をprefab化する
- whileで無限ループを作って、プレイヤーの位置から弾丸が出現するようにする

```
- Instantiate
```
一言でいえば、ゲーム中に表示される主人公や敵キャラクターなどのオブジェクトを生成する関数です。
またこの時生成されるオブジェクトはクローンとも呼ばれたりもします。
化学の分野でも、生物のコピーをクローンと呼んだりしますよね?それと同じです。
```
参考：https://www.sejuku.net/blog/48180
- order in layer
```
数値が大きいほどオブジェクトが上に表示される  
設定はインスペクターにて
```
- IEnumerator
```
IEnumeratorはコルーチンのインターフェースという意味です。
とりあえずコルーチンを作るには型をIEnumeratorにすれば作れるということです。（この表現正確にはおかしいんだけどね。）
yield return null はココで処理を中断するということです。
Unityでは、1フレーム分中断して、次のフレームで続きの行の処理を行います。
つまりココでは"1 "を出力したら１フレーム待って"2 "を出力し、さらに1フレーム待って"3 "を出力します。
```
参考：https://qiita.com/kazz4423/items/73219068684e87adc87d
- 画面外に出たgameobjectを削除する
```
# 考え方
- 画面全体をgameobjectで囲い、そのオブジェクトと弾丸のgameobjectが衝突したら弾丸を削除する
# 手順
- 空のgameobjectを作る
create emptyから
- 作ったobjectに「Box collider 2D」を設定する
Add componentから
- これをトリガーにする
is trigerにチェック
- edit colliderで衝突範囲の設定
画面いっぱいに設定
- 弾丸の設定をする
rigidbody 2Dを設定する
body typeはスクリプトによる操作だけなのでkinematic
- 弾丸の衝突範囲を設定する（当たり判定）
capsule collider 2Dを選択する
sizeをいい感じに設定する
- 削除処理のスクリプトを作成する
設定するのはBox collider 2Dを設定したオブジェクト

OnTriggerExit2D(Collider2D collision)
collisionでCollider2Dに衝突したオブジェクトを取得できる

private void OnTriggerExit2D(Collider2D collision)
{
  Destroy(collision.gameObject);
}
以上で画面外に出た弾丸は削除される
```
- 敵を設定する
```
# 仕様
- 画面下部に向けて直進する
- 画面上部から湧いてくる

# 手順
- ゲーム進行を管理するためにオブジェクトを作成する
ここを基準に敵を沸かせる
- 無限沸きの処理を作る

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
    
- 画面外に出たら敵のオブジェクトを削除する
弾丸の時と同じ
- 敵に動きをつける
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
}
- 弾丸と敵の衝突判定を作る
・敵をトリガーにする
is trigerを設定
・弾丸にタグをつける
add tagから
・enemy scriptを編集する


```
