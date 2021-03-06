﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace MultiAgentSystem
{
    /// <summary>
    /// Represents a spectator
    /// </summary>
    public class AgentSpectator : Agent
    {
        public float timeEnterTicketOfficeQueue { get; set; }

        private Vector3 _spawnLocation;
        public Vector3 spawnLocation => _spawnLocation;
        /// <summary>
        /// If has a ticket or not
        /// </summary>
        public Ticket ticket { get; set; }
        public MessageNoTicketAvailable ticketRefused { get; set; }
        public bool inQueue { get; set; }
        public Vector3 queuePosition { get; set; }
        public bool noTicketAvailable { get; set; }
        private bool _isChecked = false;
        public bool isChecked => _isChecked;
        private Team _side;
        public Team side
        {
            get => _side;
            set
            {
                _side = value;
                if (_side == Team.HOME)
                {
                    Material blue = Resources.Load("Materials/Blue", typeof(Material)) as Material;
                    _body.gameObject.GetComponent<Renderer>().material = blue;
                }
                else
                {
                    Material red = Resources.Load("Materials/Red", typeof(Material)) as Material;
                    _body.gameObject.GetComponent<Renderer>().material = red;
                }
            }
        }
        public bool notifiedTicketRefused { get; set; }
        private List<int> _hisSideCategories;
        public List<int> hisSideCategories => _hisSideCategories;

        public override Vector3 Position => _body.transform.position;

        /// <summary>
        /// Create an agent spectator
        /// </summary>
        /// <param name="name">Name of the agent</param>
        private AgentSpectator(string name) : base(name)
        {
            noTicketAvailable = false;
            _hisSideCategories = new List<int>();
        }

        public override void CreateStateMachine()
        {
            _spawnLocation = _body.transform.position;
            _stateMachine = new SpectatorStateMachine(this);
        }
        
        private static int nameCounter = 0;
        public AgentSpectator() : this("AgentSpectator_" + ++nameCounter) {
            
        }
        
        protected override void CreateBody()
        {
            CreateBody<AgentSpectatorBody>("SpectatorBody");
            Body.InitializeNavMesh();
            if (Environment.GetInstance().settings.noNavMesh)
            {
                GameObject.Destroy(_body.GetComponent<NavMeshAgent>());
                _body.GetComponent<Rigidbody>().useGravity = false;

            }
        }
        
        /// <summary>
        /// Temp : il faut gérer le champ de vision de l'agent
        /// </summary>
        /// <returns></returns>
        public AgentTicketOffice ClosestTicketOffice()
        {
            AgentTicketOffice res = null;
            float minDistance = -1;
            foreach (KeyValuePair<Guid,Agent> agent in Environment.GetInstance().Brain.Agents)
            {
                AgentTicketOffice ato = agent.Value as AgentTicketOffice;
                if (ato != null)
                {
                    float distance = Vector3.Distance(ato.Position, this.Position);
                    if (minDistance > distance || minDistance == -1)
                    {
                        res = ato;
                        minDistance = distance;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// When asking for a ticket, generate the category to ask to ticket office
        /// </summary>
        /// <returns></returns>
        public int GetCategory()
        {
            return hisSideCategories[Utils.PseudoGaussRandom(0, hisSideCategories.Count - 1)];
        }

        public override void ProcessMessage(Message message)
        {
            if(Environment.GetInstance().settings.showMessagesLog) Debug.Log(this + " received " + message.Type + " from " + message.Sender);
            switch (message.Type.messageObject())
            {
                //Spectator get ticket office queue position
                case MessageObject.GET_QUEUE_POSITION:
                    MessageSendQueuePosition msg = message.Type as MessageSendQueuePosition;
                    inQueue = true;
                    queuePosition = msg.position;
                    if (msg.numberInQueue > 4)
                    {
                        queuePosition += Random.insideUnitSphere * 0.6f;
                    }
                    break;
                //A ticket was given by the ticket office
                case MessageObject.GIVE_TICKET:
                    MessageGiveTicket mgt = message.Type as MessageGiveTicket;
                    ticket = mgt.ticket;
                    break;
                //There is no available ticket anymore
                case MessageObject.NO_TICKET_AVAILABLE:
                    //ticket is set to true only to make the agent move out the ticket office
                    notifiedTicketRefused = true;
                    ticket = null;
                    ticketRefused = (message.Type as MessageNoTicketAvailable);
                    //No more categories available : no ticket available
                    if (ticketRefused.stillAvailableCategories.Count == 0)
                    {
                        noTicketAvailable = true;
                    }
                    break;
                case MessageObject.CHECKED:
                    _isChecked = true;
                    break;
            }
            archivedMailbox.Add(message);
        }
    }
}
