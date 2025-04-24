## Jobs back-end API project
## API voor het beheren van bedrijven en vacatures

Dit project dient als voorbeeldproject voor de back-end challenge.

Doel:

Bedrijven beheren - CRUD (Aanmaken, ophalen, wijzigen en verwijderen) met filters
Vacatures beheren - CRUD (Aanmaken, ophalen, wijzigen en verwijderen) met filters

## Entiteiten
- Bedrijf 
- Vacature (van een bedrijf)

De definitie van een bedrijf, vastgesteld naar eigen idee:
- Naam (max 256 karakters)
- Locatie
- Website (optioneel)

De definitie van een locatie, geïnspireerd door internationale standaarden UPU S42 en ISO 19160‑1:
- Adresregels (min 1 regel, max 4)
- Stad (of dorp/lokaliteit)
- Staat/provincie
- Postcode (optioneel)
- Landcode (ISO 3166‑1 alpha‑2 zoals 'nl')
- Geocoordinaten (optioneel, latitude/longitude)

De definitie van een vacature bij een bedrijf, waar een bedrijf er meerdere van kan hebben, vastgesteld naar eigen idee:
- Titel (max 256 karakters)
- Beschrijving (max 4096 karakters, optioneel)
- Categorie (it, administratie, financieel, educatie, zorg, anders) (optioneel)
- Minimaal en maximaal aantal uur per week (optioneel)
- Remote werken mogelijk (optioneel)
- Contact e-mail (optioneel)
- Link naar vacature op website bedrijf (optioneel)

# Architectuur back-end project

.NET 9 ASP.NET Core API

Geen top-level statements. Geen globale usings om botsingen te voorkomen. 

Voor de architectuur ga ik bij dit project proberen Clean Architecture te volgen. Voor de syntax gebruik ik StyleCop.

### Projectstructuur
Het .NET project is opgebouwd volgens Clean Architecture principes met de volgende lagen:
- **Jobs.API**: API controllers, middleware, extensies en configuratie
- **Jobs.Application**: Business logica, services, DTOs en interfaces
- **Jobs.Domain**: Domeinmodellen, entities, value objects en domein logica
- **Jobs.Infrastructure**: Database toegang, repositories en externe services

### Gebruikte technologieën
Hierbij ga ik gebruik maken van de volgende NuGet packages:

- EF Core en gerelateerde packages voor SQLite
- Mapster (AutoMapper is commercieel gegaan)
- FluentValidation
- Ardalis.Specification
- Swagger

Er is pagination en zoekfunctionaliteit ingebouwd. De API ondersteunt filtering, sortering en paginering voor zowel bedrijven als vacatures.

### Ontwikkeltools
- Visual Studio 2022 of nieuwer
- .NET 9 SDK
- SQLite voor de database
- Entity Framework Core voor ORM

Zaken die ik nog had willen toevoegen maar ik geen tijd meer voor had:

- Authenticatie
- Wolverine (MediatR gaat commercieel)
- Streaming data (IAsyncEnumerable)
- Mapping op database niveau, echter heb ik bij complexere projecties hier nog geen goede oplossing voor gevonden
- Tests
- Schonere code: meer constants, minder repetitie, meer design patterns toepassen
- API controller versioning
- Separation of concerns perfect maken (zover mogelijk)
- Meer SRP
- Microservices, CQRS, Docker enzovoorts
- meer, vraag dit na

## Architectuur front-end project

Dit project dient als voorbeeldproject voor de front-end challenge.

Angular 19 standalone components, SCSS met Bootstrap en NGPrime. Signals, reactive forms.

Het project maakt gebruik van:
- Angular 19 standalone components met een zover mogelijk modulaire structuur
- Styling met SCSS, Bootstrap 5 en PrimeNG voor UI componenten
- Ngx-translate voor internationalisatie
- Angular Signals voor reactieve state management
- Lazy loading voor optimale performance

Ik genereer een API client op basis van de OpenAPI specificatie die de back-end genereert. Dit bespaart heel veel werk om models te beheren.

Er is iets minder tijd besteed aan het front-end project en hier hadden ook veel verbeteringen mogelijk geweest:
- Code duplicatie, herbruikbare componenten
- Tests
- Minder boilerplate code
- meer, vraag dit na

## Project uitvoeren

### Back-end starten
1. Ga naar de root van het project
2. Voer het volgende commando uit om de API te starten:
   ```
   dotnet run --project Jobs.API
   ```
3. De API is nu beschikbaar op https://localhost:7052 en http://localhost:5052
4. Swagger documentatie is beschikbaar op https://localhost:7052/swagger

### Front-end starten
1. Navigeer naar de Jobs.Frontend map:
   ```
   cd Jobs.Frontend
   ```
2. Installeer de benodigde npm packages:
   ```
   npm install
   ```
3. Start de applicatie:
   ```
   npm start
   ```
4. De applicatie is nu beschikbaar op http://localhost:4200

### API client genereren
Om de API client te genereren op basis van de Swagger documentatie:
```
npm run gen:api
```
Deze commando genereert TypeScript interfaces en services op basis van de OpenAPI specificatie.

### Database migraties
Voor het beheren van Entity Framework migraties zijn de volgende PowerShell scripts beschikbaar:
- `Add-EfMigration.ps1 -Name [MigratieName]` voor het aanmaken van een nieuwe migratie
- `Remove-EfMigration.ps1` voor het verwijderen van de laatste migratie
Hiervoor zijn wel de .NET CLI en .NET EF tools lokaal nodig (`dotnet tool install --global dotnet-ef`)
