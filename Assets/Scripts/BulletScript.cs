using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 現在位置にy座標+10にする
        transform.position += new Vector3(0f, 10f * Time.deltaTime, 0f);
    }
}
