/// <summary>
/// Clase base para los algoritmos de búsqueda.
/// Esta clase implementa la búsqueda genérica, dejando a las subclases (como AEstrella)
/// la responsabilidad de definir el cálculo de la prioridad.
/// </summary>
public class AlgoritmoDeBusqueda
{
    // Lista de candidatos (estados parciales) a explorar.
    protected ListaCandidatos lista;

    /// <summary>
    /// Inicializa una nueva instancia de la clase AlgoritmoDeBusqueda.
    /// </summary>
    /// <param name="lista">Lista de candidatos.</param>
    public AlgoritmoDeBusqueda(ListaCandidatos lista)
    {
        this.lista = lista;
    }

    /// <summary>
    /// Calcula la prioridad de una solución.
    /// Por defecto, retorna 0; la implementación en subclases puede sumar coste y heurística.
    /// </summary>
    /// <param name="nodo_info">Información del nodo (solución parcial).</param>
    /// <param name="calculo_heuristica">Función opcional para calcular la heurística.</param>
    /// <returns>Prioridad calculada.</returns>
    public virtual int calculo_de_prioridad(Solucion nodo_info, Func<Solucion, int>? calculo_heuristica = null)
    {
        return 0;
    }

    /// <summary>
    /// Realiza la búsqueda de una solución utilizando un algoritmo de exploración (por ejemplo, A*).
    /// 
    /// Detalles importantes:
    /// - Estado inicial: se parte de una solución vacía (sin reinas colocadas).
    /// - Selección de vecinos: a partir de un estado se generan todos los estados que surgen
    ///   al colocar una nueva reina en la siguiente fila en todas las columnas posibles.
    /// - Terminación: el algoritmo se detiene cuando se encuentra una solución completa y válida
    ///   (todas las reinas colocadas sin conflictos) o cuando se agotan los candidatos.
    /// </summary>
    /// <param name="solucion_inicial">Solución inicial (lista vacía de coordenadas).</param>
    /// <param name="criterio_parada">Función que determina si la solución actual cumple el criterio de parada.</param>
    /// <param name="obtener_vecinos">Función que obtiene los vecinos (siguientes estados) a partir de una solución.</param>
    /// <param name="calculo_coste">Función que calcula el coste entre dos soluciones.</param>
    /// <param name="calculo_heuristica">Función opcional para calcular la heurística.</param>
    /// <returns>Tupla con la solución encontrada (o null si no se encontró) y el número de nodos evaluados.</returns>
    public (Solucion, int) busqueda(List<(int, int)> solucion_inicial, 
                                    Func<Solucion, bool> criterio_parada, 
                                    Func<Solucion, List<(int, int)>> obtener_vecinos, 
                                    Func<Solucion, Solucion, int> calculo_coste, 
                                    Func<Solucion, int>? calculo_heuristica = null)
    {
        ListaCandidatos candidatos = lista;
        // Estado inicial: solución vacía (sin reinas colocadas)
        candidatos.anhadir(new Solucion(solucion_inicial, 0));
        Dictionary<string, int> vistos = new Dictionary<string, int>();
        bool finalizado = false;
        int revisados = 0;
        Solucion? solucion = null; // Se declara fuera del bucle para poder retornarla posteriormente

        // Bucle principal de la búsqueda
        while (candidatos.Count() > 0 && !finalizado)
        {
            // Se extrae la solución (nodo) con mayor prioridad (menor coste+heurística)
            solucion = candidatos.obtener_siguiente();
            vistos[solucion.ToString()] = solucion.coste;
            revisados++;

            // Si se cumple el criterio de parada (solución completa y sin conflictos), se termina la búsqueda
            if (criterio_parada(solucion))
            {
                finalizado = true;
                break;
            }

            // Se generan los estados vecinos a partir del estado actual
            List<(int, int)> vecinos = obtener_vecinos(solucion);
            foreach ((int, int) vecino in vecinos)
            {
                // Crear una nueva solución a partir de la solución actual añadiendo el vecino
                List<(int, int)> nuevas_coords = solucion.coords.ToList();
                nuevas_coords.Add(vecino);
                Solucion nueva_solucion = new Solucion(nuevas_coords);

                // Solo se considera la nueva solución si no ha sido evaluada previamente
                if (!vistos.ContainsKey(nueva_solucion.ToString()))
                {
                    // Actualizar el coste acumulado de la nueva solución
                    nueva_solucion.coste = solucion.coste + calculo_coste(solucion, nueva_solucion);
                    // Añadir a la lista de candidatos, calculando la prioridad (coste + heurística)
                    candidatos.anhadir(nueva_solucion, prioridad: calculo_de_prioridad(nueva_solucion, calculo_heuristica));
                }
            }
        }

        // Si se agotaron los candidatos sin encontrar una solución válida, se retorna null
        if (!finalizado)
            return (null, revisados);
        return (solucion, revisados);
    }
}

/// <summary>
/// Implementa el algoritmo A* (AEstrella) para la búsqueda de soluciones.
/// Utiliza una cola de prioridad para explorar primero los nodos con menor coste acumulado + heurística.
/// </summary>
public class AEstrella : AlgoritmoDeBusqueda
{
    /// <summary>
    /// Inicializa una nueva instancia de AEstrella, utilizando una cola de prioridad como estructura de candidatos.
    /// </summary>
    public AEstrella() : base(new ColaDePrioridad()) { }

    /// <summary>
    /// Calcula la prioridad de una solución sumando el coste acumulado y el valor heurístico.
    /// Si no se proporciona una función heurística, la prioridad es únicamente el coste acumulado.
    /// </summary>
    /// <param name="solucion">La solución actual.</param>
    /// <param name="calculo_heuristica">Función opcional para calcular la heurística.</param>
    /// <returns>Prioridad calculada.</returns>
    public override int calculo_de_prioridad(Solucion solucion, Func<Solucion, int>? calculo_heuristica = null)
    {
        if (calculo_heuristica == null)
        {
            return solucion.coste;
        }
        return solucion.coste + calculo_heuristica(solucion);
    }
}
