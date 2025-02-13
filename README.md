# Práctica 1 - Algoritmos de Búsqueda (40% de la nota de prácticas)

| Correo | Nombre y Apellidos | 
| --- | --- |
| alumno1 | Juán Fernández García |
| alumno2 | María Díaz Martínez |

## Problema: N - Reinas

**Contexto:** A una empresa se le propuso resolver el problema de las N-reinas, que trata de colocar N reinas e un tablero de NxN de tal manera que no se ataquen entre ellas. 

<p align="center">
  <img width="260" height="260" src="https://upload.wikimedia.org/wikipedia/commons/a/ad/Ocho_reinas_reina_atacar_fila.JPG">
</p>

La empresa puso a cargo de la tarea a un empleado, que resolvió el problema usando el método de búsqueda de A*. Una vez visto el resultado, a la empresa le interesaba saber si se puede mejorar el resultado mejorando el código o usando otras técnicas de búsqueda. Lamentablemente, el empleado dejó la empresa, dejando un código totalmente sin comentar, e insertado en un único fichero llamado [n_reinas.py](n_reinas.py). Además, lo hizo en un lenguaje de programación (Python) distinto al que utilizan el resto de módulos creados por la empresa (C#).

**Objetivo:** Tu trabajo consistirá en retomar el trabajo dejado por el empleado que abandonó la empresa, creando una estructura más legible, y mejorando sus funcionalidades. En concreto, se han propuesto **4 objetivos semanales a realizar**, de manera incremental, hasta alcanzar el producto final.

<div style="padding: 15px; border: 1px solid transparent; border-color: transparent; margin-bottom: 20px; border-radius: 4px; color: #a94442; background-color: #f2dede; border-color: #ebccd1;">

**IMPORTANTE**

* **Esta práctica exigirá entregas semanales**.
* **Cada semana de retraso penalizará un 10% de la nota, hasta un máximo del 50%**.
* **Las entregas tendrán que ser realizadas, en la semana especificada en el enunciado, siempre antes del horario de clase correspondiente.** Esto es, los alumnos del martes tendrán como fecha límite el martes de esa semana, mientras que los del viernes tendrán hasta el viernes.
* Habrá correcciones semanales en horarios de prácticas.
</div>


## Semana 1 - Especificación y traducción (2.5 puntos)

Tu primera tarea será reordenar y dar un significado al código. Para ello se pide:

* ***(2 puntos)*** Transforma el código de Python en código de C#. 
  1. El funcionamiento debe ser análogo al del código de Python proporcionado
  2. Se pueden modificar funciones para convertirlas en clases.
* ***(0.25 puntos)*** Estructura el código, separando en distintos ficheros su contenido. En concreto, se piden 3 ficheros:
  1. Fichero con el código ejecutable
  2. Fichero con el algoritmo de búsqueda
  3. Fichero con las estructuras de almacenamiento de datos
* ***(0.25 puntos)*** Documenta el código, explicando qué hace cada una de las funciones. En concreto, se pide tener especial atención al funcionamiento del algoritmo:
  1. ¿Cuál es el estado inicial?
  2. ¿Dado un estado, cómo se escogen los vecinos?
  3. ¿Cuándo finaliza el algoritmo?
  4. ¿Qué utilidad tienen las variables incluídas en la clase ColaDePrioridad?

<div style="padding: 15px; border: 1px solid transparent; border-color: transparent; margin-bottom: 20px; border-radius: 4px; color: #8a6d3b;; background-color: #fcf8e3; border-color: #faebcc;">

**ADVERTENCIA**
* **Las clases tienen que mantener el mismo nombre, así como sus funciones**, pero se pueden cambiar las variables que se necesitan, siempre que el comportamiento sea análogo. 
</div>

<div style="padding: 15px; border: 1px solid transparent; border-color: transparent; margin-bottom: 20px; border-radius: 4px; color: #31708f; background-color: #d9edf7; border-color: #bce8f1;">

**PISTA**
* Crea un docstring para cada función y clase del código
* **Comenta con detalle** las líneas importantes de las funciones anhadir y obtener_siguiente de la clase ColaDePrioridad
* **Comenta con detalle** las líneas importantes de las funciones obtener_vecinos y criterio_parada
</div>

<div style="padding: 15px; border: 1px solid transparent; border-color: transparent; margin-bottom: 20px; border-radius: 4px; color: #a94442; background-color: #f2dede; border-color: #ebccd1;">

**IMPORTANTE**

* Modifica el inicio de este fichero indicando los nombres y los correos de los alumnos del grupo.
* Una vez realizada la tarea, **súbela al Campus Virtual** en el slot denominado **semana 1** de tu clase de prácticas.
* Crea un fichero llamado `especificacion.md`, en donde se detallará todo el trabajo realizado.
* La fecha límite para la nota máxima es la semana del **17 de febrero de 2025**.
</div>


## Semana 2 - Búsqueda no Informada (2.5 puntos)

Tu segunda tarea será resolver el problema usando otros modelos de búsqueda no informada. Para ello se pide:

* ***(1 puntos)*** Resuelve el problema usando la búsqueda en anchura.
  1. Para realizar la tarea, se puede modificar el código de dos maneras, las cuáles se expondrán en el fichero que detalle el trabajo realizado.
  2. Se implementará sólo una de las soluciones posibles, teniendo en cuenta que sólo una es susceptible de obtener la máxima nota.
  3. Ejecuta el código, empezando en `reinas=4`, y sube el valor hasta que el número de nodos evaluados sea mayor a 1500. Indica en el fichero el número de nodos evaluados para cada valor de `reinas` probado.
* ***(1 puntos)*** Resuelve el problema usando la búsqueda en profundidad:
  1. Para realizar la tarea, se puede modificar el código de dos maneras, las cuáles se expondrán en el fichero que detalle el trabajo realizado.
  2. Se implementará sólo una de las soluciones posibles, teniendo en cuenta que sólo una es susceptible de obtener la máxima nota.
  3. Ejecuta el código, empezando en `reinas=4`, y sube el valor hasta que el número de nodos evaluados sea mayor a 1500. Indica en el fichero el número de nodos evaluados para cada valor de `reinas` probado.
* ***(0.5 puntos)*** Documenta las soluciones posibles, así como los resultados obtenidos, en un fichero llamado `busqueda_no_informada.md`. Dicho fichero contendrá:
  1. Una explicación de las dos soluciones posibles para resolver cada apartado.
  2. Un razonamiento sobre cúal de las dos soluciones se debería de implementar.
  3. Se indicará, para cada ejecución, el número de nodos evaluados para cada valor de `reinas` probado.
  4. Explica, de manera razonada, cuál de los dos modelos de búsqueda no informada es más adecuado para la resolución de esta tarea.

<div style="padding: 15px; border: 1px solid transparent; border-color: transparent; margin-bottom: 20px; border-radius: 4px; color: #8a6d3b;; background-color: #fcf8e3; border-color: #faebcc;">

**ADVERTENCIA**
* **No se puede modificar nada del código ya propuesto**, excepto las funciones `calculo_coste` y `calculo_heuristica`. 
* Se pueden crear clases que hereden de las clases `ListaCandidatos` y `AlgoritmoDeBusqueda`.
</div>
     
<div style="padding: 15px; border: 1px solid transparent; border-color: transparent; margin-bottom: 20px; border-radius: 4px; color: #31708f; background-color: #d9edf7; border-color: #bce8f1;">

**PISTA**
* Se puede crear un modelo de búsqueda en anchura modificando la función de coste y el cálculo de la heurística (50% de la nota total).
</div>

<div style="padding: 15px; border: 1px solid transparent; border-color: transparent; margin-bottom: 20px; border-radius: 4px; color: #a94442; background-color: #f2dede; border-color: #ebccd1;">

**IMPORTANTE**

* Una vez realizada la tarea, **súbela al Campus Virtual** en el slot denominado **semana 2** de tu clase de prácticas.
* Crea un fichero llamado `busqueda_no_informada.md`, en donde se detallará todo el trabajo realizado.
* La fecha límite para la nota máxima es la semana del **24 de febrero de 2025**.
</div>

## Semana 3 - Búsqueda Informada (2.5 puntos)

Tu tercera tarea será resolver el problema usando otros modelos de búsqueda no informada. Para ello se pide:

* ***(1.5 puntos)*** Mejora la resolución del problema usando A*.
  1. Modifica la función de coste para optimizar la búsqueda.
  2. Modifica la función de heurística para optimizar la búsqueda.
  3. Ejecuta el código, empezando en `reinas=4`, y sube el valor hasta que el número de nodos evaluados sea mayor a 1500. Indica en el fichero el número de nodos evaluados para cada valor de `reinas` probado.
* ***(0.5 puntos)*** Resuelve el problema usando Coste Uniforme.
  1. Ejecuta el código, empezando en `reinas=4`, y sube el valor hasta que el número de nodos evaluados sea mayor a 1500. Indica en el fichero el número de nodos evaluados para cada valor de `reinas` probado.
* ***(0.5 puntos)*** Resuelve el problema usando Búsqueda Avara.
  1. Ejecuta el código, empezando en `reinas=4`, y sube el valor hasta que el número de nodos evaluados sea mayor a 1500. Indica en el fichero el número de nodos evaluados para cada valor de `reinas` probado.

<div style="padding: 15px; border: 1px solid transparent; border-color: transparent; margin-bottom: 20px; border-radius: 4px; color: #8a6d3b;; background-color: #fcf8e3; border-color: #faebcc;">

**ADVERTENCIA**
* **No se puede modificar nada del código ya propuesto**, excepto las funciones `calculo_coste` y `calculo_heuristica`. 
* Se pueden crear clases que hereden de las clases `ListaCandidatos` y `AlgoritmoDeBusqueda`.
</div>

<div style="padding: 15px; border: 1px solid transparent; border-color: transparent; margin-bottom: 20px; border-radius: 4px; color: #31708f; background-color: #d9edf7; border-color: #bce8f1;">

**PISTA**
* Ten en cuenta las restricciones del problema para mejorar las funciones a modificar.
</div>

<div style="padding: 15px; border: 1px solid transparent; border-color: transparent; margin-bottom: 20px; border-radius: 4px; color: #a94442; background-color: #f2dede; border-color: #ebccd1;">

**IMPORTANTE**

* Una vez realizada la tarea, **súbela al Campus Virtual** en el slot denominado **semana 3** de tu clase de prácticas.
* Crea un fichero llamado `busqueda_informada.md`, en donde se detallará todo el trabajo realizado.
* La fecha límite para la nota máxima es la semana del **3 de marzo de 2025**.
</div>

## Semana 4 - Búsqueda con Restricciones (2.5 puntos)

Tu última tarea será resolver el problema teniendo en cuenta las restricciones. Para ello se pide:

* ***(1 punto)*** Crea una función que, dada una solución, nos indique si es consistente o no.
  1. Una solución es consistente si no hay ningún par de reinas que se ataquen entre si.
  2. Ejecuta el código, empezando en `reinas=4`, y sube el valor hasta que el número de nodos evaluados sea mayor a 1500. Indica en el fichero el número de nodos evaluados para cada valor de `reinas` probado.
* ***(0.5 puntos)*** Modifica la función `obtener_vecinos` para que no acepte vecinos que creen soluciones inconsistentes.
  1. Ejecuta el código, empezando en `reinas=4`, y sube el valor hasta que el número de nodos evaluados sea mayor a 1500. Indica en el fichero el número de nodos evaluados para cada valor de `reinas` probado.
* ***(1 puntos)*** Modifica el código para que acepte seleccionar un estado inicial con M reinas prefijadas.
  1. Ejecuta el código teniendo en cuenta las reinas prefijadas (0, 3) y (2, 4), empezando en `reinas=4`, y sube el valor hasta que el número de nodos evaluados sea mayor a 1500. Indica en el fichero el número de nodos evaluados para cada valor de `reinas` probado.

<div style="padding: 15px; border: 1px solid transparent; border-color: transparent; margin-bottom: 20px; border-radius: 4px; color: #8a6d3b;; background-color: #fcf8e3; border-color: #faebcc;">

**ADVERTENCIA**
* **No se puede modificar nada del código ya propuesto**, excepto las funciones `calculo_coste`, `calculo_heuristica`, `obtener_vecinos` y la variable `nodo_inicial`. 
* Se pueden crear clases que hereden de las clases `ListaCandidatos` y `AlgoritmoDeBusqueda`.
</div>

<div style="padding: 15px; border: 1px solid transparent; border-color: transparent; margin-bottom: 20px; border-radius: 4px; color: #31708f; background-color: #d9edf7; border-color: #bce8f1;">

**PISTA**
* Ten en cuenta las reinas prefijadas pueden no estar en filas consecutivas, ni en un orden preestablecido. Una ordenación podría ayudar a resolver el problema.
</div>

<div style="padding: 15px; border: 1px solid transparent; border-color: transparent; margin-bottom: 20px; border-radius: 4px; color: #a94442; background-color: #f2dede; border-color: #ebccd1;">

**IMPORTANTE**

* Una vez realizada la tarea, **súbela al Campus Virtual** en el slot denominado **semana 4** de tu clase de prácticas.
* Crea un fichero llamado `busqueda_con_restricciones.md`, en donde se detallará todo el trabajo realizado.
* La fecha límite para la nota máxima es la semana del **10 de marzo de 2025**.

</div>
