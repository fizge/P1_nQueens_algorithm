/*
class Solucion:

    def __init__(self, coords, coste=0):
        self.coords = coords
        self.coste = coste

    def __eq__(self, other):
        return str(self.coords) == str(other.coords)

    def __lt__(self, other):
        return str(self.coste) < str(other.coste)

    def __str__(self):
        return '-'.join(str(x) for x in self.coords)


class ListaCandidatos:

    def anhadir(self, solucion, prioridad=0):
        pass

    def borrar(self, solucion):
        pass

    def obtener_siguiente(self):
        pass

    def __len__(self):
        pass


class ColaDePrioridad(ListaCandidatos):

    def __init__(self):
        self.cp = []
        self.buscador = {}

    def anhadir(self, solucion, prioridad=0):
        str_solucion = str(solucion)
        if str_solucion in self.buscador:
            solucion_buscador = self.buscador[str_solucion]
            if solucion_buscador[0] <= prioridad:
                return
            self.borrar(solucion)
        entrada = [prioridad, solucion]
        self.buscador[str_solucion] = entrada
        heapq.heappush(self.cp, entrada)

    def borrar(self, solucion):
        entrada = self.buscador.pop(str(solucion))
        entrada[-1].coste = REMOVED

    def obtener_siguiente(self):
        while self.cp:
            prioridad, solucion = heapq.heappop(self.cp)
            if solucion.coste is not REMOVED:
                del self.buscador[str(solucion)]
                return solucion
        raise KeyError('no hay siguiente en una cola de prioridad vacia')

    def __len__(self):
        return len(self.buscador)
*/
using System.Runtime.InteropServices;

public class Solucion
{
    public List<(int, int)> coords;
    public int coste;

    public Solucion(List<(int, int)> coords,int coste = 0)
    {
        this.coords = coords;
        this.coste = coste;
    }

    public override bool Equals()
    {
        
    }
}

public class ListaCandidatos
{
    public virtual void anhadir(Solucion solucion, int prioridad = 0)
    {
    }

    public virtual void borrar(Solucion solucion)
    {
    }

    public virtual void obtener_siguiente()
    {
    }

    public virtual int __len__()
    {
    }

}


public class ColaDePrioridad:ListaCandidatos
{
    List<> cp;
    Dictionary<> buscador;
    public ColaDePrioridad()
    {
        this.cp = [];
        this.buscador = {};
    }
}

    
