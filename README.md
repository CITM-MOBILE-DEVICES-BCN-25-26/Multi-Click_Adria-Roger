Per aquesta entrega he utilitzat el Input.GetKeyDown i GetKeyUp per detectar quan esta premuda i es deixa de premer la tecla ESPAI.



## **Thresholds**



* Pulsació llarga (1.0 segons): El temps que cal mantenir el botó avall per registrar un "Long Press".



* Temps d'espera (0.5 segons): El marge de temps que tens un cop deixes anar el botó per tornar-lo a prémer si vols fer un doble clic.



## **Fases**



* El sistema comença a comptar. Si mantens la tecla avall durant més d'1 segon, detecta un Long Press i atura la comprovació.



* Si deixes anar la tecla abans d'aquest segon, el sistema s'espera 0.5 segons per veure què fas.



* Si tornes a prémer el botó dins d'aquests 0.5 segons d'espera, detecta un Double Press.



* Si passen els 0.5 segons i no has tocat res més, ho dona per bo com a Short Click.





