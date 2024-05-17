using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class TerritorialAgent : BasicAgent {

    [SerializeField] Vector3 cubeSize;
    [SerializeField] Transform cubePercept;
    [SerializeField] AgressiveAgentStates agentState;
    Animator animator;
    Rigidbody rb;
    [SerializeField] Collider[] perceibed;
    string currentAnimationStateName;
    bool enemyInTerrytory = false;

    void Start() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agentState = AgressiveAgentStates.Idle;
        currentAnimationStateName = "";
    }

    void Update() {
        perceptionManager();
        decisionManager();
    }

    void FixedUpdate() {
        perceibed = Physics.OverlapBox(cubePercept.position, cubeSize * .5f);
    }

    void perceptionManager() {
        if (perceibed != null) {
            foreach (Collider tmp in perceibed) {
                if (tmp.CompareTag("Player")) {
                    target = tmp.transform;
                    enemyInTerrytory = true;
                } else { 
                    enemyInTerrytory = false;
                }
            }
        }
    }

    void decisionManager() {
        AgressiveAgentStates newState;
        if (target == null) {
            newState = AgressiveAgentStates.Idle;
        } else if (enemyInTerrytory){
            newState = AgressiveAgentStates.Seeking;
            if (Vector3.Distance(transform.position, target.position) < stopThreshold) {
                newState = AgressiveAgentStates.Attack;
            }
        } else {
            newState = AgressiveAgentStates.Returning;
            target = cubePercept;
        }
        changeAgentState(newState);
        movementManager();
    }

    void changeAgentState(AgressiveAgentStates t_newState) {
        if (agentState == t_newState) {
            return;
        }
        agentState = t_newState;
    }

    void movementManager() {
        switch (agentState) {
            case AgressiveAgentStates.Idle:
                idling();
                break;
            case AgressiveAgentStates.Seeking:
                seeking();
                break;
            case AgressiveAgentStates.Attack:
                attacking();
                break;
            case AgressiveAgentStates.Returning:
                returning();
                break;
        }
    }

    private void seeking() {
        if (!currentAnimationStateName.Equals("walk")) {
            animator.Play("Walk", 0);
            currentAnimationStateName = "walk";
        }
        maxVel *= 2;
        rb.velocity = SteeringBehaviours.seek(this, target.position);
        rb.velocity = SteeringBehaviours.arrival(this, target.position, slowingRadius, stopThreshold);
        maxVel /= 2;
    }

    private void attacking() {
        if (!currentAnimationStateName.Equals("attack")) {
            animator.Play("Attack1", 0);
            currentAnimationStateName = "attack";
        }
    }

    private void idling() {
        if (!currentAnimationStateName.Equals("idle")) {
            animator.Play("Idle", 0);
            currentAnimationStateName = "idle";
        }
        rb.velocity = Vector3.zero;
    }

    private void returning() {
        if (!currentAnimationStateName.Equals("walk")) {
            animator.Play("Walk", 0);
            currentAnimationStateName = "walk";
        }
        rb.velocity = SteeringBehaviours.seek(this, target.position);
        if (Vector3.Distance(transform.position, target.position) <= slowingRadius) {
            target = null;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(cubePercept.position, cubeSize);
    }

    private enum AgressiveAgentStates {
        Idle,
        Seeking,
        Attack,
        Returning
    }
}
