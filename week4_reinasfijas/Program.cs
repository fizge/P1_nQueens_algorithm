using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        // Estado inicial: reinas fijas.
        List<(int, int)> solucion_inicial = new List<(int, int)>{ (0,3),(2,4) };
        // Número de reinas a colocar (dimensión del tablero).
        int reinas = 4;

        // Funciones locales (heurística, coste, vecinos, etc.) se mantienen igual
        int calculo_coste(Solucion solucion, Solucion nueva_solucion)
        {
            // Se asume que la nueva reina se añadió al final
            (int, int) nueva_reina = nueva_solucion.coords.Last();
            foreach ((int, int) otra_reina in solucion.coords)
            {
                if (nueva_reina.Item2 == otra_reina.Item2 ||
                    Math.Abs(nueva_reina.Item1 - otra_reina.Item1) == Math.Abs(nueva_reina.Item2 - otra_reina.Item2))
                {
                    return int.MaxValue / 2;
                }
            }
            return 1;
        }

        int calculo_heuristica(Solucion solucion)
        {
            int reinasColocadas = solucion.coords.Count;
            int nuevaFila = primera_fila_sin_reina(solucion);
            if (reinasColocadas == reinas) return 0;

            HashSet<int> columnasAtacadas = new HashSet<int>();
            foreach ((int fila, int col) in solucion.coords)
            {
                int d = Math.Abs(nuevaFila - fila);
                columnasAtacadas.Add(col);
                if (col + d < reinas)
                    columnasAtacadas.Add(col + d);
                if (col - d >= 0)
                    columnasAtacadas.Add(col - d);
            }
            if (columnasAtacadas.Count == reinas)
            {
                return int.MaxValue / 2;
            }
            return reinas - reinasColocadas;
        }

        int primera_fila_sin_reina(Solucion solucion)
        {
            HashSet<int> filasOcupadas = new HashSet<int>(solucion.coords.Select(coord => coord.Item1));
            for (int fila = 0; fila < reinas; fila++)
            {
                if (!filasOcupadas.Contains(fila))
                {
                    return fila;
                }
            }
            return reinas;
        }

        
        bool fuera_de_tablero(Solucion solucion)
        {
            foreach ((int, int) nodo in solucion.coords)
            {
                if (nodo.Item1 >= reinas || nodo.Item2 >= reinas)
                {
                    return true;
                }
            }
            return false;
        }

        List<(int, int)> obtener_vecinos(Solucion solucion)
        {
            int row = solucion.coords.Count == 0 ? -1 : primera_fila_sin_reina(solucion);
            List<(int, int)> vecinos = new List<(int, int)>();
            if (row < reinas)
            {
                for (int j = 0; j < reinas; j++)
                {
                    (int, int) nuevo_nodo = (row, j);
                    if (es_prometedor(solucion, nuevo_nodo) && !fuera_de_tablero(solucion)) 
                    {
                        vecinos.Add(nuevo_nodo);
                    }
                }
            }
            return vecinos;
        }

        bool es_prometedor(Solucion solucion, (int, int) nuevo_nodo)
        {
            foreach ((int, int) nodo in solucion.coords)
            {
                if (nodo.Item2 == nuevo_nodo.Item2 ||
                    Math.Abs(nodo.Item2 - nuevo_nodo.Item2) == Math.Abs(nodo.Item1 - nuevo_nodo.Item1))
                {
                    return false;
                }
            }
            return true;
        }

        bool criterio_parada(Solucion solucion)
        {
            if (solucion.coords.Count < reinas)
                return false;
            for (int i = 0; i < solucion.coords.Count; i++)
            {
                (int, int) nodo_i = solucion.coords[i];
                for (int j = i + 1; j < solucion.coords.Count; j++)
                {
                    (int, int) nodo_j = solucion.coords[j];
                    if (nodo_j.Item2 == nodo_i.Item2 ||
                        Math.Abs(nodo_j.Item2 - nodo_i.Item2) == Math.Abs(nodo_j.Item1 - nodo_i.Item1))
                        return false;
                }
            }
            return true;
        }

       // Listas para almacenar el número de nodos evaluados en cada búsqueda
        List<int> lista_evaluadosAstar = new List<int>();
        List<int> lista_evaluadosUCS = new List<int>();
        List<int> lista_evaluadosAvara = new List<int>();

        // Número de nodos evaluados en cada algoritmo
        int revisadosAstar = 0;
        int revisadosUCS = 0;
        int revisadosAvara = 0;

        // BÚSQUEDA A*
        while (revisadosAstar < 1500)
        {
            Console.WriteLine("\n\nBúsqueda por A*:");

            Stopwatch stopwatchAstar = new Stopwatch();
            stopwatchAstar.Start();

            AEstrella aestrella = new AEstrella();
            (Solucion solucionAstar, revisadosAstar) = aestrella.busqueda(solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica);

            stopwatchAstar.Stop();

            if(solucionAstar == null)
            {
                Console.WriteLine("A*: No se encontró solución.");
                lista_evaluadosAstar.Add(-1);
            }
            else
            {
                Console.WriteLine($"Tiempo transcurrido: {stopwatchAstar.ElapsedMilliseconds} ms");
                
                if (fuera_de_tablero(solucionAstar))
                {
                    Console.WriteLine($"La solución encontrada tiene celdas no existentes en el tablero de {reinas} reinas.");   
                    lista_evaluadosAstar.Add(-1);
                }
                else
                {
                    Console.WriteLine("Coordenadas:  " + solucionAstar.ToString());
                    lista_evaluadosAstar.Add(revisadosAstar);
                }
                Console.WriteLine("Nodos evaluados:  " + revisadosAstar);
                
            }

            reinas++;
        }
        
        // BÚSQUEDA POR COSTE UNIFORME
        reinas = 4;  // Restablecemos el número de reinas
        while (revisadosUCS < 1500)
        {
            Console.WriteLine("\n\nBúsqueda por Coste Uniforme:");
            Stopwatch stopwatchUCS = new Stopwatch();
            stopwatchUCS.Start();

            CosteUniforme costeUniforme = new CosteUniforme();
            (Solucion solucionUCS, revisadosUCS) = costeUniforme.busqueda(solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica);

            stopwatchUCS.Stop();

            if (solucionUCS == null)
            {
                Console.WriteLine("Coste Uniforme: No se encontró solución.");
                lista_evaluadosUCS.Add(-1); // Usamos -1 para indicar que no se encontró solución
            }
            else
            {
                Console.WriteLine($"Tiempo transcurrido: {stopwatchUCS.ElapsedMilliseconds} ms");
                if (fuera_de_tablero(solucionUCS))
                {
                    Console.WriteLine($"La solución encontrada tiene celdas no existentes en el tablero de {reinas} reinas."); 
                    lista_evaluadosUCS.Add(-1);
                }
                else
                {
                    Console.WriteLine("Coordenadas:  " + solucionUCS.ToString());
                    lista_evaluadosUCS.Add(revisadosUCS);
                }
                Console.WriteLine("Nodos evaluados: " + revisadosUCS);
                
            }

            reinas++;
        }

        // BÚSQUEDA AVARA
        reinas = 4;  // Restablecemos el número de reinas
        while (revisadosAvara < 1500)
        {
            Console.WriteLine("\n\nBúsqueda Avara:");
            Stopwatch stopwatchAvara = new Stopwatch();
            stopwatchAvara.Start();

            BusquedaAvara busquedaAvara = new BusquedaAvara();
            (Solucion solucionAvara, revisadosAvara) = busquedaAvara.busqueda(solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica);

            stopwatchAvara.Stop();

            if (solucionAvara == null)
            {
                Console.WriteLine("Búsqueda Avara: No se encontró solución.");
                lista_evaluadosAvara.Add(-1); // Usamos -1 para indicar que no se encontró solución
            }
            else
            {
                Console.WriteLine($"Tiempo transcurrido: {stopwatchAvara.ElapsedMilliseconds} ms");
                if (fuera_de_tablero(solucionAvara))
                {
                    Console.WriteLine($"La solución encontrada tiene celdas no existentes en el tablero de {reinas} reinas."); 
                    lista_evaluadosAvara.Add(-1);
                }
                else
                {
                    Console.WriteLine("Coordenadas:  " + solucionAvara.ToString());
                    lista_evaluadosAvara.Add(revisadosAvara);
                }
                Console.WriteLine("Nodos evaluados: " + revisadosAvara);
                
            }

            reinas++;
        }
        
        // Imprimir los resultados finales
        Console.WriteLine("\nResultados:");
        Console.WriteLine("A*: " + string.Join(", ", lista_evaluadosAstar));
        Console.WriteLine("Coste Uniforme: " + string.Join(", ", lista_evaluadosUCS));
        Console.WriteLine("Búsqueda Avara: " + string.Join(", ", lista_evaluadosAvara));
    }
}
