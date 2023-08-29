using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movepoint : MonoBehaviour
{
    public List<Transform> targetObjects = new List<Transform>(); // 移動先のゲームオブジェクトのリスト
    public float moveSpeed = 1.0f; // 移動速度
    private bool isMoving = false; // 移動中かどうかのフラグ
    private Queue<Transform> objectsQueue; // 連続して移動するゲームオブジェクトを保持するキュー

    private void Start()
    {
        objectsQueue = new Queue<Transform>(targetObjects); // リストをキューに変換
    }

    private void Update()
    {
        if (isMoving && objectsQueue.Count > 0)
        {
            Vector3 targetPosition = objectsQueue.Peek().position; // 次の移動先のゲームオブジェクトの位置を取得

            // 現在の位置と目的地の位置の間で線形補間を行う
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 目的地に近づいたらそのゲームオブジェクトをキューから削除
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                Debug.Log($"Arrived at {objectsQueue.Peek().name}"); // 到着したゲームオブジェクトの名前をコンソールに表示
                objectsQueue.Dequeue();
                transform.position = targetPosition; // 位置を正確に設定

                if (objectsQueue.Count == 0) // 全てのゲームオブジェクトを通過したら移動を停止
                {
                    isMoving = false;
                    Debug.Log("Finished moving through all objects"); // 全ての移動が完了したことをコンソールに表示
                }
            }
        }
    }

// このメソッドを呼び出すことで、オブジェクトを指定したゲームオブジェクトのリストを元に連続移動する
    public void StartMovingThroughObjects()
    {
        if(objectsQueue.Count > 0)
        {
            isMoving = true;
            Debug.Log("Started moving through objects"); // 移動を開始したことをコンソールに表示
        }
        else
        {
            Debug.Log("No objects in the queue to move to"); // 移動するゲームオブジェクトがないことをコンソールに表示
        }
    }

}
