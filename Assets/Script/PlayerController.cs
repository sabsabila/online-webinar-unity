using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PhotonView photonView;
    [SerializeField] private float walkVelocity = 2.5f;
    [SerializeField] private float runVelocity = 5f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private List<GameObject> avatars;
    [SerializeField] private TMP_Text playerNameTxt;

    private Vector2 input;
    private float angle;
    private float moveVelocity;

    private Quaternion targetRotation;
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
        moveVelocity = walkVelocity;
    }

    void Update()
    {
        if (photonView.isMine)
        {
            GetInput();

            if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1)
            {
                return;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                moveVelocity = runVelocity;
            }
            else
            {
                moveVelocity = walkVelocity;
            }

            CalculateDirection();
            Rotate();
            Move();
        }
    }

    private void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    private void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    private void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        transform.position += transform.forward * moveVelocity * Time.deltaTime;
    }

    public void RandomizeAvatar()
    {
        int randomValue = Random.Range(0, avatars.Count);
        for(int i = 0; i < avatars.Count; i++)
        {
            if (i == randomValue)
                avatars[i].SetActive(true);
            else
                avatars[i].SetActive(false);
        }
        photonView.RPC("RandomizeAvatar_RPC", PhotonTargets.Others, randomValue);
    }

    public void SetName()
    {
        playerNameTxt.text = "Player " + photonView.ownerId;
        photonView.RPC("SetName_RPC", PhotonTargets.Others);
    }


    [PunRPC]
    void RandomizeAvatar_RPC(int index)
    {
        for (int i = 0; i < avatars.Count; i++)
        {
            if (i == index)
                avatars[i].SetActive(true);
            else
                avatars[i].SetActive(false);
        }
    }

    [PunRPC]
    public void SetName_RPC()
    {
        playerNameTxt.text = "Player " + photonView.ownerId;
    }
}
