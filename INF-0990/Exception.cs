/// <summary>
/// Classe responsável pelas exceções tanto nos casos de o robo tentar sair do mapa, posição ocupada e sem energia.
/// </summary>
public class OutOfMapException : Exception {}
public class OccupiedPositionException : Exception {}
public class RanOutOfEnergyException : Exception {}
