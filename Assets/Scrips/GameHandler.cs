using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform enemyTransform;
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
        player1 = Instantiate(playerTransform, new Vector3(300, 200), Quaternion.identity).GetComponent<Player>();
        player2 = Instantiate(enemyTransform, new Vector3(1100, 200), Quaternion.identity).GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAttackPress()
    {
        if (state == State.Busy) return;
        state = State.Busy;
        player1.Attack(player2,player2.transform.position - new Vector3(100,0),"isAttack",()=> {
            state = State.WaitingForPlayer;
        });
    }

    public void OnFlyKickPress()
    {
        if (state == State.Busy) return;
        state = State.Busy;
        player1.Attack(player2, player2.transform.position - new Vector3(100, 0), "isFlyKick",() => {
            state = State.WaitingForPlayer;
        });
    }
}
