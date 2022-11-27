using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    [SerializeField] private GameObject playercam, playerLookCam;
    [SerializeField] private float walkVelocity = 2.5f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private List<GameObject> avatars;
    [SerializeField] private TMP_Text playerNameTxt;

    private Vector2 input;
    private float angle;
    private float moveVelocity;
    private PlayerInput playerInput;
    private Quaternion targetRotation;

    void Start()
    {
        moveVelocity = walkVelocity;
        playerInput = GetComponent<PlayerInput>();

        if (photonView.isMine)
        {
            playercam.SetActive(true);
            playerLookCam.SetActive(true);
        }
    }

    void Update()
    {
        if (photonView.isMine)
        {
            GetInput();

            if (Mathf.Abs(input.x) < 0.01 && Mathf.Abs(input.y) < 0.01)
            {
                return;
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
        Vector2 inputValue = playerInput.actions["Move"].ReadValue<Vector2>();
        input.x = inputValue.x;
        input.y = inputValue.y;
    }

    private void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
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
    public void RandomizeAvatar_RPC(int index)
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
