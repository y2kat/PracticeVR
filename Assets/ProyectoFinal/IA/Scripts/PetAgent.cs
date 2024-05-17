using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PetAgent : BasicAgent
{
    [SerializeField] AgentStates agentState;
    Animator anim;
    Rigidbody rb;
    string animStateName = "";
    bool isFed = false;

    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        agentState = AgentStates.idling;
    }

    void Update() {
        decisionManager();
    }

    void decisionManager() {
        AgentStates newState = isFed ? AgentStates.seeking : AgentStates.idling;
        if (isFed && Vector3.Distance(transform.position, target.position) < stopThreshold) {
            newState = AgentStates.idling;
        }
        updateState(newState);
        movementManager();
    }

    void updateState(AgentStates t_newState) {
        //cambia el estado s�lo si es diferente al estado actual
        if (agentState == t_newState) {
            return;
        }
        agentState = t_newState;
    }

    void movementManager() {
        //seg�n el estado, ejecuta idle o seek
        switch (agentState) {
            case AgentStates.idling:
                idle();
                break;
            case AgentStates.seeking:
                seek();
                break;
        }
    }

    public void feed(Transform t_target) {
        //establecemos el objetivo y marcamos como alimentado
        target = t_target;
        isFed = true;
    }

    private void seek() {
        // cambia la animaci�n a "Walk" si no est� en ese estado
        if (animStateName != "walk") {
            anim.Play("CatWalk", 0);
            animStateName = "walk";
        }
        //se multiplica temporalmente, entre m�s lejos est� m�s se va�apresurar
        maxVel *= 2;
        rb.velocity = SteeringBehaviours.seek(this, target.position);
        rb.velocity = SteeringBehaviours.arrival(this, target.position, slowingRadius, stopThreshold);
        maxVel /= 2;
    }

    private void idle() {
        if (animStateName != "idle") {
            anim.Play("CatIdle", 0);
            animStateName = "idle";
        }
        rb.velocity = Vector3.zero; //detiene la velocidad
    }

    private enum AgentStates {
        idling,
        seeking
    }
}
