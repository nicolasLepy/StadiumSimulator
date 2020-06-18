using AgentSecurity;
using UnityEngine;

namespace MultiAgentSystem
{
    public class AgentSecurity : Agent
    {
        private Queue _queue;

        /// <summary>
        /// Queue in front of the agent security
        /// </summary>
        public Queue queue => _queue;
        
        /// <summary>
        /// Create an agent security
        /// </summary>
        /// <param name="name">Name of the agent</param>
        public AgentSecurity(string name) : base(name)
        {
            _queue = new Queue(this);
        }

        public AgentSecurity() : this("AgentSecurity")
        {
            
        }

        public override void CreateStateMachine()
        {
            _stateMachine = new SecurityStateMachine(this);
        }

        public override Vector3 Position => _body.transform.position;
        protected override void CreateBody()
        {
            CreateBody<AgentSecurityBody>("SecurityBody");
        }

        public override void ProcessMessage(Message message)
        {
            if(Environment.GetInstance().settings.showMessagesLog) Debug.Log(this + " received " + message.Type + " from " + message.Sender);
            switch (message.Type.messageObject())
            {
                case MessageObject.ASK_FOR_QUEUE:
                    _queue.Add(message.Sender);
                    MessageType answer = new MessageSendQueuePosition(_queue.GetPositionForAgent(message.Sender), 
                                                                      _queue.GetNumberInQueueForAgent(message.Sender));
                    SendMessage(message.Sender, answer);
                    break;
            }
            archivedMailbox.Add(message);
        }
    }
}