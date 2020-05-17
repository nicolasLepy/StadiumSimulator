using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// When a spectator is coming, he choose the closest stadium entrance
    /// </summary>
    public class SpectatorChoseStadiumEntrance : State
    {
        public SpectatorChoseStadiumEntrance(StateMachine stateMachine) : base(stateMachine)
        {
            Vector3 closestEntrance = Vector3.zero;
            float minDistance = -1;
            foreach (GameObject entrance in GameObject.FindGameObjectsWithTag("StadiumEntrance"))
            {
                float distance = Vector3.Distance(entrance.transform.position, _stateMachine.Agent.Position);
                if (distance < minDistance || minDistance == -1)
                {
                    minDistance = distance;
                    closestEntrance = entrance.transform.position;
                }
            }

            _stateMachine.Agent.Body.MoveToDestination(closestEntrance);
        }

        public override void Action()
        {
            
        }

        public override State Next()
        {
            State res = this;
            var agent = _stateMachine.Agent as AgentSpectator;
            //When close of a ticket office, check ticket offices around him to chose closer
            // WARNING : Use trigger to collect ticket office inside the trigger instead access directly the environment   
            if (agent == null || (agent.ticket != null)) return new SpectatorStateEnterStadium(_stateMachine);
            if (_stateMachine.Agent.Body.GetClosestTicketOfficeDistance() < 50)
            {
                res = new StateGoToTicketOffice(_stateMachine);
            }

            return res;
        }
    }
}