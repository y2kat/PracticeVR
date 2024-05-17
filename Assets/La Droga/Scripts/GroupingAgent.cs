using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class GroupingAgent : BasicAgent
{
    [SerializeField] float eyesPerceptRadious, earsPerceptRadious;
    [SerializeField] Transform eyesPercept, earsPercept;
    [SerializeField] AgressiveAgentStates agentState;
    Animator animator;
    Rigidbody rb;
    [SerializeField] Collider[] perceibed, perceibed2;
    string currentAnimationStateName;

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
        perceibed = Physics.OverlapSphere(eyesPercept.position, eyesPerceptRadious);
        perceibed2 = Physics.OverlapSphere(earsPercept.position, earsPerceptRadious);
    }

    void perceptionManager() {
        if (perceibed != null) {
            foreach (Collider tmp in perceibed) {
                if (tmp.CompareTag("Companion")) {
                    target = tmp.transform;
                }
            }
        }
        if (perceibed2 != null) {
            foreach (Collider tmp in perceibed2) {
                if (tmp.CompareTag("Companion")) {
                    target = tmp.transform;
                }
            }
        }
    }

    void decisionManager() {
        AgressiveAgentStates newState;
        if (target == null) {
            newState = AgressiveAgentStates.Wander;
        } else {
            newState = AgressiveAgentStates.Seek;
            if (Vector3.Distance(transform.position, target.position) < stopThreshold) {
                newState = AgressiveAgentStates.Idle;
            }
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
            case AgressiveAgentStates.Wander:
                wandering();
                break;
            case AgressiveAgentStates.Seek:
                seeking();
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

    private void idling() {
        if (!currentAnimationStateName.Equals("idle")) {
            animator.Play("Idle", 0);
            currentAnimationStateName = "idle";
        }
        rb.velocity = Vector3.zero;
    }

    private void wandering() {
        if (!currentAnimationStateName.Equals("walk")) {
            animator.Play("Walk", 0);
            currentAnimationStateName = "walk";
        }
        if ((wanderNextPosition == null) ||
            (Vector3.Distance(transform.position, wanderNextPosition.Value) < 0.5f)) {
            wanderNextPosition = SteeringBehaviours.wanderNextPos(this);
        }
        rb.velocity = SteeringBehaviours.seek(this, wanderNextPosition.Value);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(eyesPercept.position, eyesPerceptRadious);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(earsPercept.position, earsPerceptRadious);
    }

    private enum AgressiveAgentStates {
        Idle,
        Seek,
        Wander
    }
}