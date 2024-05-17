using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class ShyAgent : BasicAgent
{
    [SerializeField] float eyesPerceptRadious, earsPerceptRadious;
    [SerializeField] Transform eyesPercept, earsPercept;
    [SerializeField] AgentStates agentState;
    Animator anim;
    Rigidbody rb;
    //dos arrays de Colliders que representan los objetos percibidos por los "ojos" y "oídos" del agente
    Collider[] perceived, perceived2;
    string animStateName = "";

    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        agentState = AgentStates.Idle;
    }

    void Update() {
        perceptionManager();
        decisionManager();
    }

    void FixedUpdate() {
        //detecta los objetos dentro del radio de percepción de los "ojos" y "oídos" del agente
        perceived = Physics.OverlapSphere(eyesPercept.position, eyesPerceptRadious);
        perceived2 = Physics.OverlapSphere(earsPercept.position, earsPerceptRadious);
    }

    void perceptionManager() {
        if (perceived != null) {
            //tmp representa a cada uno de los objetos que el agente está percibiendo en ese momento
            foreach (Collider tmp in perceived) {
                //comprueba si el objeto percibido tiene la etiqueta player
                if (tmp.CompareTag("Player")) {
                    //el agente establece ese objeto como su target
                    target = tmp.transform;
                }
            }
        }
        if (perceived2 != null) {
            foreach (Collider tmp in perceived2) {
                if (tmp.CompareTag("Player")) {
                    target = tmp.transform;
                }
            }
        }
    }

    void decisionManager() {
        AgentStates newState;
        if (target == null) {
            newState = AgentStates.Idle;
        }
        else {
            newState = AgentStates.Fleeting;
            //si la distancia al objetivo es MAYOR que el umbral de parada, se elimina el target y volvemos al idle
            if (Vector3.Distance(transform.position, target.position) > stopThreshold) {
                target = null;
            }
        }
        updateState(newState); //cambia el estado del agente
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
            case AgentStates.Idle:
                idling();
                break;
            case AgentStates.Fleeting:
                fleeting();
                break;
        }
    }

    private void idling() {
        if (animStateName != "idle") {
            anim.Play("PenguinIdle", 0);
            animStateName = "idle";
        }
        rb.velocity = Vector3.zero; //detiene la velocidad
    }

    private void fleeting() {
        if (animStateName !=  "walk") {
            anim.Play("PenguinWalk", 0);
            animStateName = "walk";
        }
        if (target == null) {
            return;
        }
        //establece la velocidad del rb para huir/fleear del target
        rb.velocity = SteeringBehaviours.flee(this, target.position);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //dibuja una esfera alrededor de la posición de los "ojos" del agente
        Gizmos.DrawWireSphere(eyesPercept.position, eyesPerceptRadious);
        Gizmos.color = Color.blue;
        //dibuja una esfera alrededor de la posición de los "oídos" del agente
        Gizmos.DrawWireSphere(earsPercept.position, earsPerceptRadious);
    }

    private enum AgentStates {
        Idle,
        Fleeting
    }
}
