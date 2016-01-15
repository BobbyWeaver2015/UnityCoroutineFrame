using UnityEngine;
using System.Collections;


public class TestCoroutine : MonoBehaviour
{
    private int num = 0;
    private CoroutineTask curCoroutineTask;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Q))
	    {
	        num++;
	        print("Start Coroutine " + num + " : ");
            curCoroutineTask = CoroutineManager.Instance.StartCoroutine(Test());
	    }
        if (Input.GetKeyDown(KeyCode.W))
        {
            print("Stop Coroutine " + num + " : ");
            CoroutineManager.Instance.StopCoroutine(curCoroutineTask);
        }
	}

    IEnumerator Test()
    {
        int temp = num;
        while (true)
        {
            print(temp);
            yield return null;
        }
    }
}
