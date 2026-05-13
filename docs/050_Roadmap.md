# SASD Personal Desktop Dashboard – Roadmap

**Projekt:** SASD Personal Desktop Dashboard  
**Repository:** `SASD-PersonalDesktopDashboard`  
**Dokument:** 050_Roadmap.md  
**Dokumenttyp:** Roadmap / Entwicklungsplan  
**Version:** 0.1  
**Status:** Entwurf  
**Datum:** 2026-05-13  
**Autor:** Robin Goerlach / SASD-GmbH – Scientific and Software Development  
**Sprache:** Deutsch  

---

## 1. Zweck dieses Dokuments

Dieses Dokument beschreibt die geplante Entwicklung des **SASD Personal Desktop Dashboard** in sinnvollen Schritten.

Die Roadmap soll verhindern, dass zu früh zu viele Funktionen eingebaut werden. Das Projekt soll schnell zu einer lauffähigen Anwendung kommen, aber ohne die Architektur zu beschädigen.

Zentrale Idee:

```text
Erst eine stabile Desktop-Dashboard-Shell.
Dann echte Datenmodule.
Dann Komfort, Datenschutz, Polishing und Integrationen.
```

---

## 2. Entwicklungsprinzipien

### 2.1 Kleine, nutzbare Schritte

Jede Version soll ein klar prüfbares Ergebnis liefern.

Nicht sinnvoll:

- monatelang nur planen,
- sofort alle APIs anbinden,
- UI, Datenquellen, Installer und Plugin-System gleichzeitig bauen.

Sinnvoll:

- erst Fenster, Layout und Struktur,
- dann Dummy-Daten,
- dann ein echtes Modul nach dem anderen,
- danach Komfortfunktionen.

### 2.2 Architektur nicht opfern

V0.1 darf einfach sein, aber nicht chaotisch. Es ist besser, Dummy-Daten sauber in Module zu kapseln, als echte Wetterdaten schnell und schmutzig direkt in ein Window zu schreiben.

### 2.3 Kein Feature ohne Nutzen

Jede Funktion muss beantworten:

- Hilft sie im Alltag?
- Verbessert sie Orientierung?
- Spart sie Aufmerksamkeit?
- Ist sie datenschutzverträglich?
- Passt sie zur Performance?

### 2.4 Desktop nicht ersetzen

Auch spätere Versionen sollen den Windows-Desktop nicht aggressiv übernehmen. Das Dashboard bleibt eine ergänzende Anwendung.

---

## 3. Zielbild

Langfristig soll das Dashboard:

- auf einem Laptop als kompakte Sidebar funktionieren,
- an der Dockingstation automatisch zum Dashboard auf dem zweiten Monitor werden,
- bei drei Monitoren sauber positioniert werden,
- Aufgaben, Termine, Wetter, Nachrichten und Systemstatus anzeigen,
- persönliche Inhalte bei Bedarf anonymisieren,
- offline mit Cache weiter nutzbar bleiben,
- erweiterbar für SASD-Projektinformationen sein,
- ruhig und professionell aussehen.

---

## 4. Version V0.1 – Technical Shell

### 4.1 Ziel

V0.1 erzeugt die technische Grundstruktur und eine erste lauffähige WPF-Anwendung mit Dummy-Dashboard.

Diese Version ist noch nicht für produktive Daten gedacht. Sie soll beweisen, dass Architektur, Fenster, Layout und Projektstruktur funktionieren.

### 4.2 Muss-Funktionen

- .NET-8-Solution angelegt
- WPF-App-Projekt angelegt
- Core-Projekt angelegt
- Infrastructure-Projekt angelegt
- Modules-Projekt angelegt
- Test-Projekt angelegt
- Solution baut erfolgreich
- Hauptfenster startet
- Dashboard zeigt Dummy-Karten
- dunkles Grundtheme
- Headerbereich vorhanden
- Karten für:
  - Wetter
  - Aufgaben
  - Kalender
  - Nachrichten
  - Systemstatus
  - SASD-Projekte
- Privacy Mode als UI-Zustand vorbereitet
- einfache Konfigurationsmodelle vorbereitet
- grundlegende README aktualisiert

### 4.3 Soll-Funktionen

- einfache Monitorerkennung vorbereitet
- Fensterpositionierung vorbereitet
- erste Unit-Tests für Core-Modelle
- Diagnose-/Statuskarte
- Settings-Platzhalter
- zentrale Mock-Datenquelle

### 4.4 Kann-Funktionen

- einfache Tray-Icon-Grundlage
- Compact Mode als separates Fenster oder Layout
- erste Icons
- leichte Transparenz
- Screenshot im Repository

### 4.5 Nicht enthalten

- echte Wetterdaten
- echte Kalenderintegration
- echte Newsfeeds
- TaskHost-Anbindung
- Autostart
- Installer
- Plugin-System
- eigene Desktop-Icon-Verwaltung

### 4.6 Erfolgskriterien

V0.1 ist erfolgreich, wenn:

- `dotnet build` erfolgreich läuft,
- die App startet,
- ein erstes Dashboard sichtbar ist,
- die Projektstruktur sauber ist,
- Karten logisch getrennt sind,
- Dummy-Daten nicht hart überall verteilt sind,
- README erklärt, wie die App gestartet wird.

### 4.7 Möglicher Commit

```text
Initialize WPF dashboard shell
```

---

## 5. Version V0.2 – Layout, Themes und Anzeigearten

### 5.1 Ziel

V0.2 verbessert die UI-Struktur und bereitet die verschiedenen Anzeigearten vor.

### 5.2 Muss-Funktionen

- Dashboard Mode sauber gestaltet
- Compact Mode vorhanden
- Focus Mode vorbereitet
- Privacy Mode sichtbar umschaltbar
- zentrale Theme-Ressourcen
- Kartenlayout mit Mindestgrößen
- bessere Dummy-Daten
- responsive Spaltenlogik grob umgesetzt

### 5.3 Soll-Funktionen

- Tray-Menü mit Grundfunktionen
- Settings-Fenster als Platzhalter
- Diagnoseansicht
- Monitorprofil-Anzeige im Header
- Fensterposition speichern/laden vorbereitet

### 5.4 Kann-Funktionen

- Dark-Glass-Theme
- optionale Transparenz
- App-Icon
- erste Screenshots für README

### 5.5 Nicht enthalten

- noch keine zwingend echten externen Datenquellen
- noch keine komplexe Konfigurations-UI
- noch kein Installer

### 5.6 Erfolgskriterien

- Anzeige wirkt wie ein echtes Produkt,
- Compact Mode ist auf Laptop sinnvoll,
- Privacy Mode ist sichtbar verständlich,
- Layout bricht bei kleinerer Breite nicht sofort kaputt.

### 5.7 Möglicher Commit

```text
Add dashboard layout modes and base theme
```

---

## 6. Version V0.3 – Konfiguration, Monitorprofile und Fensterverhalten

### 6.1 Ziel

V0.3 konzentriert sich auf eines der wichtigsten Kernprobleme: Das Dashboard muss mit verschiedenen Monitorumgebungen umgehen.

### 6.2 Muss-Funktionen

- JSON-Konfiguration laden
- Defaults verwenden, wenn Konfiguration fehlt
- Konfigurationsfehler robust behandeln
- Monitore erkennen
- primären Monitor erkennen
- Fensterposition speichern
- Fensterposition sicher wiederherstellen
- Fallback auf sichtbaren Monitor
- Displayprofil-Modell implementieren

### 6.3 Soll-Funktionen

- Profil „Laptop unterwegs“
- Profil „Büro/Dockingstation“
- Profilwahl im Header anzeigen
- einfacher Konfigurationspfad unter AppData
- Unit-Tests für Profilmatching

### 6.4 Kann-Funktionen

- Settings-UI zum Anzeigen der aktiven Konfiguration
- manuelle Profilauswahl
- Abdocken/Andocken während Laufzeit erkennen

### 6.5 Nicht enthalten

- komplexer Profil-Editor
- Cloud-Sync der Einstellungen
- mehrere Dashboard-Fenster gleichzeitig

### 6.6 Erfolgskriterien

- App startet nach Monitorwechsel sichtbar,
- Fenster landet nicht außerhalb des Bildschirms,
- Laptop-Szenario und Zwei-Monitor-Szenario sind unterscheidbar,
- Konfigurationsfehler zerstören nicht den Start.

### 6.7 Möglicher Commit

```text
Add display profile and safe window placement handling
```

---

## 7. Version V0.4 – Datenmodul-Grundlage und Caching

### 7.1 Ziel

V0.4 schafft die Grundlage für echte Datenmodule.

### 7.2 Muss-Funktionen

- einheitliche Modul-Schnittstelle
- Modulstatus:
  - Ready
  - Loading
  - Error
  - Offline
  - Disabled
  - NotConfigured
- RefreshPolicy
- Cache-Interface
- Modul-Refresh asynchron
- Fehlerisolierung pro Modul
- letzte Aktualisierung pro Karte anzeigen

### 7.3 Soll-Funktionen

- Datei-Cache für Modul-Snapshots
- manuelles Aktualisieren
- sichtbarkeitsabhängige Aktualisierung vorbereitet
- Eco-/Normal-Refreshprofile vorbereitet
- Unit-Tests für RefreshPolicy

### 7.4 Kann-Funktionen

- Diagnoseansicht für Modulstatus
- Logging der Modulaktualisierung
- Cache löschen über Settings/Diagnose

### 7.5 Erfolgskriterien

- Fehler in einem Modul stoppen nicht die App,
- UI bleibt bedienbar,
- Cache kann letzte Daten liefern,
- Module sind austauschbar.

### 7.6 Möglicher Commit

```text
Introduce dashboard module refresh and cache abstractions
```

---

## 8. Version V0.5 – Wettermodul

### 8.1 Ziel

Das Wettermodul wird als erstes echtes Datenmodul umgesetzt, weil es klar abgegrenzt ist und schnell sichtbaren Nutzen bringt.

### 8.2 Muss-Funktionen

- konfigurierbarer Ort
- aktuelle Temperatur
- Wetterzustand
- Regenwahrscheinlichkeit
- Vorhersage für nächste Stunden
- letzte Aktualisierung
- Cache bei Offline-Fall
- Fehleranzeige in Wetterkarte

### 8.3 Soll-Funktionen

- manuelle Aktualisierung
- Eco-Refresh bei Akkubetrieb
- Wetterdatenmodell unabhängig von API halten
- Wetterprovider über Interface kapseln

### 8.4 Kann-Funktionen

- mehrere Orte
- Wetterwarnungen
- Standortermittlung nur mit Zustimmung
- kleine Stundenübersicht als Mini-Chart

### 8.5 Nicht enthalten

- automatische Standortermittlung ohne Zustimmung
- Wetterdaten als kritischer Dienst
- permanente API-Abfragen

### 8.6 Erfolgskriterien

- Wetterkarte zeigt echte Daten,
- App funktioniert ohne Internet weiter mit Cache,
- API-Ausfall führt nicht zum Absturz.

### 8.7 Möglicher Commit

```text
Add weather module with cached forecast data
```

---

## 9. Version V0.6 – Aufgabenmodul

### 9.1 Ziel

Das Dashboard erhält eine erste echte Aufgabenquelle.

### 9.2 Muss-Funktionen

- lokale Aufgabenquelle
- Top-Aufgaben anzeigen
- Fälligkeit anzeigen
- überfällige Aufgaben markieren
- erledigt/offen
- Privacy Mode für Aufgaben
- Cache/lokale Speicherung

### 9.3 Soll-Funktionen

- einfache JSON-basierte Aufgabenliste
- Prioritäten
- Projektzuordnung
- Tagesfokus aus Aufgabe ableiten oder manuell setzen

### 9.4 Kann-Funktionen

- TaskHost-Integration vorbereiten
- Aufgaben in Dashboard abhaken
- Detailansicht
- Filter nach Projekt

### 9.5 Nicht enthalten

- vollständige Aufgabenverwaltung als Ersatz für TaskHost
- Mehrbenutzer-Synchronisierung
- Cloud-Pflicht

### 9.6 Erfolgskriterien

- Dashboard ist mit lokalen Aufgaben im Alltag nützlich,
- Privacy Mode anonymisiert Aufgaben zuverlässig,
- Aufgabenquelle ist später austauschbar.

### 9.7 Möglicher Commit

```text
Add local tasks module with privacy-aware display
```

---

## 10. Version V0.7 – News/RSS-Modul

### 10.1 Ziel

Das Dashboard zeigt kuratierte Nachrichtenquellen an.

### 10.2 Muss-Funktionen

- RSS-Feed-Konfiguration
- mehrere Kategorien
- Titel, Quelle, Zeit
- Cache
- Fehleranzeige pro Feed
- maximale Anzahl sichtbarer Meldungen

### 10.3 Soll-Funktionen

- Kategorien:
  - Lokal
  - Welt
  - IT/Security
  - Wissenschaft
- Feed aktivieren/deaktivieren
- manuell aktualisieren

### 10.4 Kann-Funktionen

- Schlagwortfilter
- gelesene Meldungen markieren
- Link im Browser öffnen
- Zusammenfassung später

### 10.5 Erfolgskriterien

- News-Karte informiert ohne zu nerven,
- schlechte Feeds bremsen App nicht aus,
- RSS-Ausfall wird ruhig behandelt.

### 10.6 Möglicher Commit

```text
Add RSS news module with categorized feed display
```

---

## 11. Version V0.8 – Systemstatusmodul

### 11.1 Ziel

Das Dashboard zeigt wichtige lokale Systeminformationen.

### 11.2 Muss-Funktionen

- Akku/Netzbetrieb
- CPU grob
- RAM grob
- Speicherplatz C:
- Netzwerkstatus
- Aktualisierungsintervall steuerbar

### 11.3 Soll-Funktionen

- Warnung bei wenig Speicherplatz
- Warnung bei niedrigem Akku
- Eco Mode bei Akku
- Systemstatus im Compact Mode gekürzt

### 11.4 Kann-Funktionen

- mehrere Laufwerke
- Windows-Update-Hinweis
- Backupstatus später
- Dienste/Prozesse später

### 11.5 Erfolgskriterien

- Systemstatuskarte ist nützlich,
- Abfragen belasten System nicht spürbar,
- Anzeige bleibt ruhig.

### 11.6 Möglicher Commit

```text
Add lightweight local system status module
```

---

## 12. Version V0.9 – Kalender und Tagesfokus

### 12.1 Ziel

Das Dashboard zeigt Termine und unterstützt den Tagesfokus.

### 12.2 Muss-Funktionen

- lokale oder manuelle Terminquelle
- nächster Termin
- Tagesübersicht
- Privacy Mode für Termine
- Tagesfokus-Karte

### 12.3 Soll-Funktionen

- ICS-Datei als Quelle prüfen
- manuelle Fokusnotiz
- Konflikthinweis vorbereitet

### 12.4 Kann-Funktionen

- CalDAV später
- Microsoft/Google später
- Reisezeit später
- wiederkehrende Termine später

### 12.5 Erfolgskriterien

- Nutzer sieht schnell, was heute wichtig ist,
- sensible Termine sind anonymisierbar,
- keine Cloud-Abhängigkeit für erste Version.

### 12.6 Möglicher Commit

```text
Add calendar overview and daily focus display
```

---

## 13. Version V1.0 – Erste produktiv nutzbare Version

### 13.1 Ziel

V1.0 ist die erste Version, die im Alltag sinnvoll genutzt werden kann.

### 13.2 Muss-Funktionen

- stabile WPF-App
- Dashboard Mode
- Compact Mode
- Privacy Mode
- Tray-Steuerung
- Monitorprofile
- sichere Fensterpositionierung
- Wettermodul
- lokales Aufgabenmodul
- RSS-Newsmodul
- Systemstatusmodul
- Kalender/Tagesfokus-Grundfunktion
- lokale Konfiguration
- Cache/Offline-Verhalten
- README mit Startanleitung
- sauberes Logging ohne Secrets

### 13.3 Soll-Funktionen

- Focus Mode
- Presentation Mode
- Einstellungen-UI in Basisform
- Autostart optional
- Theme-Auswahl
- Diagnoseansicht
- App-Icon
- erste Screenshots

### 13.4 Kann-Funktionen

- SASD-Projektmodul mit lokaler Projektliste
- einfacher Schnellstarter
- Dark-Glass-Theme
- Hintergrundbildoption
- Export/Import der Konfiguration

### 13.5 Nicht enthalten

- vollwertiges Plugin-System
- vollständige TaskHost-Integration
- Cloud-Synchronisierung
- eigener Kalender-Server
- eigene Desktop-Shell
- umfangreicher Installer als Pflicht
- mobile App

### 13.6 Erfolgskriterien

V1.0 ist erfolgreich, wenn:

- das Dashboard im Alltag auf Laptop und Dockingstation sinnvoll läuft,
- die wichtigsten Informationen sichtbar sind,
- Privacy Mode zuverlässig funktioniert,
- die App nicht spürbar bremst,
- externe Ausfälle nicht zum Absturz führen,
- das Projekt öffentlich vorzeigbar ist.

### 13.7 Möglicher Release-Titel

```text
v1.0.0 – First usable personal dashboard release
```

---

## 14. Version V1.1 – Polishing und Bedienkomfort

### 14.1 Ziel

Nach V1.0 werden Bedienung und Erscheinungsbild verbessert.

### 14.2 Mögliche Funktionen

- bessere Einstellungen-UI
- Theme-Editor light
- Kartenreihenfolge per Konfiguration
- bessere Icons
- Import/Export der Einstellungen
- bessere Diagnose
- Updatehinweise
- erweiterte Accessibility
- bessere Dokumentation

### 14.3 Erfolgskriterien

- App wirkt hochwertiger,
- Bedienung ist klarer,
- weniger Konfigurationsarbeit per JSON nötig.

---

## 15. Version V1.2 – SASD-Projektintegration

### 15.1 Ziel

Das Dashboard wird stärker mit SASD-Projekten verbunden.

### 15.2 Mögliche Funktionen

- lokale Projektliste
- Projektstatus
- nächste Schritte
- Git-Repository-Pfade
- letzter lokaler Commit
- offene lokale Aufgaben
- Verweis auf Dokumentationsordner

### 15.3 Spätere GitHub-Funktionen

- offene Issues
- letzter Remote-Commit
- Pull Requests
- Releases
- Build-Status, falls vorhanden

### 15.4 Erfolgskriterien

- Dashboard hilft konkret beim Überblick über SASD-Arbeit,
- keine Überladung,
- GitHub-Ausfall beeinträchtigt lokale Nutzung nicht.

---

## 16. Version V1.3 – TaskHost-Anbindung

### 16.1 Ziel

Das Dashboard kann Aufgaben aus TaskHost anzeigen.

### 16.2 Voraussetzungen

- TaskHost muss eine stabile Schnittstelle besitzen,
- Authentifizierung muss geklärt sein,
- Datenschutz muss geklärt sein,
- Fehlerfälle müssen robust behandelt werden.

### 16.3 Mögliche Funktionen

- TaskHost-Aufgaben lesen
- heute fällige Aufgaben anzeigen
- Projektfilter
- Aufgabe als erledigt markieren
- Link in TaskHost öffnen

### 16.4 Erfolgskriterien

- TaskHost-Integration ist optional,
- lokale Aufgabenquelle bleibt möglich,
- keine harte Abhängigkeit vom TaskHost-Service.

---

## 17. Version V1.4 – Erweiterte Datenschutz- und Präsentationsfunktionen

### 17.1 Ziel

Das Dashboard wird sicherer für Bildschirmfreigabe und Kundenkontakt.

### 17.2 Mögliche Funktionen

- strenger Presentation Mode
- Profile für Kundengespräche
- automatische Ausblendung sensibler Karten
- optionaler „Clean Screen“-Modus
- Hotkey-Konfiguration
- visuelle Anzeige aktiver Privacy-Modi
- Protokollierung ohne personenbezogene Details

### 17.3 Erfolgskriterien

- Nutzer kann Dashboard schnell entschärfen,
- Präsentationssituationen sind sicherer,
- sensible Inhalte werden nicht versehentlich gezeigt.

---

## 18. Version V2.0 – Erweiterbare Plattform

### 18.1 Ziel

V2.0 könnte aus dem Dashboard eine kleine Plattform machen.

### 18.2 Mögliche Funktionen

- Plugin-System
- mehrere Dashboard-Fenster
- mehrere Monitorrollen
- WebView2-Karten
- lokale SQLite-Datenbank
- erweiterte Automatisierung
- Skriptquellen
- lokale REST-Schnittstelle
- komplexere SASD-Integrationen

### 18.3 Risiken

- steigende Komplexität,
- Sicherheitsfragen,
- Performance,
- Wartbarkeit,
- Gefahr der Überladung.

### 18.4 Entscheidung

V2.0 sollte erst geplant werden, wenn V1.0 im Alltag echten Nutzen gezeigt hat.

---

## 19. Dokumentations-Roadmap

### 19.1 Bereits geplante Dokumente

```text
010_Lastenheft.md
020_Pflichtenheft_MVP.md
030_Technical_Design.md
040_UI_Concept.md
050_Roadmap.md
```

### 19.2 Später sinnvolle Dokumente

```text
060_Security_Privacy_Concept.md
070_User_Manual.md
080_Developer_Guide.md
090_Admin_Installation_Guide.md
100_Release_Notes.md
110_Test_Concept.md
120_API_Integration_Notes.md
```

### 19.3 Wann weitere Dokumente nötig werden

Security/Privacy Concept:

- sobald echte Aufgaben, Kalender oder externe Dienste integriert werden.

User Manual:

- spätestens vor V1.0.

Developer Guide:

- sobald das Projekt für andere Entwickler oder öffentliches Mitwirken gedacht ist.

Installation Guide:

- wenn Autostart, Installer oder Release-Pakete entstehen.

---

## 20. Backlog-Ideen

Diese Ideen werden bewusst nicht sofort eingeplant, aber festgehalten.

### 20.1 UI-Ideen

- Glass Theme
- Hintergrundbild mit abgedunkeltem Overlay
- Light Theme
- High Contrast Theme
- Kartenreihenfolge per Drag & Drop
- mehrere Layouts pro Monitor
- Uhr im großen Wallboard-Stil
- Ruhemodus nachts

### 20.2 Datenideen

- Wetterwarnungen
- lokale Verkehrsinformationen
- Sicherheitsmeldungen
- GitHub-Issues
- lokale Git-Repositories
- RSS-Kategorien
- Kalenderkonflikte
- Geburtstage/Erinnerungen
- Backupstatus
- Serverstatus

### 20.3 SASD-Ideen

- Integration mit TaskHost
- Integration mit LogSink
- Anzeige lokaler SASD-Dienste
- Projektfortschritt
- Dokumentationsstatus
- Release-Erinnerungen
- Kunden-/Akquise-Pipeline später nur mit Privacy Mode

### 20.4 Technische Ideen

- SQLite
- WebView2
- Plugin-System
- Skriptmodule
- lokaler Agent
- MSIX Installer
- Auto-Update
- portable Version
- Telemetrie ausdrücklich optional oder gar nicht

---

## 21. Risiken und Gegenmaßnahmen

### 21.1 Risiko: Zu viel auf einmal

Gegenmaßnahme:

- Roadmap einhalten,
- V0.1 klein halten,
- Datenmodule nacheinander bauen.

### 21.2 Risiko: Performanceprobleme

Gegenmaßnahme:

- keine Daueranimationen,
- Aktualisierungsintervalle,
- Cache,
- sichtbarkeitsabhängige Updates,
- Eco Mode.

### 21.3 Risiko: Datenschutzprobleme

Gegenmaßnahme:

- Privacy Mode,
- Presentation Mode,
- keine Secrets im Repo,
- keine sensiblen Logs,
- lokale Verarbeitung bevorzugen.

### 21.4 Risiko: Multi-Monitor-Probleme

Gegenmaßnahme:

- sichere Fensterpositionierung,
- Monitorprofile,
- Fallback auf primären Monitor,
- manuelle Tests mit Docking/Abdocken.

### 21.5 Risiko: Projekt wird nur optische Spielerei

Gegenmaßnahme:

- Fokus auf Aufgaben, Kalender, Wetter, Systemstatus,
- Tagesfokus,
- klare Nutzenprüfung pro Feature.

---

## 22. Empfohlene nächste konkrete Schritte

Nach Erstellung dieser Dokumente sollte als nächstes umgesetzt werden:

1. Repository lokal klonen.
2. .NET-Solution anlegen.
3. Projektstruktur erstellen.
4. WPF-App starten lassen.
5. Dummy-Dashboard bauen.
6. README aktualisieren.
7. Build testen.
8. Ersten Commit erstellen.

Empfohlener erster Entwicklungs-Commit:

```text
Initialize WPF solution structure
```

Danach:

```text
Add initial dashboard shell with mock cards
```

---

## 23. Zusammenfassung

Die Roadmap führt das Projekt bewusst von einer stabilen technischen Shell zu einer produktiv nutzbaren Dashboard-Anwendung.

Der wichtigste Punkt ist: V0.1 muss nicht viel können, aber richtig aufgebaut sein. Wenn Monitorprofile, Modulkonzept, Konfiguration, Privacy Mode und Layoutgrundlagen sauber vorbereitet sind, lassen sich Wetter, Aufgaben, News und Systemstatus danach schrittweise ergänzen.

Das Projekt sollte nicht als „schönes Hintergrundbild“ verstanden werden, sondern als professioneller, persönlicher Windows-Arbeitsleitstand.
