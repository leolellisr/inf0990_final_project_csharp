/// <summary>
/// Classe responsável pelos itens Jewels e espaços vazios.
/// </summary>
public abstract class ItemMap {
    private string Symbol;
    public ItemMap(string Symbol)
    {
        this.Symbol = Symbol;
    }
    public sealed override string ToString()
    {
        return Symbol;
    }
}