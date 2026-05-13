# SASD Personal Desktop Dashboard – Pflichtenheft

**Projekt:** SASD Personal Desktop Dashboard  
**Repository:** `SASD-PersonalDesktopDashboard`  
**Dokument:** Pflichtenheft / technische Spezifikation  
**Dokumentenstand:** 2026-05-13  
**Version:** 0.1  
**Autor:** Robin Goerlach / SASD-GmbH – Scientific and Software Development  
**Sprache:** Deutsch  
**Status:** Entwurf für MVP und Zielarchitektur  

---

## 1. Zweck dieses Dokuments

Dieses Pflichtenheft beschreibt, wie das **SASD Personal Desktop Dashboard** technisch und funktional umgesetzt werden soll. Es konkretisiert die zuvor diskutierte Idee eines persönlichen Windows-Desktop-Leitstands für Wetter, Aufgaben, Kalender, Nachrichten, Systemstatus und SASD-Projektinformationen.

Das Dokument beschreibt bewusst nicht nur die erste Minimalversion, sondern sammelt die vollständige geplante Funktionalität in geordneter Form. Gleichzeitig wird klar abgegrenzt, welche Funktionen bereits in **V0.1 / MVP** umgesetzt werden sollen und welche Funktionen für spätere Versionen vorgesehen sind.

Ziel ist ein solides, wartbares und erweiterbares Windows-Programm, das auf Laptop-, Zwei-Monitor- und Drei-Monitor-Arbeitsplätzen sinnvoll funktioniert, ohne den normalen Windows-Desktop, den Papierkorb, Desktop-Verknüpfungen oder die Systemleistung negativ zu beeinflussen.

---

## 2. Projektidee und Zielbild

Das SASD Personal Desktop Dashboard soll den Windows-Desktop nicht durch ein reines Hintergrundbild ersetzen, sondern zu einem **ruhigen, produktiven Informationsleitstand** erweitern.

Der Anwender soll beim Blick auf den Desktop oder auf einen zweiten/dritten Monitor schnell erkennen:

- Wie ist das aktuelle Wetter und die Vorhersage für die nächsten Stunden?
- Welche Aufgaben sind heute wichtig?
- Welche Termine stehen als Nächstes an?
- Gibt es relevante lokale, weltweite, technische oder wissenschaftliche Nachrichten?
- Wie ist der Zustand des eigenen Rechners?
- Welche SASD-Projekte oder Arbeitsbereiche benötigen Aufmerksamkeit?

Das Dashboard soll dabei nicht hektisch, überladen oder spielerisch wirken. Es soll wie ein professionelles Cockpit erscheinen: ruhig, klar, kontrastreich, datenschutzbewusst und ressourcenschonend.

---

## 3. Grundsatzentscheidungen

### 3.1 Kein Ersatz für den Windows-Desktop

Das Dashboard ersetzt nicht den Windows Explorer, den Desktop-Ordner, den Papierkorb oder die normale Verwaltung von Desktop-Symbolen.

Die folgenden Windows-Funktionen bleiben vollständig bei Windows:

- Papierkorb
- Desktop-Dateien
- Desktop-Verknüpfungen
- Desktop-Rechtsklickmenü
- Icon-Anordnung
- Taskleiste
- Fensterverwaltung
- virtuelle Desktops
- Windows-Shell-Funktionen

Das Dashboard wird als eigenständige Desktop-Anwendung umgesetzt und legt sich nicht zerstörerisch über die Windows-Shell.

### 3.2 Keine Live-Wallpaper-Lösung als V1-Grundlage

Ein dynamisches Hintergrundbild oder ein in die Windows-Shell eingebettetes Fenster wird für die erste Version nicht umgesetzt. Solche Lösungen können optisch interessant sein, sind aber potentiell fragiler, schwerer wartbar und stärker von Windows-Interna abhängig.

Das Dashboard wird stattdessen als normales oder rahmenloses Anwendungsfenster umgesetzt. Es kann auf einem zweiten Monitor vollflächig laufen oder auf einem Laptop als kompakte Sidebar angezeigt werden.

### 3.3 Windows-native Anwendung statt Web-only-Lösung

HTML, CSS und JavaScript sind nicht ausgeschlossen, aber nicht zwingend. Für die erste Umsetzung wird eine Windows-native Lösung bevorzugt.

Die empfohlene technische Basis ist:

- **C#**
- **.NET 8 oder neuer**
- **WPF** als native Windows-Oberfläche
- optional später **WebView2** für einzelne Dashboard-Karten oder HTML-basierte Ansichten

Damit stehen gute Möglichkeiten für Monitorerkennung, DPI-Verhalten, Tray-Icon, Autostart, Hotkeys, Fensterpositionierung und Windows-Integration zur Verfügung.

### 3.4 Datenschutz vor Komfort

Das Dashboard kann persönliche Daten anzeigen: Aufgaben, Termine, Projektinformationen, ggf. Nachrichten und Systeminformationen. Deshalb darf es keine sensiblen Daten unnötig offenlegen.

Es muss mindestens einen **Privacy Mode** geben, mit dem persönliche Inhalte anonymisiert oder ausgeblendet werden können.

---

## 4. Zielgruppen und Nutzungsszenarien

### 4.1 Hauptnutzer

Primärer Nutzer ist Robin Goerlach / SASD-GmbH. Das Dashboard soll persönliche Produktivität, Projektübersicht und Arbeitsorganisation unterstützen.

### 4.2 Spätere Zielgruppen

Später kann das Projekt auch als öffentliches SASD-Produkt oder Portfolio-Projekt dienen, zum Beispiel für:

- Entwickler
- Administratoren
- Freelancer
- kleine Unternehmen
- technisch interessierte Poweruser
- Nutzer mit mehreren Monitoren
- Personen, die Aufgaben, Wetter, Kalender und Systemstatus kompakt sehen möchten

### 4.3 Nutzungsszenario: Laptop allein unterwegs

Wenn der Laptop ohne externe Monitore genutzt wird, darf das Dashboard nicht zu viel Bildschirmfläche belegen.

Das Dashboard soll in diesem Szenario bevorzugt als kompakte Sidebar, kleines Fenster oder per Hotkey einblendbares Panel laufen.

Wichtige Eigenschaften:

- platzsparend
- einklappbar
- nicht aufdringlich
- reduzierte Aktualisierungsintervalle im Akkubetrieb
- Fokus auf wenige wichtige Informationen

### 4.4 Nutzungsszenario: Arbeitsplatz mit zweitem Monitor

Bei angeschlossenem zweiten Monitor soll das Dashboard diesen Monitor sinnvoll als Informationsfläche nutzen können.

Typisches Verhalten:

- Hauptmonitor bleibt Arbeitsmonitor.
- Zweiter Monitor zeigt Dashboard im Vollbild oder maximierten Fenster.
- Dashboard zeigt Wetter, Aufgaben, Termine, News, Systemstatus und Projekte in mehreren Kacheln.

### 4.5 Nutzungsszenario: Arbeitsplatz mit drei Monitoren

Bei drei Monitoren soll zunächst ein Dashboard-Fenster auf einem frei wählbaren Monitor unterstützt werden.

Spätere Versionen können mehrere Dashboard-Fenster oder modulare Panels auf verschiedenen Monitoren unterstützen.

### 4.6 Nutzungsszenario: Abdocken / Monitorwechsel

Wenn externe Monitore getrennt werden, darf das Dashboard nicht auf einem nicht mehr vorhandenen Bildschirm verschwinden.

Das Programm muss Monitorwechsel erkennen und Fenster sicher auf sichtbare Arbeitsbereiche verschieben.

### 4.7 Nutzungsszenario: Präsentation oder Bildschirmfreigabe

Bei Präsentationen, Schulungen, Kundenterminen oder Bildschirmfreigaben dürfen private Aufgaben, Termine oder Projektinformationen nicht unbeabsichtigt sichtbar sein.

Das Dashboard muss einen leicht aktivierbaren Privacy Mode besitzen.

---

## 5. Versionierung und Ausbaustufen

### 5.1 V0.1 – Technical Shell / MVP-Basis

Ziel von V0.1 ist eine lauffähige, stabile Windows-Anwendung ohne echte externe Datenquellen.

Umfang:

- WPF-Anwendung startet fehlerfrei.
- Dashboard-Hauptfenster wird angezeigt.
- Dummy-Karten für Wetter, Aufgaben, Kalender, Nachrichten, Systemstatus und SASD-Projekte existieren.
- Grundlayout ist responsiv innerhalb des Fensters.
- Monitorerkennung ist vorbereitet oder teilweise umgesetzt.
- Fensterposition kann gespeichert und wiederhergestellt werden.
- Fallback auf primären Monitor bei fehlendem gespeicherten Monitor.
- Konfiguration über lokale JSON-Datei ist vorbereitet.
- Privacy Mode ist als Konzept und UI-Schalter vorbereitet.
- Keine externen APIs.
- Keine sensiblen Daten.

### 5.2 V0.2 – Monitorprofile und Layoutstabilität

Umfang:

- Erkennung von Monitoranzahl, Auflösung, Arbeitsbereich und DPI-Skalierung.
- Profile für Laptopbetrieb und Mehrmonitorbetrieb.
- Automatischer Wechsel zwischen Compact Mode und Dashboard Mode.
- Sichere Behandlung von Monitorwechseln.
- Verbesserte Fensterpositionslogik.
- Erste Tray-Integration.

### 5.3 V0.3 – Lokale Datenmodule

Umfang:

- lokale Aufgabenliste oder Import aus einfacher lokaler Datei
- Tagesfokus
- lokale Notizen
- einfache RSS-Feed-Unterstützung
- Systemstatus mit CPU/RAM/Speicherplatz
- konfigurierbare Aktualisierungsintervalle

### 5.4 V1.0 – Produktiv nutzbares Dashboard

Umfang:

- echte Wetterdatenquelle
- Aufgabenmodul produktiv nutzbar
- Kalenderanzeige oder vorbereitete Schnittstelle
- RSS-/News-Modul
- Systemstatus
- Privacy Mode
- Autostart optional
- Tray-Menü
- stabile Konfiguration
- saubere Dokumentation
- Installationshinweise

### 5.5 Spätere Versionen

Mögliche spätere Funktionen:

- Integration mit TaskHost
- Integration mit Microsoft To Do oder anderen Aufgabenquellen
- Kalenderintegration
- GitHub-Projektstatus
- Server-/Service-Monitoring
- mehrere Dashboard-Fenster
- Themes
- Glass-/Ambient-Modus
- Export/Import von Profilen
- Plugin-System
- Benachrichtigungsregeln
- eigene Widget-Entwicklung

---

## 6. Funktionale Anforderungen

Die folgenden Anforderungen beschreiben den Zielumfang. Sie sind mit Prioritäten versehen:

- **MUSS:** erforderlich für MVP oder Kernnutzen
- **SOLL:** wichtig, aber nicht zwingend für den ersten lauffähigen Stand
- **KANN:** wünschenswert für spätere Versionen

---

## 7. Anwendung und Startverhalten

### 7.1 Programmstart

**Priorität:** MUSS

Beim Start soll das Programm:

1. die Konfiguration laden,
2. vorhandene Monitore erkennen,
3. das passende Anzeigeprofil auswählen,
4. die letzte gültige Fensterposition prüfen,
5. das Dashboard sichtbar und vollständig innerhalb eines vorhandenen Arbeitsbereiches anzeigen.

Wenn keine Konfiguration vorhanden ist, soll eine Standardkonfiguration erzeugt oder intern verwendet werden.

### 7.2 Fehlende oder beschädigte Konfiguration

**Priorität:** MUSS

Wenn die Konfigurationsdatei fehlt, fehlerhaft oder nicht lesbar ist, darf die Anwendung nicht abstürzen.

Erwartetes Verhalten:

- Anwendung startet mit Standardwerten.
- Fehler wird intern protokolliert.
- Nutzer erhält optional eine verständliche Meldung.
- beschädigte Konfiguration wird nicht blind überschrieben, außer dies ist ausdrücklich vorgesehen.

### 7.3 Tray-Icon

**Priorität:** SOLL für V0.2, MUSS für V1.0

Das Dashboard soll über ein Symbol im Windows-Infobereich steuerbar sein.

Mögliche Funktionen im Tray-Menü:

- Dashboard anzeigen
- Dashboard ausblenden
- Compact Mode aktivieren
- Dashboard Mode aktivieren
- Privacy Mode ein/aus
- Einstellungen öffnen
- Anwendung beenden

### 7.4 Autostart

**Priorität:** SOLL für V1.0

Der Nutzer soll optional festlegen können, ob das Dashboard beim Windows-Start automatisch gestartet wird.

Autostart darf nicht heimlich aktiviert werden. Der Nutzer muss diese Funktion bewusst einschalten.

---

## 8. Fenster- und Anzeigeverhalten

### 8.1 Dashboard Mode

**Priorität:** MUSS

Der Dashboard Mode ist die normale großflächige Ansicht für einen externen Monitor.

Eigenschaften:

- Fenster kann maximiert oder randlos dargestellt werden.
- Kachelraster zeigt mehrere Informationsbereiche gleichzeitig.
- Layout passt sich an Fenstergröße an.
- Dashboard darf nicht permanent rechenintensive Animationen ausführen.

### 8.2 Compact Mode

**Priorität:** MUSS

Der Compact Mode ist die platzsparende Ansicht für Laptopbetrieb.

Eigenschaften:

- schmale Seitenleiste oder kleines Fenster
- Anzeige der wichtigsten Informationen
- keine überladene Darstellung
- einfache Ein-/Ausblendung
- reduzierte Aktualisierung bei Akkubetrieb möglich

Anzuzeigende Inhalte im Compact Mode:

- aktuelle Uhrzeit und Datum
- Wetter-Kurzstatus
- nächste wichtige Aufgabe
- nächster Termin oder Tagesfokus
- optional Systemwarnung

### 8.3 Focus Mode

**Priorität:** SOLL

Der Focus Mode reduziert die Anzeige auf wenige arbeitsrelevante Inhalte.

Typische Inhalte:

- Tagesfokus
- nächste Aufgabe
- nächster Termin
- optional Wetterwarnung

Nicht angezeigt werden sollen in diesem Modus:

- umfangreiche Nachrichtenlisten
- lange Projektlisten
- visuelle Ablenkungen

### 8.4 Wallboard Mode

**Priorität:** SOLL

Der Wallboard Mode ist eine großformatige Ansicht für einen zweiten oder dritten Monitor.

Eigenschaften:

- große Kacheln
- gut aus der Entfernung lesbar
- ruhiges Layout
- automatische Aktualisierung
- keine Interaktion erforderlich

### 8.5 Silent Mode

**Priorität:** KANN

Im Silent Mode bleibt das Dashboard im Hintergrund oder im Tray, zeigt aber kein sichtbares Fenster.

Nützlich für:

- Präsentationen
- konzentriertes Arbeiten
- niedrigen Energieverbrauch
- temporäres Ausblenden

---

## 9. Multi-Monitor-Unterstützung

### 9.1 Monitorerkennung

**Priorität:** MUSS

Die Anwendung muss beim Start die verfügbaren Monitore erkennen.

Zu erfassende Informationen:

- Anzahl der Monitore
- primärer Monitor
- Auflösung
- Arbeitsbereich ohne Taskleiste
- Position im virtuellen Desktop
- DPI- bzw. Skalierungsinformationen, soweit verfügbar
- stabiler Monitor-Fingerprint, soweit technisch sinnvoll

### 9.2 Monitorprofile

**Priorität:** SOLL

Die Anwendung soll Monitorprofile unterstützen.

Beispiele:

- Laptop unterwegs
- Büro Dockingstation
- Heimarbeitsplatz
- Präsentationsmodus

Ein Profil soll festlegen können:

- bevorzugter Modus
- bevorzugter Monitor
- Fensterposition
- Fenstergröße
- sichtbare Kacheln
- Power-Profil
- Privacy-Default

### 9.3 Fallback bei fehlendem Monitor

**Priorität:** MUSS

Wenn ein gespeicherter Monitor nicht verfügbar ist, muss das Fenster automatisch auf einem sichtbaren Monitor geöffnet werden.

Das Fenster darf niemals vollständig außerhalb des sichtbaren Arbeitsbereiches liegen.

### 9.4 Abdocken und Andocken im laufenden Betrieb

**Priorität:** SOLL

Wenn Monitore im laufenden Betrieb angeschlossen oder entfernt werden, soll die Anwendung reagieren.

Erwartetes Verhalten:

- Monitorwechsel erkennen
- Profil neu bewerten
- Fenster gegebenenfalls verschieben
- Nutzerdaten nicht verlieren
- keine Abstürze

### 9.5 Unterschiedliche Auflösungen

**Priorität:** MUSS

Das Layout darf nicht auf eine feste Auflösung festgelegt sein.

Zu unterstützen:

- Laptop-Auflösungen
- Full HD
- WQHD
- 4K
- unterschiedliche Seitenverhältnisse
- Hochformatmonitore, soweit möglich

### 9.6 Unterschiedliche DPI-Skalierungen

**Priorität:** SOLL

Die Anwendung soll mit unterschiedlichen Windows-Skalierungen umgehen können, z. B. 100 %, 125 %, 150 % oder 200 %.

Text und Kacheln sollen nicht unscharf, abgeschnitten oder unbedienbar wirken.

---

## 10. Layout und Benutzeroberfläche

### 10.1 Kachelbasiertes Dashboard

**Priorität:** MUSS

Die UI soll aus klar erkennbaren Kacheln bestehen.

Vorgesehene Kacheln:

- Wetter
- Aufgaben
- Kalender / Termine
- Nachrichten
- Systemstatus
- SASD-Projekte
- Tagesfokus
- Uhr / Datum

### 10.2 Responsives Raster

**Priorität:** MUSS

Das Kachelraster soll sich an die verfügbare Breite anpassen.

Beispielregeln:

- schmale Ansicht: eine Spalte
- mittlere Ansicht: zwei Spalten
- breite Ansicht: drei oder vier Spalten
- sehr breite Ansicht: großformatige Wallboard-Anordnung

### 10.3 Lesbarkeit

**Priorität:** MUSS

Die Lesbarkeit hat Vorrang vor optischen Effekten.

Anforderungen:

- ausreichender Kontrast
- klare Schriftgrößen
- keine zu kleinen Texte
- keine wichtigen Informationen nur über Farbe vermitteln
- Kacheln dürfen nicht überladen sein

### 10.4 Designstil

**Priorität:** SOLL

Das Standarddesign soll technisch, ruhig und professionell wirken.

Empfohlener Stil:

- dunkles Theme
- Akzentfarben in Petrol, Blau oder Cyan
- klare Kacheln
- dezente Schatten
- abgerundete Ecken sparsam einsetzen
- keine verspielt wirkenden Effekte

### 10.5 Ambient-/Glass-Optik

**Priorität:** KANN

Später kann ein Ambient- oder Glass-Modus angeboten werden.

Eigenschaften:

- Hintergrundbild bleibt sichtbar
- Kacheln leicht transparent
- optional unscharfer Hintergrundeffekt
- Text bleibt klar und kontrastreich

Diese Funktion ist nicht Bestandteil der ersten technischen Version.

### 10.6 Theme-Umschaltung

**Priorität:** KANN

Später sollen Themes unterstützt werden können:

- Dark
- Light
- SASD Dark Glass
- High Contrast
- Custom Theme

---

## 11. Wettermodul

### 11.1 Zweck

**Priorität:** SOLL für V1.0

Das Wettermodul zeigt aktuelle Wetterdaten und eine Kurzvorhersage für den Standort des Nutzers.

### 11.2 Angezeigte Informationen

Das Modul soll anzeigen können:

- aktuelle Temperatur
- gefühlte Temperatur, falls verfügbar
- Wetterzustand
- Regenwahrscheinlichkeit
- Niederschlagsmenge, falls verfügbar
- Windgeschwindigkeit
- Windrichtung, falls verfügbar
- Wetterwarnungen, falls verfügbar
- Vorhersage für die nächsten Stunden

### 11.3 Vorhersagezeitraum

Standardmäßig soll die Vorhersage die nächsten 6 bis 12 Stunden abdecken.

Der Nutzer soll später einstellen können, ob er eine kürzere oder längere Vorschau möchte.

### 11.4 Standort

Der Standort soll nicht ungefragt dauerhaft oder präzise gespeichert werden.

Mögliche Varianten:

- manuell eingetragener Ort
- grober Standort
- später automatische Standorterkennung mit Zustimmung

Für V1 wird eine manuelle Ortskonfiguration bevorzugt.

### 11.5 Aktualisierung

Wetterdaten sollen nicht dauerhaft abgefragt werden.

Empfohlen:

- Normalbetrieb: alle 15 bis 30 Minuten
- Akkubetrieb: alle 30 bis 60 Minuten
- Offline: letzte bekannte Daten anzeigen

### 11.6 Fehlerverhalten

Wenn Wetterdaten nicht abgerufen werden können:

- letzte bekannte Daten anzeigen
- Zeitpunkt der letzten Aktualisierung anzeigen
- keine störenden Fehlermeldungen im Vordergrund
- Fehler protokollieren

---

## 12. Aufgabenmodul

### 12.1 Zweck

**Priorität:** MUSS als Dummy in V0.1, SOLL produktiv in V1.0

Das Aufgabenmodul zeigt die nächsten wichtigen Aufgaben des Nutzers.

### 12.2 Angezeigte Informationen

Das Modul soll anzeigen können:

- Aufgabe
- Fälligkeit
- Priorität
- Projekt oder Liste
- Status offen / erledigt
- Überfälligkeit
- kurze Beschreibung, falls vorhanden

### 12.3 Priorisierung

Die Anzeige soll nicht einfach alle Aufgaben zeigen, sondern priorisieren.

Mögliche Reihenfolge:

1. überfällige Aufgaben
2. heute fällige Aufgaben
3. Aufgaben mit hoher Priorität
4. Aufgaben des Tagesfokus
5. demnächst fällige Aufgaben

### 12.4 Datenquellen

Mögliche Datenquellen:

- lokale JSON-Datei
- lokale SQLite-Datenbank
- TaskHost Local
- später TaskHost Server
- später Microsoft To Do oder andere Dienste

Für die erste produktive Version wird eine einfache lokale Datenquelle bevorzugt.

### 12.5 Interaktion

Das Dashboard soll Aufgaben zunächst anzeigen, nicht vollständig verwalten.

Spätere Interaktionen:

- Aufgabe als erledigt markieren
- Aufgabe öffnen
- Aufgabe zurückstellen
- Aufgabe in externer Anwendung öffnen

### 12.6 Datenschutz

Im Privacy Mode müssen Aufgaben anonymisiert oder ausgeblendet werden können.

Beispiel:

- Normal: „Bewerbung bei Firma X prüfen“
- Privacy: „Private Aufgabe“

---

## 13. Kalender- und Terminmodul

### 13.1 Zweck

**Priorität:** SOLL

Das Kalender-/Terminmodul zeigt die nächsten Termine und hilft bei der Tagesplanung.

### 13.2 Angezeigte Informationen

Das Modul soll anzeigen können:

- nächster Termin
- Uhrzeit
- Dauer
- Ort oder Online-Meeting-Hinweis
- Kalendername, falls vorhanden
- Status frei / beschäftigt, falls verfügbar

### 13.3 Datenquellen

Mögliche Datenquellen:

- lokale Kalenderdatei
- ICS-Datei
- später CalDAV
- später Microsoft 365 / Outlook
- später Google Calendar

Für frühe Versionen genügt ein vorbereiteter Dummy oder eine einfache lokale Quelle.

### 13.4 Privacy Mode

Im Privacy Mode sollen Termine ohne Details dargestellt werden.

Beispiel:

- Normal: „Kundengespräch – Projekt XY“
- Privacy: „Termin“

---

## 14. Nachrichten- und RSS-Modul

### 14.1 Zweck

**Priorität:** SOLL

Das Nachrichtenmodul zeigt ausgewählte Informationsquellen kompakt an.

Es soll nicht zur Ablenkung werden, sondern einen schnellen Überblick ermöglichen.

### 14.2 Kategorien

Vorgesehene Kategorien:

- lokale Nachrichten
- Weltlage
- IT/Security
- Wissenschaft
- optional SASD-relevante Branchennews

### 14.3 Datenquellen

In V1 sollen bevorzugt RSS-Feeds unterstützt werden.

Vorteile:

- einfach
- transparent
- keine komplexe API-Anmeldung
- gut konfigurierbar

### 14.4 Anzeige

Pro Kategorie sollen nur wenige Schlagzeilen angezeigt werden.

Empfehlung:

- 3 bis 5 Schlagzeilen pro Kategorie
- Quelle anzeigen
- Alter der Meldung anzeigen, falls verfügbar
- Link öffnen im Standardbrowser

### 14.5 Aktualisierung

RSS-Feeds sollen nicht zu häufig abgefragt werden.

Empfohlen:

- alle 30 bis 60 Minuten
- manuelle Aktualisierung möglich
- Cache verwenden

### 14.6 Ablenkungsbegrenzung

Das Nachrichtenmodul darf nicht durch Animationen, rote Eilmeldungsoptik oder ständige Popups ablenken.

Eilmeldungen sind nur später und nur konfigurierbar vorzusehen.

---

## 15. Systemstatusmodul

### 15.1 Zweck

**Priorität:** SOLL für V0.3/V1.0

Das Systemstatusmodul zeigt wichtige lokale Systeminformationen.

### 15.2 Angezeigte Informationen

Mögliche Informationen:

- CPU-Auslastung
- RAM-Auslastung
- freier Speicherplatz wichtiger Laufwerke
- Akkustand
- Netzbetrieb/Akkubetrieb
- Netzwerkstatus
- optional IP-Adresse lokal
- optional Windows-Update-Hinweis

### 15.3 Aktualisierungsfrequenz

Systemdaten dürfen nicht unnötig oft abgefragt werden.

Empfehlung:

- CPU/RAM: alle 5 bis 10 Sekunden, wenn sichtbar
- Speicherplatz: alle 1 bis 5 Minuten
- Akku: alle 30 bis 60 Sekunden
- Netzwerk: alle 30 bis 60 Sekunden

### 15.4 Warnungen

Das Modul soll später Warnungen anzeigen können, z. B.:

- Akku niedrig
- Laufwerk fast voll
- RAM dauerhaft hoch
- Netzwerk offline

Warnungen sollen ruhig, klar und nicht hektisch dargestellt werden.

---

## 16. SASD-Projektmodul

### 16.1 Zweck

**Priorität:** KANN für V1.0, SOLL für spätere Version

Das SASD-Projektmodul zeigt Informationen zu aktiven SASD-Projekten.

### 16.2 Mögliche Inhalte

- aktive Projekte
- nächste Projektschritte
- offene Dokumentationsaufgaben
- lokale Repository-Zustände
- letzte Commits
- offene GitHub-Issues
- Build- oder Release-Status
- Verweise auf lokale Projektordner

### 16.3 Erste Umsetzung

Die erste Umsetzung soll lokal und einfach bleiben.

Möglich:

- Projektliste aus JSON-Datei
- Projektname
- Kurzbeschreibung
- Status
- nächster Schritt
- lokaler Pfad
- Repository-URL

### 16.4 Spätere GitHub-Integration

Eine spätere GitHub-Integration muss bewusst konfiguriert werden.

Anforderungen:

- keine Tokens im Klartext in Git-Repositories
- Secrets nicht im Dashboard anzeigen
- API-Abfragen begrenzen
- Fehler tolerant behandeln

---

## 17. Tagesfokus- und Notizmodul

### 17.1 Zweck

**Priorität:** SOLL

Das Dashboard soll dem Nutzer helfen, den Tag nicht mit zu vielen parallelen Informationen zu beginnen.

Das Tagesfokus-Modul zeigt eine bewusst kleine Anzahl wichtiger Ziele.

### 17.2 Inhalte

Mögliche Inhalte:

- Tagesfokus
- Top-3-Aufgaben
- kurze Notiz
- Erinnerung an wichtiges Projekt
- manuell gesetzte Priorität

### 17.3 Datenhaltung

Für frühe Versionen genügt lokale Speicherung in JSON oder SQLite.

### 17.4 Interaktion

Der Nutzer soll später den Tagesfokus direkt bearbeiten können.

In V0.1 reicht Dummy-Anzeige.

---

## 18. Privacy Mode

### 18.1 Zweck

**Priorität:** MUSS als Konzept, SOLL funktional bis V1.0

Der Privacy Mode schützt private oder vertrauliche Informationen bei Bildschirmfreigaben, Präsentationen oder spontanen Blicken Dritter.

### 18.2 Aktivierung

Mögliche Aktivierung:

- Schalter in der UI
- Tray-Menü
- Hotkey
- optional automatisiert bei bestimmten Modi

### 18.3 Verhalten

Im Privacy Mode sollen sensible Inhalte:

- anonymisiert,
- reduziert,
- ausgeblendet
- oder durch neutrale Platzhalter ersetzt werden.

Beispiele:

- Aufgabe → „Private Aufgabe“
- Termin → „Termin“
- Projektname → „SASD-Projekt“
- Notiz → ausgeblendet

### 18.4 Datenschutzstandard

Der Privacy Mode soll standardmäßig lieber zu viel als zu wenig ausblenden.

---

## 19. Energie- und Performanceverhalten

### 19.1 Grundsatz

**Priorität:** MUSS

Das Dashboard darf den Rechner nicht spürbar ausbremsen.

Es soll ressourcenschonend arbeiten und keine unnötigen CPU-, GPU-, Speicher- oder Netzwerkressourcen verbrauchen.

### 19.2 Aktualisierungslogik

Alle Module sollen eigene Aktualisierungsintervalle besitzen.

Das Programm soll vermeiden:

- permanente Schleifen
- unnötige Timer
- 60-FPS-Rendering ohne Grund
- ständige Netzwerkabfragen
- parallele Mehrfachabfragen derselben Quelle

### 19.3 Sichtbarkeitsabhängige Aktualisierung

Wenn das Dashboard ausgeblendet oder minimiert ist, sollen Aktualisierungen reduziert werden.

### 19.4 Akkubetrieb

Im Akkubetrieb soll ein Eco-Modus möglich sein.

Mögliche Anpassungen:

- Wetter seltener aktualisieren
- News seltener aktualisieren
- Systemwerte seltener aktualisieren
- Animationen deaktivieren
- externe Projektabfragen reduzieren

### 19.5 Speicherverbrauch

Das Dashboard soll keine großen Datenmengen dauerhaft im Arbeitsspeicher halten.

Caches sollen begrenzt sein.

---

## 20. Offline-Verhalten und Caching

### 20.1 Grundsatz

**Priorität:** SOLL

Das Dashboard soll auch ohne Internet sinnvoll starten.

### 20.2 Cache-Verhalten

Letzte erfolgreich geladene Daten sollen lokal zwischengespeichert werden können.

Beispiele:

- Wetter letzter Stand
- letzte RSS-Schlagzeilen
- lokale Aufgaben
- lokale Projektliste

### 20.3 Anzeige veralteter Daten

Wenn Daten aus dem Cache stammen, soll der Zeitpunkt der letzten Aktualisierung sichtbar oder abrufbar sein.

Beispiel:

```text
Wetter zuletzt aktualisiert: 13.05.2026, 15:40
```

### 20.4 Fehlerdarstellung

Fehler sollen ruhig und verständlich dargestellt werden.

Nicht gewünscht:

- aggressive Popups
- dauerhafte Fehlermeldungsflut
- Absturz bei fehlendem Internet

---

## 21. Konfiguration

### 21.1 Lokale Konfigurationsdatei

**Priorität:** MUSS

Die Anwendung soll eine lokale Konfigurationsdatei verwenden.

Empfohlenes Format:

```text
JSON
```

Mögliche Inhalte:

- Anzeigeprofil
- Fensterpositionen
- sichtbare Kacheln
- Wetterstandort
- RSS-Feeds
- Aktualisierungsintervalle
- Privacy-Einstellungen
- Power-Modus
- Theme

### 21.2 Beispielkonfiguration

Im Repository soll eine Beispielkonfiguration enthalten sein.

Wichtig:

- keine privaten Daten
- keine Tokens
- keine echten Zugangsdaten

### 21.3 Benutzerkonfiguration nicht committen

Echte lokale Konfigurationsdateien mit persönlichen Daten sollen durch `.gitignore` geschützt werden.

### 21.4 Einstellungen-UI

**Priorität:** KANN für spätere Versionen

Eine grafische Einstellungen-Oberfläche ist wünschenswert, aber nicht zwingend für die erste Version.

Für frühe Versionen reicht eine dokumentierte JSON-Datei.

---

## 22. Benachrichtigungen

### 22.1 Grundsatz

**Priorität:** KANN

Das Dashboard kann später Benachrichtigungen anzeigen, soll aber nicht zur Störquelle werden.

### 22.2 Mögliche Benachrichtigungen

- Wetterwarnung
- nächster Termin beginnt bald
- Aufgabe überfällig
- Akku niedrig
- Laufwerk fast voll
- Server nicht erreichbar

### 22.3 Regeln

Benachrichtigungen müssen konfigurierbar sein.

Der Nutzer soll einstellen können:

- welche Module Benachrichtigungen auslösen dürfen
- ob Toasts verwendet werden
- ob nur im Dashboard markiert wird
- Ruhezeiten

---

## 23. Sicherheit

### 23.1 Keine Secrets im Repository

**Priorität:** MUSS

API-Schlüssel, Tokens oder private URLs dürfen nicht ins Repository gelangen.

### 23.2 Sichere Konfigurationsablage

Spätere Versionen sollen prüfen, ob sensible Werte über Windows Credential Manager oder vergleichbare Mechanismen gespeichert werden können.

### 23.3 Netzwerkzugriffe

Netzwerkmodule müssen robust sein.

Anforderungen:

- Timeouts verwenden
- Fehler behandeln
- keine Anwendung blockieren
- keine sensiblen Daten loggen
- keine endlosen Wiederholungen

### 23.4 Logging

Logging soll hilfreich sein, darf aber keine privaten Inhalte unkontrolliert speichern.

Beispiele nicht loggen:

- vollständige Aufgabeninhalte
- vollständige Kalendereinträge
- Tokens
- API-Keys
- private Notizen

---

## 24. Barrierearmut und Bedienbarkeit

### 24.1 Tastaturbedienung

**Priorität:** SOLL

Wichtige Funktionen sollen per Tastatur erreichbar sein.

Mögliche Hotkeys:

- Dashboard anzeigen/ausblenden
- Privacy Mode ein/aus
- Compact Mode ein/aus
- Aktualisieren

### 24.2 Kontrast

**Priorität:** MUSS

Die Standarddarstellung muss ausreichend kontrastreich sein.

### 24.3 Skalierbare Schrift

**Priorität:** SOLL

Die Schriftgröße soll sich an DPI und Fenstergröße anpassen.

### 24.4 Reduzierte Bewegung

**Priorität:** SOLL

Animationen sollen sparsam eingesetzt und später deaktivierbar sein.

---

## 25. Desktop-Icons, Papierkorb und Windows-Shell

### 25.1 Desktop-Icons

**Priorität:** MUSS

Die Anwendung darf die normalen Desktop-Icons nicht übernehmen oder blockieren.

Dateien und Verknüpfungen unter dem Desktop-Ordner bleiben Windows-Desktop-Objekte.

### 25.2 Papierkorb

**Priorität:** MUSS

Der Papierkorb wird nicht nachgebaut und nicht verändert.

### 25.3 Desktop-Kontextmenü

**Priorität:** MUSS

Das normale Desktop-Kontextmenü bleibt erhalten, solange das Dashboard nicht aktiv im Vordergrund bedient wird.

### 25.4 Keine Shell-Hacks in V1

**Priorität:** MUSS

Das Dashboard soll in V1 keine riskanten Windows-Shell-Hacks nutzen, um sich hinter Desktop-Icons einzubetten.

---

## 26. Datenmodell auf hoher Ebene

### 26.1 DashboardCard

Eine Dashboard-Karte soll mindestens folgende Eigenschaften besitzen:

- eindeutige ID
- Titel
- Typ
- Sichtbarkeit
- Position oder Layoutgruppe
- Aktualisierungsstatus
- Zeitpunkt der letzten Aktualisierung
- Fehlerstatus

### 26.2 DisplayProfile

Ein Anzeigeprofil soll mindestens enthalten:

- Name
- Bedingungen zur Auswahl
- bevorzugter Modus
- bevorzugter Monitor
- Fallback-Monitor
- Fenstergröße
- sichtbare Kacheln
- Power-Profil
- Privacy-Vorgabe

### 26.3 ModuleData

Jedes Modul soll eigene Datenmodelle verwenden, aber einen gemeinsamen Status bereitstellen:

- Ladezustand
- letzte Aktualisierung
- Fehler
- Quelle
- Cache-Status

---

## 27. Architekturvorgaben

### 27.1 Projektstruktur

Empfohlene Struktur:

```text
SASD-PersonalDesktopDashboard/
├── README.md
├── LICENSE
├── .gitignore
├── docs/
│   ├── 010_Lastenheft.md
│   ├── 020_Pflichtenheft_MVP.md
│   ├── 030_Technical_Design.md
│   ├── 040_UI_Concept.md
│   └── 050_Roadmap.md
├── src/
│   ├── Sasd.PersonalDesktopDashboard.App/
│   ├── Sasd.PersonalDesktopDashboard.Core/
│   ├── Sasd.PersonalDesktopDashboard.Infrastructure/
│   └── Sasd.PersonalDesktopDashboard.Modules/
├── tests/
│   └── Sasd.PersonalDesktopDashboard.Core.Tests/
└── assets/
    ├── icons/
    └── mockups/
```

### 27.2 Schichten

Die Anwendung soll logisch getrennt werden in:

- App/UI
- Core
- Infrastructure
- Modules
- Tests

### 27.3 Core

Core enthält:

- gemeinsame Modelle
- Schnittstellen
- Layoutlogik
- Profil-Auswahl
- Konfigurationsmodelle
- keine direkte UI-Abhängigkeit, soweit möglich

### 27.4 Infrastructure

Infrastructure enthält:

- Dateizugriff
- HTTP-Zugriff
- Caching
- Windows-spezifische Dienste
- Monitorinformationen
- Systemstatusabfragen

### 27.5 Modules

Modules enthält fachliche Datenmodule:

- Weather
- Tasks
- Calendar
- News
- SystemStatus
- SasdProjects

### 27.6 App/UI

App enthält:

- WPF-Fenster
- Views
- ViewModels
- Tray-Integration
- Theme-Ressourcen
- Benutzerinteraktion

---

## 28. Tests

### 28.1 Unit Tests

**Priorität:** SOLL

Unit Tests sollen insbesondere für nicht-visuelle Logik erstellt werden.

Testbare Bereiche:

- Profil-Auswahl
- Konfigurationsvalidierung
- Layout-Breakpoint-Logik
- Cache-Entscheidungen
- Priorisierung von Aufgaben
- Privacy-Transformation

### 28.2 Manuelle UI-Tests

**Priorität:** MUSS

Da Monitor- und DPI-Verhalten schwer vollständig automatisierbar ist, sind manuelle Tests nötig.

Testfälle:

- Laptop allein
- zweiter Monitor
- dritter Monitor
- Monitor entfernen
- Monitor hinzufügen
- DPI-Skalierung 100 %
- DPI-Skalierung 150 %
- Fensterposition außerhalb des sichtbaren Bereichs simulieren
- Privacy Mode aktivieren

### 28.3 Performance-Tests

**Priorität:** SOLL

Es soll geprüft werden:

- CPU-Verbrauch im Idle
- Speicherverbrauch nach längerem Betrieb
- Verhalten bei Netzwerkfehlern
- Verhalten im Akkubetrieb

---

## 29. Dokumentation

### 29.1 Projektdokumentation

**Priorität:** MUSS

Mindestens folgende Dokumente sollen gepflegt werden:

- README.md
- Lastenheft
- Pflichtenheft
- Technical Design
- UI Concept
- Roadmap

### 29.2 Entwicklerdokumentation

**Priorität:** SOLL

Die Entwicklerdokumentation soll erklären:

- Projektstruktur
- Build
- Start
- Tests
- Konfiguration
- Architekturentscheidungen

### 29.3 Nutzerhinweise

**Priorität:** SOLL für V1.0

Eine spätere Nutzeranleitung soll erklären:

- Installation
- Start
- Tray-Menü
- Anzeigeprofile
- Privacy Mode
- Konfiguration
- Fehlerbehebung

---

## 30. Abgrenzungen und Nicht-Ziele

### 30.1 Keine vollständige Aufgabenverwaltung in V1

Das Dashboard zeigt Aufgaben an, ersetzt aber nicht sofort eine vollständige Aufgabenverwaltungsanwendung.

### 30.2 Kein Kalender-Vollclient in V1

Das Dashboard zeigt Termine an, ersetzt aber nicht Outlook, Google Calendar oder einen vollständigen Kalenderclient.

### 30.3 Kein Newsportal

Das Nachrichtenmodul soll nur Überblick geben, nicht zu einem vollständigen Newsreader werden.

### 30.4 Kein Systemmonitoring-Ersatz

Das Systemstatusmodul ersetzt keine professionellen Monitoringlösungen.

### 30.5 Kein Ersatz für Windows Desktop Shell

Das Dashboard ersetzt nicht Explorer, Taskleiste, Desktop-Icons oder Papierkorb.

### 30.6 Keine sensiblen Daten in V0.1

Die erste Version verwendet Dummy-Daten und keine echten persönlichen Informationen.

---

## 31. Akzeptanzkriterien für V0.1

V0.1 gilt als erfüllt, wenn:

1. Die Solution erfolgreich gebaut werden kann.
2. Die WPF-App startet.
3. Ein Dashboard-Fenster sichtbar wird.
4. Mindestens sechs Dummy-Karten angezeigt werden:
   - Wetter
   - Aufgaben
   - Kalender
   - Nachrichten
   - Systemstatus
   - SASD-Projekte
5. Das Fenster auf verschiedenen Größen sinnvoll reagiert.
6. Eine Grundkonfiguration existiert oder vorbereitet ist.
7. Fensterpositionen sicher gespeichert oder vorbereitet werden.
8. Bei ungültiger Fensterposition ein sicherer Fallback auf den primären Monitor erfolgt.
9. Keine externen APIs notwendig sind.
10. Keine sensiblen Daten verarbeitet werden.
11. README und Dokumentation den Entwicklungsstand erklären.
12. Das Projekt mit `dotnet build` gebaut werden kann.

---

## 32. Akzeptanzkriterien für V1.0

V1.0 gilt als produktiv nutzbar, wenn:

1. Das Dashboard stabil im Alltag läuft.
2. Laptopbetrieb und Mehrmonitorbetrieb sinnvoll unterstützt werden.
3. Wetterdaten real angezeigt werden.
4. Aufgaben oder Tagesfokus produktiv nutzbar sind.
5. Kalender oder Terminmodul mindestens eine einfache Datenquelle unterstützt.
6. RSS-News konfigurierbar sind.
7. Systemstatusdaten angezeigt werden.
8. Privacy Mode funktioniert.
9. Tray-Menü vorhanden ist.
10. Autostart optional möglich ist.
11. Konfiguration dokumentiert ist.
12. Offline-Verhalten nicht zu Abstürzen führt.
13. Cache-Verhalten nachvollziehbar ist.
14. Keine Secrets ins Repository gelangen.
15. Dokumentation ausreichend ist, um das Programm zu installieren und zu nutzen.

---

## 33. Risiken und Gegenmaßnahmen

### 33.1 Risiko: Dashboard wird zu überladen

Gegenmaßnahme:

- V0.1 mit Dummy-Karten und klarer Priorisierung
- Compact Mode bewusst klein halten
- Nachrichten begrenzen
- Fokus-Modus anbieten

### 33.2 Risiko: Multi-Monitor-Verhalten wird instabil

Gegenmaßnahme:

- Monitorprofile früh entwickeln
- Fallback-Logik früh testen
- keine Shell-Hacks
- Fenster immer sichtbar halten

### 33.3 Risiko: Performance leidet

Gegenmaßnahme:

- Timer sparsam verwenden
- Aktualisierungsintervalle definieren
- keine Daueranimationen
- Caching
- Eco-Modus

### 33.4 Risiko: Datenschutzprobleme

Gegenmaßnahme:

- Privacy Mode
- keine sensiblen Logs
- lokale Konfiguration schützen
- V0.1 nur Dummy-Daten

### 33.5 Risiko: API-Abhängigkeiten

Gegenmaßnahme:

- modulare Datenquellen
- Offline-Cache
- lokale Quellen zuerst
- externe APIs erst nach stabiler Shell

---

## 34. Empfohlene erste Implementierungsschritte

### Schritt 1: Repository-Grundstruktur

- Solution erstellen
- Projekte anlegen
- README aktualisieren
- `.gitignore` ergänzen
- Dokumente einfügen

### Schritt 2: WPF-Shell

- Hauptfenster
- Dashboard-Layout
- Dummy-Karten
- dunkles Grundtheme

### Schritt 3: Konfiguration

- Settings-Modell
- Beispielkonfiguration
- Laden/Speichern vorbereiten

### Schritt 4: Monitorservice

- Monitore erkennen
- primären Monitor bestimmen
- Fensterposition validieren
- Fallback implementieren

### Schritt 5: Modi vorbereiten

- Dashboard Mode
- Compact Mode
- Privacy Mode als UI-Schalter

### Schritt 6: erste Tests

- Build-Test
- einfache Unit Tests
- manuelle UI-Tests

---

## 35. Offene Entscheidungen

Folgende Entscheidungen sind noch offen und sollen in späteren Dokumenten oder Architekturentscheidungen konkretisiert werden:

1. WPF-only oder WPF mit optionalem WebView2?
2. JSON-Datei oder SQLite für lokale Aufgaben und Fokusdaten?
3. Welche Wetterdatenquelle wird zuerst verwendet?
4. Welche Kalenderquelle wird zuerst unterstützt?
5. Wie stark soll TaskHost integriert werden?
6. Welche Icons und visuelle Identität erhält das Produkt?
7. Soll das Repository langfristig öffentlich bleiben?
8. Welche Lizenz soll verwendet werden?
9. Wird ein Installer benötigt, und wenn ja, welcher?
10. Sollen Plugins unterstützt werden?

---

## 36. Zusammenfassung

Das SASD Personal Desktop Dashboard soll ein ruhiger, professioneller Windows-Leitstand werden. Es zeigt wichtige Informationen dort an, wo sie nützlich sind: auf dem Laptop kompakt, am Arbeitsplatz großflächig auf einem zweiten oder dritten Monitor.

Die Anwendung soll bewusst nicht den Windows-Desktop ersetzen, sondern ergänzen. Desktop-Icons, Papierkorb und Explorer bleiben unangetastet. Der Schwerpunkt liegt auf guter Bedienbarkeit, stabiler Monitorbehandlung, Datenschutz, geringer Systemlast und modularer Erweiterbarkeit.

Die erste Version soll eine technische Shell mit Dummy-Daten liefern. Erst danach werden echte Datenquellen wie Wetter, Aufgaben, Kalender, RSS-News, Systemstatus und SASD-Projektinformationen schrittweise ergänzt.

Damit entsteht ein Projekt, das sowohl persönlich nützlich ist als auch gut zu SASD-GmbH als technisches Portfolio- und Produktprojekt passt.
