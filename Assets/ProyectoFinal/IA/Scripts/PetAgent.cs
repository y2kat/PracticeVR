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
        //si comió y la distancia al target es menor que el umbral de parada, pos idle
        if (isFed && Vector3.Distance(transform.position, target.position) < stopThreshold) {
            newState = AgentStates.idling;
        }
        updateState(newState); //cambiar el estado del agente
        movementManager();
    }

    void updateState(AgentStates t_newState) {
        //cambia el estado sólo si es diferente al estado actual
        if (agentState == t_newState) {
            return;
        }
        agentState = t_newState;
    }

    void movementManager() {
        //dependiendo del estado del agente...
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
        // cambia la animación a "Walk" si no está en ese estado
        if (animStateName != "walk") {
            anim.Play("CatWalk", 0);
            animStateName = "walk";
        }
        //se multiplica temporalmente, entre más lejos esté más se va apresurar
        maxVel *= 2;
        //establece la velocidad del rb para buscar al objetivo
        rb.velocity = SteeringBehaviours.seek(this, target.position);
        //ajusta la velocidad del rb para llegar al objetivo
        rb.velocity = SteeringBehaviours.arrival(this, target.position, slowingRadius, stopThreshold);
        maxVel /= 2; //restaura la velocidad máxima
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
