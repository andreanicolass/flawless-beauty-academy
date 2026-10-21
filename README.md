# Flawless Beauty Academy

En webbplats för en utbildningsakademi inom estetiska behandlingar (fillers, botox och microneedling), byggd med ASP.NET Core MVC som skolprojekt.

## Funktioner

- **Kurssidor** för Fillers, Botox och Microneedling med kursinformation och intresseanmälan
- **Intresseanmälan** som sparas i en SQLite-databas, med validering och felmeddelanden på svenska
- **Inloggning** med ASP.NET Core Identity och två roller:
  - **Admin** – adminpanel med statistik och en lista över alla anmälningar (filtrering och sökning)
  - **Student** – profilsida med flikar för studier, diplom och köphistorik (prototyp)
- **Responsiv design** som fungerar på både dator och mobil

## Tekniker

- ASP.NET Core MVC (.NET 9)
- Entity Framework Core med SQLite
- ASP.NET Core Identity
- Razor-vyer, HTML, CSS och lite JavaScript

## Kom igång

Krav: [.NET 9 SDK](https://dotnet.microsoft.com/download)

```bash
git clone <länk-till-repot>
cd AppUtbildning
dotnet run --project AppUtbildning/AppUtbildning.csproj
```

Öppna sedan `http://localhost:5281` i webbläsaren. Databasen (`RegisterDB.db`) och testkontona skapas automatiskt första gången appen startar.

Du kan också öppna `AppUtbildning.sln` i JetBrains Rider eller Visual Studio och köra projektet därifrån.

## Testkonton

Testkontona skapas från `AppUtbildning/appsettings.Development.json` och finns bara i utvecklingsläge.

| Roll    | E-post               | Lösenord      |
|---------|----------------------|---------------|
| Admin   | admin@admin.com      | Admin123!     |
| Student | sara@student.se      | Student123!   |
| Student | emma@student.se      | Student123!   |
| Student | johanna@student.se   | Student123!   |

> Lösenorden är endast för lokal testning och ska bytas om sidan någon gång publiceras på riktigt.

## Projektstruktur

```
AppUtbildning/
├── Controllers/        Home, Register (anmälningar) och Account (inloggning)
├── Models/             Register (anmälan), databaskontext, seed-data och adminöversikt
├── Views/
│   ├── Home/           Startsida, kurssidor, Om oss och profil
│   ├── Register/       Anmälningslista, ny anmälan och bekräftelse
│   └── Shared/         Layout, gemensamt anmälningsformulär och felsida
└── wwwroot/            CSS och bilder
```
