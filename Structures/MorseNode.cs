namespace MorseNap.Structures
{
    public class MorseNode
    {
        public char Character { get; set; }
        public MorseNode? Left { get; set; }  // Camino del Punto '.'
        public MorseNode? Right { get; set; } // Camino de la Raya '-'

        public MorseNode(char character = '\0')
        {
            Character = character;
            Left = null;
            Right = null;
        }
    }
}