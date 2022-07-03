using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public List<GameObject> stopPoints;
    public List<int> canMove = new List<int>() { 0, 0, 0, 0};// left, right, up, down
    [SerializeField] private GameObject playerStack;
    [SerializeField] private GameObject prevStack;
    [SerializeField] private GameObject firstStack;
    [SerializeField] private float speed;
    [SerializeField] private float travelTime;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Instantiate(firstStack,transform.position,Quaternion.identity);
        for(int i = 0; i < stopPoints.Count; i++)
        {
            Debug.Log(stopPoints[i].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.LeftArrow) || MobileInput.instance.swipeLeft) && canMove[0] == 1)
        {
            transform.rotation = Quaternion.Euler(0,-90,0);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if((Input.GetKeyDown(KeyCode.RightArrow) || MobileInput.instance.swipeRight) && canMove[1] == 1)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if((Input.GetKeyDown(KeyCode.UpArrow) || MobileInput.instance.swipeUp) && canMove[2] == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if((Input.GetKeyDown(KeyCode.DownArrow) || MobileInput.instance.swipeDown) && canMove[3] == 1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
    }

    public void AddStack(GameObject stack)
    {
        stack.transform.SetParent(playerStack.transform);
        Vector3 position = prevStack.transform.localPosition;
        position.y -= 0.3f;
        stack.transform.localPosition = position;

        Vector3 playerPosition = transform.localPosition;
        playerPosition.y += 0.3f;
        transform.localPosition = playerPosition;

        prevStack.gameObject.tag = "Untagged";
        prevStack = stack;
        prevStack.GetComponent<BoxCollider>().isTrigger = false;
    }

    public void RemoveStack()
    {
        GameObject topStack = playerStack.transform.GetChild(0).gameObject;
        Destroy(topStack);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "StopPoint")
        {
            Debug.Log("Stop");
            ChangeCanMove(other.gameObject.transform);

            MobileInput.instance.swipeLeft = false;
            MobileInput.instance.swipeRight = false;
            MobileInput.instance.swipeUp = false;
            MobileInput.instance.swipeDown = false;

            Vector3 newPos = other.gameObject.transform.position;
            newPos.y = transform.position.y;
            transform.position = newPos;

            for(int i = 0; i < stopPoints.Count; i++)
            {
                stopPoints[i].SetActive(true);
            }
            other.gameObject.SetActive(false);
        }
    }

    public void ChangeCanMove(Transform curPos)
    {
        for(int i=0; i< canMove.Count; i++)
        {
            canMove[i] = 0;
        }

        for(int i=0; i<stopPoints.Count; i++)
        {
            if (stopPoints[i].transform.position.x == curPos.position.x)
            {
                if(stopPoints[i].transform.position.z > curPos.position.z)
                {
                    canMove[2] = 1;
                }
                else if(stopPoints[i].transform.position.z < curPos.position.z)
                {
                    canMove[3] = 1;
                }
            }
            else if(stopPoints[i].transform.position.z == curPos.position.z)
            {
                if(stopPoints[i].transform.position.x > curPos.position.x)
                {
                    canMove[1] = 1;
                }
                else if (stopPoints[i].transform.position.x < curPos.position.x)
                {
                    canMove[0] = 1;
                }
            }
        }

        for (int i = 0; i < canMove.Count; i++)
        {
            Debug.Log(canMove[i]);
        }
    }
}