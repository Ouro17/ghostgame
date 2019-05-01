namespace Business
{
    /// <summary>
    /// A container that represent the status of the game
    /// The status can be machineWin, playerWin, continue the game
    /// Word is the next word that the machine give to the player
    /// </summary>
    public class ResultContainer<T>
    {
        /// <summary>
        /// The next word
        /// </summary>
        public T Element { get; set; }
        
        /// <summary>
        /// The status of the game
        /// </summary>
        public StatusEnum Status { get; set; } 
    }
}
