﻿using UnityEngine;

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
            (_stateMachine.Agent as AgentSpectator).timeEnterTicketOfficeQueue = Time.time;
            _askedForTicket = false;
            _ticketOffice = ticketOffice;
        }

        public override void Action()
        {
            Vector3 target = (_stateMachine.Agent as AgentSpectator).queuePosition;
            _stateMachine.Agent.Body.MoveToDestination(target);
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
                        askForDoor = agent.GetCategory();// Utils.PseudoGaussRandom(1,  Environment.GetInstance().CategoriesNumber);
                        agent.SendMessage(_ticketOffice,new MessageAskForTicket(askForDoor));
                        _askedForTicket = true;
                    }
                    //Second demand, he get a list with available category, he ask
                    else
                    {
                        Debug.Log("new try");
                        int askForDoor =
                            agent.ticketRefused.stillAvailableCategories[Random.Range(0, agent.ticketRefused.stillAvailableCategories.Count - 1)]; // ILLEGAL : if between there a no ticket in this category
                        agent.SendMessage(_ticketOffice,new MessageAskForTicket(askForDoor)); 
                        _askedForTicket = true;
                    }
                }
            }

            if (agent.ticketRefused != null && agent.notifiedTicketRefused)
            {
                _askedForTicket = false;
                agent.notifiedTicketRefused = false;
            }
            
            if (agent.ticket != null)
            {
                _ticketOffice.times.Add(Time.time - agent.timeEnterTicketOfficeQueue);
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