using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public Animator animator;
    public GameObject[] stopPoints;
    public int stackCount;
   
    [SerializeField] private GameObject playerStack;
    [SerializeField] private GameObject prevStack;
    [SerializeField] private GameObject firstStack;
    [SerializeField] private GameObject playerBody;
    [SerializeField] private float speed;

    public bool lockUp;
    public bool lockDown;
    public bool lockLeft;
    public bool lockRight;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        stopPoints = GameObject.FindGameObjectsWithTag(StringManager.StopPoint);
        stackCount = 1;
    }

    private void Start()
    {
        Instantiate(firstStack,transform.position,Quaternion.identity);
        GameManager.instance.SetGameState(GameState.Play);
        StartCoroutine(ChangeIdleAnim());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameState.Play)
        {
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || MobileInput.instance.swipeLeft) && !lockLeft)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.position += Vector3.back * speed * Time.deltaTime;
            }
            if ((Input.GetKeyDown(KeyCode.RightArrow) || MobileInput.instance.swipeRight) && !lockRight)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.position += Vector3.forward * speed * Time.deltaTime;
            }
            if ((Input.GetKeyDown(KeyCode.UpArrow) || MobileInput.instance.swipeUp) && !lockUp)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if ((Input.GetKeyDown(KeyCode.DownArrow) || MobileInput.instance.swipeDown) && !lockDown)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
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

        prevStack.gameObject.tag = StringManager.Untagged;
        prevStack = stack;
        prevStack.GetComponent<BoxCollider>().isTrigger = false;// Doan nay em nghi bat buoc phai getComponent chu khong cache duoc a

        CameraFollow.instance.target = stack.transform;
    }

    public void RemoveStack()
    {
        playerBody.transform.position -= new Vector3(0, 0.3f, 0);
        GameObject topStack = playerStack.transform.GetChild(0).gameObject;
        if(topStack != null)
        {
            topStack.transform.parent = ObjectPooler.instance.pool.transform;
            topStack.SetActive(false);
        }
    }

    public IEnumerator ChangeIdleAnim()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Take 2"))
        {
            animator.SetInteger("renwu", 0);
        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(ChangeIdleAnim());
    }

    public IEnumerator ChangeWinAnim()
    {
        animator.SetInteger("renwu", 2);
        yield return new WaitForSeconds(0.3f);
    }
}