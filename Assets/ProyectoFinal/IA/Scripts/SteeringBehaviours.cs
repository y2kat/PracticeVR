using UnityEngine;

/// <summary>
/// Class containing static methods for implementing steering behaviors for agents.
/// </summary>
public class SteeringBehaviours {
    /// <summary>
    /// Moves the agent towards the target position.
    /// </summary>
    /// <param name="agent">The agent to be moved.</param>
    /// <param name="targetPosition">The position to move towards.</param>
    /// <returns>The steering force to apply to the agent.</returns>
    public static Vector3 seek (BasicAgent agent, Vector3 targetPosition) {
        Vector3 desiredVel = targetPosition - agent.transform.position; //diferencia entre la posición objetivo y la posición actual del agente
        return calculateSteer(agent, desiredVel); //obtiene la fuerza de dirección que se aplicará al agente
    }

    /// <summary>
    /// Moves the agent away from the target position.
    /// </summary>
    /// <param name="agent">The agent to be moved.</param>
    /// <param name="targetPosition">The position to move away from.</param>
    /// <returns>The steering force to apply to the agent.</returns>
    public static Vector3 flee (BasicAgent agent, Vector3 targetPosition) {
        Vector3 desiredVel = agent.transform.position - targetPosition; //diferencia entre la posición actual del agente y la posición objetivo
        return calculateSteer(agent, desiredVel);
    }

    /// <summary>
    /// Moves the agent towards the target position, slowing down as it approaches.
    /// </summary>
    /// <param name="agent">The agent to be moved.</param>
    /// <param name="targetPosition">The position to move towards.</param>
    /// <param name="slowingRadious">The radius at which the agent starts slowing down.</param>
    /// <param name="threshold">The threshold distance for applying slowing down.</param> //umbral de distancia
    /// <returns>The velocity to apply to the agent.</returns>
    public static Vector3 arrival (BasicAgent agent, Vector3 targetPosition, float slowingRadious, float threshold) {
        //distancia entre la posición actual del agente y la posición objetivo
        float distance = Vector3.Distance(agent.transform.position, targetPosition);
        if (distance < slowingRadious) {
            if (distance < threshold) {
                //return Vector3.zero; //no se aplica ninguna fuerza (se devuelve un vector nulo)
            }
            //devuelve una fuerza proporcional a la velocidad actual del agente en dirección al objetivo
            return agent.GetComponent<Rigidbody>().velocity * ( distance / slowingRadious );
        } else {
            //devuelve la velocidad actual del agente
            return agent.GetComponent<Rigidbody>().velocity;
        }
    }

    /// <summary>
    /// Calculates a random wandering direction for the agent.
    /// </summary>
    /// <param name="agent">The agent to wander.</param>
    /// <returns>The random wandering direction.</returns>
    public static Vector3 wanderNextPos (BasicAgent agent) {
        //copia normalizada de la velocidad actual del agente
        Vector3 velCopy = agent.transform.GetComponent<Rigidbody>().velocity;
        velCopy.Normalize();
        //multiplicamos la copia normalizada por el desplazamiento de wander
        velCopy *= agent.getWanderDisplacement();
        //dirección aleatoria en el plano XY y se normaliza
        Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        randomDirection.Normalize();
        //multiplica la dirección aleatoria por el radio de wander y se agrega a la copia normalizada
        randomDirection *= agent.getWanderRadius();
        randomDirection += velCopy;
        //suma la posición actual del agente a esta dirección y la devuelve como la siguiente posición de wander
        randomDirection += agent.transform.position;
        return randomDirection;
    }

    /// <summary>
    /// Calculates the steering force to apply to the agent.
    /// </summary>
    /// <param name="agent">The agent to be moved.</param>
    /// <param name="desiredVel">The desired velocity of the agent.</param>
    /// <returns>The steering force to apply to the agent.</returns>
    private static Vector3 calculateSteer(BasicAgent agent, Vector3 desiredVel) {
        Rigidbody agentRB = agent.GetComponent<Rigidbody>();
        desiredVel.Normalize();
        desiredVel *= agent.getMaxVel();
        Vector3 steering = desiredVel - agentRB.velocity;
        //para que no exceda la fuerza máxima de dirección y la divide por la masa del agente
        steering = truncate(steering, agent.getMaxSteerForce());
        steering /= agentRB.mass;
        //se añade la velocidad actual del agente a la fuerza de dirección
        steering += agentRB.velocity;
        //la trunca nuevamente para que no exceda la velocidad máxima del agente
        steering = truncate(steering, agent.getSpeed());
        steering.y = agentRB.velocity.y;
        //orienta visualmente al agente hacia la dirección deseada
        lookAt(agent.transform, steering);
        return steering;
    }

    /// <summary>
    /// Makes the agent look at the given direction.
    /// </summary>
    /// <param name="agent">The agent to rotate.</param>
    /// <param name="currentVel">The direction to look at.</param>
    public static void lookAt (Transform agent, Vector3 currentVel) {
        agent.transform.LookAt(agent.position + currentVel);
    }

    /// <summary>
    /// Truncates the vector if its magnitude exceeds the specified maximum value.
    /// </summary>
    /// <param name="vector">The vector to truncate.</param>
    /// <param name="maxValue">The maximum value for the vector's magnitude.</param>
    /// <returns>The truncated vector.</returns>
    private static Vector3 truncate (Vector3 vector, float maxValue) {
        if (vector.magnitude <= maxValue) {
            return vector;
        }
        vector.Normalize();
        return vector *= maxValue;
    }
}