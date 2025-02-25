public class AlgoritmoDeBusqueda
{
    protected ListaCandidatos lista;

    public AlgoritmoDeBusqueda(ListaCandidatos lista)
    {
        this.lista = lista;
    }

    public virtual int calculo_de_prioridad(Solucion nodo_info, Func<Solucion, int>? calculo_heuristica = null)
    {
        return 0;
    }

    public (Solucion, int) busqueda(List<(int, int)> solucion_inicial, Func<Solucion, bool> criterio_parada, Func<Solucion, List<(int, int)>> obtener_vecinos, Func<Solucion, Solucion, int> calculo_coste, Func<Solucion, int>? calculo_heuristica = null)
    {
        ListaCandidatos candidatos = lista;
        candidatos.anhadir(new Solucion(solucion_inicial, 0));
        Dictionary<string, int> vistos = new Dictionary<string, int>();
        bool finalizado = false;
        int revisados = 0;
        Solucion? solucion = null; // Inicializar solucion fuera del bucle while para poder acceder a ella después en el return de la función
        while (candidatos.Count() > 0 && !finalizado)
        {
            solucion = candidatos.obtener_siguiente();
            vistos[solucion.ToString()] = solucion.coste;
            revisados++;

            if (criterio_parada(solucion))
            {
                finalizado = true;
                break;
            }

            List<(int, int)> vecinos = obtener_vecinos(solucion);
            foreach ((int, int) vecino in vecinos)
            {
                List<(int, int)> nuevas_coords = solucion.coords.ToList();
                nuevas_coords.Add(vecino);

                Solucion nueva_solucion = new Solucion(nuevas_coords);

                if (!vistos.ContainsKey(nueva_solucion.ToString()))
                {
                    nueva_solucion.coste = solucion.coste + calculo_coste(solucion, nueva_solucion);
                    candidatos.anhadir(nueva_solucion, prioridad: calculo_de_prioridad(nueva_solucion, calculo_heuristica));
                }
            }
        }

        if (!finalizado)
            return (null, revisados);
        return (solucion, revisados);
    }
}

public class AEstrella : AlgoritmoDeBusqueda
{
    public AEstrella() : base(new ColaDePrioridad()) { }

    public override int calculo_de_prioridad(Solucion solucion, Func<Solucion, int>? calculo_heuristica = null)
    {
        if(calculo_heuristica==null)
        {
            return solucion.coste;
        }
        return solucion.coste + calculo_heuristica(solucion);
    }
}
