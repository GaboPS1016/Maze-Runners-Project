# Maze-Runners-Project
 La Gema de la Fortuna
 Mi Primer Proyecto de Programacion
 Gabriel Pérez Suárez C112
 Creado Con Unity 2023.2.20f1
 Para ejecutar el proyecto solo debe entrar a la escena 0 (MainMenu) y presionar el boton de play.
 También puede ejecutarlo desde las escenas 1 o 2, pero no de la 3 (Maze), ya que no tendrá los jugadores asignados, y el código no funcionará.
 La escena 1 es la Información del juego, ahí se explica la temática.
 En la escena 2 se elige el número de jugadores, y se asignan los jugadores.
 La escena 3 es el laberinto, y es donde se juega. Es la que utiliza la mayoría de los scripts.
 Para descargar el ejecutable debe descomprimir los 6 archivos Maze Runners.rar a la vez, y abrir Gema de la Fortuna.exe.

 Expliación de los scripts principales:
  1. MazeGenerator.cs: Genera el laberinto a partir de una matriz de booleanos y una matriz de enteros, en este se implementa el método PlayerMaze(f,c), que mediante el algoritmo de Lee genera una matriz de enteros enumerando a cuantos pasos queda cada casilla desde una determinadda posición.
  2. Game.cs: Es el que organiza todo el juego y gestiona el sistema de turnos.
  3. Movement.cs: Es el que gestiona el movimiento de los jugadores. Este dado el valor del dado imprime las casillas a las que te puedes mover y ordena al script selectmovecell.cs a seleccionar la casilla, luego genera el camino que debe seguir el jugador.
  4. Players.cs: Es el que contiene la información de cada tipo de jugador, las clases de cada uno, incluyendo sus habilidades especiales.
  5. Traps.cs: Es el que gestiona las trampas, las genera y aplica sus efectos al jugador correspondiente.
    
    Trampas:
     1. Pinchos: La próxima jugada del jugador será de una casilla.
     2. Fuego: El jugador se pone negro y se puede trasladar a una casilla cercana, recibiendo su efecto si es otra trampa.
     3. Trampa para osos: El jugador esperará 2 turnos.
     4. Explosión: Envía al jugador que la activa y a los cercanos a la casilla inicial.
     5. Veneno: Le suma 2 turnos de recarga a la habilidad especial del jugador que la activa, y a los cercanos.
    
    Obstáculos:
     6. Piedra: La casilla se vuelve inaccesible. No se imprimirá la casilla de selección. No se puede caer en la casilla de una piedra, pero sí se puede caer después de ella. Se puede decir que es el inverso del tronco.
     7. Tronco: Si el valr del dado es más lejano al tronco, no se imprimirá la casilla de selección, pero sí se puede caer en la casilla de un tronco, talándolo y haciendo el camino accesible. Se puede decir que es el inverso de la piedra.
    
    Portales:
    8. Portal: El jugador se teletransportará a una casilla aleatoria que no tendrá trampas.
 
 Las características de los jugadores se muestran en Players.cs y en Pdescription.cs.
 Espero que mi proyecto sea de su agrado.
 Para más información, abrir "Informe".
 Que lo disfrute.

