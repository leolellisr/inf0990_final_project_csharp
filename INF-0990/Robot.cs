/// <summary>
/// Classe responsável pelo robo, sua interação com o mapa e os itens.
/// </summary>
public class Robot : ItemMap {
    public Map map {get; private set;}
    private int x, y;
    private List<Jewel> Bag = new List<Jewel>();
    public int energy {get; set;}
    
    /// <summary>
    /// Responsável por colocar o robo em sua posição inicial, atribuir energia que inicia o nível.
    /// </summary>
    public Robot(Map map, int x=0, int y=0, int energy=5) : base("ME "){
        this.map = map;
        this.x = x;
        this.y = y;
        this.energy = energy;
        this.map.Insert(this, x, y);
    }
    /// <summary>
    /// Responsável pela movimentação.
    /// </summary>
    public void MoveNorth(){
        try
        {
            map.Update(this.x, this.y, this.x-1, this.y);
            this.x--;
            this.energy--;
        }
        catch (OccupiedPositionException e)
        {
            Console.WriteLine($"\nPosition {this.x-1}, {this.y} is occupied");
        }
        catch (OutOfMapException e)
        {
            Console.WriteLine($"\nPosition {this.x-1}, {this.y} is out of map");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nPosition is prohibit");
        }
    }
    /// <summary>
    /// Responsável pela movimentação.
    /// </summary>
    public void MoveSouth(){
        try
        {
            map.Update(this.x, this.y, this.x+1, this.y);
            this.x++;
            this.energy--;
        }
        catch (OccupiedPositionException e)
        {
            Console.WriteLine($"\nPosition {this.x+1}, {this.y} is occupied");
        }
        catch (OutOfMapException e)
        {
            Console.WriteLine($"\nPosition {this.x+1}, {this.y} is out of map");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nPosition is prohibit");
        }
    }
    /// <summary>
    /// Responsável pela movimentação.
    /// </summary>
    public void MoveEast(){
        try
        {
            map.Update(this.x, this.y, this.x, this.y+1);
            this.y++;
            this.energy--;
        }
        catch (OccupiedPositionException e)
        {
            Console.WriteLine($"\nPosition {this.x}, {this.y+1} is occupied");
        }
        catch (OutOfMapException e)
        {
            Console.WriteLine($"\nPosition {this.x}, {this.y+1} is out of map");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nPosition is prohibit");
        }
    }
    /// <summary>
    /// Responsável pela movimentação.
    /// </summary>
    public void MoveWest(){
        try
        {
            map.Update(this.x, this.y, this.x, this.y-1);
            this.y--;
            this.energy--;
        }
        catch (OccupiedPositionException e)
        {
            Console.WriteLine($"\nPosition {this.x}, {this.y-1} is occupied");
        }
        catch (OutOfMapException e)
        {
            Console.WriteLine($"\nPosition {this.x}, {this.y-1} is out of map");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nPosition is prohibit");
        }
    }
    /// <summary>
    /// Responsável por recarregar a energia do robo.
    /// </summary>
    public void Get(){
        //Console.Clear();
        Rechargeable? RechargeEnergy = map.GetRechargeable(this.x, this.y);
        RechargeEnergy?.Recharge(this);
        List<Jewel> NearJewels = map.GetJewels(this.x, this.y);
        foreach (Jewel j in NearJewels)
            Bag.Add(j);
    }
    /// <summary>
    /// Responsável por contar a quantidade de pontos.
    /// </summary>
    private (int, int) GetBagInfo()
    {
        int Points = 0;
        foreach (Jewel j in this.Bag)
            Points += j.Points;
        return (this.Bag.Count, Points);
    }
    /// <summary>
    /// Responsável por imprimir a quantidade de itens, pontos e energia.
    /// </summary>
    public void Print()
    {
        map.Print();
        (int ItensBag, int TotalPoints) = this.GetBagInfo();
        Console.WriteLine($"\nItens Bag: {ItensBag} - Total Points: {TotalPoints} - Energy: {this.energy} - x:{this.x}, y: {this.y}\n\n");
    }
    public bool HasEnergy()
    {
        return this.energy > 0;
    }
}