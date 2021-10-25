using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    private Player player1;
    private Player player2;
    private State state;
    private enum State
    {
        WaitingForPlayer,
        Busy,
    }

    // Start is called before the first frame update
    void Start()
    {
        player1 = Instantiate(playerTransform, new Vector3(-8, 0), Quaternion.identity).GetComponent<Player>();
        player2 = Instantiate(playerTransform, new Vector3(8, 0), Quaternion.identity).GetComponent<Player>();
        state = State.WaitingForPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAttackPress()
    {
        if (state == State.Busy) return;
        state = State.Busy;
        player1.Attack(player2.transform.position,(int dame)=> {
            state = State.WaitingForPlayer;
            player2.Damage(dame);
        });
    }
}
