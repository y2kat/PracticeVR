using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class AgressiveAgent : BasicAgent
{
    [SerializeField] Vector3 cubeSize;
    [SerializeField] Transform cubePercept;
    [SerializeField] AgentStates agentState;
    Animator anim;
    Rigidbody rb;
    [SerializeField] Collider[] perceived;
    string animStateName = "";
    bool enemyInTerritory = false;

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
        //actualizamos la variable perceived con los objetos que se encuentran en el cubo
        perceived = Physics.OverlapBox(cubePercept.position, cubeSize * .5f);
    }

    void perceptionManager() {
        if (perceived != null) {
            //tmp representa a cada uno de los objetos que el agente está percibiendo en ese momento
            foreach (Collider tmp in perceived) {
                //comprueba si el objeto percibido tiene la etiqueta player
                if (tmp.CompareTag("Player")) {
                    //el agente establece ese objeto como su target
                    target = tmp.transform;
                    enemyInTerritory = true;
                }
                else {
                    enemyInTerritory = false;
                }
            }
        }
    }

    void decisionManager() {
        AgentStates newState;
        if (target == null) {
            newState = AgentStates.Idle;
        }
        else if (enemyInTerritory) {
            newState = AgentStates.Seeking;
            //si la distancia al target es menor que el umbral de parada, pos ataca :B
            if (Vector3.Distance(transform.position, target.position) < stopThreshold) {
                newState = AgentStates.Attack;
            }
        }
        else {
            newState = AgentStates.Returning;
            target = cubePercept;
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
            case AgentStates.Seeking:
                seeking();
                break;
            case AgentStates.Attack:
                attacking();
                break;
            case AgentStates.Returning:
                returning();
                break;
        }
    }

    private void seeking() {
        // cambia la animación a "Walk" si no está en ese estado
        if (animStateName != "walk") {
            anim.Play("LionWalk", 0);
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

    private void attacking() {
        // cambia la animación a "Attack" si no está en ese estado
        if (animStateName != "attack") {
            anim.Play("LionJump", 0);
            animStateName = "attack";
        }
    }

    private void idling() {
        // cambia la animación a "Idle" si no está en ese estado
        if (animStateName != "idle") {
            anim.Play("LionIdle", 0);
            animStateName = "idle";
        }
        rb.velocity = Vector3.zero; //detiene la velocidad
    }

    private void returning() {
        // cambia la animación a "Walk" si no está en ese estado
        if (animStateName != "walk") {
            anim.Play("LionWalk", 0);
            animStateName = "walk";
        }
        rb.velocity = SteeringBehaviours.seek(this, target.position);
        //si la distancia al target es menor o igual que el radio de des, pos es null porque
        //ya llegó a su destino :V
        if (Vector3.Distance(transform.position, target.position) <= slowingRadius) {
            target = null;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(cubePercept.position, cubeSize);
    }

    private enum AgentStates {
        Idle,
        Seeking,
        Attack,
        Returning
    }
}
