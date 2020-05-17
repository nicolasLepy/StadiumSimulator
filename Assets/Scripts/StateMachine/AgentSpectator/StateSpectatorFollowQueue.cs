using UnityEngine;
using UnityEngine.AI;

namespace MultiAgentSystem
{
    /// <summary>
    /// The spectator follow a queue for something (ticket,...)
    /// </summary>
    public class StateSpectatorFollowQueue : State
    {

        private AgentTicketOffice _ticketOffice;
        private bool _askedForTicket;
        
        public StateSpectatorFollowQueue(StateMachine stateMachine, AgentTicketOffice ticketOffice) : base(stateMachine)
        {
            _askedForTicket = false;
            _ticketOffice = ticketOffice;
        }

        public override void Action()
        {
            Vector3 target = (_stateMachine.Agent as AgentSpectator).queuePosition;
            this._stateMachine.Agent.Body.GetComponent<NavMeshAgent>().destination = target;
        }

        public override State Next()
        {
            AgentSpectator agent = _stateMachine.Agent as AgentSpectator;
            State res = this;

            if (!_askedForTicket && _ticketOffice.queue.First() == agent)
            {
                if (Vector3.Distance(agent.Position, _ticketOffice.Position) < 2f)
                {
                    //First demand
                    if (agent.ticketRefused == null)
                    {
                        int askForDoor = 0;
                        askForDoor = Utils.PseudoGaussRandom(1,  Environment.GetInstance().CategoriesNumber);
                        agent.SendMessage(_ticketOffice,new MessageAskForTicket(askForDoor));
                        _askedForTicket = true;
                    }
                    //Second demand, he get a list with available category, he ask
                    else
                    {
                        Debug.Log("new try");
                        int askForDoor =
                            agent.ticketRefused.stillAvailableCategories[Random.Range(0, agent.ticketRefused.stillAvailableCategories.Count - 1)]; // ILLEGAL : if between there a no ticket in this category
                        //askForDoor = Utils.PseudoGaussRandom(1,  Environment.GetInstance().CategoriesNumber);
                        agent.SendMessage(_ticketOffice,new MessageAskForTicket(askForDoor)); 
                        _askedForTicket = true;
                    }
                }
            }

            if (agent.ticketRefused != null && agent.notifiedTicketRefused)
            {
                _askedForTicket = false;
                agent.notifiedTicketRefused = false;
                //res = this;  //new SpectatorStateGoOut(_stateMachine);
            }
            
            if (agent.ticket != null)
            {
                res = new SpectatorStateEnterStadium(_stateMachine);
            }

            if (agent.noTicketAvailable)
            {
                res = new SpectatorStateGoOut(_stateMachine);
            }
            return res;
        }
    }
}