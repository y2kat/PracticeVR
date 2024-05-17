using UnityEngine;

//atributos que requiere el componente CompanionAgent
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CompanionAgent : BasicAgent
{
    [SerializeField] AgressiveAgentStates agentState;
    Animator animator;
    Rigidbody rb;
    string currentAnimationStateName;
    bool feeded = false;

    void Start() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agentState = AgressiveAgentStates.Idle; //estado inicial
        currentAnimationStateName = ""; //inicializa el nombre del estado de animación actual
    }

    void Update() {
        decisionManager();
    }

    void decisionManager() {
        AgressiveAgentStates newState;
        if (!feeded) {
            newState = AgressiveAgentStates.Idle;
        } else {
            newState = AgressiveAgentStates.Seeking; //si se alimenta y está cerca del objetivo, el estado pasa a seeking
            if (Vector3.Distance(transform.position, target.position) < stopThreshold) {
                newState = AgressiveAgentStates.Idle;
            }
        }
        changeAgentState(newState);
        movementManager();
    }

    void changeAgentState(AgressiveAgentStates t_newState) {
        //cambia el estado sólo si es diferente al estado actual
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
        }
    }

    public void feed(Transform t_target) {
        //establecemos el objetivo y marcamos como alimentado
        target = t_target;
        feeded = true;
    }

    private void seeking() {
        //cambia la animación a "Walk" si no está en ese estado
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

    private enum AgressiveAgentStates {
        Idle,
        Seeking
    }
}
