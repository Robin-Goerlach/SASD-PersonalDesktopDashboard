# SASD Personal Desktop Dashboard – UI Concept

**Projekt:** SASD Personal Desktop Dashboard  
**Repository:** `SASD-PersonalDesktopDashboard`  
**Dokument:** 040_UI_Concept.md  
**Dokumenttyp:** UI-Konzept / Bedien- und Darstellungskonzept  
**Version:** 0.1  
**Status:** Entwurf für V0.1 / V1  
**Datum:** 2026-05-13  
**Autor:** Robin Goerlach / SASD-GmbH – Scientific and Software Development  
**Sprache:** Deutsch  

---

## 1. Zweck dieses Dokuments

Dieses Dokument beschreibt das Bedien- und Darstellungskonzept des **SASD Personal Desktop Dashboard**.

Das Dashboard soll nicht nur schön aussehen, sondern den Windows-Desktop sinnvoll ergänzen. Es soll dem Nutzer helfen, beim Start des Rechners und während der Arbeit wichtige Informationen sofort zu erfassen:

- Wie ist das Wetter in den nächsten Stunden?
- Welche Aufgaben sind als Nächstes wichtig?
- Welche Termine stehen an?
- Gibt es wichtige Nachrichten?
- Wie ist der Zustand des Rechners?
- Welche SASD-Projekte brauchen Aufmerksamkeit?

Das UI-Konzept beschreibt die verschiedenen Anzeigearten, Karten, Layoutregeln, Farben, Interaktionsprinzipien, Datenschutzmodi und das Verhalten bei unterschiedlichen Monitorgrößen.

---

## 2. UI-Leitbild

Das Dashboard soll wirken wie ein ruhiger, professioneller Arbeitsleitstand.

Nicht gewünscht:

- grelle Gaming-Optik,
- hektische Animationen,
- Nachrichten-Ticker mit Dauerbewegung,
- überladene Kachelwände,
- zu viel Transparenz auf Kosten der Lesbarkeit,
- Ersatz des normalen Windows-Desktops.

Gewünscht:

- ruhig,
- modern,
- technisch,
- gut lesbar,
- dunkel mit Petrol-/Blau-Akzenten,
- aufgeräumt,
- produktivitätsorientiert,
- privacy-bewusst,
- für Multi-Monitor-Arbeitsplätze geeignet.

Arbeitstitel für den Stil:

```text
SASD Dark Glass Dashboard
```

---

## 3. Designprinzipien

### 3.1 Information vor Dekoration

Das Dashboard darf gut aussehen, aber Schönheit ist nicht Selbstzweck. Jede Karte soll eine klare Aufgabe erfüllen.

Beispiele:

- Wetterkarte: Soll sagen, ob in den nächsten Stunden Regen droht.
- Aufgabenkarte: Soll sagen, was als Nächstes wichtig ist.
- Systemstatuskarte: Soll warnen, wenn Akku, Speicher oder Netzwerk problematisch sind.
- Nachrichtenkarte: Soll Orientierung geben, nicht ablenken.

### 3.2 Ruhige Informationsdichte

Das Dashboard soll nicht den ganzen Tag Aufmerksamkeit erzwingen.

Deshalb:

- wenige, gut gewählte Karten,
- keine dauernd blinkenden Hinweise,
- keine unnötigen Animationen,
- klare Prioritäten,
- große Abstände,
- gute Typografie.

### 3.3 Desktop-Ergänzung statt Desktop-Ersatz

Windows-Desktop, Papierkorb, Verknüpfungen und Explorer-Kontextmenüs bleiben unverändert. Das Dashboard ist eine zusätzliche App, die in verschiedenen Modi sichtbar oder unsichtbar sein kann.

### 3.4 Privacy by UI Design

Sensible Informationen müssen schnell ausblendbar sein.

Dazu gehören:

- Aufgabeninhalte,
- Kalendereinträge,
- Projektdetails,
- ggf. Nachrichtenquellen,
- Systeminformationen bei Bildschirmfreigabe.

Privacy Mode und Präsentationsmodus sind daher feste UI-Konzepte.

### 3.5 Multi-Monitor-First

Das Dashboard wird so gedacht, dass es auf einem zweiten oder dritten Monitor besonders nützlich ist. Auf einem einzelnen Laptop-Display muss es sich zurücknehmen.

---

## 4. Anzeigearten

### 4.1 Dashboard Mode

Der Dashboard Mode ist die normale große Ansicht für einen externen Monitor.

Einsatz:

- Schreibtisch mit zwei oder drei Monitoren,
- Dockingstation,
- dauerhafte Übersicht während der Arbeit,
- optional Vollbild auf Nebenmonitor.

Darstellung:

```text
┌────────────────────────────────────────────────────────────┐
│ Header: Datum, Uhrzeit, Modus, Status                      │
├──────────────┬──────────────┬──────────────┬───────────────┤
│ Wetter       │ Aufgaben     │ Kalender     │ Systemstatus  │
├──────────────┴──────────────┬──────────────┴───────────────┤
│ Nachrichten                  │ SASD-Projekte                │
├──────────────────────────────┴──────────────────────────────┤
│ Tagesfokus / Hinweise / Diagnose                            │
└──────────────────────────────────────────────────────────────┘
```

Eigenschaften:

- mehrere Spalten,
- klare Karten,
- große Schrift für wichtige Werte,
- ruhige Statuszeile,
- geeignet für 1920x1080, 2560x1440 und 4K.

### 4.2 Compact Mode

Der Compact Mode ist für den Laptopbetrieb.

Einsatz:

- nur ein Display vorhanden,
- unterwegs,
- wenig Platz,
- Dashboard soll nicht stören.

Darstellung:

```text
┌─────────────────────┐
│ Uhr / Datum         │
├─────────────────────┤
│ Wetter kurz         │
├─────────────────────┤
│ Nächste Aufgabe     │
├─────────────────────┤
│ Nächster Termin     │
├─────────────────────┤
│ Status / Warnungen  │
└─────────────────────┘
```

Eigenschaften:

- einspaltig,
- schmale Sidebar,
- einklappbar,
- wenige Informationen,
- keine großen Newslisten,
- reduzierte Aktualisierung im Akku-Modus.

### 4.3 Focus Mode

Der Focus Mode zeigt nur das, was für die nächsten Stunden entscheidend ist.

Einsatz:

- konzentriertes Arbeiten,
- Programmieren,
- Schreiben,
- Meetings vorbereiten,
- Ablenkung reduzieren.

Inhalte:

- aktueller Tagesfokus,
- nächste wichtige Aufgabe,
- nächster Termin,
- Wetterwarnung nur bei Relevanz,
- Systemwarnung nur bei Relevanz.

Nicht anzeigen:

- allgemeine Nachrichten,
- lange Aufgabenlisten,
- Projektlisten,
- dekorative Elemente.

### 4.4 Privacy Mode

Der Privacy Mode anonymisiert oder verbirgt sensible Informationen.

Einsatz:

- Bildschirmfreigabe,
- Kundenbesuch,
- Schulung,
- spontane Einsicht durch Dritte,
- Arbeiten in öffentlicher Umgebung.

Beispiele:

```text
Normal:
"Angebot für Kunde Müller finalisieren"

Privacy Mode:
"Geschäftliche Aufgabe"
```

```text
Normal:
"Arzttermin 14:30"

Privacy Mode:
"Privater Termin 14:30"
```

Privacy Mode soll schnell erreichbar sein:

- Tray-Menü,
- Tastenkürzel,
- Button im Header.

### 4.5 Presentation Mode

Presentation Mode ist eine strengere Variante des Privacy Mode.

Einsatz:

- Online-Meeting mit Screensharing,
- Präsentationen,
- Kundendemos,
- Videoaufzeichnung.

Verhalten:

- persönliche Aufgaben ausblenden,
- Kalenderdetails ausblenden,
- Projektinterna ausblenden,
- Benachrichtigungen unterdrücken,
- optional nur Uhr/Wetter/Systemstatus anzeigen.

### 4.6 Silent Mode

Silent Mode bedeutet: Dashboard läuft, ist aber nicht sichtbar.

Einsatz:

- wenn kein Platz ist,
- während Präsentationen,
- bei schwacher Performance,
- wenn nur Tray-Zugriff gewünscht ist.

Zugriff:

- Tray-Icon,
- Hotkey,
- Startmenü.

---

## 5. Kartenkonzept

### 5.1 Allgemeines Kartenlayout

Jede Karte soll ähnlich aufgebaut sein:

```text
┌───────────────────────────────┐
│ Titel               Status    │
├───────────────────────────────┤
│ Hauptinformation              │
│ Detailinformationen           │
│ optionale Liste / Mini-Chart   │
├───────────────────────────────┤
│ letzte Aktualisierung / Aktion│
└───────────────────────────────┘
```

Bestandteile:

- Titel
- optionales Icon
- Statusindikator
- Hauptwert
- Details
- Aktualisierungszeit
- optionaler Aktionsbutton

### 5.2 Kartenprioritäten

Nicht alle Karten sind gleich wichtig.

Prioritäten:

```text
High:
- Tagesfokus
- Aufgaben
- Kalender
- Warnungen

Medium:
- Wetter
- Systemstatus
- SASD-Projekte

Low:
- News
- zusätzliche Statistiken
- dekorative Informationen
```

Bei wenig Platz werden Karten niedriger Priorität ausgeblendet oder gekürzt.

### 5.3 Kartenzustände

Jede Karte kann folgende Zustände haben:

```text
Ready
Loading
Warning
Error
Offline
Disabled
NotConfigured
PrivacyHidden
```

Die Anzeige soll ruhig bleiben. Fehler werden nicht als aggressive Popups angezeigt.

---

## 6. Vorgesehene Karten

### 6.1 Header-Karte / Kopfbereich

Der Header zeigt den globalen Zustand des Dashboards.

Inhalte:

- Datum
- Uhrzeit
- aktiver Modus
- aktives Monitorprofil
- Netzwerk-/Online-Status
- Privacy Mode Status
- kurzer App-Status

Beispiel:

```text
Mittwoch, 13. Mai 2026 · 20:15 · Dashboard Mode · Büroprofil · Online
```

### 6.2 Wetterkarte

Zweck:

Die Wetterkarte soll schnell beantworten, ob das Wetter in den nächsten Stunden für den Nutzer relevant wird.

V0.1:

- Dummy-Daten
- Layout vorbereiten

V1-Inhalte:

- aktuelle Temperatur
- Wetterzustand
- Regenwahrscheinlichkeit
- Wind
- Vorhersage für 6 bis 12 Stunden
- letzte Aktualisierung
- Warnhinweis bei relevanten Wetterereignissen

Darstellungsidee:

```text
Wetter
18 °C · bewölkt
Regen ab 22:00 möglich
Wind: 18 km/h
Nächste Stunden: 18° 17° 16° 15°
```

Im Compact Mode:

```text
18 °C · Regen ab 22:00
```

### 6.3 Aufgabenkarte

Zweck:

Die Aufgabenkarte zeigt die nächsten wirklich wichtigen Aufgaben, nicht die gesamte Aufgabenwelt.

V0.1:

- drei Dummy-Aufgaben

V1-Inhalte:

- Top 3 Aufgaben
- heute fällig
- überfällig
- Priorität
- Status erledigt/offen
- optional Projektzuordnung

Darstellung:

```text
Aufgaben
1. Pflichtenheft prüfen                 Heute
2. Repository-Struktur anlegen          Heute
3. UI-Konzept überarbeiten              Morgen
```

Privacy Mode:

```text
Aufgaben
1. Geschäftliche Aufgabe
2. Projektaufgabe
3. Private Aufgabe
```

### 6.4 Kalenderkarte

Zweck:

Die Kalenderkarte zeigt den nächsten relevanten Termin und optional die nächsten Termine des Tages.

V0.1:

- Dummy-Termine

V1-Inhalte:

- nächster Termin
- Uhrzeit
- Dauer
- Ort/Online-Hinweis
- Tagesübersicht
- Konflikthinweise optional

Darstellung:

```text
Kalender
Nächster Termin: 14:30 – Projektbesprechung
Danach: 16:00 – Dokumentation
```

Privacy Mode:

```text
Kalender
14:30 – Geschäftlicher Termin
16:00 – Termin
```

### 6.5 Nachrichtenkarte

Zweck:

Die Nachrichtenkarte soll Orientierung geben, nicht ablenken.

Kategorien:

- Lokal
- Welt
- IT/Security
- Wissenschaft

Darstellung:

```text
Nachrichten
[Lokal] Überschrift der wichtigsten lokalen Meldung
[IT] Kritische Sicherheitsmeldung
[Wissenschaft] Forschungsnews
```

Regeln:

- keine Auto-Scroll-Zwangsanimation,
- maximal wenige Zeilen,
- Quelle und Zeit sichtbar,
- keine reißerische Darstellung,
- keine Dauerbenachrichtigung für normale News.

### 6.6 Systemstatuskarte

Zweck:

Die Systemstatuskarte zeigt den Zustand des lokalen Rechners.

Inhalte:

- Akku/Netzbetrieb
- CPU grob
- RAM grob
- freier Speicherplatz
- Netzwerkstatus
- optional Warnungen

Darstellung:

```text
System
Netzbetrieb · Akku 92 %
CPU 8 % · RAM 46 %
C: 128 GB frei
Netzwerk: verbunden
```

Im Compact Mode nur Warnungen und Kernwerte.

### 6.7 SASD-Projektkarte

Zweck:

Die SASD-Projektkarte zeigt, welche eigenen Projekte Aufmerksamkeit brauchen.

V0.1:

- Beispielprojekte

V1:

- lokale Projektliste
- Status
- nächste Aktion
- Link/Repository-Referenz

Darstellung:

```text
SASD Projekte
TaskHost Local                 UI-Aktionsstruktur
Personal Desktop Dashboard     V0.1-Shell
LogSink                        Doku/Lehrplan
```

Später:

- GitHub-Status,
- offene Issues,
- letzter Commit,
- Release-Hinweise.

### 6.8 Tagesfokus-Karte

Zweck:

Diese Karte hilft, nicht in vielen Projekten zu versinken.

Inhalte:

- heutiger Hauptfokus
- sekundärer Fokus
- eine Notiz
- optional „Nicht heute“-Hinweis

Darstellung:

```text
Tagesfokus
Heute: Dashboard-Grundstruktur sauber aufsetzen
Nicht verzetteln: Wetter-API erst später
```

### 6.9 Diagnose-/Statuskarte

Zweck:

Zeigt technische Hinweise zur App selbst.

Inhalte:

- aktive Version
- letzte Datenaktualisierung
- fehlerhafte Module
- aktives Profil
- Cache-Status

Diese Karte ist in V0.1 für Entwicklung hilfreich, in V1 optional ausblendbar.

---

## 7. Layoutverhalten nach Bildschirmgröße

### 7.1 Laptop / kleine Breite

Bei schmalem Bildschirm:

- eine Spalte,
- Compact Mode bevorzugen,
- News ausblenden oder stark kürzen,
- Systemstatus nur kompakt,
- keine großen Charts.

### 7.2 Full-HD-Monitor

Bei 1920x1080:

- zwei bis drei Spalten,
- Wetter, Aufgaben, Kalender oben,
- News und Projekte darunter,
- Header kompakt.

### 7.3 WQHD-Monitor

Bei 2560x1440:

- drei bis vier Spalten möglich,
- Karten großzügiger,
- mehr Details pro Karte,
- gute Lesbarkeit aus etwas Entfernung.

### 7.4 4K-Monitor

Bei 4K:

- größere Schrift statt einfach mehr Informationen,
- maximale Lesbarkeit,
- keine winzigen Karten,
- optional Wallboard-Layout.

---

## 8. Multi-Monitor-UX

### 8.1 Laptop allein

Automatisches Verhalten:

- Compact Mode,
- reduzierte Karten,
- keine Überdeckung wichtiger Arbeitsfläche,
- optional Start minimiert im Tray.

### 8.2 Zwei Monitore

Automatisches Verhalten:

- Dashboard auf zweitem Monitor, falls konfiguriert,
- Hauptmonitor bleibt für Arbeit frei,
- Fensterposition wird gespeichert.

### 8.3 Drei Monitore

V0.1/V1:

- ein Dashboard-Fenster auf bevorzugtem Monitor.

Später:

- mehrere Dashboard-Fenster,
- getrennte Kartenbereiche,
- ein Monitor für News/System,
- ein Monitor für Aufgaben/Projekte.

### 8.4 Abdocken

Wenn ein externer Monitor entfernt wird:

- Dashboard wird nicht unsichtbar,
- Fenster wechselt auf Hauptmonitor,
- Compact Mode kann aktiviert werden,
- Hinweis im Statusbereich.

### 8.5 Andocken

Wenn externe Monitore wieder verfügbar sind:

- passendes Monitorprofil auswählen,
- Dashboard wieder auf bevorzugten Monitor setzen,
- kein aggressives Verschieben laufender Arbeitsfenster.

---

## 9. Farb- und Stilkonzept

### 9.1 Grundfarben

Empfohlene Stilrichtung:

```text
Hintergrund: sehr dunkles Blau/Anthrazit
Karten: dunkles Blau-Grau
Akzent: Petrol / Cyan-Blau
Text primär: nahezu Weiß
Text sekundär: helles Grau
Warnung: warmes Gelb/Orange
Fehler: dezentes Rot
Erfolg: ruhiges Grün
```

Konkrete Farbcodes können später in `Themes/Colors.xaml` festgelegt werden.

### 9.2 Transparenz

Transparenz soll optional sein.

Regeln:

- nicht auf Kosten der Lesbarkeit,
- Textflächen ausreichend kontrastreich,
- keine vollständige Durchsichtigkeit,
- Blur/Frosted-Glass-Effekt nur wenn Performance akzeptabel,
- abschaltbar.

### 9.3 Dark Glass Style

Empfohlene Darstellung:

- leicht transparente Karten,
- dezenter Schatten,
- runde Ecken,
- klare Trennlinien,
- ruhige Akzentlinien,
- Hintergrundbild optional abgedunkelt.

### 9.4 Hintergrundbild

Ein Hintergrundbild kann optional sichtbar bleiben, wenn:

- es nicht vom Inhalt ablenkt,
- die Karten ausreichend deckend sind,
- der Text lesbar bleibt,
- der Nutzer es deaktivieren kann.

Für V0.1 ist kein eigener Wallpaper-Mechanismus nötig.

---

## 10. Typografie

### 10.1 Grundregeln

- klare Sans-Serif-Schrift,
- gute Lesbarkeit,
- keine verspielten Fonts,
- starke Werte größer darstellen,
- Detailtexte kleiner, aber nicht winzig.

### 10.2 Größenhierarchie

Beispiel:

```text
Dashboard-Titel:      groß
Karten-Hauptwert:     sehr groß
Kartentitel:          mittel
Listeninhalt:         normal
Metadaten:            klein, aber lesbar
```

### 10.3 Textlängen

Lange Texte sollen nicht das Layout sprengen.

Regeln:

- Überschriften kürzen,
- Tooltips oder Detailansicht später,
- Textumbruch nur wenn sinnvoll,
- Ellipsis bei begrenzten Karten.

---

## 11. Icons

### 11.1 Stil

Icons sollen:

- schlicht,
- einfarbig oder zweifarbig,
- gut erkennbar,
- nicht verspielt,
- passend zu SASD.

### 11.2 Icon-Bereiche

Benötigte Icons:

- App-Icon
- Wetter
- Aufgaben
- Kalender
- Nachrichten
- Systemstatus
- Projekte
- Privacy Mode
- Einstellungen
- Fehler/Warnung
- Online/Offline
- Akku/Netz

### 11.3 Desktop-Icons

Das Dashboard übernimmt nicht die Windows-Desktop-Icons. Es darf später optional Schnellstarter-Karten bieten, aber die Windows-Shell bleibt zuständig.

---

## 12. Interaktionskonzept

### 12.1 Tray-Menü

Das Tray-Menü ist der zentrale Schnellzugriff.

Einträge:

```text
Dashboard anzeigen
Compact Mode anzeigen
Focus Mode anzeigen
Privacy Mode ein/aus
Presentation Mode ein/aus
Aktualisieren
Einstellungen
Diagnose
Beenden
```

### 12.2 Header-Aktionen

Im Dashboard-Header:

- Modus anzeigen,
- Privacy Mode umschalten,
- manuell aktualisieren,
- Einstellungen öffnen,
- Fenster minimieren.

### 12.3 Kartenaktionen

Jede Karte kann optionale Aktionen haben:

- Aktualisieren,
- Details öffnen,
- Quelle öffnen,
- Modul deaktivieren,
- Einstellungen für Modul.

V0.1 muss nicht alle Aktionen implementieren, aber das Layout soll sie berücksichtigen.

### 12.4 Hotkeys

Geplante Hotkeys:

```text
Dashboard anzeigen/verbergen
Privacy Mode ein/aus
Presentation Mode ein/aus
Focus Mode
```

Hotkeys müssen konfigurierbar oder zumindest abschaltbar sein, damit sie nicht mit anderen Programmen kollidieren.

---

## 13. Einstellungen

### 13.1 V0.1

V0.1 kann Einstellungen zunächst über JSON-Datei vorbereiten. Eine vollständige Einstellungsoberfläche ist nicht zwingend.

### 13.2 V1

V1 sollte eine einfache Einstellungsansicht bieten:

- Anzeigeart
- Monitorprofil
- Theme
- Module aktivieren/deaktivieren
- Aktualisierungsintervalle
- Privacy Mode
- News-Quellen
- Wetterort
- Autostart
- Diagnose

### 13.3 Einstellungs-UX

Einstellungen sollen verständlich sein und nicht zu technisch wirken.

Beispiel:

Nicht nur:

```text
refreshInterval = 1800
```

sondern:

```text
Wetter alle 30 Minuten aktualisieren
```

---

## 14. Benachrichtigungen

Das Dashboard soll nicht zu einem störenden Benachrichtigungssystem werden.

V0.1:

- keine aggressiven Popups nötig.

V1:

- dezente Hinweise innerhalb der Karten.

Später:

- Windows-Notifications optional,
- nur bei wichtigen Ereignissen:
  - Akku kritisch,
  - Wetterwarnung,
  - Termin bald,
  - wichtige Aufgabe überfällig,
  - Systemproblem.

---

## 15. Accessibility und Bedienbarkeit

Das UI soll auch bei längerer Nutzung angenehm bleiben.

Anforderungen:

- ausreichender Kontrast,
- skalierbare Texte,
- keine alleinige Farbcodierung,
- klare Zustände,
- Tastaturbedienung für wichtige Funktionen,
- keine schnellen Blinkeffekte,
- keine unleserlichen Transparenzen.

---

## 16. V0.1 UI-Umfang

V0.1 soll zeigen, wie sich das Produkt anfühlt, ohne schon alle Datenquellen zu integrieren.

Muss:

- WPF-Hauptfenster,
- Dashboard-Layout,
- Dummy-Karten für Wetter, Aufgaben, Kalender, News, Systemstatus, SASD-Projekte,
- Header,
- Grundtheme dunkel,
- responsive Kartenanordnung grob,
- Privacy Mode als sichtbarer Zustand,
- Diagnose-/Statushinweis.

Soll:

- Compact Mode als einfache Sidebar,
- Tray-Icon-Grundlage,
- einfacher Settings-Platzhalter,
- Mock-Daten zentral verwaltet.

Kann:

- leichte Transparenz,
- Dark-Glass-Effekt,
- Beispiel-Hintergrundbild im Mockup-Ordner,
- erste Icons.

Nicht:

- echte Wetter-API,
- echte Newsfeeds,
- echte Kalenderanbindung,
- echte TaskHost-Anbindung,
- eigene Desktop-Icon-Verwaltung,
- Wallpaper-Hack.

---

## 17. V1 UI-Umfang

V1 soll produktiv nutzbar sein.

Muss:

- echte oder lokal konfigurierbare Wetterdaten,
- echte lokale Aufgabenquelle,
- RSS-Nachrichten,
- Systemstatus,
- Monitorprofile,
- Fensterposition speichern,
- Privacy Mode,
- Tray-Steuerung,
- App-Konfiguration,
- Cache-/Offline-Hinweise.

Soll:

- Einstellungen-UI,
- Focus Mode,
- Presentation Mode,
- Theme-Auswahl,
- Autostart,
- Diagnoseansicht.

Kann:

- Glass Theme,
- Hintergrundbild-Unterstützung,
- einfache Projektstatusintegration,
- lokale Schnellstarter.

---

## 18. Beispiel: Dashboard Mode Layout

```text
┌──────────────────────────────────────────────────────────────────────────┐
│ SASD Dashboard       Mi, 13.05.2026 20:15      Büroprofil · Online       │
├────────────────────┬────────────────────┬──────────────────────────────┤
│ Wetter             │ Aufgaben           │ Kalender                     │
│ 18 °C              │ 3 wichtige Tasks   │ 14:30 Projektbesprechung     │
│ Regen ab 22:00     │ 1 überfällig       │ 16:00 Dokumentation          │
├────────────────────┼────────────────────┼──────────────────────────────┤
│ System             │ SASD Projekte      │ Tagesfokus                  │
│ CPU 8 % RAM 46 %   │ TaskHost           │ V0.1 sauber aufsetzen        │
│ Akku 92 % Netz     │ Dashboard          │ Keine Wetter-API heute       │
├────────────────────┴────────────────────┴──────────────────────────────┤
│ Nachrichten: Lokal · IT/Security · Wissenschaft                         │
└──────────────────────────────────────────────────────────────────────────┘
```

---

## 19. Beispiel: Compact Mode Layout

```text
┌────────────────────────────┐
│ Mi 13.05 · 20:15           │
├────────────────────────────┤
│ Wetter: 18 °C · Regen 22h  │
├────────────────────────────┤
│ Fokus: V0.1-Struktur       │
├────────────────────────────┤
│ Nächste Aufgabe            │
│ Pflichtenheft prüfen       │
├────────────────────────────┤
│ Nächster Termin            │
│ 14:30 Projektbesprechung   │
├────────────────────────────┤
│ System: Netz · RAM 46 %    │
└────────────────────────────┘
```

---

## 20. Beispiel: Privacy Mode

```text
┌────────────────────────────────────────────────────────────┐
│ SASD Dashboard · Privacy Mode aktiv                        │
├────────────────────┬────────────────────┬────────────────┤
│ Wetter             │ Aufgaben           │ Kalender       │
│ 18 °C              │ Geschäftliche Aufg.│ Termin 14:30   │
│ Regen ab 22:00     │ Private Aufgabe    │ Termin 16:00   │
├────────────────────┴────────────────────┴────────────────┤
│ Systemstatus sichtbar · Projektinterna ausgeblendet        │
└────────────────────────────────────────────────────────────┘
```

---

## 21. Abgrenzungen

Das UI-Konzept beschreibt nicht:

- finale API-Datenquellen,
- vollständige Datenbankstruktur,
- finale Implementierung aller Module,
- endgültiges Branding,
- vollständiges Benutzerhandbuch,
- Installer-/Deployment-Konzept.

Es legt aber fest, wie die Oberfläche grundsätzlich funktionieren soll, damit die spätere Implementierung konsistent bleibt.

---

## 22. Fazit

Das UI des SASD Personal Desktop Dashboard soll einen ruhigen, professionellen und nützlichen Arbeitsleitstand bieten. Entscheidend ist nicht ein spektakulärer Desktop-Effekt, sondern eine klare, gut lesbare und zuverlässige Anzeige wichtiger Informationen.

Für V0.1 reicht eine überzeugende technische UI-Shell mit Dummy-Daten. Der Wert entsteht dadurch, dass die Anzeigearten, Karten, Privacy-Regeln und Multi-Monitor-Logik von Anfang an sauber vorbereitet werden.
