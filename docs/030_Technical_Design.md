# SASD Personal Desktop Dashboard – Technical Design

**Projekt:** SASD Personal Desktop Dashboard  
**Repository:** `SASD-PersonalDesktopDashboard`  
**Dokument:** 030_Technical_Design.md  
**Dokumenttyp:** Technisches Architektur- und Entwurfsdokument  
**Version:** 0.1  
**Status:** Entwurf für V0.1 / MVP-Grundlage  
**Datum:** 2026-05-13  
**Autor:** Robin Goerlach / SASD-GmbH – Scientific and Software Development  
**Sprache:** Deutsch  

---

## 1. Zweck dieses Dokuments

Dieses Dokument beschreibt die technische Architektur des **SASD Personal Desktop Dashboard**. Ziel ist eine robuste, wartbare und erweiterbare Windows-Desktop-Anwendung, die als persönlicher Arbeitsleitstand dient.

Das Dashboard soll Wetter, Aufgaben, Kalenderinformationen, Nachrichten, Systemstatus und SASD-Projektinformationen anzeigen. Dabei soll es unterschiedliche Nutzungssituationen unterstützen:

- Laptop allein unterwegs
- Laptop an Dockingstation
- Desktop-Arbeitsplatz mit zwei oder drei Monitoren
- Präsentations- oder Bildschirmfreigabe-Situationen
- konzentriertes Arbeiten ohne Ablenkung
- später eventuell Wallboard-/Ambient-Betrieb

Dieses Dokument beschreibt nicht nur die gewünschte technische Lösung, sondern auch bewusste Abgrenzungen. Besonders wichtig ist, dass das Dashboard **den Windows-Desktop nicht ersetzt**, sondern ergänzt. Windows Explorer bleibt für Desktop-Icons, Papierkorb, Kontextmenüs und Dateiverknüpfungen zuständig.

---

## 2. Architekturziele

Die Architektur soll folgenden Zielen dienen:

1. **Stabilität**  
   Die Anwendung darf den Windows-Desktop nicht destabilisieren, keine Shell-Hacks voraussetzen und bei Monitorwechseln nicht unsichtbar außerhalb des sichtbaren Bereichs landen.

2. **Wartbarkeit**  
   Der Code soll klar in Projekte, Schichten und Module getrennt werden. Änderungen an Wetter, Aufgaben, Nachrichten oder Systemstatus dürfen nicht den Anwendungskern destabilisieren.

3. **Erweiterbarkeit**  
   Neue Dashboard-Karten und Datenmodule sollen später ohne größere Umbauten ergänzt werden können.

4. **Performance und Energieeffizienz**  
   Die Anwendung darf Windows nicht spürbar ausbremsen. Daten sollen asynchron geladen, gecacht und nur mit sinnvollen Intervallen aktualisiert werden.

5. **Datenschutz und Privacy-by-Design**  
   Da Aufgaben, Kalender und Projektinformationen persönliche oder geschäftliche Daten enthalten können, muss das Dashboard Datenschutz- und Sichtbarkeitsmodi unterstützen.

6. **Multi-Monitor-Fähigkeit**  
   Die Anwendung muss unterschiedliche Monitor-Konfigurationen erkennen, speichern und bei Änderungen sauber reagieren.

7. **V1-fähige Basis**  
   V0.1 soll bewusst klein bleiben, aber so gebaut werden, dass spätere echte Datenmodule sauber integriert werden können.

---

## 3. Grundsatzentscheidung: eigenständige Windows-App statt Desktop-Ersatz

Das Dashboard wird als eigenständige Windows-Desktop-Anwendung umgesetzt.

Es wird **nicht** versucht:

- den Windows-Desktop zu ersetzen,
- den Explorer-Desktop zu manipulieren,
- Papierkorb oder Desktop-Icons selbst zu zeichnen,
- Fenster hinter Desktop-Icons einzuhängen,
- Windows-Shell-Hacks für Live-Wallpaper-Funktionalität zu verwenden.

Begründung:

- Desktop-Icons und Papierkorb sind Teil der Windows-Shell.
- Shell-Manipulationen sind fragil und können nach Windows-Updates brechen.
- Ein Dashboard muss auf mehreren Monitoren und in verschiedenen Situationen zuverlässig funktionieren.
- Für ein SASD-Produkt sind Stabilität und Wartbarkeit wichtiger als ein spektakulärer Wallpaper-Effekt.

Das Dashboard läuft daher als eigenes Fenster beziehungsweise als Tray-Anwendung mit verschiedenen Anzeigearten.

---

## 4. Empfohlener Technologiestack

### 4.1 Zielplattform

- Betriebssystem: Windows 10/11, Schwerpunkt Windows 11
- Laufzeitumgebung: .NET 8
- Sprache: C#
- UI-Technologie: WPF
- Architektur: modulare Desktop-Anwendung mit Core-, Infrastructure-, Module- und App-Schicht
- Tests: xUnit für Core-Logik

### 4.2 Warum WPF?

WPF ist für dieses Projekt eine gute Wahl, weil:

- es für klassische Windows-Desktop-Anwendungen stabil und bewährt ist,
- Fensterpositionierung, Tray-Verhalten und Multi-Monitor-Szenarien gut integrierbar sind,
- Layouts deklarativ über XAML erstellt werden können,
- Datenbindung und MVVM-Strukturen unterstützt werden,
- .NET 8 moderne C#-Entwicklung ermöglicht,
- keine zusätzliche Web-Runtime für V0.1 nötig ist.

### 4.3 Warum nicht zuerst HTML/CSS/JavaScript?

HTML/CSS/JavaScript kann später für einzelne Dashboard-Views oder eine WebView2-basierte Variante interessant sein. Für V0.1 wird es jedoch nicht als Kerntechnologie benötigt.

Gründe gegen WebView2 als primäre V0.1-Basis:

- zusätzliche Runtime-Abhängigkeit,
- mehr Komplexität bei Host-Kommunikation,
- potenzielle Overhead-Probleme,
- Sicherheits- und Sandboxing-Fragen bei lokalen/externen Inhalten,
- bei V0.1 noch kein Vorteil gegenüber nativer WPF-UI.

Das Projekt soll aber so gestaltet werden, dass eine spätere WebView2-Integration nicht ausgeschlossen wird.

---

## 5. Solution-Struktur

Die Solution soll klar in Verantwortungsbereiche getrennt werden.

```text
SASD-PersonalDesktopDashboard/
├── README.md
├── LICENSE
├── .gitignore
├── Sasd.PersonalDesktopDashboard.sln
│
├── docs/
│   ├── 010_Lastenheft.md
│   ├── 020_Pflichtenheft_MVP.md
│   ├── 030_Technical_Design.md
│   ├── 040_UI_Concept.md
│   └── 050_Roadmap.md
│
├── assets/
│   ├── icons/
│   ├── mockups/
│   └── screenshots/
│
├── src/
│   ├── Sasd.PersonalDesktopDashboard.App/
│   ├── Sasd.PersonalDesktopDashboard.Core/
│   ├── Sasd.PersonalDesktopDashboard.Infrastructure/
│   └── Sasd.PersonalDesktopDashboard.Modules/
│
└── tests/
    └── Sasd.PersonalDesktopDashboard.Core.Tests/
```

---

## 6. Projektverantwortlichkeiten

### 6.1 `Sasd.PersonalDesktopDashboard.App`

Das App-Projekt enthält die WPF-Anwendung.

Verantwortlichkeiten:

- Startpunkt der Anwendung
- XAML-Fenster und Views
- ViewModels für UI-Zustände
- Tray-Icon-Integration
- Fensterverwaltung
- User-Interaktion
- Anzeige der Dashboard-Karten
- Hotkeys
- Theme-Auswahl
- Zusammensetzen der Services aus Core, Infrastructure und Modules

Das App-Projekt darf Infrastruktur-Services verwenden, soll aber keine fachliche Logik enthalten, die in Core gehört.

Typische Ordner:

```text
Sasd.PersonalDesktopDashboard.App/
├── App.xaml
├── App.xaml.cs
├── MainWindow.xaml
├── MainWindow.xaml.cs
├── Views/
│   ├── DashboardWindow.xaml
│   ├── CompactSidebarWindow.xaml
│   ├── SettingsWindow.xaml
│   └── AboutWindow.xaml
├── ViewModels/
│   ├── DashboardViewModel.cs
│   ├── DashboardCardViewModel.cs
│   ├── CompactSidebarViewModel.cs
│   └── SettingsViewModel.cs
├── Controls/
│   ├── DashboardCard.xaml
│   ├── WeatherCard.xaml
│   ├── TaskCard.xaml
│   ├── CalendarCard.xaml
│   ├── NewsCard.xaml
│   └── SystemStatusCard.xaml
├── Themes/
│   ├── Colors.xaml
│   ├── Typography.xaml
│   ├── Spacing.xaml
│   └── DarkGlassTheme.xaml
└── Services/
    ├── TrayIconService.cs
    ├── WindowPlacementService.cs
    └── HotkeyService.cs
```

### 6.2 `Sasd.PersonalDesktopDashboard.Core`

Das Core-Projekt enthält fachliche Modelle, Schnittstellen, Enumerationen und domänennahe Logik.

Verantwortlichkeiten:

- zentrale Modelle
- Modulverträge
- Kartenmodelle
- Dashboard-Zustand
- Konfigurationsmodelle
- Monitorprofil-Modelle
- Regeln für Layoutauswahl
- Privacy- und Display-Modi als fachliche Konzepte
- keine WPF-Abhängigkeit
- keine direkten Windows-API-Abhängigkeiten
- keine HTTP-Implementierungen

Core soll testbar sein und möglichst wenig externe Abhängigkeiten besitzen.

Typische Ordner:

```text
Sasd.PersonalDesktopDashboard.Core/
├── Abstractions/
│   ├── IDashboardModule.cs
│   ├── IDashboardDataProvider.cs
│   ├── IConfigurationStore.cs
│   ├── IClock.cs
│   ├── ILogger.cs
│   ├── ICacheStore.cs
│   └── IMonitorProfileService.cs
├── Models/
│   ├── DashboardCard.cs
│   ├── DashboardCardState.cs
│   ├── DashboardDataSnapshot.cs
│   ├── DashboardLayoutProfile.cs
│   ├── DisplayProfile.cs
│   ├── MonitorInfo.cs
│   ├── RefreshPolicy.cs
│   └── PrivacyMode.cs
├── Configuration/
│   ├── DashboardSettings.cs
│   ├── ModuleSettings.cs
│   ├── WindowSettings.cs
│   └── ThemeSettings.cs
├── Layout/
│   ├── LayoutMode.cs
│   ├── LayoutBreakpoint.cs
│   └── LayoutSelectionService.cs
├── Modules/
│   ├── ModuleId.cs
│   ├── ModuleStatus.cs
│   └── ModuleRefreshResult.cs
└── Diagnostics/
    ├── DashboardError.cs
    └── DashboardHealthState.cs
```

### 6.3 `Sasd.PersonalDesktopDashboard.Infrastructure`

Das Infrastructure-Projekt enthält technische Implementierungen.

Verantwortlichkeiten:

- Lesen und Schreiben von Konfigurationsdateien
- JSON-Serialisierung
- HTTP-Client-Kapselung
- lokaler Cache
- Windows-spezifische Monitor- und Power-State-Erkennung
- Dateisystemzugriffe
- Logging-Implementierung
- eventuell später verschlüsselte Secret-Speicherung
- Integration mit Windows-Autostart

Typische Ordner:

```text
Sasd.PersonalDesktopDashboard.Infrastructure/
├── Configuration/
│   ├── JsonConfigurationStore.cs
│   └── ConfigurationDefaults.cs
├── Caching/
│   ├── FileCacheStore.cs
│   └── MemoryCacheStore.cs
├── Http/
│   ├── DashboardHttpClient.cs
│   └── HttpRetryPolicy.cs
├── Windows/
│   ├── MonitorDetectionService.cs
│   ├── PowerStateService.cs
│   ├── StartupRegistrationService.cs
│   └── DesktopKnownFolderService.cs
├── Logging/
│   ├── FileLogger.cs
│   └── DebugLogger.cs
└── Storage/
    ├── AppDataPathProvider.cs
    └── JsonFileStore.cs
```

### 6.4 `Sasd.PersonalDesktopDashboard.Modules`

Das Modules-Projekt enthält die konkreten Dashboard-Module. In V0.1 liefern viele Module zunächst Dummy-Daten oder statische Beispielwerte.

Verantwortlichkeiten:

- Wettermodul
- Aufgabenmodul
- Kalendermodul
- Nachrichtenmodul
- Systemstatusmodul
- SASD-Projektmodul
- spätere Integrationen mit externen Diensten

Typische Ordner:

```text
Sasd.PersonalDesktopDashboard.Modules/
├── Weather/
│   ├── WeatherModule.cs
│   ├── WeatherSnapshot.cs
│   └── WeatherSettings.cs
├── Tasks/
│   ├── TasksModule.cs
│   ├── TaskItem.cs
│   └── TaskSettings.cs
├── Calendar/
│   ├── CalendarModule.cs
│   ├── CalendarEventItem.cs
│   └── CalendarSettings.cs
├── News/
│   ├── NewsModule.cs
│   ├── NewsItem.cs
│   └── NewsSettings.cs
├── SystemStatus/
│   ├── SystemStatusModule.cs
│   ├── SystemStatusSnapshot.cs
│   └── SystemStatusSettings.cs
└── SasdProjects/
    ├── SasdProjectsModule.cs
    ├── ProjectStatusItem.cs
    └── SasdProjectsSettings.cs
```

### 6.5 `Sasd.PersonalDesktopDashboard.Core.Tests`

Dieses Projekt testet Core-Logik, insbesondere:

- Layoutauswahl bei unterschiedlichen Bildschirmgrößen
- Monitorprofil-Matching
- Refresh-Policy-Entscheidungen
- Privacy-Mode-Regeln
- Validierung von Konfigurationsobjekten
- Fallback-Verhalten bei ungültigen Einstellungen

---

## 7. Schichtenmodell

Das Projekt soll einem klaren Schichtenmodell folgen.

```text
UI / WPF App
    ↓
ViewModels / Presentation Logic
    ↓
Application Services
    ↓
Core Models and Interfaces
    ↓
Infrastructure Implementations
    ↓
External Sources / Windows / Files / APIs
```

### 7.1 UI-Schicht

Die UI-Schicht ist für Darstellung und Benutzerinteraktion zuständig.

Sie darf:

- Karten anzeigen,
- Zustände visualisieren,
- Benutzeraktionen entgegennehmen,
- ViewModels binden.

Sie soll nicht:

- externe APIs direkt aufrufen,
- Konfigurationsdateien direkt schreiben,
- komplexe Geschäftsregeln enthalten,
- Windows-Systeminformationen direkt ohne Service-Kapselung abfragen.

### 7.2 Core-Schicht

Die Core-Schicht enthält fachliche Konzepte und Regeln.

Sie darf:

- Dashboard-Modelle definieren,
- Layout-Entscheidungen treffen,
- Schnittstellen definieren,
- Validierungslogik enthalten.

Sie soll nicht:

- WPF-Typen referenzieren,
- konkrete Dateipfade kennen,
- HTTP-Aufrufe durchführen,
- Windows-API direkt verwenden.

### 7.3 Infrastructure-Schicht

Die Infrastructure-Schicht enthält konkrete technische Implementierungen.

Sie darf:

- Dateien lesen und schreiben,
- HTTP-Aufrufe durchführen,
- Windows-APIs kapseln,
- Cache-Dateien verwalten,
- Systeminformationen auslesen.

Sie soll:

- technische Details hinter Interfaces verstecken,
- keine UI-Logik enthalten,
- robust gegen Fehler sein.

### 7.4 Modules-Schicht

Die Modules-Schicht stellt Daten für einzelne Dashboard-Karten bereit.

Jedes Modul soll nach außen einen einheitlichen Vertrag erfüllen:

- Modul-ID
- Anzeigename
- Status
- Daten-Snapshot
- Aktualisierungsintervall
- Fehlerzustand
- Privacy-Filterung

---

## 8. Modulkonzept

### 8.1 Grundidee

Jede Dashboard-Karte wird von einem Modul gespeist. Ein Modul kann Daten aus lokalen Quellen, Konfigurationsdateien, Windows-APIs oder später externen APIs beziehen.

Ein Modul ist nicht identisch mit einer UI-Karte. Ein Modul stellt Daten bereit; die UI entscheidet, wie diese Daten dargestellt werden.

Beispiel:

```text
WeatherModule → WeatherSnapshot → WeatherCard
TasksModule   → TaskListSnapshot → TaskCard
NewsModule    → NewsSnapshot    → NewsCard
```

### 8.2 Basisschnittstelle für Module

Vorgesehene Kernidee:

```csharp
/// <summary>
/// Represents a data module that provides information for one or more dashboard cards.
/// </summary>
public interface IDashboardModule
{
    ModuleId Id { get; }

    string DisplayName { get; }

    RefreshPolicy RefreshPolicy { get; }

    Task<ModuleRefreshResult> RefreshAsync(
        DashboardContext context,
        CancellationToken cancellationToken);
}
```

Die genaue Implementierung kann später angepasst werden. Wichtig ist die Trennung zwischen Modul, Daten-Snapshot und UI.

### 8.3 Modulzustände

Jedes Modul soll einen Zustand melden:

```text
NotConfigured
Loading
Ready
Warning
Error
Offline
Disabled
```

Die UI soll diese Zustände ruhig anzeigen. Ein Modulfehler darf nicht die gesamte Anwendung abbrechen.

### 8.4 Fehlerverhalten

Wenn ein Modul fehlschlägt:

- wird der Fehler protokolliert,
- der letzte gültige Cachewert bleibt sichtbar,
- die Karte zeigt einen dezenten Hinweis,
- die Anwendung bleibt stabil,
- andere Module werden weiter aktualisiert.

---

## 9. Datenmodule

### 9.1 Wettermodul

Das Wettermodul soll später Wetterinformationen für den aktuellen Standort oder einen konfigurierten Ort anzeigen.

V0.1:

- Dummy-Daten
- Kartenlayout vorbereiten
- Modell für Wetterdaten vorbereiten

V1:

- aktuelle Temperatur
- Wetterzustand
- Regenwahrscheinlichkeit
- Wind
- Vorhersage für die nächsten Stunden
- letzte Aktualisierung
- Fehler-/Offline-Hinweis

Später:

- Warnungen
- mehrere Orte
- manuelle Ortseinstellung
- Standortermittlung nur mit ausdrücklicher Zustimmung

### 9.2 Aufgabenmodul

Das Aufgabenmodul soll die nächsten zu erledigenden Aufgaben anzeigen.

V0.1:

- Dummy-Aufgaben
- Modell für TaskItem
- Darstellung von Top-Aufgaben

V1:

- lokale Aufgabenliste aus Konfiguration oder lokaler JSON-Datei
- Anzeige von:
  - heute fällig
  - überfällig
  - nächste wichtige Aufgaben
  - erledigt/offen
- Privacy-Mode-Unterstützung

Später:

- TaskHost-Integration
- Microsoft To Do
- lokale SQLite-Aufgabenverwaltung
- Projektzuordnung
- Prioritäten
- Wiederholungen

### 9.3 Kalendermodul

Das Kalendermodul soll die nächsten Termine anzeigen.

V0.1:

- Dummy-Termine
- UI und Datenmodell vorbereiten

V1:

- lokale Kalenderquelle oder manuell gepflegte Termine
- Anzeige nächster Termin
- Anzeige Tagesübersicht
- Privacy Mode mit anonymisierten Terminen

Später:

- ICS-Kalender
- CalDAV
- Microsoft/Google-Anbindung nur nach bewusster Entscheidung
- Konfliktanzeige
- Reisezeit/Ortshinweise

### 9.4 Nachrichtenmodul

Das Nachrichtenmodul soll aktuelle Informationen anzeigen.

V0.1:

- Dummy-News
- RSS-Datenmodell vorbereiten

V1:

- RSS-Feeds
- Kategorien:
  - Lokal
  - Welt
  - IT/Security
  - Wissenschaft
- Anzeige Titel, Quelle, Uhrzeit
- keine ablenkenden Bilder als Pflichtbestandteil

Später:

- Quellenverwaltung
- Filter
- Schlagwortsuche
- Zusammenfassungen
- Speicherung gelesener Artikel

### 9.5 Systemstatusmodul

Das Systemstatusmodul zeigt den Zustand des lokalen Rechners.

V0.1:

- Dummy-/Basiswerte
- UI-Karte vorbereiten

V1:

- Akku-/Netzbetrieb
- CPU-Auslastung grob
- RAM-Auslastung grob
- freier Speicherplatz
- Netzwerkstatus
- letzte Aktualisierung

Später:

- detaillierte Laufwerksübersicht
- Windows-Update-Hinweise
- Backupstatus
- Dienstestatus
- Ereignisprotokoll-Auswertung

### 9.6 SASD-Projektmodul

Das SASD-Projektmodul zeigt Informationen zu eigenen Projekten.

V0.1:

- statische Beispielprojekte
- Kartenlayout

V1:

- lokale Projektliste
- Projektstatus
- nächste Schritte
- Repository-Link als Text/Metadatum

Später:

- GitHub-Integration
- offene Issues
- letzter Commit
- lokale Git-Repositories
- Build-/Release-Status

---

## 10. Fenster- und Anzeigearten

### 10.1 Dashboard Mode

Großes Dashboard-Fenster, bevorzugt auf zweitem oder drittem Monitor.

Eigenschaften:

- kachelbasiertes Layout
- mehrere Spalten
- gut aus größerer Entfernung lesbar
- nicht zwingend always-on-top
- Vollbild oder randlos möglich
- geeignet für Dockingstation/Arbeitsplatz

### 10.2 Compact Mode

Kompakte Sidebar für Laptopbetrieb.

Eigenschaften:

- schmale Seitenleiste
- wenige Karten
- keine Überladung
- einklappbar
- reduzierte Aktualisierung möglich
- geeignet für kleine Bildschirme

### 10.3 Focus Mode

Minimalansicht für konzentriertes Arbeiten.

Eigenschaften:

- nur Tagesfokus
- nächste Aufgabe
- nächster Termin
- eventuell Wetter-Kurzinfo
- keine Nachrichtenflut

### 10.4 Privacy Mode

Datenschutzansicht für Bildschirmfreigabe, Besuch oder Kundensituation.

Eigenschaften:

- Aufgaben anonymisieren
- Kalendereinträge anonymisieren
- Projektinformationen optional ausblenden
- Nachrichten/Wetter/Systemstatus bleiben möglich
- schnell per Hotkey aktivierbar

### 10.5 Silent Mode

Tray-Modus ohne sichtbares Dashboard.

Eigenschaften:

- App läuft im Hintergrund
- keine dauerhafte Anzeige
- Zugriff über Tray-Icon
- geeignet für Präsentationen oder leistungsschwache Situationen

---

## 11. Multi-Monitor-Konzept

### 11.1 Monitorerkennung

Die Anwendung soll beim Start und bei Display-Änderungen erkennen:

- Anzahl der Monitore
- primärer Monitor
- Arbeitsbereich je Monitor
- Auflösung je Monitor
- Position im virtuellen Desktop
- Skalierung/DPI soweit verfügbar
- gespeicherte Fensterposition

### 11.2 Monitorprofile

Ein Monitorprofil beschreibt, wie sich das Dashboard in einer bestimmten Umgebung verhalten soll.

Beispiele:

```text
Laptop unterwegs:
- genau ein Monitor
- Compact Mode
- rechts angedockt
- Eco-Aktualisierung

Büro Dockingstation:
- zwei oder mehr Monitore
- Dashboard Mode
- bevorzugt zweiter Monitor
- normales Aktualisierungsprofil

Präsentation:
- Silent Mode
- Privacy Mode aktiv
```

### 11.3 Fallback bei fehlendem Monitor

Wenn ein gespeicherter Monitor nicht vorhanden ist:

1. Fenster auf primären Monitor verschieben.
2. Größe begrenzen.
3. Compact Mode aktivieren.
4. Hinweis im Statusbereich anzeigen.
5. Keine unsichtbaren Fenster außerhalb des Bildschirms.

### 11.4 Fensterpositionen

Fensterpositionen dürfen nicht blind wiederhergestellt werden. Vor jeder Wiederherstellung muss geprüft werden:

- Liegt das Fenster im sichtbaren Arbeitsbereich?
- Ist mindestens ein sinnvoller Teil sichtbar?
- Ist die Größe passend zur aktuellen Auflösung?
- Hat sich die DPI-Skalierung verändert?
- Existiert der bevorzugte Monitor noch?

---

## 12. Layout- und DPI-Konzept

Das Dashboard darf nicht pixelstarr aufgebaut sein.

### 12.1 Breakpoints

Vorgesehene Layout-Breakpoints:

```text
< 1200 px Breite:
  Compact / eine Spalte

1200–1800 px:
  zwei Spalten

1800–2500 px:
  drei Spalten

> 2500 px:
  drei bis vier Spalten, größere Karten
```

### 12.2 Kartengrößen

Jede Karte soll definieren können:

- minimale Breite
- bevorzugte Breite
- minimale Höhe
- bevorzugte Höhe
- Wichtigkeit
- Sichtbarkeit in Modi

### 12.3 DPI und Skalierung

Die Anwendung soll so gestaltet werden, dass sie bei 100 %, 125 %, 150 % und 200 % Skalierung nutzbar bleibt.

Dazu gehören:

- relative Layouts
- keine hart codierten Pixelwerte für zentrale UI-Struktur
- Mindestgrößen
- Textumbruch
- ausreichend große Schrift
- keine übermäßig kleinen Icons
- keine abgeschnittenen Inhalte bei hoher Skalierung

---

## 13. Konfiguration

### 13.1 Konfigurationsdatei

Die Anwendung soll eine lokale Konfiguration verwenden. Für V0.1 reicht JSON.

Vorgeschlagener Speicherort:

```text
%APPDATA%\SASD\PersonalDesktopDashboard\dashboard.settings.json
```

Die Repository-Beispiele dürfen eine Beispielkonfiguration enthalten, aber keine echten Tokens oder privaten Daten.

### 13.2 Beispielstruktur

```json
{
  "app": {
    "startWithWindows": false,
    "defaultMode": "Auto",
    "language": "de-DE"
  },
  "privacy": {
    "privacyModeHotkey": "Ctrl+Alt+P",
    "hideTaskDetails": true,
    "hideCalendarDetails": true
  },
  "displayProfiles": [
    {
      "name": "Laptop unterwegs",
      "minMonitorCount": 1,
      "maxMonitorCount": 1,
      "mode": "Compact",
      "powerProfile": "Eco"
    },
    {
      "name": "Büro",
      "minMonitorCount": 2,
      "mode": "Dashboard",
      "preferredMonitor": "Secondary",
      "powerProfile": "Normal"
    }
  ],
  "modules": {
    "weather": {
      "enabled": true,
      "refreshMinutes": 30
    },
    "tasks": {
      "enabled": true,
      "refreshMinutes": 5
    },
    "news": {
      "enabled": true,
      "refreshMinutes": 30
    }
  }
}
```

### 13.3 Validierung

Beim Laden der Konfiguration muss geprüft werden:

- Datei vorhanden?
- JSON gültig?
- Version kompatibel?
- Pflichtfelder vorhanden?
- Werte plausibel?
- Hotkeys gültig?
- Module bekannt?
- Aktualisierungsintervalle sinnvoll?

Bei fehlerhafter Konfiguration:

- Defaults verwenden,
- Fehler protokollieren,
- Benutzerhinweis anzeigen,
- Anwendung nicht abbrechen.

---

## 14. Caching und Offline-Verhalten

### 14.1 Grundsatz

Das Dashboard soll beim Start schnell eine Anzeige liefern, auch wenn Netzwerkquellen langsam oder nicht erreichbar sind.

### 14.2 Cache-Prinzip

- letzte gültige Daten pro Modul speichern,
- Cache mit Zeitstempel versehen,
- beim Start zuerst Cache anzeigen,
- Aktualisierung asynchron starten,
- Fehler dezent anzeigen.

### 14.3 Offline-Fall

Wenn keine Internetverbindung vorhanden ist:

- Wetter zeigt letzten Stand mit Hinweis,
- News zeigen letzte Überschriften mit Hinweis,
- lokale Aufgaben bleiben verfügbar,
- Systemstatus bleibt verfügbar,
- Dashboard bleibt nutzbar.

---

## 15. Aktualisierungs- und Performance-Konzept

### 15.1 Aktualisierungsintervalle

Die Anwendung darf keine unnötig kurzen Timer verwenden.

Vorgeschlagene Intervalle:

```text
Uhrzeit:             1 Minute
Wetter:              15–30 Minuten
Aufgaben:            1–5 Minuten
Kalender:            5–15 Minuten
News:                15–60 Minuten
Systemstatus:        5–10 Sekunden, nur wenn sichtbar
SASD-Projekte:       5–15 Minuten
Remote-Checks:       30–120 Sekunden oder länger
```

### 15.2 Sichtbarkeitsabhängige Aktualisierung

Wenn das Dashboard nicht sichtbar ist:

- UI-Updates reduzieren,
- Systemstatus seltener erfassen,
- keine permanenten Animationen,
- Netzwerkanfragen nur nach Policy.

### 15.3 Akku-Modus

Im Akkubetrieb soll ein Eco-Profil möglich sein:

- längere Aktualisierungsintervalle,
- weniger Systemabfragen,
- Animationen aus,
- keine häufigen Remote-Checks,
- optional Compact Mode.

### 15.4 UI-Rendering

Die UI soll nur aktualisiert werden, wenn Daten geändert wurden.

Zu vermeiden:

- permanente 60-FPS-Animationen,
- animierte Hintergrundvideos,
- aufwändige Transparenzeffekte auf schwacher Hardware,
- unnötig häufiges Neuberechnen des Layouts.

---

## 16. Datenschutz und Privacy Mode

### 16.1 Warum Privacy Mode?

Das Dashboard kann persönliche und geschäftliche Informationen anzeigen:

- Aufgaben
- Termine
- Projektstatus
- Nachrichteninteressen
- Systemzustand
- eventuell Kundennamen oder Bewerbungsdaten

Daher muss es möglich sein, sensible Inhalte schnell auszublenden.

### 16.2 Privacy-Regeln

Im Privacy Mode sollen Inhalte je nach Einstellung anonymisiert oder ausgeblendet werden.

Beispiele:

```text
Normale Anzeige:
"Angebot für Kunde Müller prüfen"

Privacy Mode:
"Private Aufgabe"
```

```text
Normale Anzeige:
"Arzttermin 14:30"

Privacy Mode:
"Termin 14:30"
```

### 16.3 Präsentationsmodus

Der Präsentationsmodus ist eine strengere Variante:

- Aufgaben ausblenden oder anonymisieren,
- Kalenderdetails ausblenden,
- Projektinterna ausblenden,
- Systemstatus optional sichtbar,
- Wetter/Uhr sichtbar,
- News optional aus.

### 16.4 Secrets

API-Keys, Tokens oder Zugangsdaten dürfen nicht im Repository liegen.

Falls später externe Dienste angebunden werden:

- Secrets nicht im Klartext in Beispielkonfigurationen,
- lokale sichere Speicherung prüfen,
- keine Anzeige sensibler Tokens im UI,
- keine Secrets in Logs schreiben.

---

## 17. Logging und Diagnose

### 17.1 Ziele

Logging soll bei Fehleranalyse helfen, darf aber keine privaten Daten unnötig speichern.

### 17.2 Log-Inhalte

Sinnvolle Log-Inhalte:

- Start/Stop der Anwendung
- geladene Konfiguration ohne Secrets
- erkannte Monitore
- aktives Displayprofil
- Modulfehler
- Netzwerkfehler
- Cachefehler
- unerwartete Exceptions

Nicht loggen:

- vollständige Aufgabeninhalte, wenn vermeidbar
- Kalenderdetails
- API-Tokens
- personenbezogene Inhalte
- vollständige externe Responses ohne Prüfung

### 17.3 Debug/Release

Für Entwicklung:

- detaillierteres Debug-Logging
- Konsolenausgabe optional im DEBUG-Build

Für Release:

- ruhiges Datei-Logging
- begrenzte Loggröße
- keine privaten Inhalte

---

## 18. Fehlerbehandlung

Die Anwendung soll robust gegen Fehler sein.

### 18.1 Fehlerkategorien

- Konfigurationsfehler
- Netzwerkfehler
- Modulfehler
- Cachefehler
- Monitorprofilfehler
- UI-Fehler
- unerwartete Exceptions

### 18.2 Benutzeranzeige

Fehler sollen nicht aggressiv stören.

Statt permanenter Popups:

- Statussymbol,
- dezenter Hinweis in Karte,
- Detailansicht im Einstellungs-/Diagnosefenster,
- Tray-Menüpunkt „Diagnose anzeigen“.

### 18.3 Fehlerisolierung

Ein Fehler in einem Modul darf nicht das gesamte Dashboard beenden.

Beispiel:

- News-Feed nicht erreichbar → News-Karte zeigt „nicht aktualisierbar“
- Wetter-API nicht erreichbar → Wetter-Karte zeigt Cache
- Systemstatusfehler → Systemkarte zeigt Warnung
- Anwendung bleibt lauffähig

---

## 19. Desktop-Icons und Papierkorb

### 19.1 Grundsatz

Das Dashboard unterstützt Desktop-Icons dadurch, dass es sie nicht ersetzt.

Windows Explorer bleibt zuständig für:

- Papierkorb
- Desktop-Dateien
- Verknüpfungen
- Rechtsklick-Menü
- Icon-Anordnung
- öffentliche und benutzerspezifische Desktop-Ordner

### 19.2 Keine eigene Desktop-Icon-Verwaltung in V0.1/V1

Das Projekt soll keine eigene Icon-Verwaltung implementieren.

Nicht Bestandteil:

- Desktop-Icons selbst zeichnen
- Papierkorb selbst anzeigen
- Verknüpfungen selbst verwalten
- Explorer-Kontextmenüs nachbauen
- Shell-Namespace integrieren

### 19.3 Spätere optionale Integration

Später denkbar:

- Anzeige einer kleinen Liste wichtiger Desktop-Verknüpfungen
- Schnellstarter-Karte
- Links zu Projektordnern
- Öffnen des Desktop-Ordners
- Anzeige „Desktop enthält X Dateien“

Das ersetzt aber nicht den Windows-Desktop.

---

## 20. Autostart und Tray-Verhalten

### 20.1 Tray-App

Die Anwendung soll über ein Tray-Icon steuerbar sein.

Tray-Menü:

```text
Dashboard anzeigen
Compact Mode anzeigen
Privacy Mode ein/aus
Präsentationsmodus ein/aus
Einstellungen
Diagnose
Beenden
```

### 20.2 Autostart

Autostart soll optional sein.

V0.1:

- Konfigurationsoption vorbereiten
- manuelle Aktivierung später möglich

V1:

- Start mit Windows aktivierbar/deaktivierbar
- keine erzwungene Autostart-Aktivierung

### 20.3 Startverhalten

Beim Start:

1. Konfiguration laden.
2. Monitore erkennen.
3. passendes Displayprofil wählen.
4. Datenmodule initialisieren.
5. Cache laden.
6. UI anzeigen oder Tray-Modus aktivieren.
7. Aktualisierungen asynchron starten.

---

## 21. Testkonzept

### 21.1 Unit-Tests

Unit-Tests sollen vor allem Core-Logik abdecken.

Beispiele:

- LayoutSelectionService wählt korrekten Modus
- Monitorprofil erkennt Laptop-Szenario
- ungültige Konfiguration fällt auf Defaults zurück
- Privacy Mode anonymisiert korrekt
- RefreshPolicy berechnet sinnvolle nächste Aktualisierung

### 21.2 Manuelle Tests

Für Desktop-Verhalten sind manuelle Tests nötig:

- Start auf einem Monitor
- Start mit zwei Monitoren
- Start mit drei Monitoren
- Abdocken während Anwendung läuft
- Andocken während Anwendung läuft
- Änderung der Skalierung
- Änderung der Auflösung
- Sleep/Wake
- Akku/Netzbetrieb
- Privacy Mode per Hotkey
- Tray-Menü

### 21.3 Spätere Integrationstests

Später möglich:

- Wetter-API mit Mockserver
- RSS-Feed mit Testdatei
- TaskHost-Testinstanz
- Konfigurationsmigrationen

---

## 22. V0.1-Umfang

V0.1 soll die technische Grundlage liefern.

Muss enthalten:

- .NET-8-WPF-Solution
- Projektstruktur App/Core/Infrastructure/Modules/Tests
- Dashboard-Hauptfenster
- Dummy-Karten
- Grundlayout
- einfache Konfigurationsklasse
- Monitorerkennung vorbereitet
- Fensterpositionierung vorbereitet
- Privacy Mode als Zustand vorbereitet
- Logging-Grundlage
- Build und Tests lauffähig

Soll enthalten:

- Tray-Icon-Grundlage
- Compact Mode als zweites Fenster oder Layout
- einfache Theme-Ressourcen
- Cache-Interfaces
- erste Unit-Tests

Nicht enthalten:

- echte Wetter-API
- echte Kalenderintegration
- echte Newsfeeds
- TaskHost-Integration
- Cloud-Anmeldung
- Shell-Hacks
- Live-Wallpaper-Modus

---

## 23. Spätere Architektur-Erweiterungen

### 23.1 Plugin-Modell

Später kann geprüft werden, ob Module als Plugins geladen werden sollen. Für V0.1 ist das nicht nötig.

### 23.2 WebView2

WebView2 kann später für folgende Fälle interessant sein:

- besonders flexible Dashboard-Karten
- HTML-basierte Themes
- lokale Mini-Webapps
- Diagrammkomponenten

Für V0.1 bleibt WPF ausreichend.

### 23.3 Lokale Datenbank

Eine lokale SQLite-Datenbank kann später sinnvoll sein für:

- Aufgaben
- gelesene Nachrichten
- Verlauf
- Projektstatus
- Konfigurationshistorie

Für V0.1 reicht JSON.

### 23.4 TaskHost-Integration

Langfristig kann das Dashboard ein Frontend für TaskHost-Aufgaben werden. Dazu muss TaskHost eine stabile lokale oder REST-basierte Schnittstelle anbieten.

---

## 24. Architekturentscheidungen

### ADR-001: WPF statt HTML-first

**Entscheidung:** V0.1 wird als WPF-App umgesetzt.  
**Begründung:** Bessere native Windows-Integration, geringere Laufzeitkomplexität, gute Eignung für Tray/Multi-Monitor.  
**Konsequenz:** HTML/WebView2 bleibt spätere Option.

### ADR-002: Dashboard ersetzt nicht den Windows-Desktop

**Entscheidung:** Explorer bleibt für Desktop-Icons und Papierkorb zuständig.  
**Begründung:** Stabilität und Wartbarkeit.  
**Konsequenz:** Kein Live-Wallpaper-Hack in V0.1.

### ADR-003: Module liefern Daten, UI zeigt Karten

**Entscheidung:** Datenmodule und UI-Karten werden getrennt.  
**Begründung:** Erweiterbarkeit und Testbarkeit.  
**Konsequenz:** WeatherModule ist nicht WeatherCard.

### ADR-004: JSON-Konfiguration für V0.1

**Entscheidung:** Konfiguration zunächst als JSON-Datei.  
**Begründung:** Einfach, transparent, versionierbar als Beispiel.  
**Konsequenz:** Spätere UI für Einstellungen möglich.

### ADR-005: Privacy Mode als Kernkonzept

**Entscheidung:** Privacy Mode wird nicht nachträglich angeflanscht, sondern früh als Zustand modelliert.  
**Begründung:** Dashboard kann sensible Daten anzeigen.  
**Konsequenz:** Module müssen später Privacy-Filterung unterstützen.

---

## 25. Offene Punkte

Folgende Punkte müssen später weiter entschieden werden:

- genaue Lizenz
- öffentliches oder privates Entwicklungsmodell
- Wetterdatenquelle
- Kalenderdatenquelle
- Aufgabenquelle
- News-Quellen
- ob SQLite benötigt wird
- ob WebView2 später integriert wird
- ob ein eigenes Iconset erstellt wird
- ob Installer/MSIX später benötigt wird
- ob das Dashboard als SASD-Produkt oder internes Tool geführt wird

---

## 26. Fazit

Das SASD Personal Desktop Dashboard soll als robuste, modulare WPF-Anwendung entstehen. Der Schwerpunkt liegt zunächst nicht auf möglichst vielen externen Datenquellen, sondern auf einer stabilen technischen Basis:

- saubere Solution-Struktur,
- klare Schichten,
- modulare Datenquellen,
- Multi-Monitor-Fähigkeit,
- Privacy Mode,
- Performance-Bewusstsein,
- keine Shell-Hacks,
- spätere Erweiterbarkeit.

Damit entsteht eine gute Grundlage für ein seriöses SASD-Projekt und nicht nur eine optische Desktop-Spielerei.
