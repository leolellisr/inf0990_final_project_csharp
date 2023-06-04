/// <summary>
/// Classe responsável pelos delegates e eventos de movimentação do robo.
/// </summary>
public class JewelCollector {
    delegate void MoveNorth();
    delegate void MoveSouth();
    delegate void MoveEast();

    delegate void MoveWest();

    static event MoveNorth OnMoveNorth;
    static event MoveSouth OnMoveSouth;
    static event MoveEast OnMoveEast;

    static event MoveWest OnMoveWest;

    static event MoveWest Get;
/// <summary>
/// Método principal, inicia o jogo.
/// </summary>
    public static void Main() {
        int w = 10;
        int h = 10;
        int level = 1;

        while(true)
        {
            Map map = new Map (w, h, level);
            Robot robot = new Robot(map);

            Console.WriteLine($"Level: {level}");

            try{
                bool Result = Run(robot);
                if(Result)
                {
                    w++;
                    h++;
                    level++;
                }
                else
                {
                    break;
                }
            }
            catch(RanOutOfEnergyException e)
            {
                Console.WriteLine("Robot ran out of energy!");
            }
        }
    }
    /// <summary>
    /// Responsável pela leitura do teclado, e atribuição aos eventos.
    /// </summary>
    private static bool Run(Robot robot)  
    {
        OnMoveNorth += robot.MoveNorth;
        OnMoveSouth += robot.MoveSouth;
        OnMoveEast += robot.MoveEast;
        OnMoveWest += robot.MoveWest;
        Get += robot.Get;

        do {
            if(!robot.HasEnergy()) throw new RanOutOfEnergyException();
            robot.Print();
            Console.WriteLine("\n Enter the command: ");
            ConsoleKeyInfo command = Console.ReadKey(true);

            switch (command.Key.ToString())
            {
                case "W": Console.WriteLine($"\n Comando:{command.Key.ToString()}"); OnMoveNorth() ; break;
                case "S" : Console.WriteLine($"\n Comando:{command.Key.ToString()}"); OnMoveSouth() ; break;
                case "D" : Console.WriteLine($"\n Comando:{command.Key.ToString()}"); OnMoveEast() ; break;
                case "A" : Console.WriteLine($"\n Comando:{command.Key.ToString()}"); OnMoveWest() ; break;
                case "G" : Console.WriteLine($"\n Comando:{command.Key.ToString()}"); Get() ; break;
                case "quit" : return false;
                default: Console.WriteLine($"\n Comando inválido:{command.Key.ToString()}"); break;
            }
        } while (!robot.map.IsDone());
        return true;
    }
}