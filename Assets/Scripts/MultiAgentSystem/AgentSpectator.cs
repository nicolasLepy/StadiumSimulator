using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MultiAgentSystem;
using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// Represents a spectator
    /// </summary>
    public class AgentSpectator : Agent
    {

        public override Vector3 Position => _body.transform.position;

        /// <summary>
        /// Create an agent spectator
        /// </summary>
        /// <param name="name">Name of the agent</param>
        private AgentSpectator(string name) : base(name)
        {
            _stateMachine = new SpectatorStateMachine(this);
        }

        public AgentSpectator() : this("AgentSpectator_") {
            
        }

        public override void ReadMailbox()
        {
            foreach (Message m in _mailbox)
            {
                Debug.Log(this + " received " + m.Type + " from " + m.Sender);
            }

            _mailbox.Clear();
        }

        protected override void CreateBody()
        {
            CreateBody<AgentSpectatorBody>("SpectatorBody");
        }

        public override void OnNext(Message value)
        {
            if (value.Receiver == this || value.Receiver == null)
            {
                _mailbox.Add(value);
            }

        }

        /// <summary>
        /// Temp : il faut gérer le champ de vision de l'agent
        /// </summary>
        /// <returns></returns>
        public AgentTicketOffice ClosestTicketOffice()
        {
            AgentTicketOffice res = null;
            foreach (var a in Environment.GetInstance().Brain.Agents.Where(a => a.Value is AgentTicketOffice))
            {
                res = a.Value as AgentTicketOffice;
            }
            return res;
        }
    }
}
