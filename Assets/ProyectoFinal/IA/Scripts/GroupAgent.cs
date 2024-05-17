using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class GroupAgent : BasicAgent
{
    [SerializeField] float eyesPerceptRadious, earsPerceptRadious;
    [SerializeField] Transform eyesPercept, earsPercept;
    [SerializeField] AgentStates agentState;
    Animator anim;
    Rigidbody rb;
    //dos arrays de Colliders que representan los objetos percibidos por los "ojos" y "o�dos" del agente
    [SerializeField] Collider[] perceived, perceived2; 
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
        //detecta los objetos dentro del radio de percepci�n de los "ojos" y "o�dos" del agente
        perceived = Physics.OverlapSphere(eyesPercept.position, eyesPerceptRadious);
        perceived2 = Physics.OverlapSphere(earsPercept.position, earsPerceptRadious);
    }

    void perceptionManager() {
        if (perceived != null) {
            //tmp representa a cada uno de los objetos que el agente est� percibiendo en ese momento
            foreach (Collider tmp in perceived) {
                //comprueba si el objeto percibido tiene la etiqueta compadre
                if (tmp.CompareTag("Compadre")) {
                    //el agente establece ese objeto como su target
                    target = tmp.transform;
                }
            }
        }
        if (perceived2 != null) {
            foreach (Collider tmp in perceived2) {
                if (tmp.CompareTag("Compadre")) {
                    target = tmp.transform;
                }
            }
        }
    }

    void decisionManager() {
        AgentStates newState;
        if (target == null) {
            newState = AgentStates.Wander;
        }
        else {
            newState = AgentStates.Seek;
            //si la distancia al target es menor que el umbral de parada, pos idle
            if (Vector3.Distance(transform.position, target.position) < stopThreshold) {
                newState = AgentStates.Idle;
            }
        }
        updateState(newState); //cambia el estado del agente
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
        //dependiendo del estado del agente...
        switch (agentState) {
            case AgentStates.Idle:
                idle();
                break;
            case AgentStates.Wander:
                wander();
                break;
            case AgentStates.Seek:
                seek();
                break;
        }
    }

    private void seek() {
        // cambia la animaci�n a "Walk" si no est� en ese estado
        if (animStateName != "walk") {
            anim.Play("ChickenWalk", 0);
            animStateName = "walk";
        }
        //se multiplica temporalmente, entre m�s lejos est� m�s se va�apresurar
        maxVel *= 2;
        //establece la velocidad del rb para buscar al objetivo
        rb.velocity = SteeringBehaviours.seek(this, target.position);
        //ajusta la velocidad del rb para llegar al objetivo
        rb.velocity = SteeringBehaviours.arrival(this, target.position, slowingRadius, stopThreshold);
        maxVel /= 2; //restaura la velocidad m�xima
    }

    private void idle() {
        if (animStateName != "idle") {
            anim.Play("ChickenIdle", 0);
            animStateName = "idle";
        }
        rb.velocity = Vector3.zero; //detiene la velocidad
    }

    private void wander() {
        if (animStateName != "walk") {
            anim.Play("ChickenWalk", 0);
            animStateName = "walk";
        }
        //si no hay una pr�xima posici�n para wander o si la distancia a la pr�xima posici�n para wander es menor que 0.5...
        if ((wanderNextPosition == null) ||
            (Vector3.Distance(transform.position, wanderNextPosition.Value) < 0.5f)) {
            //establece la pr�xima posici�n para wander
            wanderNextPosition = SteeringBehaviours.wanderNextPos(this);
        }
        //establece la velocidad del rb para buscar la pr�xima posici�n para wanderear
        rb.velocity = SteeringBehaviours.seek(this, wanderNextPosition.Value);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //dibuja una esfera alrededor de la posici�n de los "ojos" del agente
        Gizmos.DrawWireSphere(eyesPercept.position, eyesPerceptRadious);
        Gizmos.color = Color.blue;
        //dibuja una esfera alrededor de la posici�n de los "o�dos" del agente
        Gizmos.DrawWireSphere(earsPercept.position, earsPerceptRadious);
    }

    private enum AgentStates {
        Idle,
        Seek,
        Wander
    }
}
