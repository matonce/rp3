# Projektni zadatak: Daktilograf
## Zadatak: 

Daktilograf - napravite program koji korisniku omogućuje vježbanje brzog tipkanja na računalu. Na sučelju se mora nalaziti slika tipkovnice (osnovnih tipaka), a korisniku su ponuđene vježbe raznih težina. Program može nuditi razne opcije, da se korisniku učita neka od vježbi s odabranog nivoa (vježbe su unaprijed pripremljene), zatim da si sam sastavlja vježbu (omogućuje mu se odabir slova koje želi vježbati, a zatim program sam generira vježbu (može se birati duljina vježbe, duljina riječi i sl.)). Generirane vježbe se mogu i spremati i ponovno učitavati. Moguće je birati opciju prikaza slova koje se treba upisati na tipkovnici na ekranu te da se preskače slovo koje nije točno upisano ili da program čeka da se upiše točno slovo. Na kraju vježbe treba omogućiti dobivanje podataka poput vremena tipkanja, broja točno upisanih slova i sl. Omogućeno je i spremanje statističkih podataka u cilju praćenja napretka.


## Naslovna stranica:
Na naslovnoj stranici imamo 3 gumba. „Igraj levele“ vodi na unaprijed priremljene levele raznih težina, „Vježba“ vodi na vježbe koje možemo sami generirati i spremiti ili vježbati na pravim riječima te učitati već spremljene vježbe i klikom na „Statistika“ ispisuju nam se neki podaci o igranju do sada.


## Leveli:
Prvo odabiremo grupu levela s određene težine. Nije moguće preskakati levele, npr. potrebno je prijeći sve Vježbe s Levela1: Home row da bismo mogli otvoriti Level2: Top row. 

 
Kada odaberemo grupu levela, prikažu nam se dostupne vježbe s te težine. Ispod gumba za pokretanje svakog levela nalazi se trenutni rekord. Po završetku levela, automatski se pokreće sljedeći, a potrebno je ostvariti uvjete od barem 80% preciznosti i brzine od barem 3 riječi po minuti kako bi se otključao sljedeći. 

 

## Kontrola za tipkanje: 
Nakon pokretanja odabrane vježbe, riječi se čitaju iz baze i ispisuju na ekranu. Ispod riječi se nalazi tipkovnica na kojoj je moguće dodati opciju da se pokaže trenutno slovo. Isto tako je moguće dodati opciju preskakanja krivo napisanog slova, dok po defaultu program čeka dok se ne upiše točno slovo. 

 
Nakon završene vježbe, dobijemo podatke o preciznosti, vremenu i brzini za upravo odigranu vježbu. Ti podaci se automatski zapisuju i u statistiku. Ako smo upravo ostvarili rekord za taj level, dobit ćemo poruku i o tome, ili ako nismo ostvarili uvjet za sljedeći level.  U svakom trenutku je moguće prekinuti level i vratiti se natrag.

 

## Vježbe:
Klikom na vježbe dobijemo razne opcije vezane za vježbe. Moguće je „checkirati“ slova koja želimo vježbati, odrediti duljinu svake riječi i broj riječi u vježbi. Tada program sam generira vježbu od označenog broja riječi u kojoj se nalaze riječi točno označene duljine i samo s označenim slovima.  
Ako želimo spremiti vježbu s zadanim postavkama, moramo još unijeti i proizvoljno ime te vježbe i kliknuti na gumb „Spremi generiranu vježbu“.
Ako kliknemo na gumb „Započni vježbu sa stvarnim slovima“ pokreće se vježba koja se sastoj od odabranog broja riječi. Riječi u ovom tipu vježbe se nasumično biraju iz unaprijed pripremljene baze. 

 
Klikom na „Pregled spremljenih vježbi“ dobijemo popis prethodno spremljenih vježbi koje je moguće pokrenuti klikom na odgovarajući gumb.

 
## Statistika:
Ako na naslovnoj stranici kliknemo na „Statistika“ prikažu nam se podaci o zadnjih 15 vježbi (ako još nismo odigrali 15, prikažu se sve dosada) te što smo vježbali, brzina i preciznost te vježbe. Također prikazana je lista 5 slova koja nam najbolje idu i 5 slova koja nam najgore idu zajedno sa svojim postotkom točnosti. 

