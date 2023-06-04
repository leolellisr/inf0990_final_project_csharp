/// <summary>
/// Classe reponsável pelo Mapa.
/// </summary>
public class Map{
    private ItemMap[,] Matriz;
    public int h {get; private set;}
    public int w {get; private set;}

    /// <summary>
    /// Classe reponsável por gerar o mapa com tamanho 10x10.
    /// Posteriormente conforme avança, incrementa +1 ao tamanho do mapa, até o limite 30x30.
    /// </summary>
    public Map (int w=10, int h=10, int level=1)
    {
        this.w = w <= 30 ? w : 30;
        this.h = h <= 30 ? h : 30;
        Matriz = new ItemMap[w, h];
        for (int i = 0; i < Matriz.GetLength(0); i++) {
            for (int j = 0; j < Matriz.GetLength(1); j++) {
                Matriz[i, j] = new Empty();
            }
        }
        if (level == 1) GenerateFixed();
        else GenerateRandom();
    }
    /// <summary>
    /// Responsável por posicionar itens no mapa dentro da matriz.
    /// </summary>
    public void Insert (ItemMap Item, int x, int y)
    {
        Matriz[x, y] = Item;
    }
    /// <summary>
    /// Responsável por atualizar o Status do mapa.
    /// </summary>
    public void Update(int x_old, int y_old, int x, int y)
    {
        if (x < 0 || y < 0 || x> this.w-1 || y> this.h-1)
        {
            Console.WriteLine($"\nOutOfMapException:x({x}) > w({this.w-1}) ou y({y}) > h({this.w-1})");
            throw new OutOfMapException();
        }
        if (IsAllowed(x, y))
        {
            Matriz[x, y] = Matriz[x_old, y_old];
            Matriz[x_old, y_old] = new Empty();
        }
        else
        {
            Console.WriteLine($"\n OccupiedPositionException:x({x}), y({y})");

            throw new OccupiedPositionException();
        }
    }
    /// <summary>
    /// Responsável por atualizar a quantidade de Jóias coletadas.
    /// </summary>
    public List<Jewel> GetJewels(int x, int y){
        List<Jewel> NearJewels = new List<Jewel>();
        int[,] Coords = GenerateCoord(x, y);
        for (int i = 0; i < Coords.GetLength(0); i++){
            Jewel? jewel = GetJewel(Coords[i, 0], Coords[i, 1]);
            if (jewel is not null) NearJewels.Add(jewel);
        }
        return NearJewels;
    }
    /// <summary>
    /// Responsável por atualizar a posição do mapa onde havia a joia para um item vazio.
    /// </summary>
    private Jewel? GetJewel(int x, int y)
    {
        if (Matriz[x, y] is Jewel jewel)
        {
            Matriz[x, y] = new Empty();
            return jewel;
        }
        return null;
    }
    /// <summary>
    /// Responsável por incrementar a energia do robo conforme coletados itens válidos.
    /// </summary>
    public Rechargeable? GetRechargeable(int x, int y){
        int[,] Coords = GenerateCoord(x, y);
        for (int i = 0; i < Coords.GetLength(0); i++)
            if (Matriz[Coords[i, 0], Coords[i, 1]] is Rechargeable r) return r;
        return null;
    }
    /// <summary>
    /// Responsável por Gerar coordenadas.
    /// </summary>
    private int[,] GenerateCoord(int x, int y)
    {
        int[,] Coords = new int[4, 2]{
            {x,  y+1 < w-1 ? y+1 : w-1},
            {x, y-1 > 0 ? y-1 : 0},
            {x+1 < h-1 ? x+1 : h-1, y},
            {x-1 > 0 ? x-1 : 0, y}
        };
        return Coords;
    }
    /// <summary>
    /// Booleana que verifica se a posição está vazia e permitida para robo transitar.
    /// </summary>
    private bool IsAllowed(int x, int y){
        return Matriz[x, y] is Empty;
    }
    public void Print() {
        for (int i = 0; i < Matriz. GetLength(0); i++){
            for (int j = 0; j < Matriz.GetLength(1); j++){
                Console.Write(Matriz[i, j]);
            }
            Console.Write("\n");
        }
    }
    public bool IsDone()
    {
        for (int i = 0; i < Matriz.GetLength(0); i++) {
            for (int j= 0; j < Matriz.GetLength(1); j++){
                if (Matriz[i, j] is Jewel) return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Gera a posição inicial das Joias, água e árvores no primeiro nível.
    /// </summary>
    private void GenerateFixed()
    {
        this.Insert(new JewelRed(), 1, 9);
        this.Insert(new JewelRed(), 8, 8);
        this.Insert(new JewelGreen(), 9, 1);
        this.Insert(new JewelGreen(), 7, 6);
        this.Insert(new JewelBlue(), 3, 4);
        this.Insert(new JewelBlue(), 2, 1);

        this.Insert(new Water(), 5, 0);
        this.Insert(new Water(), 5, 1);
        this.Insert(new Water(), 5, 2);
        this.Insert(new Water(), 5, 3);
        this.Insert(new Water(), 5, 4);
        this.Insert(new Water(), 5, 5);
        this.Insert(new Water(), 5, 6);
        this.Insert(new Tree(), 5, 9);
        this.Insert(new Tree(), 3, 9);
        this.Insert(new Tree(), 8, 3);
        this.Insert(new Tree(), 2, 5);
        this.Insert(new Tree(), 1, 4);
    }
    /// <summary>
    /// Gera posição aleatória dos itens do mapa no nível 2 em diante.
    /// </summary>
    private void GenerateRandom()
    {
        Random r = new Random(1);
        for(int x = 0; x < 3; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);
            this.Insert(new JewelBlue(), xRandom, yRandom);
        }
        for(int x = 0; x < 3; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);
            this.Insert(new JewelGreen(), xRandom, yRandom);
        }
        for(int x = 0; x < 3; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);
            this.Insert(new JewelRed(), xRandom, yRandom);
        }
        for(int x = 0; x < 10; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);
            this.Insert(new Water(), xRandom, yRandom);
        }
        for(int x = 0; x < 10; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);
            this.Insert(new Tree(), xRandom, yRandom);
        }
    }
}
