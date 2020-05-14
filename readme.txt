___________Gruppe Medlemmer_______
Oskar Vedel - au580178
Sebastian Brahe - au611810

___________Schema_________________
Vores entiteter kan ses på diagrammet der findes i mappen. Vi har valgt entiterne - 'User','Post','Comment' og 'Circle'.
Disse er valgt ud fra opgavebeskrivelsen da disse bliver nødt til at blive lagret, og have attributer tilhørende sig. 
I Post og Usser entiteterne har vi valgt at lave en liste af ints for de circles dee skal vises i, for at undgå at duplikere 
circle entiteten i begge, og dermed skulle tage højde for synkroniseringen deraf!

Det samme gør sig gældende for User entiteten, indeholder 'FollowedUseres' og 'BlockedUsers' hvor vi har lavet dem vha. en list af
strenge, for at undgå at embedde en masse data, hvis en User fulgte mange personer, eller blokerede mange personer. Disse objekter
ville dermed indeholde hele user objekter.

I forlængelse af ovenstående benytter vi i forvejen ikke al User data i 'FollowedUseres' og 'BlockedUsers' og derfor ville det være et spild.

___________Sharding_______________
Sharding går ud på at distribuere applikationens data ud på flere maskiner. Dette vil man typisk gøre ved store datamængder.
I vores tilfælde vil et socialt netværk efter al forventing benytte meget 'querying' og dette vil derfor kunne blive nødvendigt
at benytte sig af sharding.

Som programmet er lige nu, vil det bestemt ikke være nødvendigt, da det er et meget begrænset program, og ikke tager særlig meget
CPU-kraft, men hvis det var en reel social netværks-platform, ville det efter al sandsynlighed begynde at køre langsomt hvorefter
dataen skulle splittes op.

For at kunne splitte dataen ud i ligefordelt shards, skal der benyttes en shard key. Denne er vigtig at vælge da et forkert valg potentielt
kan ende med at ødelægge kørslen af programmet, og ikke skabe nogle forbedringer.
I vores tilfælde med det sociale netværk, kunne et bud på en shard key være id'et for en 'user'. Dette vil gøre at alle shards
for en user kan være på én shard, hvor man dermed kan få alt den relevante data for en user, såsom comments, post, circles osv.

___________Mangler________________
Der er ikke taget højde for log in i denne implementering.

___________Opstart________________
Programmet kører i konsollen og startes ved at køre hele Solution. 
Man er fra starten logget ind som 'Jodle Birge' og kan bruge det sociale netværk som ham.

1. Man bliver herefter præsenteret for en Menu, hvorfra man kan vælge hvilken hvad man ønsker at foretage sig.
