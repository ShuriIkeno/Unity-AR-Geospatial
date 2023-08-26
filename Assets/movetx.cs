using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetx : MonoBehaviour
{

    // public float radius = 100.0f;  // 円の半径
    // public float speed = 1.0f;     // 速度
    // public float upwardSpeed = 20; // 上方向への速度
    // public float escapeSpeed = 10.0f; // 逃げる速度

    // private float angle = 0.0f;
    // private bool isEscaping = false;
    // private float startTime;
    // public float delayTime = 5f;

    // public Color skyColor = new Color(0.1f, 0.1f, 0.3f); // 暗い青色

    // void Start()
    // {
    //     // 空の色を変更
    //     RenderSettings.skybox.SetColor("_Tint", skyColor);
    //     startTime = Time.time;
    // }

    // void Update()
    // {
    //     if((Time.time-startTime)>=delayTime)
    //     {
    //         if (isEscaping)
    //         {
    //             transform.position += new Vector3(0, escapeSpeed, escapeSpeed) * Time.deltaTime;
    //             transform.eulerAngles = new Vector3(0, 90, 30);
    //             if (transform.position.y > 300.0f)
    //             {
    //                 Destroy(gameObject);
    //             }
    //         }
    //         else
    //         {
    //             angle += speed * Time.deltaTime;
    //             float x = radius * Mathf.Cos(angle);
    //             float z = radius * Mathf.Sin(angle);
    //             transform.position = new Vector3(x, transform.position.y + upwardSpeed * Time.deltaTime, z);

    //             // Y軸の回転に角度を直接代入
    //             transform.eulerAngles = new Vector3(0, 90 -angle * Mathf.Rad2Deg, 0);

    //             if (angle > Mathf.PI * 3)
    //             {
    //                 isEscaping = true;
    //             }
    //         }
    //     }
    // }

//     public Transform centerPoint; // 中心点のTransform
//     public float radius = 100.0f;  // 円の半径
//     public float rotationSpeed = -1f; // 負の値に変更

//     private float angle = 0.0f;

//     void Update()
//     {
//         // 角度を増加させて円運動を実現
//         angle += rotationSpeed * Time.deltaTime;

//         // 新しい位置を計算
//         float x = centerPoint.position.x + radius * Mathf.Cos(angle);
//         float y = centerPoint.position.y;
//         float z = centerPoint.position.z + radius * Mathf.Sin(angle);

//         // 3Dモデルの位置を更新
//         transform.position = new Vector3(x, y, z);

//         // モデルの向きを円運動の進行方向に向ける
//         Vector3 lookAtPoint = centerPoint.position;
//         lookAtPoint.y = transform.position.y; // Y軸の高さを合わせる
//         transform.LookAt(lookAtPoint);
//     }
// }

//     public float ellipseSemiMajorAxis = 2.0f;    // 楕円の半長軸
//     public float ellipseSemiMinorAxis = 1.0f;    // 楕円の半短軸
//     public float ellipseSpeed = 1.0f;            // 楕円運動の速さ
//     public float straightSpeed = 2.0f;           // 直進運動の速さ
//     public float rotationSpeed = 45.0f;          // Uターンの角速度
//     public float up = 1.0f;                      // 上昇運動の速さ
//     private float y;
//     private Vector3 initialPosition;
//     private float startTime;

//     private void Start()
//     {
//         initialPosition = transform.position;
//         startTime = Time.time;
//     }

//     private void Update()
//     {
//         float elapsedTime = Time.time - startTime;
//         Vector3 forward = transform.forward;

//         // 楕円運動
//         float x = Mathf.Sin(elapsedTime * ellipseSpeed) * ellipseSemiMajorAxis;
//         float z = Mathf.Sin(elapsedTime * ellipseSpeed/2) * ellipseSemiMinorAxis;
//         y += up;
//         Vector3 ellipsePosition = initialPosition + new Vector3(x, y, z);

//         // 直進運動とUターン
//         if (ellipsePosition.x <= initialPosition.x)
//         {
//             float rotationAngle = rotationSpeed * Time.deltaTime;
//             transform.Rotate(Vector3.up, rotationAngle);

            
//             transform.Translate(forward * straightSpeed * Time.deltaTime);

//             if (transform.position.x <= initialPosition.x - ellipseSemiMajorAxis)
//             {
//                 startTime = Time.time;
//             }
//         }
//         else
//         {
//             transform.position = ellipsePosition;
//             transform.LookAt(transform.position + forward);
//         }
//     }

// }

    public float ellipseSemiMajorAxis = 2.0f;
    public float ellipseSemiMinorAxis = 1.0f;
    public float ellipseSpeed = 1.0f;
    public float straightSpeed = 2.0f;
    public float rotationSpeed = 45.0f;
    public float halfCircleRadius = 1.0f;
    public float halfCircleSpeed = 1.0f;
    public float quarterCircleRadius = 1.0f;
    public float quarterCircleSpeed = 1.0f;


    private Vector3 initialPosition;
    private Vector3 ellipsePosition;
    private Vector3 straightPosition;
    private Vector3 halfCirclePosition;

    private float startTime;

    private void Start()
    {
        initialPosition = transform.position;
        startTime = Time.time;
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime;
        Vector3 forward = transform.forward;

        // 半楕円運動
        if (elapsedTime <= Mathf.PI)
        {
            float x = Mathf.Cos(elapsedTime * ellipseSpeed) * ellipseSemiMajorAxis;
            float z = Mathf.Sin(elapsedTime * ellipseSpeed) * ellipseSemiMinorAxis;
            ellipsePosition = initialPosition + new Vector3(x, 0, z);
            transform.position = ellipsePosition;
            transform.LookAt(transform.position + forward);
        }
        // 直進運動
        else if (elapsedTime <= Mathf.PI + (ellipseSemiMajorAxis / straightSpeed))
        {
            float distance = (elapsedTime - Mathf.PI) * straightSpeed;
            straightPosition = ellipsePosition - forward * distance;
            transform.position = straightPosition;
            transform.LookAt(straightPosition + forward);
        }
        // 半円運動
        else if (elapsedTime <= Mathf.PI * 2 + (ellipseSemiMajorAxis / straightSpeed))
        {
            float angle = (elapsedTime - Mathf.PI - (ellipseSemiMajorAxis / straightSpeed)) * halfCircleSpeed;
            float x = Mathf.Sin(angle) * halfCircleRadius;
            float z = Mathf.Sin(angle+Mathf.PI) * halfCircleRadius;
            halfCirclePosition = straightPosition + new Vector3(x, 0, z);
            transform.position = halfCirclePosition;
            transform.LookAt(transform.position + forward);
        }
        // 1/4円運動
        else if (elapsedTime <= Mathf.PI * 2 + (ellipseSemiMajorAxis / straightSpeed) + (Mathf.PI / (2 * quarterCircleSpeed)))
        {
            float angle = (elapsedTime - Mathf.PI * 2 - (ellipseSemiMajorAxis / straightSpeed)) * quarterCircleSpeed;
            float x = Mathf.Cos(angle) * quarterCircleRadius;
            float z = Mathf.Sin(angle) * quarterCircleRadius;
            Vector3 quarterCirclePosition = halfCirclePosition + new Vector3(-x, 0, z);
            transform.position = quarterCirclePosition;
            transform.LookAt(transform.position + forward);
        }
    }
}
