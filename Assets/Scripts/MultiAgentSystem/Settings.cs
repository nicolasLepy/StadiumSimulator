namespace MultiAgentSystem
{
    /// <summary>
    /// General settings of the simulator
    /// </summary>
    public class Settings
    {
        private int _timeToBuyTicket;

        /// <summary>
        /// Time spent by an agent in front of ticket office to buy a ticket
        /// </summary>
        public int timeToBuyTicket => _timeToBuyTicket;
        public bool showMessagesLog { get; }
        
        public bool noNavMesh { get; set; }

        public Settings(int timeToBuyTicket)
        {
            noNavMesh = false;
            _timeToBuyTicket = timeToBuyTicket;
            showMessagesLog = false;
        }
    }
}