# Lastenheft  
# SASD Personal Desktop Dashboard

**Projekt:** SASD Personal Desktop Dashboard  
**Repository:** `Robin-Goerlach/SASD-PersonalDesktopDashboard`  
**Dokumenttyp:** Lastenheft / fachliche Anforderungen  
**Version:** 0.1 Entwurf  
**Datum:** 2026-05-13  
**Autor / Auftraggeber:** Robin Goerlach, SASD-GmbH – Scientific and Software Development  
**Status:** Arbeitsfassung für Repository-Dokumentation und spätere Pflichtenheft-Erstellung  
**Sprache:** Deutsch  

---

## 1. Zweck des Dokuments

Dieses Lastenheft beschreibt die fachlichen Anforderungen an das Projekt **SASD Personal Desktop Dashboard**.  
Es beantwortet die Frage:

> **Was soll das System aus Sicht des Anwenders leisten?**

Das Dokument beschreibt bewusst einen etwas größeren Zielumfang als die erste technische Version. Dadurch sollen alle wichtigen Ideen, Betriebsarten und Randbedingungen früh gesammelt werden, ohne die erste Umsetzung unnötig zu überladen.

Die konkrete technische Umsetzung, Architektur, Klassenstruktur, Datenformate, Framework-Auswahl und Reihenfolge der Entwicklung werden später im Pflichtenheft, im Technical Design und in der Roadmap festgelegt.

---

## 2. Ausgangssituation

Ein Windows-Desktop zeigt häufig nur ein statisches Hintergrundbild und Desktop-Icons an. Für einen produktiven Arbeitsalltag bietet diese Fläche nur begrenzten Nutzen.

Der Anwender arbeitet häufig an unterschiedlichen Arbeitsplätzen:

- manchmal nur mit dem Laptop-Display,
- oft mit einem zweiten Monitor,
- teilweise mit einem dritten Monitor,
- mit wechselnden Auflösungen,
- mit wechselnder DPI-/Skalierung,
- mit unterschiedlichen Arbeitsmodi: Entwicklung, Recherche, Dokumentation, Präsentation, Planung.

Gleichzeitig müssen viele Informationen im Blick behalten werden:

- Wetter am aktuellen Standort,
- nächste Aufgaben,
- Termine,
- lokale und weltweite Nachrichten,
- IT-/Security-Meldungen,
- Systemzustand des eigenen Rechners,
- Status von SASD-Projekten,
- ggf. später Status eigener Server, Dienste oder Repositories.

Der Desktop soll deshalb nicht nur dekorativ sein, sondern als **ruhiger persönlicher Arbeitsleitstand** dienen.

---

## 3. Ziel des Systems

Das SASD Personal Desktop Dashboard soll eine Windows-Anwendung werden, die dem Anwender wichtige persönliche, technische und projektbezogene Informationen übersichtlich, ruhig und datenschutzbewusst anzeigt.

Das System soll insbesondere:

1. den Windows-Desktop sinnvoll ergänzen,
2. Wetter, Aufgaben, Kalender, Nachrichten, Systemstatus und SASD-Projektinformationen anzeigen,
3. mit Laptop-, Zwei-Monitor- und Drei-Monitor-Arbeitsplätzen sinnvoll umgehen,
4. bei wechselnden Monitoren, Auflösungen und Skalierungen robust reagieren,
5. den normalen Windows-Desktop mit Papierkorb, Desktop-Icons und Explorer-Funktionen nicht ersetzen,
6. den Rechner nicht spürbar ausbremsen,
7. Privatsphäre und Präsentationssituationen berücksichtigen,
8. später als eigenständiges SASD-Produkt oder Portfolio-Projekt weiterentwickelbar sein.

---

## 4. Produktvision

Das Produkt soll sich wie ein **ruhiges Cockpit** für den Arbeitstag anfühlen.

Nicht gewünscht ist ein unruhiges, blinkendes, ablenkendes Dashboard.  
Gewünscht ist eine klare, sachliche und moderne Oberfläche, die schnell Orientierung gibt:

- Was ist heute wichtig?
- Was steht als Nächstes an?
- Muss ich mit Regen, Sturm oder Glätte rechnen?
- Gibt es wichtige Nachrichten?
- Gibt es technische Warnungen?
- Gibt es offene SASD-Aufgaben?
- Ist mein System in Ordnung?
- Welche Arbeit sollte jetzt Priorität haben?

Das Dashboard soll dem Anwender mentale Last abnehmen und nicht zusätzliche Aufmerksamkeit erzwingen.

---

## 5. Abgrenzung

### 5.1 Was das System sein soll

Das System soll sein:

- eine Windows-Desktop-Anwendung,
- ein persönliches Dashboard,
- ein Multi-Monitor-fähiger Arbeitsleitstand,
- ein Informations- und Orientierungswerkzeug,
- ein datenschutzbewusster Begleiter im Arbeitsalltag,
- ein später erweiterbares SASD-Werkzeug.

### 5.2 Was das System nicht sein soll

Das System soll ausdrücklich **nicht** sein:

- kein Ersatz für den Windows Explorer,
- kein Ersatz für den Windows-Desktop,
- kein Ersatz für den Papierkorb,
- kein vollständiger Task-Manager wie Microsoft To Do, Todoist oder Trello,
- kein vollständiger Kalender-Client,
- kein vollständiger Newsreader,
- kein Ersatz für professionelle Monitoring-Systeme,
- kein Passwortmanager,
- kein Ort für geheime Zugangsdaten,
- kein System zur Anzeige vertraulicher Kundendaten auf dem Desktop,
- kein animiertes Spaß-Wallpaper als Hauptzweck.

---

## 6. Zielgruppen

### 6.1 Primäre Zielgruppe

Die primäre Zielgruppe ist zunächst der Anwender selbst:

- Softwareentwickler,
- Linux-/Windows-/Systemadministrator,
- Inhaber der SASD-GmbH,
- Nutzer mehrerer Monitore,
- Nutzer mit vielen parallelen Projekten,
- Anwender mit Bedarf an Tagesorientierung und Projektübersicht.

### 6.2 Sekundäre Zielgruppe

Später kann das Projekt auch interessant sein für:

- Selbstständige IT-Dienstleister,
- kleine Softwarefirmen,
- technische Berater,
- Entwickler mit mehreren Monitoren,
- Administratoren mit Bedarf an leichtem Desktop-Monitoring,
- Nutzer, die lokale, datenschutzbewusste Produktivitätswerkzeuge bevorzugen.

---

## 7. Grundprinzipien

### 7.1 Windows bleibt zuständig für Desktop und Icons

Das Dashboard soll den nativen Windows-Desktop nicht ersetzen.  
Der Windows Explorer bleibt zuständig für:

- Papierkorb,
- Desktop-Dateien,
- Desktop-Verknüpfungen,
- Rechtsklick-Menüs,
- Icon-Anordnung,
- Drag & Drop auf dem Desktop,
- Anzeige von Dateien im Desktop-Ordner.

Das Dashboard darf diese Funktionen nicht blockieren, beschädigen oder nachbauen müssen.

### 7.2 Dashboard ergänzt statt überdeckt

Das Dashboard soll als eigenes Fenster, Sidebar oder Wallboard arbeiten.  
Es soll sich in den Desktop-Arbeitsfluss einfügen, ohne ständig im Weg zu sein.

### 7.3 Ruhige Informationsdarstellung

Informationen sollen geordnet, priorisiert und ruhig angezeigt werden.  
Es sollen keine unnötigen Animationen, blinkenden Elemente oder dauernden Pop-ups verwendet werden.

### 7.4 Datenschutz zuerst

Das Dashboard kann persönliche Aufgaben, Termine und Projektinformationen anzeigen.  
Deshalb muss es Modi geben, mit denen sensible Informationen ausgeblendet oder anonymisiert werden können.

### 7.5 Performance zuerst

Das Dashboard darf den Rechner nicht spürbar ausbremsen.  
Es soll Ressourcen schonend nutzen, Daten cachen und Aktualisierungen kontrolliert durchführen.

### 7.6 Erweiterbarkeit

Das System soll modular gedacht werden.  
Neue Informationsquellen sollen später ergänzt werden können, ohne die Grundanwendung umzubauen.

---

## 8. Einsatzszenarien

## 8.1 Laptop allein unterwegs

Der Anwender arbeitet nur mit dem Laptop-Display.

Erwartetes Verhalten:

- Dashboard startet im Kompaktmodus.
- Es nimmt wenig Platz ein.
- Es kann als Sidebar, kleines Fenster oder per Hotkey einblendbares Dashboard erscheinen.
- Es reduziert Aktualisierungsintervalle im Akkubetrieb.
- Es zeigt nur die wichtigsten Informationen.
- Es darf den normalen Arbeitsbereich nicht dominieren.

Typische Inhalte:

- Uhrzeit,
- Wetter jetzt und nächste Stunden,
- nächste Aufgabe,
- nächster Termin,
- Akku-/Netzwerkstatus,
- ggf. kurze Warnungen.

## 8.2 Arbeitsplatz mit zweitem Monitor

Der Anwender arbeitet mit Laptop und einem externen Monitor oder Desktop-PC mit zwei Monitoren.

Erwartetes Verhalten:

- Dashboard kann auf dem zweiten Monitor als großes Fenster oder Wallboard laufen.
- Hauptmonitor bleibt für produktive Arbeit frei.
- Dashboard nutzt die größere Fläche für mehrere Karten.
- Layout passt sich an Auflösung und Skalierung an.
- Beim Trennen des Monitors wechselt das Dashboard sauber auf den primären Monitor oder in den Kompaktmodus.

Typische Inhalte:

- Wetterverlauf,
- Tagesaufgaben,
- Kalender,
- Nachrichten,
- Systemstatus,
- SASD-Projekte.

## 8.3 Arbeitsplatz mit drei Monitoren

Der Anwender arbeitet mit drei Monitoren.

Erwartetes Verhalten:

- Dashboard kann auf einem dedizierten Monitor laufen.
- Optional können später mehrere Dashboard-Fenster auf verschiedene Monitore verteilt werden.
- Vorerst soll ein Hauptdashboard-Fenster genügen.
- Der Anwender soll bevorzugten Dashboard-Monitor und Fallback-Verhalten konfigurieren können.

Mögliche spätere Aufteilung:

- Monitor 1: Hauptarbeit,
- Monitor 2: Dokumentation / Recherche,
- Monitor 3: Dashboard.

## 8.4 Abdocken und Monitorwechsel

Der Anwender trennt während des Betriebs externe Monitore.

Erwartetes Verhalten:

- Dashboard darf nicht unsichtbar außerhalb des sichtbaren Arbeitsbereichs bleiben.
- Dashboard erkennt, dass ein Monitor nicht mehr verfügbar ist.
- Dashboard verschiebt sich auf einen verfügbaren Monitor.
- Falls kein externer Monitor vorhanden ist, wird ein kompakter Modus aktiviert.
- Gespeicherte Fensterpositionen dürfen nur wiederhergestellt werden, wenn sie noch gültig sind.

## 8.5 Präsentation oder Bildschirmfreigabe

Der Anwender teilt seinen Bildschirm in einer Besprechung.

Erwartetes Verhalten:

- Dashboard soll einen Privacy Mode besitzen.
- Persönliche Aufgaben und Termine können ausgeblendet oder anonymisiert werden.
- Projekt- und Kundennamen können ausgeblendet werden.
- Nur neutrale Informationen wie Uhr, Wetter oder Systemstatus bleiben sichtbar.
- Privacy Mode soll schnell aktivierbar sein.

## 8.6 Konzentrierte Arbeit

Der Anwender möchte sich auf eine Aufgabe konzentrieren.

Erwartetes Verhalten:

- Dashboard kann in einen Fokusmodus wechseln.
- Fokusmodus zeigt nur sehr wenige Informationen.
- Nachrichten und weniger wichtige Karten werden ausgeblendet.
- Die wichtigste Aufgabe und der nächste Termin bleiben sichtbar.

## 8.7 Offline-Betrieb

Der Anwender hat keine Internetverbindung.

Erwartetes Verhalten:

- Dashboard bleibt nutzbar.
- Zuletzt geladene Wetter- und Nachrichtendaten können weiter angezeigt werden.
- Jede Karte zeigt bei Bedarf den Zeitpunkt der letzten Aktualisierung.
- Lokale Aufgaben und lokale Konfiguration bleiben verfügbar.
- Fehler werden ruhig angezeigt und erzeugen keine störenden Meldungsschleifen.

---

## 9. Hauptfunktionen im Überblick

Die folgenden Funktionsbereiche sollen im Lastenheft berücksichtigt werden:

1. Dashboard-Oberfläche,
2. Anzeige- und Betriebsmodi,
3. Multi-Monitor-Unterstützung,
4. Layout- und Skalierungsverhalten,
5. Wettermodul,
6. Aufgabenmodul,
7. Kalendermodul,
8. Nachrichtenmodul,
9. Systemstatusmodul,
10. SASD-Projektmodul,
11. Desktop-/Explorer-Verträglichkeit,
12. Konfiguration,
13. Datenschutz und Privacy Mode,
14. Offline- und Cache-Verhalten,
15. Benachrichtigungen,
16. Performance- und Energiesparverhalten,
17. Erweiterbarkeit,
18. Design und Themes,
19. Barrierearmut und Lesbarkeit,
20. Installation, Autostart und Betrieb.

---

# 10. Funktionale Anforderungen

## 10.1 Dashboard-Oberfläche

### 10.1.1 Kartenbasierte Darstellung

Das Dashboard soll Informationen in klar getrennten Karten anzeigen.

Beispiele für Karten:

- Wetter,
- Heute,
- Aufgaben,
- Kalender,
- Nachrichten,
- Systemstatus,
- SASD-Projekte,
- Hinweise/Warnungen.

Jede Karte soll eine klare Überschrift, einen Hauptwert und ergänzende Details haben.

Beispiel:

```text
Wetter
17 °C · bewölkt
Regen ab 18:00 wahrscheinlich
Wind: 21 km/h
```

### 10.1.2 Priorisierte Informationsanzeige

Wichtige Informationen sollen visuell stärker hervorgehoben werden als unwichtige.

Beispiele:

- überfällige Aufgaben,
- kritischer Akkustand,
- volle Festplatte,
- Wetterwarnung,
- nächster Termin in weniger als 15 Minuten.

### 10.1.3 Ruhiges Layout

Das Layout soll ruhig und technisch-professionell wirken.

Nicht gewünscht:

- blinkende Elemente,
- unnötige Animationen,
- überladene Tabellen,
- grelle Farben,
- Werbecharakter,
- News-Ticker mit dauernder Bewegung.

### 10.1.4 Karten ein- und ausblendbar

Der Anwender soll festlegen können, welche Karten angezeigt werden.

Beispiele:

- Wetterkarte aktivieren/deaktivieren,
- Nachrichten ausblenden,
- Systemstatus nur bei Bedarf anzeigen,
- SASD-Projekte erst später aktivieren.

### 10.1.5 Kartenreihenfolge

Der Anwender soll perspektivisch die Reihenfolge der Karten beeinflussen können.

Für die erste Version genügt eine feste sinnvolle Reihenfolge.

---

## 10.2 Anzeige- und Betriebsmodi

## 10.2.1 Compact Mode

Der Compact Mode ist für den Laptopbetrieb gedacht.

Eigenschaften:

- kleine Darstellung,
- wenig Platzbedarf,
- reduzierte Kartenanzahl,
- optional als Sidebar,
- optional per Hotkey einblendbar,
- energiesparende Aktualisierung.

Mindestinhalt:

- aktuelle Uhrzeit,
- Wetterkurzinfo,
- nächste Aufgabe,
- nächster Termin,
- Akku-/Netzwerkstatus.

## 10.2.2 Dashboard Mode

Der Dashboard Mode ist der normale große Modus.

Eigenschaften:

- mehrere Karten gleichzeitig sichtbar,
- optimiert für zweiten Monitor,
- gute Übersicht über den Tag,
- ruhige visuelle Darstellung.

Mindestinhalt:

- Wetter,
- Aufgaben,
- Kalender,
- Nachrichten,
- Systemstatus.

## 10.2.3 Wallboard Mode

Der Wallboard Mode ist für einen dedizierten Monitor gedacht.

Eigenschaften:

- großflächige Darstellung,
- lesbar aus etwas Entfernung,
- reduzierter Interaktionsbedarf,
- automatische Aktualisierung,
- optional Vollbild.

## 10.2.4 Focus Mode

Der Focus Mode reduziert Ablenkung.

Eigenschaften:

- nur Tagesfokus,
- wichtigste Aufgabe,
- nächster Termin,
- Wetterhinweis nur bei Relevanz,
- keine normalen Nachrichten,
- keine dekorativen Elemente.

## 10.2.5 Privacy Mode

Der Privacy Mode schützt sensible Inhalte.

Eigenschaften:

- persönliche Aufgaben anonymisieren,
- Kalenderdetails ausblenden,
- Projektnamen optional verbergen,
- keine Kundennamen anzeigen,
- keine vertraulichen Notizen anzeigen,
- schnell aktivierbar.

Beispiel:

Normal:

```text
14:30 Arzttermin Dr. Mustermann
15:30 Angebot für Kunde Beispiel GmbH prüfen
```

Privacy Mode:

```text
14:30 Privater Termin
15:30 Projektaufgabe
```

## 10.2.6 Silent Mode

Der Silent Mode lässt das Dashboard im Hintergrund laufen.

Eigenschaften:

- keine sichtbare Oberfläche,
- Zugriff über Tray-Icon oder Hotkey,
- keine störenden Benachrichtigungen,
- Daten können im Hintergrund aktualisiert werden, sofern erlaubt.

## 10.2.7 Ambient Mode

Der Ambient Mode ist ein optionaler schöner Modus für ruhige Anzeige auf einem Nebenmonitor.

Eigenschaften:

- dezente Glas-/Transparenzoptik,
- optional sichtbares Hintergrundbild,
- große Uhr,
- Wetter,
- wenige ruhige Karten.

Wichtig: Ambient Mode darf die Lesbarkeit nicht verschlechtern und darf nicht Hauptziel der ersten Version sein.

---

## 10.3 Multi-Monitor-Unterstützung

## 10.3.1 Monitorerkennung

Das System soll erkennen:

- Anzahl der Monitore,
- primärer Monitor,
- verfügbare Arbeitsbereiche,
- Auflösung,
- relative Position der Monitore,
- Skalierung/DPI soweit relevant.

## 10.3.2 Monitorprofile

Das System soll unterschiedliche Monitorprofile unterstützen.

Beispiele:

```text
Profil: Laptop unterwegs
- 1 Monitor
- Compact Mode
- energiesparend

Profil: Büro Dockingstation
- 2 oder 3 Monitore
- Dashboard auf externem Monitor
- normale Aktualisierung

Profil: Präsentation
- Privacy Mode
- keine persönlichen Details
```

## 10.3.3 Sicheres Wiederherstellen von Fensterpositionen

Gespeicherte Fensterpositionen dürfen nur wiederhergestellt werden, wenn sie auf einem aktuell vorhandenen Monitor sichtbar sind.

Wenn nicht, soll das System eine sichere Fallback-Position verwenden.

## 10.3.4 Fallback bei fehlendem Dashboard-Monitor

Wenn der bevorzugte Dashboard-Monitor fehlt:

1. Dashboard auf primärem Monitor öffnen,
2. Compact Mode aktivieren,
3. optional Hinweis anzeigen,
4. keine Fenster außerhalb des sichtbaren Bereichs erzeugen.

## 10.3.5 Unterstützung unterschiedlicher Auflösungen

Das Dashboard soll mit typischen Auflösungen umgehen können:

- Laptop-Displays,
- Full HD,
- WQHD,
- 4K,
- Hochkant-Monitore,
- unterschiedliche Skalierungen.

## 10.3.6 Keine Annahme fester Monitorreihenfolge

Das System darf nicht blind annehmen, dass „Monitor 2“ immer derselbe physische Monitor ist.  
Monitorprofile sollen robust gegen wechselnde Geräte- und Anschlussreihenfolgen sein.

---

## 10.4 Layout und Skalierung

## 10.4.1 Responsive Layout

Das Dashboard soll seine Karten abhängig von verfügbarer Breite und Höhe anordnen.

Beispielhafte Logik:

```text
schmal:
- eine Spalte

mittel:
- zwei Spalten

breit:
- drei oder vier Spalten

Wallboard:
- große Kacheln
```

## 10.4.2 Lesbarkeit

Texte müssen gut lesbar bleiben.

Anforderungen:

- ausreichender Kontrast,
- nicht zu kleine Schrift,
- klare Hierarchie,
- wichtige Informationen auf einen Blick,
- keine winzigen Tabellen in großen Karten.

## 10.4.3 DPI-/Skalierungsfreundlichkeit

Das Dashboard soll auch bei 125 %, 150 % oder anderen Windows-Skalierungen sinnvoll aussehen.

## 10.4.4 Keine harte Pixelabhängigkeit

Das Layout soll nicht ausschließlich auf festen Pixelwerten basieren.  
Feste Mindestgrößen sind erlaubt, starre Gesamtlayouts sollen vermieden werden.

---

## 10.5 Wettermodul

## 10.5.1 Aktuelles Wetter

Das Wettermodul soll aktuelles Wetter für den Standort des Anwenders anzeigen.

Mögliche Werte:

- Temperatur,
- gefühlte Temperatur,
- Wetterzustand,
- Niederschlagswahrscheinlichkeit,
- Wind,
- Luftfeuchtigkeit,
- ggf. Luftdruck.

## 10.5.2 Wetter der nächsten Stunden

Das Modul soll besonders die nächsten Stunden anzeigen, da diese für Tagesplanung nützlich sind.

Mindestziel:

- Vorhersage für die nächsten 6 Stunden.

Optional:

- 12 Stunden,
- 24 Stunden,
- Tagesübersicht.

## 10.5.3 Relevante Hinweise

Das Modul soll relevante Hinweise hervorheben.

Beispiele:

- Regen beginnt bald,
- starker Wind,
- Glätte möglich,
- Gewitterrisiko,
- Hitze,
- Frost.

## 10.5.4 Standortverwaltung

Der Standort soll konfigurierbar sein.

Mögliche Varianten:

- manuelle Stadt/Postleitzahl,
- Koordinaten,
- später automatische Standortermittlung,
- mehrere gespeicherte Orte.

Für die erste Version genügt ein manuell konfigurierter Standort.

## 10.5.5 Aktualisierungsintervall

Das Wetter soll nicht permanent neu geladen werden.  
Ein Intervall von 15 bis 60 Minuten ist je nach Modus ausreichend.

## 10.5.6 Offline-Verhalten

Wenn Wetterdaten nicht geladen werden können, soll der letzte bekannte Stand angezeigt werden.

---

## 10.6 Aufgabenmodul

## 10.6.1 Anzeige nächster Aufgaben

Das Dashboard soll die wichtigsten nächsten Aufgaben anzeigen.

Mindestinformationen:

- Titel,
- Fälligkeitsstatus,
- Priorität oder Wichtigkeit,
- Projektbezug optional,
- Status offen/erledigt.

## 10.6.2 Heute fällige Aufgaben

Das System soll Aufgaben hervorheben, die heute fällig sind.

## 10.6.3 Überfällige Aufgaben

Überfällige Aufgaben sollen erkennbar sein, aber nicht unnötig aggressiv dargestellt werden.

## 10.6.4 Top-3-Aufgaben

Das Dashboard soll eine reduzierte Liste der wichtigsten Aufgaben anzeigen können.

Ziel: Orientierung statt Überforderung.

## 10.6.5 Tagesfokus

Der Anwender soll einen Tagesfokus sehen oder später setzen können.

Beispiel:

```text
Tagesfokus:
Pflichtenheft für SASD Personal Desktop Dashboard abschließen
```

## 10.6.6 Datenquellen für Aufgaben

Mögliche Datenquellen:

- lokale JSON-Datei,
- lokale SQLite-Datenbank,
- TaskHost Local,
- später Microsoft To Do,
- später GitHub Issues,
- später andere Systeme.

Für frühe Versionen genügt eine lokale Datenquelle oder Dummy-Daten.

## 10.6.7 Keine vollständige Aufgabenverwaltung in V1 erforderlich

Das Dashboard muss nicht sofort ein kompletter Aufgabenmanager sein.  
Es soll Aufgaben zunächst anzeigen und ggf. einfache Statusaktionen ermöglichen.

---

## 10.7 Kalendermodul

## 10.7.1 Nächster Termin

Das Dashboard soll den nächsten Termin anzeigen.

Mindestinformationen:

- Uhrzeit,
- Titel,
- verbleibende Zeit,
- Ort oder Online-Hinweis optional.

## 10.7.2 Tagesübersicht

Das Dashboard soll perspektivisch eine kurze Tagesübersicht anzeigen.

Beispiel:

```text
10:00 Projektarbeit
13:30 Telefonat
16:00 Dokumentation
```

## 10.7.3 Privacy Mode für Kalender

Im Privacy Mode müssen Kalendereinträge anonymisiert oder ausgeblendet werden können.

## 10.7.4 Datenquellen für Kalender

Mögliche Datenquellen:

- lokale ICS-Datei,
- Outlook/Windows-Kalender,
- Google Kalender,
- Microsoft 365,
- manuell gepflegte Datei.

Für die erste Version genügt Dummy-Darstellung oder lokale Konfiguration.

---

## 10.8 Nachrichtenmodul

## 10.8.1 Nachrichtenübersicht

Das Dashboard soll ausgewählte Nachrichtenquellen anzeigen können.

Kategorien:

- lokale Nachrichten,
- Weltlage,
- IT/Security,
- Wissenschaft,
- optional Wirtschaft,
- optional Open Source / Softwareentwicklung.

## 10.8.2 Keine Reizüberflutung

Das Nachrichtenmodul darf nicht zum Doomscrolling verleiten.

Anforderungen:

- begrenzte Anzahl von Meldungen,
- keine endlosen Feeds,
- keine hektischen Ticker,
- Aktualisierung nur in sinnvollen Intervallen,
- Quellen konfigurierbar.

## 10.8.3 RSS-Unterstützung

Das Dashboard soll perspektivisch RSS-Feeds unterstützen.

RSS ist geeignet, weil es relativ einfach, offen und gut konfigurierbar ist.

## 10.8.4 Priorisierte Nachrichten

Später können wichtige Meldungen hervorgehoben werden.

Beispiele:

- IT-Sicherheitswarnungen,
- lokale Unwetter-/Gefahrenmeldungen,
- wichtige Weltlage-Ereignisse.

## 10.8.5 Quellenauswahl

Der Anwender soll bestimmen können, welche Quellen verwendet werden.

Keine ungefragte Nachrichtenquelle soll fest eingebaut werden.

---

## 10.9 Systemstatusmodul

## 10.9.1 Lokaler Systemstatus

Das Dashboard soll grundlegende Systeminformationen anzeigen.

Mögliche Werte:

- CPU-Auslastung,
- RAM-Auslastung,
- freier Speicherplatz,
- Akkustand,
- Netzbetrieb/Akkubetrieb,
- Netzwerkstatus,
- WLAN/LAN-Hinweis,
- Uptime optional.

## 10.9.2 Warnungen

Das System soll einfache Warnungen anzeigen.

Beispiele:

- Akku niedrig,
- Festplatte fast voll,
- keine Netzwerkverbindung,
- hoher Speicherverbrauch,
- lange kein Backup erkannt, falls später implementiert.

## 10.9.3 Reduzierte Abfragefrequenz

Systemstatus darf nicht zu häufig abgefragt werden.  
Werte sollen nur so oft aktualisiert werden, wie es für ein Dashboard sinnvoll ist.

## 10.9.4 Keine professionelle Monitoringlösung ersetzen

Das Systemstatusmodul dient der persönlichen Orientierung.  
Es ersetzt kein vollwertiges Monitoring-System wie Zabbix, Prometheus, Grafana oder andere Lösungen.

---

## 10.10 SASD-Projektmodul

## 10.10.1 Projektübersicht

Das Dashboard soll perspektivisch eine Übersicht über aktive SASD-Projekte anzeigen.

Beispiele:

- TaskHost,
- LogSink,
- SASD Desktop Secret Manager,
- SASD ResearchHub,
- SASD Personal Desktop Dashboard,
- andere aktive Repositories.

## 10.10.2 Projektstatus

Mögliche Informationen:

- letzter Commit,
- offene Aufgaben,
- offene Issues,
- aktueller Arbeitsstand,
- nächste geplante Aktion,
- Dokumentationsstatus,
- Release-Status.

## 10.10.3 Lokale Projektliste

Für frühe Versionen kann eine lokale Projektliste genügen.

Beispiel:

```json
{
  "projects": [
    {
      "name": "SASD Personal Desktop Dashboard",
      "status": "active",
      "nextAction": "V0.1 Shell erstellen"
    }
  ]
}
```

## 10.10.4 GitHub-Integration optional

Eine spätere Version kann GitHub-Repositories, Issues oder Releases einlesen.

Für V0.1 ist keine GitHub-Integration erforderlich.

---

## 10.11 Desktop- und Explorer-Verträglichkeit

## 10.11.1 Papierkorb bleibt Windows-Funktion

Der Papierkorb soll nicht nachgebaut werden.  
Er bleibt ein Windows-/Explorer-Feature.

## 10.11.2 Desktop-Dateien bleiben Windows-Funktion

Dateien und Verknüpfungen auf dem Desktop bleiben sichtbar und bedienbar, soweit das Dashboard sie nicht bewusst überdeckt.

## 10.11.3 Keine Beschädigung der Icon-Anordnung

Das Dashboard darf die Icon-Anordnung nicht verändern.

## 10.11.4 Keine aggressive Always-on-Top-Strategie

Das Dashboard soll nicht dauerhaft alles überlagern, außer der Anwender aktiviert dies bewusst.

## 10.11.5 Optionaler späterer Launchpad-Modus

Später kann ein Launchpad-Modul entwickelt werden, das wichtige Programme oder Desktop-Verknüpfungen anzeigt.

Dabei muss klar sein:

- Es ersetzt den Windows-Desktop nicht.
- Es ist eine zusätzliche Schnellstartansicht.
- Desktop-Icons bleiben weiterhin durch Windows verwaltet.

---

## 10.12 Konfiguration

## 10.12.1 Konfigurierbare Einstellungen

Das System soll Einstellungen speichern können.

Beispiele:

- bevorzugter Modus,
- bevorzugter Monitor,
- Standort für Wetter,
- aktive Karten,
- Feed-URLs,
- Datenschutzoptionen,
- Aktualisierungsintervalle,
- Theme,
- Fensterposition,
- Autostart.

## 10.12.2 Lokale Konfigurationsdatei

Für frühe Versionen ist eine lokale Konfigurationsdatei ausreichend.

Mögliche Formate:

- JSON,
- später YAML oder UI-basierte Einstellungen.

## 10.12.3 Sichere Behandlung sensibler Werte

Sensible Werte wie API-Keys dürfen nicht versehentlich im Repository landen.

Anforderungen:

- Beispielkonfiguration ohne echte Geheimnisse,
- echte lokale Konfiguration ignorierbar durch `.gitignore`,
- keine Ausgabe sensibler Werte in Logs,
- keine Anzeige sensibler Werte im Dashboard.

## 10.12.4 Import/Export

Später soll Konfiguration exportiert und importiert werden können.

Nützlich für:

- neue Rechner,
- Neuinstallation,
- Backup,
- Dokumentation.

---

## 10.13 Datenschutz und Privatsphäre

## 10.13.1 Privacy Mode

Das System muss einen Privacy Mode vorsehen.

Im Privacy Mode können ausgeblendet oder anonymisiert werden:

- Aufgabentitel,
- Kalendereinträge,
- Projektnamen,
- Kundennamen,
- private Hinweise,
- persönliche Notizen.

## 10.13.2 Präsentationssicherheit

Der Anwender soll das Dashboard schnell in einen präsentationssicheren Zustand versetzen können.

Beispiele:

- per Hotkey,
- per Tray-Menü,
- per sichtbarem Button.

## 10.13.3 Keine sensiblen Inhalte im Standarddesign erzwingen

Karten mit potenziell sensiblen Daten müssen deaktivierbar sein.

## 10.13.4 Lokale Daten bevorzugen

Soweit möglich, sollen lokale Datenquellen unterstützt werden.  
Cloud-Integrationen sollen optional bleiben.

---

## 10.14 Offline- und Cache-Verhalten

## 10.14.1 Anzeige letzter bekannter Daten

Wenn externe Daten nicht erreichbar sind, soll das Dashboard letzte bekannte Daten anzeigen können.

## 10.14.2 Aktualisierungszeitpunkt anzeigen

Karten sollen bei Bedarf anzeigen:

```text
Zuletzt aktualisiert: 13.05.2026 16:45
```

## 10.14.3 Fehler ruhig behandeln

Fehler sollen nicht ständig als Pop-up erscheinen.

Stattdessen:

- dezenter Hinweis in der Karte,
- optional Fehlerdetails in Logdatei,
- keine Dauerschleife störender Meldungen.

## 10.14.4 Manuelle Aktualisierung

Der Anwender soll alle oder einzelne Karten manuell aktualisieren können.

---

## 10.15 Benachrichtigungen

## 10.15.1 Zurückhaltende Benachrichtigungen

Benachrichtigungen sollen sparsam eingesetzt werden.

Geeignete Fälle:

- Termin beginnt bald,
- Akku kritisch,
- Wetterwarnung,
- wichtige Aufgabe fällig,
- Verbindungsproblem zu wichtiger Datenquelle.

## 10.15.2 Benachrichtigungen deaktivierbar

Alle Benachrichtigungen müssen deaktivierbar oder einschränkbar sein.

## 10.15.3 Kein Nachrichten-Spam

Normale Nachrichtenüberschriften sollen nicht ständig als Systembenachrichtigung erscheinen.

---

## 10.16 Performance und Energieverbrauch

## 10.16.1 Geringe Grundlast

Das Dashboard soll im Normalbetrieb eine geringe CPU- und RAM-Last verursachen.

## 10.16.2 Keine dauerhaften Hochfrequenz-Timer

Das System soll keine unnötig häufigen Timer verwenden.

## 10.16.3 Aktualisierung nach Bedarf

Karten sollen nur aktualisiert werden, wenn:

- ein Intervall abgelaufen ist,
- der Anwender manuell aktualisiert,
- ein relevanter Systemzustand sich ändert,
- das Fenster sichtbar ist und Daten veraltet sind.

## 10.16.4 Sichtbarkeitsabhängige Aktualisierung

Wenn das Dashboard nicht sichtbar ist, können Aktualisierungen reduziert werden.

## 10.16.5 Akkubetrieb berücksichtigen

Im Akkubetrieb soll das Dashboard einen sparsameren Modus verwenden können.

Beispiele:

- längere Aktualisierungsintervalle,
- weniger Systemabfragen,
- keine Animationen,
- reduzierte Netzwerkzugriffe.

---

## 10.17 Erweiterbarkeit

## 10.17.1 Modulkonzept

Fachlich soll das Dashboard aus Modulen bestehen.

Beispiele:

- WeatherModule,
- TaskModule,
- CalendarModule,
- NewsModule,
- SystemStatusModule,
- SasdProjectsModule.

## 10.17.2 Module aktivierbar/deaktivierbar

Module sollen einzeln aktivierbar und deaktivierbar sein.

## 10.17.3 Spätere Plugin-Fähigkeit

Langfristig kann eine Plugin-Architektur geprüft werden.

Für frühe Versionen genügt eine klare interne Modularisierung.

## 10.17.4 Datenquellen austauschbar

Ein Modul soll perspektivisch mehrere Datenquellen unterstützen können.

Beispiel Aufgabenmodul:

- lokale Datei,
- TaskHost,
- Microsoft To Do,
- GitHub Issues.

---

## 10.18 Design und Themes

## 10.18.1 Grunddesign

Das Dashboard soll modern, ruhig und technisch-professionell wirken.

Passend zur SASD-GmbH:

- dunkle Grundfläche,
- Petrol-/Blau-Akzente,
- klare Karten,
- dezente Schatten,
- gute Lesbarkeit,
- sachlicher Charakter.

## 10.18.2 Dark Theme

Ein Dark Theme soll bevorzugt werden.

## 10.18.3 Light Theme optional

Ein helles Theme kann später ergänzt werden.

## 10.18.4 Glass-/Acrylic-Look optional

Ein halbtransparenter Modus kann später ergänzt werden.

Wichtig:

- Lesbarkeit bleibt wichtiger als optischer Effekt.
- Transparenz darf nicht vom Inhalt ablenken.
- Hintergrundbilder dürfen Inhalte nicht unlesbar machen.

## 10.18.5 Keine Pflicht zu HTML/CSS/JavaScript

Das System muss nicht zwingend HTML/CSS/JavaScript verwenden.  
Die Technologie soll nach Robustheit, Wartbarkeit, Windows-Integration und Performance gewählt werden.

---

## 10.19 Barrierearmut und Bedienbarkeit

## 10.19.1 Gute Lesbarkeit

Schriftgrößen, Kontrast und Abstände sollen angenehm sein.

## 10.19.2 Skalierbare Darstellung

Der Anwender soll die Darstellung größer oder kleiner wählen können.

## 10.19.3 Tastaturzugriff

Wichtige Funktionen sollen per Tastatur oder Hotkey erreichbar sein.

Beispiele:

- Dashboard anzeigen/verbergen,
- Privacy Mode umschalten,
- Aktualisieren,
- Compact/Dashboard Mode wechseln.

## 10.19.4 Keine rein farbliche Bedeutung

Warnungen sollen nicht nur über Farbe erkennbar sein.  
Text oder Symbolik soll die Bedeutung unterstützen.

---

## 10.20 Installation, Autostart und Betrieb

## 10.20.1 Einfache Installation

Das System soll später einfach installierbar sein.

Mögliche Varianten:

- ZIP-Release,
- Setup-Datei,
- MSIX oder anderer Installer.

Für frühe Versionen reicht ein lokal gebauter Debug-/Release-Start.

## 10.20.2 Autostart

Das Dashboard soll optional mit Windows starten können.

## 10.20.3 Tray-Icon

Das System soll perspektivisch ein Tray-Icon besitzen.

Mögliche Aktionen:

- Dashboard anzeigen,
- Dashboard verbergen,
- Modus wechseln,
- Privacy Mode aktivieren,
- Einstellungen öffnen,
- Beenden.

## 10.20.4 Sauberes Beenden

Das System soll sauber beendet werden können.

## 10.20.5 Logging

Das System soll einfache lokale Logs schreiben können.

Ziel:

- Fehleranalyse,
- API-Probleme nachvollziehen,
- keine sensiblen Daten protokollieren.

---

# 11. Nicht-funktionale Anforderungen

## 11.1 Stabilität

Das Dashboard soll stabil laufen und nicht durch einzelne Datenquellen abstürzen.

Wenn z. B. Wetterdaten nicht geladen werden können, darf das gesamte Dashboard nicht ausfallen.

## 11.2 Robustheit bei Monitorwechseln

Monitorwechsel, Abdocken und unterschiedliche Auflösungen müssen als normale Nutzungsszenarien betrachtet werden, nicht als Sonderfälle.

## 11.3 Wartbarkeit

Das Projekt soll so strukturiert sein, dass spätere Erweiterungen möglich sind.

Anforderungen:

- klare Trennung von Anzeige, Datenlogik und Konfiguration,
- sprechende Namen,
- gute Dokumentation,
- verständliche Kommentare im Code,
- nachvollziehbare Architekturentscheidungen.

## 11.4 Performance

Das Dashboard soll ressourcenschonend arbeiten.

Es darf nicht:

- dauerhaft hohe CPU-Last erzeugen,
- unnötig Netzwerkverkehr verursachen,
- dauerhaft intensive Animationen rendern,
- bei jedem UI-Refresh alle Daten neu laden.

## 11.5 Sicherheit

Das Dashboard soll keine unnötigen Risiken erzeugen.

Anforderungen:

- keine Speicherung von Geheimnissen im Repository,
- lokale Konfiguration schützen,
- API-Keys nicht anzeigen,
- Fehlerausgaben ohne sensible Details,
- keine ungeprüfte Ausführung fremder Inhalte,
- keine gefährlichen Skriptmechanismen in V1.

## 11.6 Datenschutz

Persönliche Informationen müssen kontrollierbar sein.

Anforderungen:

- sensible Karten deaktivierbar,
- Privacy Mode,
- lokale Daten bevorzugt,
- klare Trennung von echter Konfiguration und Beispielkonfiguration.

## 11.7 Erweiterbarkeit

Neue Module sollen später ergänzt werden können, ohne die Grundanwendung neu zu schreiben.

## 11.8 Testbarkeit

Kernlogik soll testbar sein.

Beispiele:

- Monitorprofil-Auswahl,
- Konfigurationslogik,
- Cache-Entscheidungen,
- Priorisierung von Aufgaben,
- Privacy-Anonymisierung.

## 11.9 Dokumentierbarkeit

Das Projekt soll begleitende Dokumente erhalten.

Mindestens sinnvoll:

- README,
- Lastenheft,
- Pflichtenheft,
- Technical Design,
- UI-Konzept,
- Roadmap,
- später Benutzerhandbuch.

---

# 12. Priorisierung der Funktionen

Die folgende Priorisierung dient der Orientierung.  
Sie ersetzt nicht die spätere Roadmap.

## 12.1 Muss-Anforderungen für frühe technische Basis

- Windows-Desktop-Anwendung,
- Hauptfenster/Dashboard-Fenster,
- Dummy-Karten,
- einfache Konfiguration,
- sichere Fensterposition,
- Grundlayout,
- Monitoranzahl erkennen,
- Compact/Dashboard-Grundmodus,
- keine Störung von Desktop-Icons,
- keine sensiblen Daten im Repository.

## 12.2 Sollte-Anforderungen für V1

- Wettermodul mit echtem Standort,
- Aufgabenanzeige,
- Kalenderkurzanzeige,
- RSS-News,
- Systemstatus,
- Tray-Icon,
- Autostart optional,
- Privacy Mode,
- Cache für externe Daten,
- Monitorprofile,
- Einstellungen per Konfigurationsdatei.

## 12.3 Kann-Anforderungen für spätere Versionen

- GitHub-Integration,
- TaskHost-Integration,
- Microsoft To Do-Integration,
- Google-/Outlook-Kalenderintegration,
- mehrere Dashboard-Fenster,
- Glass-/Transparenzmodus,
- eigener Launchpad-Modus,
- serverseitige SASD-Statusintegration,
- Plugin-System,
- Import/Export von Profilen,
- Theme-Editor.

---

# 13. Fachliche Featureliste mit Beschreibung

## 13.1 Tagesübersicht

Die Tagesübersicht ist die zentrale Karte des Dashboards.

Sie soll anzeigen:

- Datum,
- Uhrzeit,
- Wochentag,
- Tagesfokus,
- nächster Termin,
- wichtigste Aufgabe,
- kurze Statusmeldung.

Ziel: Der Anwender soll nach dem Blick auf diese Karte wissen, wie der Tag beginnt.

## 13.2 Wettervorhersage

Die Wetterkarte soll kurzfristig nützliche Informationen liefern, nicht nur dekorative Wetterdaten.

Wichtig sind:

- Muss ich einen Regenschirm mitnehmen?
- Wird es heute gefährlich windig?
- Gibt es Glätte oder Gewitter?
- Kann ich später noch rausgehen?
- Wie entwickelt sich das Wetter in den nächsten Stunden?

## 13.3 Aufgabenübersicht

Die Aufgabenkarte soll nicht alle Aufgaben anzeigen, sondern die nächsten relevanten.

Sie soll helfen:

- Prioritäten zu erkennen,
- Überfälliges nicht zu vergessen,
- Tagesfokus zu halten,
- Projekte im Blick zu behalten.

## 13.4 Kalenderübersicht

Die Kalenderkarte soll verhindern, dass Termine überraschend kommen.

Sie soll besonders anzeigen:

- nächster Termin,
- Zeit bis zum Termin,
- heutige Termine,
- optionale Hinweise vor Beginn.

## 13.5 Nachrichtenübersicht

Die Nachrichtenkarte soll Überblick geben, aber nicht ablenken.

Sie soll:

- wenige wichtige Schlagzeilen zeigen,
- Quellen respektieren,
- keine hektische Darstellung nutzen,
- Security-/IT-Meldungen separat ermöglichen.

## 13.6 Systemzustand

Die Systemkarte soll einfache technische Orientierung geben.

Sie soll anzeigen:

- ob der Rechner im Akkubetrieb ist,
- ob Speicher knapp wird,
- ob Netzwerk verfügbar ist,
- ob das System ungewöhnlich belastet ist.

## 13.7 SASD-Projektübersicht

Die SASD-Projektkarte soll langfristig helfen, mehrere eigene Projekte nicht aus dem Blick zu verlieren.

Sie soll anzeigen:

- aktive Projekte,
- nächster sinnvoller Schritt,
- Status,
- offene Dokumentations- oder Entwicklungsaufgaben,
- optional GitHub-/Issue-Informationen.

## 13.8 Privacy-Anzeige

Der Privacy Mode ist kein Zusatzdetail, sondern ein wichtiges fachliches Merkmal.

Das System soll jederzeit zwischen normaler und anonymisierter Darstellung wechseln können.

## 13.9 Monitorprofil-Anzeige

Der Anwender soll erkennen können, in welchem Profil das Dashboard läuft.

Beispiele:

- Laptop unterwegs,
- Büro,
- Präsentation,
- Wallboard.

---

# 14. Datenquellen

## 14.1 Wetterdaten

Mögliche Quellen:

- Wetter-API,
- nationale Wetterdaten,
- später austauschbare Provider.

Fachliche Anforderungen:

- Standort konfigurierbar,
- Daten aktualisierbar,
- Cache möglich,
- Fehler sichtbar, aber unaufdringlich.

## 14.2 Aufgaben

Mögliche Quellen:

- lokale Datei,
- lokale Datenbank,
- TaskHost Local,
- spätere externe Aufgaben-APIs.

## 14.3 Kalender

Mögliche Quellen:

- lokale Datei,
- ICS,
- Outlook,
- Google,
- Microsoft 365.

## 14.4 Nachrichten

Mögliche Quellen:

- RSS,
- ausgewählte Nachrichtenseiten,
- IT-Security-Feeds,
- lokale Nachrichtenquellen.

## 14.5 Systemstatus

Mögliche Quellen:

- Windows-Systeminformationen,
- lokale Performance-Daten,
- Energie-/Akkuinformationen,
- Speicherplatzinformationen.

## 14.6 SASD-Projekte

Mögliche Quellen:

- lokale Projektliste,
- Git-Repositories,
- GitHub,
- TaskHost,
- spätere SASD-interne Dienste.

---

# 15. Benutzerinteraktion

## 15.1 Direkte Interaktion

Das Dashboard soll grundsätzlich wenig Interaktion verlangen.  
Es ist primär eine Anzeige- und Orientierungsoberfläche.

## 15.2 Mögliche Aktionen

Sinnvolle Aktionen:

- Aktualisieren,
- Karte ein-/ausblenden,
- Modus wechseln,
- Privacy Mode aktivieren,
- Einstellungen öffnen,
- Aufgabe als erledigt markieren, später optional,
- Link zu Nachricht öffnen, später optional,
- Projekt öffnen, später optional.

## 15.3 Hotkeys

Mögliche Hotkeys:

- Dashboard anzeigen/verbergen,
- Privacy Mode umschalten,
- Fokusmodus aktivieren,
- Aktualisieren,
- Wechsel zwischen Compact und Dashboard Mode.

Konkrete Tastenkombinationen werden im Pflichtenheft festgelegt.

---

# 16. Fehlerfälle

## 16.1 Wetterdienst nicht erreichbar

Erwartetes Verhalten:

- letzte bekannte Daten anzeigen,
- Hinweis in Wetterkarte,
- kein Absturz,
- kein störendes Pop-up.

## 16.2 Nachrichtenfeed ungültig

Erwartetes Verhalten:

- betroffene Quelle markieren,
- andere Quellen weiter anzeigen,
- Fehler protokollieren.

## 16.3 Konfigurationsdatei fehlerhaft

Erwartetes Verhalten:

- sichere Standardwerte verwenden,
- verständliche Fehlermeldung,
- keine Daten verlieren,
- optional Backup der fehlerhaften Datei.

## 16.4 Monitor fehlt

Erwartetes Verhalten:

- Dashboard auf sichtbaren Monitor verschieben,
- Compact Mode aktivieren,
- keine unsichtbaren Fenster.

## 16.5 Keine Internetverbindung

Erwartetes Verhalten:

- lokale Funktionen bleiben aktiv,
- externe Karten zeigen letzten Stand,
- ruhiger Hinweis.

---

# 17. Qualitätskriterien

## 17.1 Alltagstauglichkeit

Das Dashboard gilt als alltagstauglich, wenn es mehrere Tage im Hintergrund laufen kann, ohne zu stören oder instabil zu werden.

## 17.2 Schnelle Orientierung

Der Anwender soll innerhalb weniger Sekunden erkennen:

- Wetterlage,
- nächste Aufgabe,
- nächster Termin,
- technische Warnungen.

## 17.3 Keine Ablenkung

Das Dashboard soll weniger ablenken als ein Browser mit offenen News-, Kalender- und Task-Tabs.

## 17.4 Sauberes Verhalten bei Monitorwechsel

Abdocken und Andocken müssen ohne manuelle Reparatur funktionieren.

## 17.5 Keine Störung des Windows-Desktops

Papierkorb, Desktop-Icons und normale Fensterarbeit müssen weiterhin funktionieren.

---

# 18. Mögliche Versionierung

## 18.1 V0.1 Technical Shell

Ziel: technische Grundlage.

Umfang:

- WPF-/Windows-App-Grundgerüst,
- Dashboard-Fenster,
- Dummy-Karten,
- Basislayout,
- einfache Konfiguration,
- Monitorerkennung rudimentär,
- sichere Fensterposition,
- README und Dokumentation.

## 18.2 V0.2 Dashboard Layout

Ziel: Oberfläche nutzbar machen.

Umfang:

- Compact Mode,
- Dashboard Mode,
- Kartensystem,
- erste Einstellungen,
- Theme-Grundlagen,
- manuelles Aktualisieren.

## 18.3 V0.3 Wetter und Systemstatus

Ziel: erste echte Daten.

Umfang:

- Wetterdaten,
- Cache,
- Systemstatus,
- Fehleranzeige,
- Aktualisierungsintervalle.

## 18.4 V0.4 Aufgaben und Kalender

Ziel: Tagesplanung.

Umfang:

- lokale Aufgabenquelle,
- nächste Aufgaben,
- Tagesfokus,
- einfache Kalenderquelle oder Dummy/ICS,
- Privacy-Anonymisierung.

## 18.5 V0.5 Nachrichten und Tray

Ziel: täglicher Betrieb.

Umfang:

- RSS-News,
- Tray-Icon,
- Moduswechsel,
- Autostart optional.

## 18.6 V1.0 Personal Dashboard

Ziel: erste stabile persönliche Version.

Umfang:

- Wetter,
- Aufgaben,
- Kalender,
- Nachrichten,
- Systemstatus,
- Privacy Mode,
- Monitorprofile,
- stabile Konfiguration,
- sinnvolle Dokumentation.

## 18.7 Spätere Versionen

Mögliche Erweiterungen:

- GitHub-Integration,
- TaskHost-Integration,
- mehrere Fenster,
- Plugin-System,
- Glass-/Ambient-Design,
- serverseitige Statusabfragen,
- Team-/SASD-Modus,
- öffentliche Produktveröffentlichung.

---

# 19. Risiken und offene Punkte

## 19.1 Risiko: Zu viele Funktionen auf einmal

Das Dashboard kann schnell zu groß werden.

Gegenmaßnahme:

- klare Versionierung,
- V0.1 klein halten,
- echte Datenquellen schrittweise ergänzen.

## 19.2 Risiko: Ablenkende Oberfläche

Ein Dashboard kann ablenken, wenn zu viele Daten angezeigt werden.

Gegenmaßnahme:

- Fokusmodus,
- begrenzte News,
- ruhiges Design,
- Karten deaktivierbar.

## 19.3 Risiko: Datenschutz

Aufgaben und Termine können sensible Informationen enthalten.

Gegenmaßnahme:

- Privacy Mode,
- keine Kundendaten im Standard,
- lokale Konfiguration,
- keine sensiblen Logs.

## 19.4 Risiko: Performance

Live-Dashboards können zu viel CPU, Speicher oder Netzwerk verbrauchen.

Gegenmaßnahme:

- sparsame Timer,
- Caching,
- keine Daueranimationen,
- reduzierte Updates bei Akkubetrieb.

## 19.5 Risiko: Multi-Monitor-Komplexität

Unterschiedliche Monitore, Auflösungen und DPI-Werte sind komplex.

Gegenmaßnahme:

- Monitorprofile,
- sichere Fallbacks,
- responsive Layouts,
- Fenster nie außerhalb sichtbarer Bereiche öffnen.

## 19.6 Risiko: Desktop-Integration wird zu kompliziert

Der Wunsch nach Integration in den Desktop kann zu Shell-Hacks führen.

Gegenmaßnahme:

- Windows-Desktop nicht ersetzen,
- Explorer zuständig lassen,
- Dashboard als eigenes Fenster/Sidebar/Wallboard starten.

---

# 20. Offene fachliche Entscheidungen

Diese Punkte müssen später entschieden werden:

1. Soll V1 eine reine WPF-App werden oder WPF mit WebView2?
2. Welche Wetterdatenquelle wird zuerst genutzt?
3. Welche Aufgabenquelle wird zuerst genutzt?
4. Wird der Kalender zuerst lokal, per ICS oder gar nicht echt integriert?
5. Welche Nachrichtenquellen sind Standard?
6. Soll das Repository dauerhaft öffentlich bleiben?
7. Welche Lizenz soll verwendet werden?
8. Wie stark soll TaskHost integriert werden?
9. Soll es langfristig ein SASD-Produkt oder persönliches Werkzeug bleiben?
10. Soll Transparenz/Ambient-Design schon vor V1 umgesetzt werden?

---

# 21. Abnahmekriterien für das Lastenheft

Dieses Lastenheft gilt als fachlich brauchbar, wenn:

- die Grundidee des Dashboards verständlich beschrieben ist,
- Laptop-, Zwei-Monitor- und Drei-Monitor-Szenarien berücksichtigt sind,
- Desktop-Icons und Papierkorb korrekt als Windows-Funktionen behandelt werden,
- Wetter, Aufgaben, Kalender, Nachrichten, Systemstatus und SASD-Projekte beschrieben sind,
- Datenschutz und Privacy Mode enthalten sind,
- Performance und Akkubetrieb berücksichtigt sind,
- Offline- und Cache-Verhalten beschrieben sind,
- spätere Erweiterungen nicht vergessen wurden,
- V0.1 nicht mit V1 verwechselt wird,
- das Dokument als Grundlage für ein Pflichtenheft dienen kann.

---

# 22. Zusammenfassung

Das SASD Personal Desktop Dashboard soll den Windows-Desktop zu einem produktiven, ruhigen und datenschutzbewussten Arbeitsleitstand erweitern.

Der Kernnutzen besteht darin, wichtige Informationen ohne Suche in vielen Programmen sichtbar zu machen:

- Was ist jetzt wichtig?
- Was steht als Nächstes an?
- Wie ist das Wetter?
- Welche Aufgaben warten?
- Gibt es Termine?
- Gibt es wichtige Nachrichten?
- Ist mein System in Ordnung?
- Welche SASD-Projekte brauchen Aufmerksamkeit?

Das Dashboard soll besonders für wechselnde Arbeitsumgebungen geeignet sein: Laptop allein, zweiter Monitor, dritter Monitor, Dockingstation, Präsentation und Offline-Situationen.

Die erste technische Umsetzung soll klein und robust beginnen.  
Der fachliche Zielumfang bleibt aber bewusst breiter, damit spätere Versionen sauber geplant werden können.
